using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for RoleDetails entity 
    /// </summary>
    [Route("api/roleDetails")]
    [ApiController]
    [Authorize]
    public class RoleDetailsController : BaseAPIController
    {
        IRoleDetailsService _roleDetailsService;
        /// <summary>
        /// Used to initialize controller and inject roledetails service
        /// </summary>
        /// <param name="menuMasterService"></param>
        public RoleDetailsController(IRoleDetailsService roleDetailsService)
        {
            _roleDetailsService = roleDetailsService;
        }

        /// <summary>
        /// To get role details by record ID 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        [HttpGet("{recordId}")]
        [ProducesResponseType(typeof(RoleDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRoleDetailsById(long recordId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var roleDetailsModel = _roleDetailsService.GetRoleDetailsById(recordId, ref errorResponseModel);

                if (roleDetailsModel != null)
                {
                    return Ok(roleDetailsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all roledetails
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetRoleDetails")]
        [ProducesResponseType(typeof(RoleDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRoleDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var roleDetailsModelList = _roleDetailsService.GetRoleDetails(ref errorResponseModel);

                if (roleDetailsModelList != null)
                {
                    return Ok(roleDetailsModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new roleDetails
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RoleDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveRoleDetails(RoleDetailsModel roleDetailsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var roledetailsmodel = _roleDetailsService.SaveRoleDetails(roleDetailsModel, ref errorResponseModel);

                if (roledetailsmodel != null)
                {
                    return Ok(roledetailsmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete RoleDetails
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteRoleDetails")]
        [ProducesResponseType(typeof(RoleDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteRoleDetails(RoleDetailsModel roleDetailsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var roledetailsmodel = _roleDetailsService.DeleteRoleDetails(roleDetailsModel, ref errorResponseModel);

                if (roledetailsmodel != null)
                {
                    return Ok(roledetailsmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
    }
}