using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>
    /// Same Homeocentrum favicon as the SPA — covers /favicon.* and Swagger UI's own favicon URLs.
    /// </summary>
    public sealed class FaviconMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _wwwroot;

        public FaviconMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _wwwroot = Path.Combine(env.ContentRootPath ?? "", "wwwroot");
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            if (HttpMethods.IsGet(context.Request.Method) && IsFaviconPath(path))
            {
                var bytes = ReadFile("favicon.png")
                    ?? ReadFile("favicon.ico");
                if (bytes != null && bytes.Length > 0)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    // Always serve the SPA dashboard icon (red diamond PNG).
                    context.Response.ContentType = "image/png";
                    context.Response.Headers["Cache-Control"] = "public,max-age=86400";
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    return;
                }
            }

            await _next(context);
        }

        private byte[] ReadFile(string name)
        {
            try
            {
                var full = Path.Combine(_wwwroot, name);
                if (!File.Exists(full)) return null;
                return File.ReadAllBytes(full);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsFaviconPath(string path)
        {
            return string.Equals(path, "/favicon.ico", StringComparison.OrdinalIgnoreCase)
                || string.Equals(path, "/favicon.png", StringComparison.OrdinalIgnoreCase)
                || string.Equals(path, "/swagger/favicon-32x32.png", StringComparison.OrdinalIgnoreCase)
                || string.Equals(path, "/swagger/favicon-16x16.png", StringComparison.OrdinalIgnoreCase)
                || string.Equals(path, "/swagger/favicon.ico", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static class FaviconMiddlewareExtensions
    {
        public static IApplicationBuilder UseHomeocentrumFavicon(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FaviconMiddleware>();
        }
    }
}
