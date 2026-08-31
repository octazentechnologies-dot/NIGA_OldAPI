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
    /// APIs for Clinical Question entity 
    /// </summary>
    [Route("api/clinicalquestions")]
    [ApiController]
    [Authorize]
    public class ClinicalQuestionsController : BaseAPIController
    {
        IClinicalQuestionsService _clinicalquestionsService;
        /// <summary>
        /// Used to initialize controller and inject clinical questions
        /// </summary>
        /// <param name="clinicalquestionsService"></param>
        public ClinicalQuestionsController(IClinicalQuestionsService clinicalquestionsService)
        {
            _clinicalquestionsService = clinicalquestionsService;
        }

        /// <summary>
        /// To get clinical questions by Clinical Questions ID 
        /// </summary>
        /// <param name="questionsId"></param>
        /// <returns></returns>
        [HttpGet("{questionsId}")]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestionsById(long questionsId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModel = _clinicalquestionsService.GetClinicalQuestionsById(questionsId, ref errorResponseModel);

                if (clinicalquestionModel != null)
                {
                    return Ok(clinicalquestionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all clinical questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestions()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModelList = _clinicalquestionsService.GetClinicalQuestions(ref errorResponseModel);

                if (clinicalquestionModelList != null)
                {
                    return Ok(clinicalquestionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new clinical questions 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveClinicalQuestions(List< ClinicalQuestionsModel> clinicalquestionsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModel = _clinicalquestionsService.SaveClinicalQuestions(clinicalquestionsModel, ref errorResponseModel);

                if (clinicalquestionModel != null)
                {
                    return Ok(clinicalquestionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete clinical questions 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteClinicalQuestions")]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteClinicalQuestions(ClinicalQuestionsModel clinicalquestionsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionmodel = _clinicalquestionsService.DeleteClinicalQuestions(clinicalquestionsModel, ref errorResponseModel);

                if (clinicalquestionmodel != null)
                {
                    return Ok(clinicalquestionmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all clinical questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClinicalQuestionsByGroup/{QuestionGroupId}")]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestionsByGroup(long QuestionGroupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModelList = _clinicalquestionsService.GetQuestionsByGroupId(QuestionGroupId,ref errorResponseModel);

                if (clinicalquestionModelList != null)
                {
                    return Ok(clinicalquestionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To get all clinical questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuestionsBySelectedId")]
        [ProducesResponseType(typeof(ClinicalQueKeywordModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionsBySelectedId(long QuestionGroupId, long QuestionSectionId, long QuestionSubgroupId=0, long BodyPartId = 0)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModelList = _clinicalquestionsService.GetQuestionsBySelectedId(QuestionGroupId, QuestionSectionId, ref errorResponseModel, QuestionSubgroupId, BodyPartId);

                if (clinicalquestionModelList != null)
                {
                    return Ok(clinicalquestionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
        /// <summary>
        /// To get all clinical questions For Admin
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetClinicalQuestionBodyPartList")]
        [ProducesResponseType(typeof(List<ClinicalQuestionViewModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestionBodyPartList()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModel = _clinicalquestionsService.GetClinicalQuestionBodyPartList(ref errorResponseModel);

                if (clinicalquestionModel != null)
                {
                    return Ok(clinicalquestionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To get all clinical questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClinicalQuestionBodyPartDataById")]
        [ProducesResponseType(typeof(ClinicalQueKeywordModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestionBodyPartDataById(int questionId, int QBType)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModelList = _clinicalquestionsService.GetClinicalQuestionBodyPartDataById(questionId, QBType, ref errorResponseModel);

                if (clinicalquestionModelList != null)
                {
                    return Ok(clinicalquestionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To add new clinical questions 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEditClinicalQuestionsBodyPart")]
        [ProducesResponseType(typeof(ClinicalQuestionsBodyPartModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddEditClinicalQuestionsBodyPart(ClinicalQuestionsBodyPartModel clinicalQuestionsBodyPart)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModel = _clinicalquestionsService.AddEditClinicalQuestionsBodyPart(clinicalQuestionsBodyPart, ref errorResponseModel);

                if (clinicalquestionModel != null)
                {
                    return Ok(clinicalquestionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To delete clinical questions / Body part & rubric
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteQuestionBodyPartData")]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteQuestionBodyPartData(int questionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionmodel = _clinicalquestionsService.DeleteClinicalQuestionBodyPart(questionId, 0, ref errorResponseModel);

                if (clinicalquestionmodel != null)
                {
                    return Ok(clinicalquestionmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To delete clinical questions / Body part & rubric
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteQuestionBodyPartRubricData")]
        [ProducesResponseType(typeof(ClinicalQuestionsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteClinicalRubricData(int clinicalRubricId, int clinicalQuestionBodyPartId, int qbType)
        {
            ErrorResponseModel errorResponseModel = null;
            int userId = 0;
            try
            {
                var clinicalquestionmodel = _clinicalquestionsService.DeleteClinicalRubricData(clinicalRubricId, clinicalQuestionBodyPartId, qbType, ref errorResponseModel);

                if (clinicalquestionmodel != null)
                {
                    return Ok(clinicalquestionmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //Doctor Side

        /// <summary>
        /// To add new clinical questions 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClinicalQuestionsKeyWordBodyPart")]
        [ProducesResponseType(typeof(List<QuestionKeyWordBodyPartOutputModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQuestionsKeyWordBodyPart(QuestionKeyWordBodyPartInputModel inputModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModel = _clinicalquestionsService.GetClinicalQuestionsKeyWordBodyPart(inputModel, ref errorResponseModel);

                if (clinicalquestionModel != null)
                {
                    return Ok(clinicalquestionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all clinical questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClinicalRubricData")]
        [ProducesResponseType(typeof(List<QuestionKeyWordBodyPartRubricOutputModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalRubricData(QuestionKeyWordBodyPartRubricInputModel rubricInputModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var clinicalquestionModelList = _clinicalquestionsService.GetClinicalRubricData(rubricInputModel, ref errorResponseModel);

                if (clinicalquestionModelList != null)
                {
                    return Ok(clinicalquestionModelList);
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