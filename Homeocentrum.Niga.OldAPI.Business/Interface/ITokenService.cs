
using System.Threading.Tasks;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;

namespace Homeocentrum.Niga.OldAPI.Business.Interfaces
{
    public interface ITokenService
    {
        /// <param name="roleName">RoleMaster.RoleName — embedded in JWT for AdminPortal ACL (M02 W0).</param>
        /// <param name="doctorId">M01 SEC-01.02 — DoctorID claim for ownership checks.</param>
        Task<string> CreateToken(UserMaster user, int expiryMin = 0, string roleName = null, int? doctorId = null);

        Task<string> CreateReceptionStaffToken(
            int receptionStaffId,
            string userId,
            int doctorId,
            string fullName,
            string roleName,
            int? roleId,
            int? doctorUserId,
            int expiryMin = 0);
    }
}
