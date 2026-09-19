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
            return jwtUserId.HasValue && jwtUserId.Value == (int)targetUserId;
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
        /// CLN-02.02 — Reception JWT cannot run case-taking / clinical mutate APIs.
        /// AdminPortal is allowed. Matches New-API DoctorOwnership.ForbidIfReception.
        /// </summary>
        public static IActionResult ForbidIfReception(ClaimsPrincipal user)
        {
            if (IsAdminPortalUser(user))
                return null;

            var role = user?.FindFirst(ClaimTypes.Role)?.Value
                ?? user?.FindFirst("RoleName")?.Value;
            if (!string.IsNullOrWhiteSpace(role)
                && role.Equals("Reception", StringComparison.OrdinalIgnoreCase))
            {
                return new ObjectResult(new { success = false, message = "Only the treating doctor can run case taking." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            return null;
        }
    }
}
