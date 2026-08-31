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
            var userEntity = _centrumContext.UserMaster.FirstOrDefault(x => x.UserName == userName && x.UserPassword == password && x.IsUserActivated==true);

            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not  found. Please enter valid credentials";
                return null;
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
                var userSubscription = _centrumContext.PackageEntryDetails.Where(x => x.DoctorId == userEntity.UserId && x.IsActive==true).FirstOrDefault();
                if (userSubscription != null)
                {
                    userData.IsPlanActive= true;
                    TimeSpan difference =Convert.ToDateTime(userSubscription.ExpiryDate) - DateTime.Now;
                    if (difference.Days <= 5)
                    {
                        userData.IslastFiveDays = true;
                        userData.DaysRemaining  = difference.Days;
                    }
                }
                else
                {
                    userData.IsPlanActive = false;
                    userData.IslastFiveDays = false;
                    userData.DaysRemaining = 0;
                }
            
            }


            return userData;
        }
    }
}
