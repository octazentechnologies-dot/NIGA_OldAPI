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
    /// APIs for Qualification entity 
    /// </summary>
    [Route("api/qualification")]
    [ApiController]
    [Authorize]
    public class QualificationController : BaseAPIController
    {
        IQualificationService _qualificationService;
        /// <summary>
        /// Used to initialize controller and inject qualification service
        /// </summary>
        /// <param name="qualificationService"></param>
        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }

        /// <summary>
        /// To get qualification by Qualification ID 
        /// </summary>
        /// <param name="qualificationId"></param>
        /// <returns></returns>
        [HttpGet("{qualificationId}")]
        [ProducesResponseType(typeof(QualificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQualificationById(long qualificationId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var qualificationModel = _qualificationService.GetQualificationById(qualificationId, ref errorResponseModel);

                if (qualificationModel != null)
                {
                    return Ok(qualificationModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all qualifications
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetQualifications")]
        [ProducesResponseType(typeof(QualificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQualifications()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var qualificationModelList = _qualificationService.GetQualifications(ref errorResponseModel);

                if (qualificationModelList != null)
                {
                    return Ok(qualificationModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Qualification 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(QualificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveQualification(QualificationModel qualificationModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var qualificationmodel = _qualificationService.SaveQualification(qualificationModel, ref errorResponseModel);

                if (qualificationmodel != null)
                {
                    return Ok(qualificationmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Qualification 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteQualification")]
        [ProducesResponseType(typeof(QualificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteQualification(QualificationModel qualificationModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var qualificationmodel = _qualificationService.DeleteQualification(qualificationModel, ref errorResponseModel);

                if (qualificationmodel != null)
                {
                    return Ok(qualificationmodel);
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