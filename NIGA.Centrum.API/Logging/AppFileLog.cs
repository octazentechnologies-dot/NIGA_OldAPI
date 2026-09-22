using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Logging
{
    public static class AppFileLog
    {
        private static readonly object Gate = new object();
        private static readonly ConcurrentDictionary<string, DateTime> LastAlert = new ConcurrentDictionary<string, DateTime>();
        private static readonly AsyncLocal<Dictionary<string, string>> RequestSnapshot = new AsyncLocal<Dictionary<string, string>>();
        private static string _root = Path.Combine(AppContext.BaseDirectory, "Logs");
        private static string _application = "NIGA Old-API (NIGA.Centrum.API)";
        private static bool _fileEnabled = true;
        private static bool _alertEnabled = true;
        private static int _cooldownMinutes = 10;
        private static string[] _recipients = Array.Empty<string>();
        private static SmtpSettingsModel _smtp;

        public static string LogsDirectory { get { return _root; } }
        public static bool IsFileEnabled { get { return _fileEnabled; } }
        public static bool IsAlertEnabled { get { return _alertEnabled; } }

        public static void SetRequestSnapshot(Dictionary<string, string> details)
        {
            RequestSnapshot.Value = details;
        }

        public static void ClearRequestSnapshot()
        {
            RequestSnapshot.Value = null;
        }

        public static void Initialize(string contentRoot, IConfiguration config)
        {
            _root = Path.Combine(contentRoot ?? AppContext.BaseDirectory, "Logs");
            if (config != null)
            {
                var fileEnabled = config["FileLog:Enabled"];
                if (!string.IsNullOrEmpty(fileEnabled))
                    bool.TryParse(fileEnabled, out _fileEnabled);

                var enabled = config["ErrorAlert:Enabled"];
                if (!string.IsNullOrEmpty(enabled))
                    bool.TryParse(enabled, out _alertEnabled);
                var cool = config["ErrorAlert:CooldownMinutes"];
                if (!string.IsNullOrEmpty(cool))
                    int.TryParse(cool, out _cooldownMinutes);
                _recipients = config.GetSection("ErrorAlert:Recipients").GetChildren()
                    .Select(c => c.Value)
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .ToArray();
                _smtp = new SmtpSettingsModel();
                config.GetSection("smtp").Bind(_smtp);
            }
            if (_fileEnabled)
            {
                Directory.CreateDirectory(_root);
                Write("app", "INFO", "AppFileLog",
                    "File logging started at " + _root + " FileLog.Enabled=" + _fileEnabled + " ErrorAlert.Enabled=" + _alertEnabled,
                    null, null, false);
            }
        }

        public static void Write(string kind, string level, string category, string message)
        {
            Write(kind, level, category, message, null, null, true);
        }

        public static void Write(string kind, string level, string category, string message, Exception ex)
        {
            Write(kind, level, category, message, ex, null, true);
        }

        public static void Write(string kind, string level, string category, string message, Exception ex, IDictionary<string, string> details)
        {
            Write(kind, level, category, message, ex, details, true);
        }

        public static void Write(string kind, string level, string category, string message, Exception ex, IDictionary<string, string> details, bool sendAlert)
        {
            try
            {
                details = MergeSnapshot(details);
                if (_fileEnabled)
                {
                    Directory.CreateDirectory(_root);
                    var day = DateTime.Now.ToString("yyyyMMdd");
                    var line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + level + "\t" + category + "\t" + Sanitize(message);
                    if (details != null)
                    {
                        foreach (var kv in details)
                        {
                            if (string.IsNullOrWhiteSpace(kv.Key) || string.IsNullOrWhiteSpace(kv.Value))
                                continue;
                            line += "\t" + kv.Key + "=" + Sanitize(kv.Value);
                        }
                    }
                    if (ex != null)
                        line += Environment.NewLine + ex;
                    lock (Gate)
                    {
                        File.AppendAllText(Path.Combine(_root, "niga-all-" + day + ".log"), line + Environment.NewLine);
                        File.AppendAllText(Path.Combine(_root, "niga-" + kind + "-" + day + ".log"), line + Environment.NewLine);
                    }
                }

                if (sendAlert && IsFailureLevel(level))
                    TryEmail(level, category, message, ex, details);
            }
            catch
            {
            }
        }

        public static void Audit(string action, string entity, long? userId, string role)
        {
            Write("audit", "INFO", entity, "user=" + userId + " role=" + role + " action=" + action, null, null, false);
        }

        public static Dictionary<string, string> BaseDetails()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Application", _application },
                { "Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "" },
                { "Machine", Environment.MachineName },
                { "ProcessId", Process.GetCurrentProcess().Id.ToString() },
                { "LoggedAtLocal", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                { "LoggedAtUtc", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "Z" },
                { "OccurredAtLocal", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                { "OccurredAtUtc", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "Z" },
                { "ServerTimeZone", TimeZoneInfo.Local.DisplayName },
                { "LogsDirectory", _root }
            };
        }

        private static IDictionary<string, string> MergeSnapshot(IDictionary<string, string> details)
        {
            var snap = RequestSnapshot.Value;
            if (snap == null || snap.Count == 0)
                return details;
            var merged = new Dictionary<string, string>(snap, StringComparer.OrdinalIgnoreCase);
            if (details != null)
            {
                foreach (var kv in details)
                {
                    if (!string.IsNullOrWhiteSpace(kv.Key) && !string.IsNullOrWhiteSpace(kv.Value))
                        merged[kv.Key] = kv.Value;
                }
            }
            return merged;
        }

        private static bool IsFailureLevel(string level)
        {
            return string.Equals(level, "ERROR", StringComparison.OrdinalIgnoreCase)
                || string.Equals(level, "CRITICAL", StringComparison.OrdinalIgnoreCase)
                || string.Equals(level, "FAIL", StringComparison.OrdinalIgnoreCase);
        }

        private static string Sanitize(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("\r", " ").Replace("\n", " | ");
        }

        private static void TryEmail(string level, string category, string message, Exception ex, IDictionary<string, string> details)
        {
            if (!_alertEnabled || _recipients == null || _recipients.Length == 0 || _smtp == null)
                return;

            var key = (level + "|" + category + "|" + (message ?? "")).GetHashCode().ToString();
            var cooldown = TimeSpan.FromMinutes(_cooldownMinutes <= 0 ? 10 : _cooldownMinutes);
            var now = DateTime.UtcNow;
            DateTime prev;
            if (LastAlert.TryGetValue(key, out prev) && now - prev < cooldown)
                return;
            LastAlert[key] = now;

            var merged = BaseDetails();
            merged["Level"] = level ?? "";
            merged["Category"] = category ?? "";
            if (details != null)
            {
                foreach (var kv in details)
                {
                    if (!string.IsNullOrWhiteSpace(kv.Key) && kv.Value != null)
                        merged[kv.Key] = kv.Value;
                }
            }

            string method;
            string path;
            var who = First(merged, "UserName", "DisplayName", "User", "ClientUserName");
            var browser = First(merged, "Browser");
            var device = First(merged, "DeviceName");
            var subject = "[" + _application + "] " + level + " — " + category;
            if (!string.IsNullOrWhiteSpace(who))
                subject += " user=" + who;
            if (!string.IsNullOrWhiteSpace(browser))
                subject += " " + browser;
            if (!string.IsNullOrWhiteSpace(device))
                subject += " / " + device;
            if (merged.TryGetValue("Method", out method) && merged.TryGetValue("Path", out path))
                subject += " " + method + " " + Truncate(path, 80);

            var body = BuildAlertHtml(level, message, ex, merged);
            var sender = new EmailSenderService();
            foreach (var to in _recipients)
            {
                if (string.IsNullOrWhiteSpace(to)) continue;
                var model = new EmailSenderModel
                {
                    ToAddress = to.Trim(),
                    Subject = Truncate(subject, 180),
                    Body = body,
                    isHtml = true
                };
                var ok = sender.SendMail(model, _smtp);
                if (!ok)
                {
                    Write("errors", "WARN", "ErrorAlert", "Alert email failed to " + to, null, null, false);
                }
            }
        }

        private static string BuildAlertHtml(string level, string message, Exception ex, IDictionary<string, string> details)
        {
            var sb = new StringBuilder();
            sb.Append("<div style='font-family:Segoe UI,Arial,sans-serif;font-size:13px;color:#222'>");
            sb.Append("<h2 style='margin:0 0 8px'>Homeocentrum runtime ").Append(WebUtility.HtmlEncode(level)).Append("</h2>");
            sb.Append("<p style='margin:0 0 12px'>Who hit this, when, from which browser/device, and the detailed error log.</p>");

            RenderSection(sb, "Who", details, new[] { "UserName", "DisplayName", "User", "UserId", "Role", "DoctorId", "DoctorUserId", "Authenticated" });
            RenderSection(sb, "When", details, new[] { "OccurredAtLocal", "OccurredAtUtc", "LoggedAtLocal", "LoggedAtUtc", "ServerTimeZone", "ElapsedMs" });
            RenderSection(sb, "Where", details, new[] { "Application", "Environment", "Host", "Scheme", "Path", "Method", "Referrer", "RemoteIp", "ForwardedFor", "Machine", "TraceId", "Status" });
            RenderSection(sb, "Browser / device", details, new[] { "Browser", "BrowserVersion", "Os", "DeviceType", "DeviceName", "UserAgent" });

            sb.Append("<h3>Message</h3><pre style='white-space:pre-wrap;background:#f8f8f8;padding:10px;border:1px solid #ddd'>")
                .Append(WebUtility.HtmlEncode(message ?? "")).Append("</pre>");

            sb.Append("<h3>Detailed error log</h3>");
            if (ex != null)
            {
                sb.Append("<pre style='white-space:pre-wrap;background:#fff4f4;padding:10px;border:1px solid #e0b0b0'>")
                    .Append(WebUtility.HtmlEncode(ex.ToString())).Append("</pre>");
            }
            var tail = ReadRecentLogTail();
            if (!string.IsNullOrWhiteSpace(tail))
            {
                sb.Append("<h4>Recent log file tail</h4><pre style='white-space:pre-wrap;background:#111;color:#eee;padding:10px;border:1px solid #333'>")
                    .Append(WebUtility.HtmlEncode(tail)).Append("</pre>");
            }

            sb.Append("<h3>All diagnostic fields</h3>");
            sb.Append("<table cellpadding='6' cellspacing='0' style='border-collapse:collapse;border:1px solid #ccc'>");
            foreach (var kv in details)
            {
                sb.Append("<tr><td style='border:1px solid #ccc;background:#f6f6f6;white-space:nowrap'><b>")
                    .Append(WebUtility.HtmlEncode(kv.Key))
                    .Append("</b></td><td style='border:1px solid #ccc'>")
                    .Append(WebUtility.HtmlEncode(kv.Value ?? ""))
                    .Append("</td></tr>");
            }
            sb.Append("</table>");
            sb.Append("<p>Log files: ").Append(WebUtility.HtmlEncode(_root)).Append("</p></div>");
            return sb.ToString();
        }

        private static void RenderSection(StringBuilder sb, string title, IDictionary<string, string> details, string[] keys)
        {
            sb.Append("<h3>").Append(WebUtility.HtmlEncode(title)).Append("</h3>");
            sb.Append("<table cellpadding='6' cellspacing='0' style='border-collapse:collapse;border:1px solid #ccc;margin-bottom:12px'>");
            for (var i = 0; i < keys.Length; i++)
            {
                string value;
                details.TryGetValue(keys[i], out value);
                sb.Append("<tr><td style='border:1px solid #ccc;background:#f6f6f6;white-space:nowrap'><b>")
                    .Append(WebUtility.HtmlEncode(keys[i]))
                    .Append("</b></td><td style='border:1px solid #ccc'>")
                    .Append(WebUtility.HtmlEncode(value ?? ""))
                    .Append("</td></tr>");
            }
            sb.Append("</table>");
        }

        private static string First(IDictionary<string, string> details, params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                string value;
                if (details.TryGetValue(keys[i], out value) && !string.IsNullOrWhiteSpace(value))
                    return value;
            }
            return "";
        }

        private static string ReadRecentLogTail()
        {
            try
            {
                if (!_fileEnabled) return "";
                var day = DateTime.Now.ToString("yyyyMMdd");
                var path = Path.Combine(_root, "niga-errors-" + day + ".log");
                if (!File.Exists(path))
                    path = Path.Combine(_root, "niga-all-" + day + ".log");
                if (!File.Exists(path)) return "";
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fs.Length > 12000)
                        fs.Seek(-12000, SeekOrigin.End);
                    using (var reader = new StreamReader(fs, Encoding.UTF8, true, 1024, true))
                    {
                        var text = reader.ReadToEnd();
                        var lines = text.Replace("\r\n", "\n").Split('\n');
                        var take = lines.Length < 40 ? lines.Length : 40;
                        var start = lines.Length - take;
                        return string.Join("\n", lines, start, take).Trim();
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        private static string Truncate(string text, int max)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= max) return text ?? "";
            return text.Substring(0, max) + "...";
        }
    }

    public sealed class AppFileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new AppFileLogger(categoryName);
        }

        public void Dispose() { }
    }

    internal sealed class AppFileLogger : ILogger
    {
        private readonly string _category;
        public AppFileLogger(string category) { _category = category; }

        public IDisposable BeginScope<TState>(TState state) { return NullScope.Instance; }
        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None) return false;
            var microsoft = _category != null && (_category.StartsWith("Microsoft.", StringComparison.Ordinal)
                || _category.StartsWith("System.", StringComparison.Ordinal));
            if (microsoft && logLevel < LogLevel.Warning) return false;
            if (AppFileLog.IsFileEnabled && logLevel >= LogLevel.Debug) return true;
            if (AppFileLog.IsAlertEnabled && logLevel >= LogLevel.Error) return true;
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            string level = "INFO";
            if (logLevel == LogLevel.Critical) level = "CRITICAL";
            else if (logLevel == LogLevel.Error) level = "ERROR";
            else if (logLevel == LogLevel.Warning) level = "WARN";
            else if (logLevel == LogLevel.Debug) level = "DEBUG";
            else if (logLevel == LogLevel.Trace) level = "TRACE";
            var kind = exception != null || logLevel >= LogLevel.Error ? "errors" : "app";
            AppFileLog.Write(kind, level, _category, formatter(state, exception), exception);
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new NullScope();
            public void Dispose() { }
        }
    }
}
