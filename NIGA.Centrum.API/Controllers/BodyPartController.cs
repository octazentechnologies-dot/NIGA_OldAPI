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
    /// APIs for bodypart entity 
    /// </summary>
    [Route("api/bodypart")]
    [ApiController]
    [Authorize]
    public class BodyPartController : BaseAPIController
    {
        IBodyPartService _bodypartService;
        /// <summary>
        /// Used to initialize controller and inject bodypart service
        /// </summary>
        /// <param name="bodypartService"></param>
        public BodyPartController(IBodyPartService bodypartService)
        {
            _bodypartService = bodypartService;
        }

        /// <summary>
        /// To get body part by BodyPart ID 
        /// </summary>
        /// <param name="bodypartId"></param>
        /// <returns></returns>
        [HttpGet("GetBodyPartById/{bodyPartId}")]
        [ProducesResponseType(typeof(BodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetBodyPartById(long bodyPartId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var bodypartModel = _bodypartService.GetBodyPartById(bodyPartId, ref errorResponseModel);

                if (bodypartModel != null)
                {
                    return Ok(bodypartModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all bodyparts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetBodyParts()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var bodypartModelList = _bodypartService.GetBodyParts(ref errorResponseModel);

                if (bodypartModelList != null)
                {
                    return Ok(bodypartModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new BodyPart 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveBodyPart(BodyPartModel bodypartModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var bodyPartModel = _bodypartService.SaveBodyPart(bodypartModel, ref errorResponseModel);

                if (bodyPartModel != null)
                {
                    return Ok(bodyPartModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete bodypart 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteBodyPart")]
        [ProducesResponseType(typeof(BodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteBodyPart(BodyPartModel bodypartModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var bodyPartModel = _bodypartService.DeleteBodyPart(bodypartModel, ref errorResponseModel);

                if (bodyPartModel != null)
                {
                    return Ok(bodyPartModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To get body part by BodyPart ID 
        /// </summary>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        [HttpGet("GetBodyPartsBySection/{SectionId}")]
        [ProducesResponseType(typeof(BodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetBodyPartsBySection(long SectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var bodypartModel = _bodypartService.GetBodyPartBySection(SectionId, ref errorResponseModel);

                if (bodypartModel != null)
                {
                    return Ok(bodypartModel);
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