using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>
    /// The only body a client sees for a server failure. Internal details go to the error log under the same error id.
    /// </summary>
    public sealed class SafeErrorBody
    {
        public bool Success { get; set; }
        public int Status { get; set; } = 500;
        public string Message { get; set; } = SafeError.GenericMessage;
        public string ErrorId { get; set; } = "";
        public string TraceId { get; set; }
    }

    public static class SafeError
    {
        public const string GenericMessage = "Something went wrong on our side. Please try again. If it keeps happening, contact support with the error id.";

        public static string NewErrorId()
        {
            var bytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);
            return "E" + DateTime.UtcNow.ToString("yyMMdd") + "-" + BitConverter.ToString(bytes).Replace("-", "");
        }

        public static SafeErrorBody Capture(Exception ex, HttpContext context, string where, int status = 500)
        {
            var errorId = NewErrorId();
            Log(errorId, ex == null ? "" : ex.GetType().Name, ex, context, where);
            return new SafeErrorBody { Status = status, ErrorId = errorId, TraceId = context == null ? null : context.TraceIdentifier };
        }

        public static SafeErrorBody CaptureText(string text, HttpContext context, int status, string where)
        {
            var errorId = NewErrorId();
            Log(errorId, text, null, context, where);
            return new SafeErrorBody { Status = status, ErrorId = errorId, TraceId = context == null ? null : context.TraceIdentifier };
        }

        public static string ToJson(SafeErrorBody body)
        {
            return JsonConvert.SerializeObject(new
            {
                success = body.Success,
                status = body.Status,
                message = body.Message,
                errorId = body.ErrorId,
                traceId = body.TraceId
            });
        }

        private static void Log(string errorId, string summary, Exception ex, HttpContext context, string where)
        {
            var details = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { ["ErrorId"] = errorId };
            if (context != null)
            {
                details["TraceId"] = context.TraceIdentifier ?? "";
                details["Method"] = context.Request.Method ?? "";
                details["Path"] = context.Request.Path.Value ?? "";
            }
            AppFileLog.Write("errors", "ERROR", string.IsNullOrEmpty(where) ? "ServerError" : where,
                ("errorId=" + errorId + " " + (summary ?? "")).Trim(), ex, details, true);
        }
    }

    /// <summary>
    /// Safety net for any 5xx result that still carries raw text or an exception message. The text is logged
    /// with an error id and the client receives <see cref="SafeErrorBody"/>.
    /// </summary>
    public sealed class SafeServerErrorResultFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var obj = context.Result as ObjectResult;
            if (obj != null && (obj.StatusCode ?? 200) >= 500 && obj.StatusCode != 503 && !(obj.Value is SafeErrorBody))
            {
                var status = obj.StatusCode ?? 500;
                context.Result = new ObjectResult(SafeError.CaptureText(Describe(obj.Value), context.HttpContext, status,
                    context.ActionDescriptor.DisplayName)) { StatusCode = status };
                return;
            }
            var content = context.Result as ContentResult;
            if (content != null && (content.StatusCode ?? 200) >= 500)
            {
                var code = content.StatusCode ?? 500;
                context.Result = new ObjectResult(SafeError.CaptureText(content.Content, context.HttpContext, code,
                    context.ActionDescriptor.DisplayName)) { StatusCode = code };
            }
        }

        public void OnResultExecuted(ResultExecutedContext context) { }

        private static string Describe(object value)
        {
            if (value == null) return "(empty 5xx body)";
            var s = value as string;
            if (s != null) return s;
            try { return JsonConvert.SerializeObject(value); }
            catch { return value.GetType().Name; }
        }
    }
}
