using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>Fixed window per IP. Health and the Swagger sign-in page are not counted.</summary>
    public sealed class SimpleRateLimitMiddleware
    {
        private static readonly ConcurrentDictionary<string, Window> Windows = new ConcurrentDictionary<string, Window>();
        private readonly RequestDelegate _next;
        private readonly int _limit;
        private readonly int _windowSeconds;

        public SimpleRateLimitMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _limit = 300;
            _windowSeconds = 60;
            int parsed;
            if (int.TryParse(configuration["RateLimit:PermitLimit"], out parsed) && parsed > 0)
                _limit = parsed;
            if (int.TryParse(configuration["RateLimit:WindowSeconds"], out parsed) && parsed > 0)
                _windowSeconds = parsed;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            if (path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var ip = context.Connection.RemoteIpAddress != null ? context.Connection.RemoteIpAddress.ToString() : "unknown";
            var now = DateTime.UtcNow;
            var window = Windows.GetOrAdd(ip, _ => new Window { Start = now, Count = 0 });
            var blocked = false;
            lock (window)
            {
                if ((now - window.Start).TotalSeconds >= _windowSeconds)
                {
                    window.Start = now;
                    window.Count = 0;
                }
                window.Count++;
                blocked = window.Count > _limit;
            }

            if (blocked)
            {
                AppFileLog.Write("api", "WARN", "RateLimit", "429 " + context.Request.Method + " " + path, null, null, false);
                await ApiProblem.WriteAsync(context, 429, "Too many requests. Please wait a moment and try again.");
                return;
            }

            await _next(context);
        }

        private sealed class Window
        {
            public DateTime Start;
            public int Count;
        }
    }
}
