using System;
using System.Data.SqlClient;
using System.Security.Claims;
using Homeocentrum.Niga.OldAPI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Homeocentrum.Niga.OldAPI.Security
{
    /// <summary>
    /// Patient-level ownership check (same rules as the New API PatientAccessGuard):
    /// admin portal users, the doctor (or that doctor's reception) who has a case or appointment
    /// for the patient, or the patient's own / family login.
    /// </summary>
    public static class PatientAccess
    {
        private static string _connectionString;

        public static void Initialize(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public static bool CanAccess(ClaimsPrincipal user, long patientId)
        {
            if (user == null || patientId <= 0)
                return false;
            if (DoctorOwnership.IsAdminPortalUser(user))
                return true;
            if (string.IsNullOrEmpty(_connectionString))
                return false;

            var doctorId = DoctorOwnership.GetDoctorId(user);
            var userId = DoctorOwnership.GetUserId(user);
            var role = DoctorOwnership.GetRoleName(user) ?? "";
            var isPatientRole = role.Equals("Patient", StringComparison.OrdinalIgnoreCase)
                || role.Equals("Caregiver", StringComparison.OrdinalIgnoreCase);

            const string sql = @"
SELECT CASE WHEN
    (@doctorId IS NOT NULL AND (
        EXISTS (SELECT 1 FROM dbo.CaseEntryDetails WHERE PatientId = @patientId AND DoctorId = @doctorId AND ISNULL(DeleteStatus, 0) = 0)
        OR EXISTS (SELECT 1 FROM dbo.PatientAppointment WHERE PatientId = @patientId AND DoctorId = @doctorId)))
    OR (@patientUserId IS NOT NULL AND
        EXISTS (SELECT 1 FROM dbo.PatientUserMap WHERE PatientId = @patientId AND UserId = @patientUserId AND DeleteStatus = 0))
THEN 1 ELSE 0 END";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@patientId", patientId);
                command.Parameters.AddWithValue("@doctorId", isPatientRole || !doctorId.HasValue ? (object)DBNull.Value : doctorId.Value);
                command.Parameters.AddWithValue("@patientUserId", isPatientRole && userId.HasValue ? (object)userId.Value : DBNull.Value);
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
        }

        /// <summary>Patient id of an existing lab row, or null when the row does not exist.</summary>
        public static int? LabRowPatientId(string table, long rowId)
        {
            if (rowId <= 0 || string.IsNullOrEmpty(_connectionString))
                return null;
            string sql;
            if (table == "PatientLabOrder")
                sql = "SELECT PatientId FROM dbo.PatientLabOrder WHERE PatientOrderedTestId = @id";
            else if (table == "PatientLabEntry")
                sql = "SELECT PatientId FROM dbo.PatientLabEntry WHERE PatientLabId = @id";
            else
                throw new ArgumentOutOfRangeException(nameof(table));

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", rowId);
                connection.Open();
                var value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
            }
        }

        public static IActionResult ForbidIfNoAccess(ClaimsPrincipal user, long patientId)
        {
            if (CanAccess(user, patientId))
                return null;
            return Forbidden();
        }

        public static IActionResult Forbidden()
        {
            return new ObjectResult(new { success = false, message = "Access denied for this patient." })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}
