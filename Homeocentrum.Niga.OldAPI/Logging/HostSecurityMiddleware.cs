using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>Security headers and a trace id on every Old API response. Swagger keeps its own scripts.</summary>
    public sealed class HostSecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public HostSecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            var swagger = path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase);
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;
                headers["X-Content-Type-Options"] = "nosniff";
                headers["X-Frame-Options"] = "DENY";
                headers["Referrer-Policy"] = "no-referrer";
                headers["X-Permitted-Cross-Domain-Policies"] = "none";
                headers["X-Trace-Id"] = context.TraceIdentifier;
                if (!swagger)
                    headers["Content-Security-Policy"] = "default-src 'none'; frame-ancestors 'none'";
                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}
