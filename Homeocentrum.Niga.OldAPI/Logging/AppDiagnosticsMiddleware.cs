using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    public sealed class AppDiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;

        public AppDiagnosticsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            ApplyCorrelation(context);
            AppFileLog.SetRequestSnapshot(RequestDetails(context, null, 0));
            var flags = FeatureFlags.Current;
            var requestPath = context.Request.Path.Value ?? "";
            var requestBody = flags.EnableRequestLogging ? await ReadJsonRequestBodyAsync(context) : null;
            var bodySuffix = string.IsNullOrEmpty(requestBody) ? "" : " body=" + requestBody;

            Stream originalResponseBody = null;
            MemoryStream responseBuffer = null;
            if (flags.EnableResponseLogging && requestPath.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                originalResponseBody = context.Response.Body;
                responseBuffer = new MemoryStream();
                context.Response.Body = responseBuffer;
            }

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
                sw.Stop();
                var status = context.Response != null ? context.Response.StatusCode : 0;
                var path = context.Request.Path.Value ?? "";
                if (IsSkipped(path))
                    return;
                if (status >= 400)
                {
                    var level = status >= 500 && status != 503 ? "ERROR" : "WARN";
                    var kind = level == "ERROR" ? "errors" : "api";
                    AppFileLog.Write(kind, level, "Http",
                        status + " " + context.Request.Method + " " + path + context.Request.QueryString + " " + sw.ElapsedMilliseconds + "ms trace=" + context.TraceIdentifier + bodySuffix,
                        null,
                        RequestDetails(context, status, sw.ElapsedMilliseconds),
                        level == "ERROR");
                }
                else if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
                {
                    var slow = AppFileLog.SlowRequestMilliseconds;
                    var isSlow = slow > 0 && sw.ElapsedMilliseconds >= slow;
                    if (isSlow || flags.EnableRequestLogging)
                    {
                        AppFileLog.Write(isSlow ? "perf" : "api", isSlow ? "WARN" : "INFO", isSlow ? "Performance" : "Http",
                            status + " " + context.Request.Method + " " + path + " " + sw.ElapsedMilliseconds + "ms trace=" + context.TraceIdentifier + bodySuffix,
                            null,
                            RequestDetails(context, status, sw.ElapsedMilliseconds),
                            isSlow);
                    }
                    if (IsMutating(context.Request.Method) && !IsNoisy(path))
                        AppFileLog.Audit(context.Request.Method + " " + path, "Http", null, null);
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                var path = context.Request.Path.Value ?? "";
                var errorId = SafeError.NewErrorId();
                var details = RequestDetails(context, 500, sw.ElapsedMilliseconds);
                details["ErrorId"] = errorId;
                AppFileLog.Write("errors", "ERROR", "Http",
                    "UNHANDLED errorId=" + errorId + " " + context.Request.Method + " " + path + " " + sw.ElapsedMilliseconds + "ms trace=" + context.TraceIdentifier + bodySuffix,
                    ex,
                    details);
                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(SafeError.ToJson(new SafeErrorBody
                    {
                        ErrorId = errorId,
                        TraceId = context.TraceIdentifier,
                        Exception = SafeError.Details(ex)
                    }));
                }
            }
            finally
            {
                if (responseBuffer != null)
                    await FlushResponseBufferAsync(context, originalResponseBody, responseBuffer, sw.ElapsedMilliseconds);
                AppFileLog.ClearRequestSnapshot();
            }
        }

        private const int MaxLoggedBodyChars = 4000;

        /// <summary>First 4000 chars of a POST/PUT/PATCH JSON body. The body is buffered and rewound so MVC still reads it.</summary>
        private static async Task<string> ReadJsonRequestBodyAsync(HttpContext context)
        {
            var request = context.Request;
            if (!(HttpMethods.IsPost(request.Method) || HttpMethods.IsPut(request.Method) || HttpMethods.IsPatch(request.Method)))
                return null;
            if (!IsJson(request.ContentType) || request.ContentLength == 0)
                return null;
            try
            {
                request.EnableRewind();
                var buffer = new char[MaxLoggedBodyChars];
                int read;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 4096, true))
                    read = await reader.ReadBlockAsync(buffer, 0, buffer.Length);
                request.Body.Position = 0;
                return read == 0 ? null : new string(buffer, 0, read);
            }
            catch (Exception ex)
            {
                AppFileLog.Write("api", "WARN", "Http", "Request body could not be read for logging.", ex, null, false);
                try { if (request.Body.CanSeek) request.Body.Position = 0; } catch { }
                return null;
            }
        }

        private static async Task FlushResponseBufferAsync(HttpContext context, Stream original, MemoryStream buffer, long elapsedMs)
        {
            context.Response.Body = original;
            try
            {
                var contentType = context.Response.ContentType ?? "";
                if (buffer.Length > 0 && (IsJson(contentType) || contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)))
                {
                    buffer.Position = 0;
                    var chars = new char[MaxLoggedBodyChars];
                    int read;
                    using (var reader = new StreamReader(buffer, Encoding.UTF8, true, 4096, true))
                        read = await reader.ReadBlockAsync(chars, 0, chars.Length);
                    var status = context.Response.StatusCode;
                    AppFileLog.Write("api", "INFO", "HttpResponse",
                        status + " " + context.Request.Method + " " + (context.Request.Path.Value ?? "") + " " + elapsedMs + "ms trace=" + context.TraceIdentifier
                        + " response=" + new string(chars, 0, read),
                        null,
                        RequestDetails(context, status, elapsedMs),
                        false);
                }
            }
            catch (Exception ex)
            {
                AppFileLog.Write("api", "WARN", "HttpResponse", "Response body could not be logged.", ex, null, false);
            }
            buffer.Position = 0;
            await buffer.CopyToAsync(original);
            buffer.Dispose();
        }

        private static bool IsJson(string contentType)
        {
            return !string.IsNullOrEmpty(contentType)
                && contentType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void ApplyCorrelation(HttpContext context)
        {
            var incoming = context.Request.Headers["X-Correlation-Id"].ToString();
            if (IsSafeTrace(incoming))
                context.TraceIdentifier = incoming.Trim();
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Correlation-Id"] = context.TraceIdentifier ?? "";
                return Task.CompletedTask;
            });
        }

        private static bool IsSafeTrace(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 80) return false;
            foreach (var ch in value.Trim())
            {
                if (!(char.IsLetterOrDigit(ch) || ch == '-' || ch == '_')) return false;
            }
            return true;
        }

        private static bool IsSkipped(string path)
        {
            return path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)
                || path.Equals("/json/version", StringComparison.OrdinalIgnoreCase)
                || path.IndexOf("/Diagnostics", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static Dictionary<string, string> RequestDetails(HttpContext context, int? status, long elapsedMs)
        {
            var details = AppFileLog.BaseDetails();
            details["TraceId"] = context.TraceIdentifier ?? "";
            details["Method"] = context.Request.Method ?? "";
            details["Path"] = (context.Request.Path.Value ?? "") + context.Request.QueryString;
            details["Status"] = status.HasValue ? status.Value.ToString() : "";
            details["ElapsedMs"] = elapsedMs.ToString();
            details["OccurredAtLocal"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            details["OccurredAtUtc"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "Z";
            details["ContentType"] = context.Request.ContentType ?? "";
            ClientEnvironment.ApplyUser(details, context.User);
            ClientEnvironment.ApplyRequestPlace(details, context);
            ClientEnvironment.ApplyUserAgent(details, context.Request.Headers["User-Agent"].ToString());
            return details;
        }

        private static bool IsNoisy(string path)
        {
            if (string.IsNullOrEmpty(path)) return true;
            return path.IndexOf("/Login", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("/Logout", StringComparison.OrdinalIgnoreCase) >= 0
                || path.IndexOf("/Diagnostics", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsMutating(string method)
        {
            return string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase)
                || string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase)
                || string.Equals(method, "PATCH", StringComparison.OrdinalIgnoreCase)
                || string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase);
        }
    }
}
