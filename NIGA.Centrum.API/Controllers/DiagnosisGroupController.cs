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
    /// APIs for Diagnosis Group entity 
    /// </summary>
    [Route("api/diagnosisgroup")]
    [ApiController]
    [Authorize]
    public class DiagnosisGroupController : BaseAPIController
    {
        IDiagnosisGroupService _diagnosigroupService;
        /// <summary>
        /// Used to initialize controller and inject diagnosis group service
        /// </summary>
        /// <param name="diagnosisgroupService"></param>
        public DiagnosisGroupController(IDiagnosisGroupService diagnosisgroupService)
        {
            _diagnosigroupService = diagnosisgroupService;
        }

        /// <summary>
        /// To get diagnosis group by DiagnosisGroup ID 
        /// </summary>
        /// <param name="diagnosisgroupId"></param>
        /// <returns></returns>
        [HttpGet("{diagnosisgroupId}")]
        [ProducesResponseType(typeof(DiagnosisGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisGroupById(long diagnosisgroupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisgroupModel = _diagnosigroupService.GetDiagnosisGroupById(diagnosisgroupId, ref errorResponseModel);

                if (diagnosisgroupModel != null)
                {
                    return Ok(diagnosisgroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all diagnosis group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(DiagnosisGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisGroups()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisgroupModel = _diagnosigroupService.GetDiagnosisGroups(ref errorResponseModel);

                if (diagnosisgroupModel != null)
                {
                    return Ok(diagnosisgroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all diagnosis group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DiagnosisGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveDiagnosisGroups(DiagnosisGroupModel diagnosisGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisgroupModel = _diagnosigroupService.SaveDiagnosisGroup(diagnosisGroupModel,ref errorResponseModel);

                if (diagnosisgroupModel != null)
                {
                    return Ok(diagnosisgroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all diagnosis group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDiagnosisGroup")]
        [ProducesResponseType(typeof(DiagnosisGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteDiagnosisGroup(DiagnosisGroupModel diagnosisGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisgroupModel = _diagnosigroupService.DeleteDiagnosisGroup(diagnosisGroupModel, ref errorResponseModel);

                if (diagnosisgroupModel != null)
                {
                    return Ok(diagnosisgroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all diagnosis group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDiagnosis")]
        [ProducesResponseType(typeof(DiagnosisGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosis()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisgroupModel = _diagnosigroupService.GetDiagnosisGroupViewModels(ref errorResponseModel);

                if (diagnosisgroupModel != null)
                {
                    return Ok(diagnosisgroupModel);
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