using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Homeocentrum.Niga.OldAPI.Security
{
    /// <summary>
    /// Failed-login throttle per user name and per client IP (same rules as the New API):
    /// 5 failures for a user name or 30 from one IP within 15 minutes block further attempts for 15 minutes.
    /// </summary>
    public static class LoginThrottle
    {
        public const int MaxFailuresPerUser = 5;
        public const int MaxFailuresPerIp = 30;
        public static readonly TimeSpan Window = TimeSpan.FromMinutes(15);
        public static readonly TimeSpan Block = TimeSpan.FromMinutes(15);

        private sealed class Entry
        {
            public int Failures;
            public DateTime WindowStartUtc;
            public DateTime BlockedUntilUtc;
        }

        private static readonly ConcurrentDictionary<string, Entry> Entries = new ConcurrentDictionary<string, Entry>(StringComparer.Ordinal);

        public static TimeSpan? BlockedFor(string userName, string ip)
        {
            var now = DateTime.UtcNow;
            TimeSpan? longest = null;
            foreach (var key in Keys(userName, ip))
            {
                Entry e;
                if (!Entries.TryGetValue(key, out e))
                    continue;
                lock (e)
                {
                    if (e.BlockedUntilUtc > now && (longest == null || e.BlockedUntilUtc - now > longest))
                        longest = e.BlockedUntilUtc - now;
                }
            }
            return longest;
        }

        public static void RecordFailure(string userName, string ip)
        {
            var now = DateTime.UtcNow;
            if (Entries.Count > 50000)
                Purge(now);
            foreach (var key in Keys(userName, ip))
            {
                var isUser = key.StartsWith("u:", StringComparison.Ordinal);
                var limit = isUser ? MaxFailuresPerUser : MaxFailuresPerIp;
                var e = Entries.GetOrAdd(key, _ => new Entry { WindowStartUtc = now });
                var blockStarted = false;
                lock (e)
                {
                    if (now - e.WindowStartUtc > Window)
                    {
                        e.WindowStartUtc = now;
                        e.Failures = 0;
                    }
                    e.Failures++;
                    if (e.Failures >= limit)
                    {
                        blockStarted = e.Failures == limit;
                        e.BlockedUntilUtc = now + Block;
                    }
                }
                if (blockStarted)
                {
                    Logging.AppFileLog.SendOpsAlert("login-block|" + key, isUser ? "Sign-in blocked for a user name" : "Sign-in blocked for a client IP",
                        new Dictionary<string, string>
                        {
                            { isUser ? "User name" : "Client IP", key.Substring(key.IndexOf(':') + 1) },
                            { "Failed attempts", limit + " within " + (int)Window.TotalMinutes + " minutes" },
                            { "Blocked until (local)", (now + Block).ToLocalTime().ToString("dd-MMM-yyyy HH:mm") },
                            { "Meaning", isUser ? "Password guessing against one account" : "Many accounts tried from one address" },
                        });
                }
            }
        }

        public static void RecordSuccess(string userName)
        {
            Entry removed;
            if (!string.IsNullOrWhiteSpace(userName))
                Entries.TryRemove("u:" + userName.Trim().ToLowerInvariant(), out removed);
        }

        private static IEnumerable<string> Keys(string userName, string ip)
        {
            if (!string.IsNullOrWhiteSpace(userName)) yield return "u:" + userName.Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(ip)) yield return "ip:" + ip.Trim();
        }

        private static void Purge(DateTime now)
        {
            foreach (var pair in Entries)
            {
                Entry removed;
                lock (pair.Value)
                {
                    if (pair.Value.BlockedUntilUtc < now && now - pair.Value.WindowStartUtc > Window)
                        Entries.TryRemove(pair.Key, out removed);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LoginThrottleAttribute : ActionFilterAttribute
    {
        private const string UserKey = "__loginThrottleUser";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = ReadUserName(context.ActionArguments.Values);
            context.HttpContext.Items[UserKey] = userName;
            var wait = LoginThrottle.BlockedFor(userName, ClientIp(context.HttpContext));
            if (wait == null)
                return;
            var minutes = Math.Max(1, (int)Math.Ceiling(wait.Value.TotalMinutes));
            context.HttpContext.Response.Headers["Retry-After"] = ((int)wait.Value.TotalSeconds).ToString();
            context.Result = new ObjectResult(new
            {
                success = false,
                code = "TOO_MANY_FAILED_LOGINS",
                message = "Too many failed sign-in attempts. Try again in " + minutes + " minute(s)."
            })
            { StatusCode = StatusCodes.Status429TooManyRequests };
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userName = context.HttpContext.Items[UserKey] as string;
            int status;
            var objectResult = context.Result as ObjectResult;
            var statusResult = context.Result as StatusCodeResult;
            if (objectResult != null && objectResult.StatusCode.HasValue) status = objectResult.StatusCode.Value;
            else if (statusResult != null) status = statusResult.StatusCode;
            else status = context.Exception == null ? 200 : 500;

            if (status == StatusCodes.Status401Unauthorized)
                LoginThrottle.RecordFailure(userName, ClientIp(context.HttpContext));
            else if (status >= 200 && status < 300)
                LoginThrottle.RecordSuccess(userName);
        }

        /// <summary>
        /// Loopback means the caller came through a local proxy or tunnel that hid the real address, so every user
        /// would share one IP bucket; only the per-user-name limit applies then.
        /// </summary>
        private static string ClientIp(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress;
            if (ip == null) return null;
            if (ip.IsIPv4MappedToIPv6) ip = ip.MapToIPv4();
            return System.Net.IPAddress.IsLoopback(ip) ? null : ip.ToString();
        }

        private static string ReadUserName(IEnumerable<object> arguments)
        {
            foreach (var arg in arguments)
            {
                if (arg == null) continue;
                var s = arg as string;
                if (s != null) return s;
                foreach (var name in new[] { "UserName", "Username", "Email", "MobileNo" })
                {
                    var property = arg.GetType().GetProperty(name);
                    var value = property == null ? null : property.GetValue(arg) as string;
                    if (!string.IsNullOrWhiteSpace(value)) return value;
                }
            }
            return null;
        }
    }
}
