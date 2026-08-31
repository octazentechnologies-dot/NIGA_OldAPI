
using System.Threading.Tasks;
using NIGA.Centrum.Entity.DataModels;

namespace NIGA.Centrum.Business.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserMaster user, int expiryMin = 0);

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
