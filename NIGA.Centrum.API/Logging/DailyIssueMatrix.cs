using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NIGA.Centrum.API.Logging
{
    /// <summary>One row of the midnight issue matrix.</summary>
    public sealed class DailyIssueMatrixRow
    {
        public string Source { get; set; }
        public string RootCause { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Builds the daily issue matrix from Logs/niga-errors for one calendar day.
    /// This host's rows are Old API. The mail at local midnight covers the day that just ended.
    /// </summary>
    public static class DailyIssueMatrix
    {
        private static readonly Regex Stamp = new Regex(
            @"^(?<time>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3})\t(?<level>ERROR|CRITICAL|FAIL|WARN)\t(?<category>[^\t]*)\t(?<message>.*)$",
            RegexOptions.Compiled);

        public static IList<DailyIssueMatrixRow> Read(string logsDirectory, DateTime day, string hostSource)
        {
            var counts = new Dictionary<string, DailyIssueMatrixRow>(StringComparer.OrdinalIgnoreCase);
            var dayKey = day.ToString("yyyyMMdd");
            AddFile(counts, Path.Combine(logsDirectory ?? "", "niga-errors-" + dayKey + ".log"), hostSource, false);
            AddFile(counts, Path.Combine(logsDirectory ?? "", "niga-ui-" + dayKey + ".log"), hostSource, true);
            return counts.Values
                .OrderBy(row => row.Source)
                .ThenByDescending(row => row.Count)
                .ThenBy(row => row.RootCause)
                .ToList();
        }

        public static string ToHtml(DateTime day, string hostSource, IList<DailyIssueMatrixRow> rows)
        {
            var total = rows.Sum(row => row.Count);
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset=\"utf-8\"></head>");
            sb.Append("<body style=\"margin:0;padding:16px;background:#e2e8f0;font-family:Segoe UI,Arial,sans-serif;\">");
            sb.Append("<table width=\"680\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" bgcolor=\"#ffffff\" style=\"border:1px solid #94a3b8;\">");
            sb.Append("<tr><td bgcolor=\"#0f172a\" style=\"padding:16px 20px;color:#ffffff;\">");
            sb.Append("<div style=\"font-size:20px;font-weight:700;\">Homeocentrum daily issue matrix</div>");
            sb.Append("<div style=\"font-size:13px;margin-top:6px;\">")
                .Append(WebUtility.HtmlEncode(day.ToString("dd-MMM-yyyy")))
                .Append(" · sent by ")
                .Append(WebUtility.HtmlEncode(hostSource))
                .Append("</div></td></tr>");
            sb.Append("<tr><td style=\"padding:16px 20px;color:#0f172a;\">");
            sb.Append("<p style=\"margin:0 0 12px;\">Issues logged on this host. Total ")
                .Append(total)
                .Append(".</p>");
            if (total == 0)
            {
                sb.Append("<p style=\"margin:0;\">No runtime issues were logged for this day.</p>");
            }
            else
            {
                sb.Append("<table width=\"100%\" cellpadding=\"8\" cellspacing=\"0\" border=\"0\" style=\"border-collapse:collapse;\">");
                sb.Append("<tr bgcolor=\"#e2e8f0\"><th align=\"left\">Source</th><th align=\"left\">Root cause</th><th align=\"right\">Count</th></tr>");
                foreach (var row in rows)
                {
                    sb.Append("<tr><td style=\"border-top:1px solid #cbd5e1;\">")
                        .Append(WebUtility.HtmlEncode(row.Source))
                        .Append("</td><td style=\"border-top:1px solid #cbd5e1;\">")
                        .Append(WebUtility.HtmlEncode(row.RootCause))
                        .Append("</td><td align=\"right\" style=\"border-top:1px solid #cbd5e1;\">")
                        .Append(row.Count)
                        .Append("</td></tr>");
                }
                sb.Append("</table>");
            }
            sb.Append("</td></tr></table></body></html>");
            return sb.ToString();
        }

        private static void AddFile(Dictionary<string, DailyIssueMatrixRow> counts, string path, string hostSource, bool includeWarnings)
        {
            if (!File.Exists(path))
                return;

            string text;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
                text = reader.ReadToEnd();

            string level = null;
            string category = null;
            string message = null;
            var trail = new StringBuilder();

            Action flush = () =>
            {
                if (level == null)
                    return;
                var keep = level == "ERROR" || level == "CRITICAL" || level == "FAIL" || (includeWarnings && level == "WARN");
                if (keep)
                    Add(counts, hostSource, category ?? "", message ?? "", trail.ToString());
                level = null;
                category = null;
                message = null;
                trail.Clear();
            };

            foreach (var raw in text.Split('\n'))
            {
                var line = raw.TrimEnd('\r');
                var match = Stamp.Match(line);
                if (match.Success)
                {
                    flush();
                    level = match.Groups["level"].Value;
                    category = match.Groups["category"].Value;
                    message = match.Groups["message"].Value;
                    continue;
                }
                if (level != null && line.Length > 0)
                    trail.AppendLine(line);
            }
            flush();
        }

        private static void Add(Dictionary<string, DailyIssueMatrixRow> counts, string hostSource, string category, string message, string trail)
        {
            var source = string.Equals(category, "UI", StringComparison.OrdinalIgnoreCase) ? "UI" : hostSource;
            var cause = RootCause(message, trail);
            var key = source + "\n" + cause;
            DailyIssueMatrixRow row;
            if (!counts.TryGetValue(key, out row))
            {
                row = new DailyIssueMatrixRow { Source = source, RootCause = cause };
                counts[key] = row;
            }
            row.Count++;
        }

        private static string RootCause(string message, string trail)
        {
            var blob = (message ?? "") + "\n" + (trail ?? "");
            if (blob.IndexOf("SqlException", StringComparison.OrdinalIgnoreCase) >= 0
                && blob.IndexOf("Timeout", StringComparison.OrdinalIgnoreCase) >= 0)
                return "SQL timeout";

            var match = Regex.Match(blob, @"([\w\.]+Exception)");
            if (match.Success)
            {
                var name = match.Groups[1].Value;
                var dot = name.LastIndexOf('.');
                if (dot >= 0 && dot < name.Length - 1)
                    name = name.Substring(dot + 1);
                var detail = FirstSentence(message);
                if (string.IsNullOrWhiteSpace(detail) || detail.IndexOf(name, StringComparison.Ordinal) >= 0)
                    return Trim(name);
                return Trim(name + " — " + detail);
            }

            return Trim(string.IsNullOrWhiteSpace(message) ? "Unknown issue" : FirstSentence(message));
        }

        private static string FirstSentence(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return "";
            var text = message.Trim();
            var cut = text.IndexOf(". ", StringComparison.Ordinal);
            if (cut > 0 && cut < 160)
                text = text.Substring(0, cut + 1);
            return text;
        }

        private static string Trim(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Unknown issue";
            var text = value.Replace("\r", " ").Replace("\n", " ").Trim();
            return text.Length <= 180 ? text : text.Substring(0, 180);
        }
    }
}
