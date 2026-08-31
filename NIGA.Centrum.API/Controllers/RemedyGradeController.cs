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
    /// APIs for Remedy grade entity 
    /// </summary>
    [Route("api/remedygrade")]
    [ApiController]
    [Authorize]
    public class RemedyGradeController : BaseAPIController
    {
        IRemedyGradeService _remedygradeService;
        /// <summary>
        /// Used to initialize controller and inject remedy grade service
        /// </summary>
        /// <param name="remedygradeService"></param>
        public RemedyGradeController(IRemedyGradeService remedygradeService)
        {
            _remedygradeService = remedygradeService;
        }

        /// <summary>
        /// To get remedy grade by RemedyGrade ID 
        /// </summary>
        /// <param name="remedygradeId"></param>
        /// <returns></returns>
        [HttpGet("{gradeId}")]
        [ProducesResponseType(typeof(RemedyGradeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRemedyGradeById(long gradeId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var countryModel = _remedygradeService.GetRemedyGradeById(gradeId, ref errorResponseModel);

                if (countryModel != null)
                {
                    return Ok(countryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all remedygrades
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetRemedyGrades")]
        [ProducesResponseType(typeof(RemedyGradeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRemedyGrades()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedygradeModelList = _remedygradeService.GetRemedyGrades(ref errorResponseModel);

                if (remedygradeModelList != null)
                {
                    return Ok(remedygradeModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Remedy Grade 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RemedyGradeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveRemedyGrade(RemedyGradeModel remedyGradeModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedygradeModel = _remedygradeService.SaveRemedyGrade(remedyGradeModel, ref errorResponseModel);

                if (remedygradeModel != null)
                {
                    return Ok(remedygradeModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Remedy Grade 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteRemedyGrade")]
        [ProducesResponseType(typeof(RemedyGradeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteRemedyGrade(RemedyGradeModel remedyGradeModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedygradeModel = _remedygradeService.DeleteRemedyGrade(remedyGradeModel, ref errorResponseModel);

                if (remedygradeModel != null)
                {
                    return Ok(remedygradeModel);
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