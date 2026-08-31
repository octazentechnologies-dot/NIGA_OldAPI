using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DropdownListController : BaseAPIController
    {
        IDropdownListService _dropdownListService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public DropdownListController(IDropdownListService dropdownListService)
        {
            _dropdownListService = dropdownListService;
        }

        /// <summary>
        /// To delete diagnosis 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllThermalDDL")]
        [ProducesResponseType(typeof(ThermalModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllThermalDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetAllThermalDDL();

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                else
                {
                    errorResponseModel.Message = "Not data found";
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete diagnosis 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAuthorforMateriaMedicaDDL")]
        [ProducesResponseType(typeof(ThermalModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAuthorforMateriaMedicaDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetAuthorforMateriaMedica();

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                else
                {
                    errorResponseModel.Message = "Not data found";
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To delete diagnosis 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPatientLabTestDDL")]
        [ProducesResponseType(typeof(ThermalModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientLabTestDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetPatientLabTestDDl();

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                else
                {
                    errorResponseModel.Message = "Not data found";
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get question group 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuestionGroupDDL")]
        [ProducesResponseType(typeof(List<QuestionGroupModelDDL>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionGroupDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetQuestionGroupDDL(ref errorResponseModel);

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Question Section
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuestionSectionsDDL")]
        [ProducesResponseType(typeof(List<QuestionSectionModelDDL>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSectionsDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetQuestionSectionsDDL(ref errorResponseModel);

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Question SubGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuestionSubGroupDDL")]
        [ProducesResponseType(typeof(List<QuestionSubGroupModelDDL>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSubGroupDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetQuestionSubGroupDDL(ref errorResponseModel);

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                else
                {
                    errorResponseModel.Message = "Not data found";
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get body part by sectionID 
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBodyPartDDL/{sectionId}")]
        [ProducesResponseType(typeof(List<BodyPartDDLModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetBodyPartDDL(int sectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetBodyPartDDL(sectionId, ref errorResponseModel);

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get sub question group by questionGroupId & questionSectionId
        /// </summary>
        /// <param name="questionGroupId"></param>
        /// <param name="questionSectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSubQuestionGroupByQGIDQSIDDDL/{questionGroupId}/{questionSectionId}")]
        [ProducesResponseType(typeof(List<QuestionSubGroupModelDDL>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubQuestionGroupByQGIDQSIDDDL(int questionGroupId, int questionSectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _dropdownListService.GetSubQuestionGroupByQGIDQSIDDDL(questionGroupId, questionSectionId, ref errorResponseModel);

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all subsections
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubsectionBySection/{sectionId}")]
        [ProducesResponseType(typeof(SubSectionDDLModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubsectionBySection(long sectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _dropdownListService.GetSubsectionBySection(sectionId, ref errorResponseModel);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
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

