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
    /// APIs for Partlocation entity 
    /// </summary>
    [Route("api/partlocation")]
    [ApiController]
    [Authorize]
    public class PartLocationController : BaseAPIController
    {
        IPartLocationService _partlocationService;
        /// <summary>
        /// Used to initialize controller and inject part location service
        /// </summary>
        /// <param name="partlocationService"></param>
        public PartLocationController(IPartLocationService partlocationService)
        {
            _partlocationService = partlocationService;
        }

        /// <summary>
        /// To get part location by partlocation ID 
        /// </summary>
        /// <param name="partlocationId"></param>
        /// <returns></returns>
        [HttpGet("{partlocationId}")]
        [ProducesResponseType(typeof(PartLocationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPartLocationById(long partlocationId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var partlocationModel = _partlocationService.GetPartLocationById(partlocationId, ref errorResponseModel);

                if (partlocationModel != null)
                {
                    return Ok(partlocationModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all partlocations
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetPartLocations")]
        [ProducesResponseType(typeof(PartLocationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPartLocations()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var partlocationModelList = _partlocationService.GetPartLocations(ref errorResponseModel);

                if (partlocationModelList != null)
                {
                    return Ok(partlocationModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new part location 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PartLocationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePartLocation(PartLocationModel partLocationModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var partlocationModel = _partlocationService.SavePartLocation(partLocationModel, ref errorResponseModel);

                if (partlocationModel != null)
                {
                    return Ok(partlocationModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete part location 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePartLocation")]
        [ProducesResponseType(typeof(PartLocationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeletePartLocation(PartLocationModel partLocationModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var partlocationModel = _partlocationService.DeletePartLocation(partLocationModel, ref errorResponseModel);

                if (partlocationModel != null)
                {
                    return Ok(partlocationModel);
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