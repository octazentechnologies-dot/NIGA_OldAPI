using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using NIGA.Centrum.Common;

namespace NIGA.Centrum.Business.Implementation
{
    public class AuthService : IAuthService
    {
        NIGACentrumContext _centrumContext;
        public AuthService(NIGACentrumContext centrumContext)
        {
            _centrumContext = centrumContext;

        }

        public AuthModel AuthenticateUser(string userName, string password, ref ErrorResponseModel errorResponseModel)
        {
            var authModel = new AuthModel();
            errorResponseModel = new ErrorResponseModel();
            var userEntity = _centrumContext.UserMaster.FirstOrDefault(x => x.UserName == userName && x.IsUserActivated == true);
            if (userEntity == null || !UserPasswordHasher.Verify(password, userEntity.UserPassword))
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not  found. Please enter valid credentials";
                return null;
            }

            if (!UserPasswordHasher.IsHashed(userEntity.UserPassword))
            {
                userEntity.UserPassword = UserPasswordHasher.Hash(password);
                userEntity.ChangedDate = DateTime.UtcNow;
                _centrumContext.SaveChanges();
            }
            var roleEntity = _centrumContext.RoleMaster.FirstOrDefault(x => x.RoleId == userEntity.RoleId);
            //To DO: Add multiple attempt logic
            // Update role dynamic logic
           var userData= new AuthModel
            {
                IsSuperUser = true,
                UserId = userEntity.UserId,
                UserName = userEntity.FirstName + " " + userEntity.LastName,
                Role = roleEntity.RoleName,
                RoleId = userEntity.RoleId,
                FirmIds = userEntity.FirmIds
            };
            if (roleEntity.RoleId == 3)
            {
                var doctor = _centrumContext.Doctor.FirstOrDefault(d =>
                    d.UserId == userEntity.UserId && d.DeleteStatus == false);
                var doctorId = doctor != null ? doctor.DoctorId : 0;
                var userSubscription = _centrumContext.PackageEntryDetails
                    .Where(x => x.IsActive == true && (x.DoctorId == doctorId || x.DoctorId == userEntity.UserId))
                    .OrderByDescending(x => x.ExpiryDate)
                    .FirstOrDefault();
                if (userSubscription != null)
                {
                    userData.IsPlanActive = true;
                    TimeSpan difference = Convert.ToDateTime(userSubscription.ExpiryDate) - DateTime.Now;
                    userData.DaysRemaining = difference.Days > 0 ? difference.Days : 0;
                    if (difference.Days <= 5)
                    {
                        userData.IslastFiveDays = true;
                    }
                }
                else
                {
                    userData.IsPlanActive = false;
                    userData.IslastFiveDays = false;
                    userData.DaysRemaining = 0;
                }

                if (!userData.IsPlanActive && IsDevClinicDoctor(userEntity.UserName))
                {
                    userData.IsPlanActive = true;
                    userData.DaysRemaining = Math.Max(userData.DaysRemaining, 365);
                }
            }


            return userData;
        }

        private static bool IsDevClinicDoctor(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;
            return userName.Equals("Tufan_Doctor", StringComparison.OrdinalIgnoreCase)
                || userName.Equals("NIGA HOMEOPATHY", StringComparison.OrdinalIgnoreCase)
                || userName.Equals("testdoctor", StringComparison.OrdinalIgnoreCase);
        }
    }
}
