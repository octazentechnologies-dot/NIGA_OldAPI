using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NIGA.Centrum.Common
{
    /// <summary>
    /// SEC-05.01 — Doctor ownership helpers for patient / appointment / case / Rx / notes / labs.
    /// Prefer JWT DoctorID claim; AdminPortal users may bypass ownership checks.
    /// </summary>
    public static class DoctorOwnership
    {
        public const string DoctorIdClaim = "DoctorID";

        public static int? GetDoctorId(ClaimsPrincipal user)
        {
            if (user == null)
                return null;

            var value = user.FindFirst(DoctorIdClaim)?.Value
                ?? user.FindFirst("DoctorId")?.Value
                ?? user.FindFirst("doctorId")?.Value;

            return int.TryParse(value, out var doctorId) && doctorId > 0 ? doctorId : (int?)null;
        }

        public static int? GetUserId(ClaimsPrincipal user)
        {
            if (user == null)
                return null;

            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("UserId")?.Value
                ?? user.FindFirst("userId")?.Value;

            return int.TryParse(value, out var userId) && userId > 0 ? userId : (int?)null;
        }

        public static bool IsAdminPortalUser(ClaimsPrincipal user)
            => AdminAuthorizationPolicies.IsAdminPortalUser(user);

        public static int? GetDoctorUserId(ClaimsPrincipal user)
        {
            if (user == null)
                return null;

            var value = user.FindFirst("DoctorUserID")?.Value
                ?? user.FindFirst("DoctorUserId")?.Value;
            return int.TryParse(value, out var doctorUserId) && doctorUserId > 0 ? doctorUserId : (int?)null;
        }

        public static string GetRoleName(ClaimsPrincipal user)
        {
            if (user == null)
                return null;
            return user.FindFirst(ClaimTypes.Role)?.Value
                ?? user.FindFirst("RoleName")?.Value;
        }

        public static bool EnsureDoctorOwns(ClaimsPrincipal user, int resourceDoctorId)
        {
            if (IsAdminPortalUser(user))
                return true;

            var jwtDoctorId = GetDoctorId(user);
            return jwtDoctorId.HasValue && jwtDoctorId.Value == resourceDoctorId;
        }

        public static bool EnsureDoctorOwns(ClaimsPrincipal user, int? resourceDoctorId)
        {
            if (!resourceDoctorId.HasValue || resourceDoctorId.Value <= 0)
                return IsAdminPortalUser(user);

            return EnsureDoctorOwns(user, resourceDoctorId.Value);
        }

        public static IActionResult ForbidIfNotOwner(ClaimsPrincipal user, int? resourceDoctorId)
        {
            if (EnsureDoctorOwns(user, resourceDoctorId))
                return null;

            return new ObjectResult(new { success = false, message = "Access denied for this doctor resource." })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        public static bool EnsureCallerIsUserOrAdmin(ClaimsPrincipal user, long targetUserId)
        {
            if (IsAdminPortalUser(user))
                return true;

            var jwtUserId = GetUserId(user);
            if (jwtUserId.HasValue && jwtUserId.Value == (int)targetUserId)
                return true;

            var doctorUserId = GetDoctorUserId(user);
            return doctorUserId.HasValue && doctorUserId.Value == (int)targetUserId;
        }

        public static IActionResult ForbidIfNotCallerOrAdmin(ClaimsPrincipal user, long targetUserId)
        {
            if (EnsureCallerIsUserOrAdmin(user, targetUserId))
                return null;

            return new ObjectResult(new { success = false, message = "Access denied for this doctor resource." })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }

        /// <summary>
        /// CLN-02.02 — Only the treating doctor (or AdminPortal) may run case-taking.
        /// Reception and Patient JWTs are 403 even when DoctorID is present (reception staff).
        /// Matches New-API DoctorOwnership.ForbidIfReception.
        /// </summary>
        public static IActionResult ForbidIfReception(ClaimsPrincipal user)
            => ForbidIfNotTreatingDoctor(user);

        public static IActionResult ForbidIfNotTreatingDoctor(ClaimsPrincipal user)
        {
            if (IsAdminPortalUser(user))
                return null;

            var role = GetRoleName(user);
            if (!string.IsNullOrWhiteSpace(role)
                && (role.Equals("Reception", StringComparison.OrdinalIgnoreCase)
                    || role.Equals("Patient", StringComparison.OrdinalIgnoreCase)))
            {
                return new ObjectResult(new { success = false, message = "Only the treating doctor can run case taking." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            if (!string.IsNullOrWhiteSpace(role)
                && role.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                return null;

            if (GetDoctorId(user).HasValue)
                return null;

            return new ObjectResult(new { success = false, message = "Only the treating doctor can run case taking." })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}
