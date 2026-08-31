using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosisTherapeuticsDetailController : BaseAPIController
    {
        IDiagnosisTherapeuticsDetailService _diagnosisTherapeuticsDetailService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="diagnosisTherapeuticsDetailService"></param>
        public DiagnosisTherapeuticsDetailController(IDiagnosisTherapeuticsDetailService diagnosisTherapeuticsDetailService)
        {
            _diagnosisTherapeuticsDetailService = diagnosisTherapeuticsDetailService;
        }



        /// <summary>
        /// To get DiagnosisSystem by diagnosisSystemId 
        /// </summary>
        /// <param name="diagnosisTherapeuticsDetailId"></param>
        /// <returns></returns>
        [HttpGet("{diagnosisTherapeuticsDetailId}")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisTherapeuticsDetailById(long diagnosisTherapeuticsDetailId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemModel = _diagnosisTherapeuticsDetailService.GetDiagnosisTherapeuticsDetailById(diagnosisTherapeuticsDetailId, ref errorResponseModel);

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
        [HttpGet("GetDiagnosisTherapeuticsDetail")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDiagnosisTherapeuticsDetail()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemModelList = _diagnosisTherapeuticsDetailService.GetDiagnosisTherapeuticsDetails(ref errorResponseModel);

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
        [HttpPost("SaveDiagnosisTherapeuticsDetail")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemEntity = _diagnosisTherapeuticsDetailService.SaveDiagnosisTherapeuticsDetail(diagnosisTherapeuticsDetailModel, ref errorResponseModel);

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
        [Route("DeleteDiagnosisTherapeuticsDetail")]
        [ProducesResponseType(typeof(DiagnosisSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisSystemEntity = _diagnosisTherapeuticsDetailService.DeleteDiagnosisTherapeuticsDetail(diagnosisTherapeuticsDetailModel, ref errorResponseModel);

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
