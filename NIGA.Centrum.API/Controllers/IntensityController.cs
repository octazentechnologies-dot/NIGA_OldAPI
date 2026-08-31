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
    /// APIs for Intensity entity 
    /// </summary>
    [Route("api/intensity")]
    [ApiController]
    [Authorize]
    public class IntensityController : BaseAPIController
    {
        IIntensityService _intensityService;
        /// <summary>
        /// Used to initialize controller and inject country service
        /// </summary>
        /// <param name="intensityService"></param>
        public IntensityController(IIntensityService intensityService)
        {
            _intensityService = intensityService;
        }

        /// <summary>
        /// To get intensity by Intensity ID 
        /// </summary>
        /// <param name="intensityId"></param>
        /// <returns></returns>
        [HttpGet("{intensityId}")]
        [ProducesResponseType(typeof(IntensityModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetIntensityById(long intensityId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var intensityModel = _intensityService.GetIntensityById(intensityId, ref errorResponseModel);

                if (intensityModel != null)
                {
                    return Ok(intensityModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all Intensities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IntensityModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetIntensities()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var intensityModelList = _intensityService.GetIntensities(ref errorResponseModel);

                if (intensityModelList != null)
                {
                    return Ok(intensityModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Intensity 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IntensityModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveIntensity(IntensityModel intensityModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var intensitymodel = _intensityService.SaveIntensity(intensityModel, ref errorResponseModel);

                if (intensitymodel != null)
                {
                    return Ok(intensitymodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Intensity 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteIntensity")]
        [ProducesResponseType(typeof(IntensityModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteIntensity(IntensityModel intensityModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var intensitymodel = _intensityService.DeleteIntensity(intensityModel, ref errorResponseModel);

                if (intensitymodel != null)
                {
                    return Ok(intensitymodel);
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