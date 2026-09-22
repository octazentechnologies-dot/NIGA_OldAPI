using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NIGA.Centrum.API.Logging
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
            AppFileLog.SetRequestSnapshot(RequestDetails(context, null, 0));
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
                sw.Stop();
                var status = context.Response != null ? context.Response.StatusCode : 0;
                var path = context.Request.Path.Value ?? "";
                if (status >= 400)
                {
                    var level = status >= 500 ? "ERROR" : "WARN";
                    var kind = status >= 500 ? "errors" : "api";
                    AppFileLog.Write(kind, level, "Http",
                        status + " " + context.Request.Method + " " + path + context.Request.QueryString + " " + sw.ElapsedMilliseconds + "ms",
                        null,
                        RequestDetails(context, status, sw.ElapsedMilliseconds));
                }
                else if (status < 400 && IsMutating(context.Request.Method) && !IsNoisy(path))
                {
                    AppFileLog.Audit(context.Request.Method + " " + path, "Http", null, null);
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                var path = context.Request.Path.Value ?? "";
                AppFileLog.Write("errors", "ERROR", "Http",
                    "UNHANDLED " + context.Request.Method + " " + path + " " + sw.ElapsedMilliseconds + "ms",
                    ex,
                    RequestDetails(context, 500, sw.ElapsedMilliseconds));
                throw;
            }
            finally
            {
                AppFileLog.ClearRequestSnapshot();
            }
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
