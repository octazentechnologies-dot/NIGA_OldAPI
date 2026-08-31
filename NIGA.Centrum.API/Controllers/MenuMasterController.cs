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
    /// APIs for MenuMaster entity 
    /// </summary>
    [Route("api/menuMaster")]
    [ApiController]
    [Authorize]
    public class MenuMasterController : BaseAPIController
    {
        IMenuMasterService _menuMasterService;
        /// <summary>
        /// Used to initialize controller and inject menumaster service
        /// </summary>
        /// <param name="menuMasterService"></param>
        public MenuMasterController(IMenuMasterService menuMasterService)
        {
            _menuMasterService = menuMasterService;
        }

        /// <summary>
        /// To get menu by menu ID 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet("{menuId}")]
        [ProducesResponseType(typeof(MenuMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMenuById(long menuId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var menuMasterModel = _menuMasterService.GetMenuById(menuId, ref errorResponseModel);

                if (menuMasterModel != null)
                {
                    return Ok(menuMasterModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all menumaster
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetMenuMaster")]
        [ProducesResponseType(typeof(MenuMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMenuMaster()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var menuMasterModelList = _menuMasterService.GetMenuMaster(ref errorResponseModel);

                if (menuMasterModelList != null)
                {
                    return Ok(menuMasterModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new menuMaster 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MenuMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveMenuMaster(MenuMasterModel menuMasterModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var menumastermodel = _menuMasterService.SaveMenuMaster(menuMasterModel, ref errorResponseModel);

                if (menumastermodel != null)
                {
                    return Ok(menumastermodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete MenuMaster 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteMenuMaster")]
        [ProducesResponseType(typeof(MenuMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteMenuMaster(MenuMasterModel menuMasterModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var menumastermodel = _menuMasterService.DeleteMenuMaster(menuMasterModel, ref errorResponseModel);

                if (menumastermodel != null)
                {
                    return Ok(menumastermodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get menu by userID 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet("GetMenuByUserId/{userId}")]
        [ProducesResponseType(typeof(MenuMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMenuByUserId(long userId, int? firmIds)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var menuMasterModel = _menuMasterService.GetMenuByUserId(userId, firmIds, ref errorResponseModel);

                if (menuMasterModel != null)
                {
                    return Ok(menuMasterModel);
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