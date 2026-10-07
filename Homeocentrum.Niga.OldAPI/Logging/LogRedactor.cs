using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>
    /// Masks personal data and secrets before anything is written to log files or alert emails:
    /// JWTs, bearer tokens, OTPs, passwords, signatures, mobile numbers and email addresses.
    /// </summary>
    public static class LogRedactor
    {
        private const RegexOptions Opts = RegexOptions.Compiled | RegexOptions.CultureInvariant;
        private static readonly TimeSpan MatchTimeout = TimeSpan.FromMilliseconds(250);

        private static readonly Regex Jwt = new Regex(@"eyJ[A-Za-z0-9_\-]{5,}\.[A-Za-z0-9_\-]{5,}\.[A-Za-z0-9_\-]*", Opts, MatchTimeout);
        private static readonly Regex Bearer = new Regex(@"(?i)\bBearer\s+[A-Za-z0-9_\-\.=+/]+", Opts, MatchTimeout);
        private static readonly Regex SecretPair = new Regex(
            @"(?i)(?<k>\b(?:otp|otpcode|otp_code|password|newpassword|oldpassword|confirmpassword|pwd|pin|token|accesstoken|access_token|refreshtoken|refresh_token|sig|signature|secret|keysecret|apikey|api_key|authkey|cvv|aadhaar|aadhar)\b)(?<sep>""?\s*[=:]\s*)(?<q>""?)(?<v>[^&\s"",;}]+)",
            Opts, MatchTimeout);
        private static readonly Regex OtpPath = new Regex(@"(?i)(?<p>/otp[a-z]*/(?:[^/?\s]+/)*?)(?<v>\d{4,8})(?=[/?\s]|$)", Opts, MatchTimeout);
        private static readonly Regex OtpPhrase = new Regex(@"(?i)(?<p>\b(?:otp|one[\s\-]time\s+password|verification\s+code|security\s+code)\b[^0-9\r\n]{0,24})(?<v>\d{4,8})(?!\d)", Opts, MatchTimeout);
        private static readonly Regex Mobile = new Regex(@"(?<![\d\w])(?:\+?91[\-\s]?)?[6-9]\d{9}(?![\d\w])", Opts, MatchTimeout);
        private static readonly Regex Email = new Regex(@"(?<u>[A-Za-z0-9._%+\-])[A-Za-z0-9._%+\-]*@(?<d>[A-Za-z0-9.\-]+\.[A-Za-z]{2,})", Opts, MatchTimeout);

        public static string Redact(string text)
        {
            if (string.IsNullOrEmpty(text)) return text ?? "";
            try
            {
                var value = Jwt.Replace(text, "[jwt]");
                value = Bearer.Replace(value, "Bearer [redacted]");
                value = SecretPair.Replace(value, m => m.Groups["k"].Value + m.Groups["sep"].Value + m.Groups["q"].Value + "[redacted]");
                value = OtpPath.Replace(value, m => m.Groups["p"].Value + "[otp]");
                value = OtpPhrase.Replace(value, m => m.Groups["p"].Value + "[otp]");
                value = Mobile.Replace(value, m => MaskDigits(m.Value));
                value = Email.Replace(value, m => m.Groups["u"].Value + "***@" + m.Groups["d"].Value);
                return value;
            }
            catch (RegexMatchTimeoutException)
            {
                return "[redacted: log text could not be scanned]";
            }
        }

        public static IDictionary<string, string> RedactDetails(IDictionary<string, string> details)
        {
            if (details == null || details.Count == 0) return details;
            var copy = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in details)
            {
                if (string.IsNullOrWhiteSpace(kv.Key)) continue;
                copy[kv.Key] = IsSecretKey(kv.Key) ? "[redacted]" : Redact(kv.Value);
            }
            return copy;
        }

        public static string MaskMobile(string mobile)
        {
            return string.IsNullOrWhiteSpace(mobile) ? "" : MaskDigits(mobile);
        }

        private static bool IsSecretKey(string key)
        {
            var k = key.Replace("-", "").Replace("_", "");
            return k.Equals("Authorization", StringComparison.OrdinalIgnoreCase)
                || k.Equals("Cookie", StringComparison.OrdinalIgnoreCase)
                || k.Equals("SetCookie", StringComparison.OrdinalIgnoreCase)
                || k.IndexOf("Password", StringComparison.OrdinalIgnoreCase) >= 0
                || k.IndexOf("Token", StringComparison.OrdinalIgnoreCase) >= 0
                || k.IndexOf("Otp", StringComparison.OrdinalIgnoreCase) >= 0
                || k.IndexOf("Secret", StringComparison.OrdinalIgnoreCase) >= 0
                || k.Equals("RequestBody", StringComparison.OrdinalIgnoreCase)
                || k.Equals("Body", StringComparison.OrdinalIgnoreCase);
        }

        private static string MaskDigits(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            if (digits.Length <= 4) return "****";
            return new string('*', digits.Length - 2) + digits.Substring(digits.Length - 2);
        }
    }
}
