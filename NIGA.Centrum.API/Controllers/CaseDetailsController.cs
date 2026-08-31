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
    public class CaseDetailsController : BaseAPIController
    {
        ICaseDetailsService _casedetailsService;
        /// <summary>
        /// Used to initialize controller and inject bodypart service
        /// </summary>
        /// <param name="bodypartService"></param>
        public CaseDetailsController(ICaseDetailsService casedetailsService)
        {
            _casedetailsService = casedetailsService;
        }
        //[AllowAnonymous]

        /// <summary>
        /// To add new CaseDetails 
        /// </summary>
        /// <param name="casedetailsModel"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CaseDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveCaseDetails(List<CaseDetailsModel> casedetailsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedyModel = _casedetailsService.SaveCaseDetails(casedetailsModel, ref errorResponseModel);

                if (remedyModel != null)
                {
                    return Ok(remedyModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// To get GetPatientBackHostory by patientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("GetPatientBackHostoryById/{patientId}")]
        [ProducesResponseType(typeof(PatientAppointmentModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientBackHostoryById(long patientId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientAppointmentModel = _casedetailsService.GetPatientBackHostoryById(patientId, ref errorResponseModel);

                if (patientAppointmentModel != null)
                {
                    return Ok(patientAppointmentModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }








        ///// <summary>
        ///// To add new CaseDetails 
        ///// </summary>
        ///// <param name=" CaseDetailId"></param>
        ///// <param name=""></param>
        ///// <returns></returns>
        //[HttpGet("GetCaseDetailsById/{CaseDetailId}")]
        //[ProducesResponseType(typeof(CaseDetailsModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public IActionResult GetCaseDetailsById(long CaseDetailId)
        //{
        //    ErrorResponseModel errorResponseModel = null;
        //    try
        //    {
        //        var remedyModel = _casedetailsService.GetCaseDetailsById(CaseDetailId, ref errorResponseModel);

        //        if (remedyModel != null)
        //        {
        //            return Ok(remedyModel);
        //        }
        //        return ReturnErrorResponse(errorResponseModel);
        //    }
        //    catch (Exception )
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


        //// <summary>
        /////  Get details to edit Case Details
        ///// </summary>
        ///// <param name="subSectionId"></param>
        ///// <param name=""></param>
        ///// <returns></returns>
        //[HttpGet("GetCaseDetailsToEdit/{subSectionId}/{caseId}")]
        //[ProducesResponseType(typeof(CaseDetailsModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public IActionResult GetCaseDetailsToEdit(int subSectionId, int caseId)
        //{
        //    ErrorResponseModel errorResponseModel = null;
        //    try
        //    {
        //        var rubricRemedyDetails = _casedetailsService.GetCaseDetailsToEdit(subSectionId, caseId, ref errorResponseModel);

        //        if (rubricRemedyDetails != null)
        //        {
        //            return Ok(rubricRemedyDetails);
        //        }
        //        return ReturnErrorResponse(errorResponseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
