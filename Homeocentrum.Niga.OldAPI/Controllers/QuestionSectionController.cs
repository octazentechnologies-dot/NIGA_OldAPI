using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;

using Homeocentrum.Niga.OldAPI.Common;
namespace Homeocentrum.Niga.OldAPI.Controllers
{
    /// <summary>
    /// APIs for Question Section entity 
    /// </summary>
    [Route("api/questionsection")]
    [ApiController]
    [Authorize]
    [DoctorOnly]
    public class QuestionSectionController : BaseAPIController
    {
        IQuestionSectionService _questionsectionService;
        /// <summary>
        /// Used to initialize controller and inject question section service
        /// </summary>
        /// <param name="questionsectionService"></param>
        public QuestionSectionController(IQuestionSectionService questionsectionService)
        {
            _questionsectionService = questionsectionService;
        }

        /// <summary>
        /// To get question section by QuestionSection ID 
        /// </summary>
        /// <param name="questionsectionId"></param>
        /// <returns></returns>
        [HttpGet("{questionsectionId}")]
        [ProducesResponseType(typeof(QuestionSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSectionById(long questionsectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionsectionModel = _questionsectionService.GetQuestionSectionById(questionsectionId, ref errorResponseModel);

                if (questionsectionModel != null)
                {
                    return Ok(questionsectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all questionsections
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(QuestionSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSections()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionsectionModelList = _questionsectionService.GetQuestionSections(ref errorResponseModel);

                if (questionsectionModelList != null)
                {
                    return Ok(questionsectionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new question section 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(QuestionSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult SaveQuestionSection(QuestionSectionModel questionSectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionsectionModel = _questionsectionService.SaveQuestionSection(questionSectionModel, ref errorResponseModel);

                if (questionsectionModel != null)
                {
                    return Ok(questionsectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete question section 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteQuestionSection")]
        [ProducesResponseType(typeof(QuestionSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult DeleteQuestionSection(QuestionSectionModel questionSectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionsectionModel = _questionsectionService.DeleteQuestionSection(questionSectionModel, ref errorResponseModel);

                if (questionsectionModel != null)
                {
                    return Ok(questionsectionModel);
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