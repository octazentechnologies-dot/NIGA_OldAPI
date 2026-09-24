using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Common;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the user operations 
    /// </summary>
    public class UserService : IUserService
    {
        NIGACentrumContext context;
        EmailSenderService emailSenderService = new EmailSenderService();
        private readonly ConfigurationModel _site;
        private readonly IConfiguration _config;

        public UserService(NIGACentrumContext centrumContext)
            : this(centrumContext, null, null)
        {
        }

        public UserService(
            NIGACentrumContext centrumContext,
            IOptions<ConfigurationModel> siteOptions,
            IConfiguration config)
        {
            context = centrumContext;
            _site = siteOptions != null ? siteOptions.Value : new ConfigurationModel();
            _config = config;
        }

        /// <summary>
        /// Local vs other env: set ConfigurationModel:SiteUrl (or HostName) in that environment's appsettings.
        /// Dev default is http://localhost:3000. SPA login path is /login.
        /// </summary>
        private string ResolveUiSiteUrl()
        {
            string raw = null;
            if (_site != null)
            {
                if (!string.IsNullOrWhiteSpace(_site.SiteUrl))
                    raw = _site.SiteUrl;
                else if (!string.IsNullOrWhiteSpace(_site.HostName))
                    raw = _site.HostName;
            }
            if (string.IsNullOrWhiteSpace(raw) && _config != null)
            {
                raw = _config["AppSettings:UiBaseUrl"];
            }
            if (string.IsNullOrWhiteSpace(raw))
            {
                raw = "http://localhost:3000";
            }
            return raw.Trim().TrimEnd('/');
        }

        private string BuildWelcomeLoginUrl(string encryptedUserId)
        {
            return ResolveUiSiteUrl()
                + "/login?UserId="
                + Uri.EscapeDataString(encryptedUserId ?? string.Empty);
        }

        public string AddUser(UserModel model, SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel)
        {
            var Message = "";
            // To Do:
            // Convert UserModel object into UserMaster table entity
            // CHeck before save email id and mobile number is already exists or not
            // If user exists then send proper message
            var existingUser = context.UserMaster.Where(x => x.EmailId == model.EmailId).FirstOrDefault();
            if (existingUser != null)
            {
                Message = "User already exists";
            }
            else
            {
                /*Save record in user */
                var userEntity = new UserMaster();
                userEntity.UserName = model.UserName;
                userEntity.UserPassword = UserPasswordHasher.IsHashed(model.UserPassword)
                    ? model.UserPassword
                    : UserPasswordHasher.Hash(model.UserPassword ?? string.Empty);
                userEntity.MobileNo = "";
                userEntity.EmailId = model.EmailId;
                userEntity.CountryId = model.CountryId;
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.CompanyName = model.CompanyName;
                userEntity.DeleteStatus = false;
                userEntity.EnteredBy = model.EnteredBy;
                userEntity.EnteredDate = DateTime.Now;
               // userEntity.RoleId = 16;
                userEntity.RoleId = model.RoleId != null ? model.RoleId : 3;
                userEntity.IsUserActivated = true;
                context.UserMaster.Add(userEntity);
                context.SaveChanges();
                var UserId = userEntity.UserId;
                /*Save record in doctor. */
                var doctorEntity = new Doctor();
                doctorEntity.UserId = Convert.ToInt32(UserId);
                doctorEntity.FirstName = userEntity.FirstName;
                doctorEntity.LastName = userEntity.LastName;
                doctorEntity.MobileNo = userEntity.MobileNo;
                doctorEntity.EmailId = userEntity.EmailId;
                doctorEntity.EnteredBy = userEntity.EnteredBy;
                doctorEntity.EnteredDate = DateTime.Now;
                doctorEntity.DeleteStatus = false;
                context.Add(doctorEntity);
                ////Send mail to user////
                try
                {
                    var EncryptedUserId = EncryptionHelper.Encrypt(userEntity.UserId.ToString());
                    var loginUrl = BuildWelcomeLoginUrl(EncryptedUserId);
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("<P>Click below link to verify your Account</P>");
                    strBody.Append("<h2><a href='" + loginUrl + "'>Click here to redirect</a></h2>");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailSenderModel();
                    emailSenderModel.ToAddress = userEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = "Niga-Centrum Account Verification";
                    emailSenderModel.sentStatus = emailSenderService.SendMail(emailSenderModel, smtpSettingsModel);
                }
                catch (Exception ex)
                {

                }

                context.SaveChanges();
                Message = "Activation link is sent to your email address.Please check your inbox to activate account.";
            }
             { 
               var userEntity = context.UserMaster.FirstOrDefault(x => x.UserId == model.UserId);
               if (userEntity != null)

               {
                    userEntity.UserName = model.UserName;
                    if (!string.IsNullOrWhiteSpace(model.UserPassword))
                    {
                        userEntity.UserPassword = UserPasswordHasher.IsHashed(model.UserPassword)
                            ? model.UserPassword
                            : UserPasswordHasher.Hash(model.UserPassword);
                    }
                    userEntity.FirstName = model.FirstName;
                    userEntity.LastName = model.LastName;
                    userEntity.RoleId = model.RoleId;
                    userEntity.MobileNo = model.MobileNo;
                    userEntity.EmailId = model.EmailId;
                    context.SaveChanges();
                    Message = "User Updated Successfully";
               }
             }
              return Message;
        }

        public UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userEntity = context.UserMaster.FirstOrDefault(x => x.UserId == userId && !x.DeleteStatus);

            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not found";
            }
            return new UserModel
            {
                UserId = userEntity.UserId,
                UserName = userEntity.UserName,
                MobileNo = userEntity.MobileNo,
                EmailId = userEntity.EmailId,
                UserStatus = true,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserPassword = null, // M01 — never return password hash/plaintext
                RoleId = (int)userEntity.RoleId,

                //LastName = "Admin",
                
            };

        }

        public bool ActivateUser(UserModel model, ref ErrorResponseModel errorResponseModel)
        {
            // To Do:
            // Get userentity by user id 
            // Convert UserModel object into UserMaster table entity
            // Check mobile number/ email already exist or not 
            // If user exists then send proper message
            // Update only required fields
            var decryptedUserId = EncryptionHelper.Decrypt(model.EncryptedUserId);
            var UserId = Convert.ToInt32(decryptedUserId);
            var userEntity = context.UserMaster.Where(x => x.UserId == UserId).FirstOrDefault();
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not found";
                return false;

            }
            else
            {
                //UserMaster userMaster = new UserMaster();
                userEntity.IsUserActivated = true;
                userEntity.ChangedBy = model.ChangedBy;
                userEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }

        public int GetCount(ref ErrorResponseModel errorResponseModel)
        {
            var users = context.UserMaster.Where(x => x.UserStatus ==true && x.RoleId == 3 && x.DeleteStatus==false).Count();
            int count=users;
            return count;
        }

        public List<NewUserModel> GetAllUser(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userModelList = new List<NewUserModel>();
            var userEntityList = context.UserMaster.Where(x => x.DeleteStatus == false).ToList();
            if (userEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "User not found";
            }
            userEntityList.ForEach(item =>
            {
                userModelList.Add(new NewUserModel
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    UserStatus = item.UserStatus,
                    EmailId = item.EmailId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    RoleId = item.RoleId,
                });
            });
            return userModelList;
        }

        public string DeleteUser(UserModel userModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var userEntity = context.UserMaster.FirstOrDefault(x => x.UserId == userModel.UserId);
            if (userEntity != null)
            {
                userEntity.DeleteStatus = true;
                //context.Remove(authorEntity);
                context.SaveChanges();
                Message = "User Deleted Successfully";



            }
            return Message;
        }

        /// <summary>
                /// Method is used for forget password.
                /// </summary>
                /// <param name="email"></param>
                /// <param name="errorResponseModel"></param>
                /// <returns></returns>
        public string ForgetPassword(string email, SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel)
        {
            string message = "";
            var userEntity = context.UserMaster.FirstOrDefault(x => x.EmailId == email);
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                message = "Email Not Found";
            }
            else
            {
                try
                {
                    // M01 SEC-02.02 — stop emailing plaintext/hash passwords.
                    // Use New-API POST /api/Account/ForgotPassword (reset link) after PasswordResetToken table exists.
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("Hello " + userEntity.UserName);
                    strBody.Append("<p>A password reset was requested for your Homeo Centrum account.</p>");
                    strBody.Append("<p>For security, passwords are no longer sent by email. Please use the in-app Forgot Password flow (reset link) or contact your administrator.</p>");
                    strBody.Append("</body>");
                    var emailModel = new EmailSenderModel();
                    emailModel.ToAddress = email;
                    emailModel.Body = strBody.ToString();
                    emailModel.isHtml = true;
                    emailModel.Subject = GlobalConstants.ForgotPassword;
                    if (!string.IsNullOrEmpty(emailModel.ToAddress))
                    {
                        emailModel.sentStatus = emailSenderService.SendMail(emailModel, smtpSettingsModel);
                    }
                    message = "If the email exists, reset instructions were sent.";
                }
                catch (Exception ex)
                {
                }
            }
            return message;
        }
    }
}
