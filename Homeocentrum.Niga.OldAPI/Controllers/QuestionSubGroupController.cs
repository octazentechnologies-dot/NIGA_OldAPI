using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;
using System;

using Homeocentrum.Niga.OldAPI.Common;
namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [DoctorOnly]
    public class QuestionSubGroupController : BaseAPIController
    {
        IQuestionSubGroupService _questionsubgroupService;
        /// <summary>
        /// Used to initialize controller and inject QuestionSubGroup service
        /// </summary>
        /// <param name="materiamedicaheadService"></param>
        public QuestionSubGroupController(IQuestionSubGroupService questionsubgroupService)
        {
            _questionsubgroupService = questionsubgroupService;
        }
        /// <summary>
        /// To get GetQuestionSubGroup by questionSubgroupId
        /// </summary>
        /// <param name="questionSubgroupId"></param>
        /// <returns></returns>

        [HttpGet("{questionSubgroupId}")]
        [ProducesResponseType(typeof(QuestionSubGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSubGroupById(long questionSubgroupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaheadModel = _questionsubgroupService.GetQuestionSubGroupById(questionSubgroupId, ref errorResponseModel);



                if (materiamedicaheadModel != null)
                {
                    return Ok(materiamedicaheadModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all QuestionSubGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [HttpGet("GetQuestionSubGroup")]
        [ProducesResponseType(typeof(QuestionSubGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSubGroup()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionSubGroupList = _questionsubgroupService.GetQuestionSubGroup(ref errorResponseModel);



                if (questionSubGroupList != null)
                {
                    return Ok(questionSubGroupList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new QuestionSubGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(QuestionSubGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult SaveQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionSubGroupModel1 = _questionsubgroupService.SaveQuestionSubGroup(questionSubGroupModel, ref errorResponseModel);



                if (questionSubGroupModel1 != null)
                {
                    return Ok(questionSubGroupModel1);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete QuestionSubGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DeleteQuestionSubGroup")]
        [ProducesResponseType(typeof(QuestionSubGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult DeleteQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionSubGroupModel1 = _questionsubgroupService.DeleteQuestionSubGroup(questionSubGroupModel, ref errorResponseModel);



                if (questionSubGroupModel1 != null)
                {
                    return Ok(questionSubGroupModel1);
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
