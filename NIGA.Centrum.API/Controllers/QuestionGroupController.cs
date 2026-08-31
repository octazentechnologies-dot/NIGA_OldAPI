using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for Question Group entity 
    /// </summary>
    [Route("api/questiongroup")]
    [ApiController]
    [Authorize]
    public class QuestionGroupController : BaseAPIController
    {
        IQuestionGroupService _questiongroupService;
        /// <summary>
        /// Used to initialize controller and inject question group
        /// </summary>
        /// <param name="questiongroupService"></param>
        public QuestionGroupController(IQuestionGroupService questiongroupService)
        {
            _questiongroupService = questiongroupService;
        }

        /// <summary>
        /// To get questiongroup by QuestionGroup ID 
        /// </summary>
        /// <param name="questiongroupId"></param>
        /// <returns></returns>
        [HttpGet("{questiongroupId}")]
        [ProducesResponseType(typeof(QuestionGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionGroupById(long questiongroupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questiongroupModel = _questiongroupService.GetQuestionGroupById(questiongroupId, ref errorResponseModel);

                if (questiongroupModel != null)
                {
                    return Ok(questiongroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all questiongroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(QuestionGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionGroup()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questiongroupModelList = _questiongroupService.GetQuestionGroup(ref errorResponseModel);

                if (questiongroupModelList != null)
                {
                    return Ok(questiongroupModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new questions group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(QuestionGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveQuestionGroup(QuestionGroupModel questiongroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionGroupModel = _questiongroupService.SaveQuestionGroup(questiongroupModel, ref errorResponseModel);

                if (questionGroupModel != null)
                {
                    return Ok(questionGroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete questions group 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteQuestionGroup")]
        [ProducesResponseType(typeof(QuestionGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteQuestionGroup(QuestionGroupModel questiongroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionGroupModel = _questiongroupService.DeleteQuestionGroup(questiongroupModel, ref errorResponseModel);

                if (questionGroupModel != null)
                {
                    return Ok(questionGroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

     
[HttpGet("GetQuestionGroupExistance")] [ProducesResponseType(typeof(QuestionGroupModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionGroupExistance()
        {
            ErrorResponseModel errorResponseModel = null; try
            {
                var materiamedicaheadList = _questiongroupService.GetQuestionGroupExistance(ref errorResponseModel);

                if (materiamedicaheadList != null) { return Ok(materiamedicaheadList); }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }





        /// <summary>
        /// To get GetQuestionGroup by ExistanceId
        /// </summary>
        /// <param name="QuestionSectionId"></param>
        /// <returns></returns>
        [HttpGet("GetQuestionGroupByExistanceId/{QuestionSectionId}")]
        [ProducesResponseType(typeof(QuestionGroupModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionGroupByExistanceId(long QuestionSectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questiongroupModel1 = _questiongroupService.GetQuestionGroupByExistanceId(QuestionSectionId, ref errorResponseModel);

                if (questiongroupModel1 != null)
                {
                    return Ok(questiongroupModel1);
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