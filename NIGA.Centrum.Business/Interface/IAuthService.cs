using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// This method is used to validate user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AuthModel AuthenticateUser(string userName, string password, ref ErrorResponseModel errorResponseModel);
    }
}
