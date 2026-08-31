using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Interface used for user related operations
/// </summary>
namespace NIGA.Centrum.Business.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Method is used to get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method used to create user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddUser(UserModel model,SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used to update user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        bool ActivateUser(UserModel model, ref ErrorResponseModel errorResponseModel);
        int GetCount(ref ErrorResponseModel errorResponseModel);
         List<NewUserModel> GetAllUser(ref ErrorResponseModel errorResponseModel);
        string DeleteUser(UserModel userModel, ref ErrorResponseModel errorResponseModel);


        /// <summary>
                /// Method is used to Forget password
                /// </summary>
                /// <param name="email"></param>
                /// <returns></returns>
        string ForgetPassword(string email, SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel);

    }
}
