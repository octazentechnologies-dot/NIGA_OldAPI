using System;
using System.Linq;
using System.Security.Claims;

namespace NIGA.Centrum.Common
{
    /// <summary>
    /// M02 W0 foundation: shared Admin Portal authorization for clinical masters mutate APIs.
    /// Apply with [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)] on mutate endpoints (W1+).
    /// New probe/verify APIs live on New-API only; this policy is for Old-API mutate hardening.
    /// </summary>
    public static class AdminAuthorizationPolicies
    {
        public const string AdminPortal = "AdminPortal";

        /// <summary>RoleId 1 = SuperUser / Admin in HomeoCentrum RoleMaster.</summary>
        public const int SuperUserRoleId = 1;

        public static readonly string[] AdminPortalRoleNames =
        {
            "Admin",
            "Management"
        };

        public static bool IsAdminPortalUser(ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return false;

            var roleIdValue = user.FindFirst("RoleId")?.Value
                ?? user.FindFirst("roleId")?.Value;
            if (int.TryParse(roleIdValue, out var roleId) && roleId == SuperUserRoleId)
                return true;

            foreach (var claim in user.FindAll(ClaimTypes.Role))
            {
                if (IsAdminPortalRoleName(claim.Value))
                    return true;
            }

            var roleName = user.FindFirst("RoleName")?.Value
                ?? user.FindFirst("role")?.Value;
            return IsAdminPortalRoleName(roleName);
        }

        public static bool IsAdminPortalRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            return AdminPortalRoleNames.Any(allowed =>
                string.Equals(allowed, roleName.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public static int? GetRoleId(ClaimsPrincipal user)
        {
            var roleIdValue = user?.FindFirst("RoleId")?.Value
                ?? user?.FindFirst("roleId")?.Value;
            if (int.TryParse(roleIdValue, out var roleId))
                return roleId;
            return null;
        }

        public static string GetRoleName(ClaimsPrincipal user)
        {
            return user?.FindFirst("RoleName")?.Value
                ?? user?.FindFirst(ClaimTypes.Role)?.Value
                ?? user?.FindFirst("role")?.Value;
        }
    }
}
