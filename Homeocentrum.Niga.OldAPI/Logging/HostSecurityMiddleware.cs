using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>Security headers and a trace id on every Old API response. Swagger keeps its own scripts.
    /// FeatureFlags:EnableSecurityHeaders=false leaves only the trace id.</summary>
    public sealed class HostSecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _securityHeaders;

        public HostSecurityMiddleware(RequestDelegate next)
        {
            _next = next;
            _securityHeaders = FeatureFlags.Current.EnableSecurityHeaders;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            var swagger = path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase);
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;
                headers["X-Trace-Id"] = context.TraceIdentifier;
                if (!_securityHeaders)
                    return Task.CompletedTask;
                headers["X-Content-Type-Options"] = "nosniff";
                headers["X-Frame-Options"] = "DENY";
                headers["Referrer-Policy"] = "no-referrer";
                headers["X-Permitted-Cross-Domain-Policies"] = "none";
                if (!swagger)
                    headers["Content-Security-Policy"] = "default-src 'none'; frame-ancestors 'none'";
                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}
