using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosisSystemController : BaseAPIController
    {
        IDiagnosisSystemService _diagnosisSystemService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="diagnosisSystemService"></param>
        public DiagnosisSystemController(IDiagnosisSystemService diagnosisSystemService)
        {
            _diagnosisSystemService = diagnosisSystemService;
        }



        /// <summary>
        /// To get DiagnosisSystem by diagnosisSystemId 
        /// </summary>
        /// <param name="diagnosisSystemId"></param>
        /// <returns></returns>
        [HttpGet("{diagnosisSystemId}")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisSystemById(long diagnosisSystemId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemModel = _diagnosisSystemService.GetDiagnosisSystemById(diagnosisSystemId, ref errorResponseModel);

                if (diagnosisSystemModel != null)
                {
                    return Ok(diagnosisSystemModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        /// <summary>
        /// To get all GetDiagnosisSystem
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetDiagnosisSystem")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisSystem()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemModelList = _diagnosisSystemService.GetDiagnosisSystem(ref errorResponseModel);

                if (diagnosisSystemModelList != null)
                {
                    return Ok(diagnosisSystemModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        /// <summary>
        /// To add new DiagnosisSystem 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("SaveDiagnosisSystem")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemEntity = _diagnosisSystemService.SaveDiagnosisSystem(diagnosissystemModel, ref errorResponseModel);

                if (diagnosisSystemEntity != null)
                {
                    return Ok(diagnosisSystemEntity);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// To delete DeleteDiagnosisSystem 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDiagnosisSystem")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemEntity = _diagnosisSystemService.DeleteDiagnosisSystem(diagnosissystemModel, ref errorResponseModel);

                if (diagnosisSystemEntity != null)
                {
                    return Ok(diagnosisSystemEntity);
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
