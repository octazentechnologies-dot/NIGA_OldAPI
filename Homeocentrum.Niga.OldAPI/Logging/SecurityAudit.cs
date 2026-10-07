using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Homeocentrum.Niga.OldAPI.Logging
{
    /// <summary>
    /// Writes the shared tamper-evident trail (New API script 09, dbo.usp_SecurityAudit_Append).
    /// Canonical text and HMAC match the New API SecurityAuditRow exactly so one chain covers both APIs.
    /// </summary>
    public static class SecurityAudit
    {
        public const string SourceApi = "OldAPI";
        public const string Login = "LOGIN";
        public const string TokenIssued = "TOKEN_ISSUED";

        private static string _connectionString;
        private static byte[] _key;
        private static string _keyId = "tk1";

        public static void Initialize(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            var explicitKey = config["SecurityAudit:HmacKey"];
            if (!string.IsNullOrWhiteSpace(explicitKey))
            {
                var id = config["SecurityAudit:KeyId"];
                _keyId = string.IsNullOrWhiteSpace(id) ? "k1" : id.Trim();
                _key = Encoding.UTF8.GetBytes(explicitKey);
                return;
            }
            var secret = config["JWT:Secret"] ?? "";
            if (secret.Length == 0)
                return;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                _key = hmac.ComputeHash(Encoding.UTF8.GetBytes("homeocentrum-security-audit-v1"));
        }

        public static async Task WriteAsync(string eventType, HttpContext context, string outcome = "SUCCESS",
            long? actorUserId = null, string actorRole = null, string subject = null, string detail = null)
        {
            if (_key == null || string.IsNullOrEmpty(_connectionString))
                return;
            try
            {
                var user = context?.User;
                if (!actorUserId.HasValue)
                {
                    long parsed;
                    var raw = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (long.TryParse(raw, out parsed) && parsed > 0) actorUserId = parsed;
                }
                if (actorRole == null)
                    actorRole = user?.FindFirst("RoleName")?.Value ?? user?.FindFirst(ClaimTypes.Role)?.Value;

                var now = DateTime.UtcNow;
                var at = new DateTime(now.Ticks - now.Ticks % TimeSpan.TicksPerMillisecond, DateTimeKind.Utc);
                var f = new[]
                {
                    Clip(eventType, 60), Clip(outcome, 20), Clip(actorRole, 50),
                    Clip(subject == null ? null : LogRedactor.Redact(subject), 150), Clip(SourceApi, 20),
                    Clip(MaskIp(context?.Connection.RemoteIpAddress), 64), Clip(context?.TraceIdentifier, 80),
                    Clip(detail == null ? null : LogRedactor.Redact(detail), 500), Clip(_keyId, 20)
                };
                var canonical = string.Join("|", "v1", at.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture),
                    f[0], f[1], actorUserId.HasValue ? actorUserId.Value.ToString(CultureInfo.InvariantCulture) : "",
                    f[2] ?? "", f[3] ?? "", f[4], f[5] ?? "", f[6] ?? "", f[7] ?? "", f[8]);
                string mac;
                using (var hmac = new HMACSHA256(_key))
                    mac = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical))).Replace("-", "");

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.usp_SecurityAudit_Append", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add("@OccurredAtUtc", SqlDbType.DateTime2).Value = at;
                    command.Parameters.Add("@EventType", SqlDbType.VarChar, 60).Value = f[0];
                    command.Parameters.Add("@Outcome", SqlDbType.VarChar, 20).Value = f[1];
                    command.Parameters.Add("@ActorUserId", SqlDbType.BigInt).Value = (object)actorUserId ?? DBNull.Value;
                    command.Parameters.Add("@ActorRole", SqlDbType.NVarChar, 50).Value = (object)f[2] ?? DBNull.Value;
                    command.Parameters.Add("@Subject", SqlDbType.NVarChar, 150).Value = (object)f[3] ?? DBNull.Value;
                    command.Parameters.Add("@SourceApi", SqlDbType.VarChar, 20).Value = f[4];
                    command.Parameters.Add("@ClientIp", SqlDbType.VarChar, 64).Value = (object)f[5] ?? DBNull.Value;
                    command.Parameters.Add("@CorrelationId", SqlDbType.NVarChar, 80).Value = (object)f[6] ?? DBNull.Value;
                    command.Parameters.Add("@Detail", SqlDbType.NVarChar, 500).Value = (object)f[7] ?? DBNull.Value;
                    command.Parameters.Add("@KeyId", SqlDbType.VarChar, 20).Value = f[8];
                    command.Parameters.Add("@Canonical", SqlDbType.NVarChar, 2000).Value = canonical;
                    command.Parameters.Add("@Mac", SqlDbType.Char, 64).Value = mac;
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                AppFileLog.Write("errors", "WARN", "SecurityAudit", "Security audit write failed for " + eventType + ". Run New API script 09 if the table is missing.", ex, null, sendAlert: false);
            }
        }

        private static string Clip(string value, int max)
        {
            if (value == null) return null;
            var clean = value.Replace('|', '/').Replace('\r', ' ').Replace('\n', ' ').Trim();
            return clean.Length <= max ? clean : clean.Substring(0, max);
        }

        private static string MaskIp(IPAddress ip)
        {
            if (ip == null) return null;
            if (ip.IsIPv4MappedToIPv6) ip = ip.MapToIPv4();
            var bytes = ip.GetAddressBytes();
            if (bytes.Length == 4) { bytes[3] = 0; return new IPAddress(bytes) + "/24"; }
            for (var i = 6; i < bytes.Length; i++) bytes[i] = 0;
            return new IPAddress(bytes) + "/48";
        }
    }

    /// <summary>LOGIN row per call (SUCCESS / DENIED / FAILURE); on success also TOKEN_ISSUED with the user id from the response.</summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LoginAuditAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string subject = null;
            foreach (var arg in context.ActionArguments.Values)
            {
                var prop = arg?.GetType().GetProperty("UserName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var text = prop?.GetValue(arg) as string;
                if (!string.IsNullOrWhiteSpace(text)) { subject = "UserName=" + text.Trim(); break; }
            }

            var executed = await next();
            int status;
            if (executed.Exception != null && !executed.ExceptionHandled) status = 500;
            else if (executed.Result is ObjectResult) status = ((ObjectResult)executed.Result).StatusCode ?? 200;
            else if (executed.Result is StatusCodeResult) status = ((StatusCodeResult)executed.Result).StatusCode;
            else status = context.HttpContext.Response.StatusCode;
            var outcome = status >= 200 && status < 300 ? "SUCCESS" : (status == 401 || status == 403) ? "DENIED" : "FAILURE";
            var detail = context.HttpContext.Request.Method + " " + context.HttpContext.Request.Path + " -> " + status;

            await SecurityAudit.WriteAsync(SecurityAudit.Login, context.HttpContext, outcome, subject: subject, detail: detail);

            if (outcome == "SUCCESS")
            {
                var data = Read((executed.Result as ObjectResult)?.Value, "data");
                var actor = PositiveId(Read(data, "ReceptionStaffId")) ?? PositiveId(Read(data, "UserId"));
                await SecurityAudit.WriteAsync(SecurityAudit.TokenIssued, context.HttpContext, "SUCCESS", actor,
                    Convert.ToString(Read(data, "Role"), CultureInfo.InvariantCulture), subject, "via " + context.HttpContext.Request.Path);
            }
        }

        private static long? PositiveId(object value)
        {
            long id;
            return long.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), out id) && id > 0 ? id : (long?)null;
        }

        private static object Read(object source, string name)
        {
            if (source == null) return null;
            var prop = source.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return prop?.GetValue(source);
        }
    }
}
