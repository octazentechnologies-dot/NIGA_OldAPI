using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Configuration;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    /// <summary>
    /// APIs for User entity 
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseAPIController
    {
        IUserService _userService;
        //IConfiguration _iconfiguration;
        private readonly IOptions<SmtpSettingsModel> _mailSettings;
        /// <summary>
        /// Used to initialize controller and inject user service
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService, /*IConfiguration iconfiguration*/ IOptions<SmtpSettingsModel> mailSettings)
        {
            _userService = userService;
            _mailSettings = mailSettings;
        }

        /// <summary>
        /// To get user by User ID 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                if(userId <= 0)
                {
                    return BadRequest("Invalid data");
                }
                long callerId;
                var isSelf = long.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out callerId) && callerId == userId;
                if (!isSelf && !Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.IsAdminPortalUser(User))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { success = false, message = "Access denied for this user." });
                }
                var userModel = _userService.GetUserById(userId, ref errorResponseModel);

                if(userModel != null)
                {
                    return Ok(userModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult Post(UserModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request, please verify details");
            }

            try
            {
                var errorMessage = new ErrorResponseModel();
                //var Settings= _iconfiguration.GetValue<SmtpSettingsModel>("smtp");
                var gemailSettings = _mailSettings.Value;
                var userModel = _userService.AddUser(model, gemailSettings, ref errorMessage);
                if(userModel!="")
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorMessage);
                
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }

        }


        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ActivateUser")]
        public IActionResult ActivateUser(UserModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request, please verify details");
            }

            try
            {
                var errorMessage = new ErrorResponseModel();
                //var Settings= Configuration.GetValue<SmtpSettingsModel>("smtp");
                var gemailSettings = _mailSettings.Value;
                var userModel = _userService.ActivateUser(model,ref errorMessage);
                if (userModel)
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorMessage);

            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }

        }

        [HttpGet]
         [Authorize]
        [Route("GetCount")]
       // [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
               
                var userModel = _userService.GetCount( ref errorResponseModel);

                if (userModel != null)
                {
                    return Ok(userModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

      
        [HttpGet]
        [Authorize(Policy = Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.AdminPortal)]
        [ProducesResponseType(typeof(NewUserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllUser()
        {
            ErrorResponseModel errorResponseModel = null; try
            {

                var userModel = _userService.GetAllUser(ref errorResponseModel);

                if (userModel != null) { return Ok(userModel); }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex) 
            { 
                return ServerError(ex); 
            }
        }

        [HttpPost]
        [Route("DeleteUser")]
        [Authorize(Policy = Homeocentrum.Niga.OldAPI.Common.AdminAuthorizationPolicies.AdminPortal)]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteUser(UserModel userModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var UserModel = _userService.DeleteUser(userModel, ref errorResponseModel);



                if (UserModel != null)
                {
                    return Ok(UserModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        [HttpPost("ForgetPassword")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult ForgetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request, please verify details");
            }

            try
            {
                var gemailSettings = _mailSettings.Value;
                var errorMessage = new ErrorResponseModel();
                var userModel = _userService.ForgetPassword(email, gemailSettings, ref errorMessage);
                if (userModel != "")
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorMessage);



            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
    }
}