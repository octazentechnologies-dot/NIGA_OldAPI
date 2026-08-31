using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// API's for Patient entity
    /// </summary>
    /// 
    /// <summary>
    /// APIs for User entity 
    /// </summary>
    [Route("api/patient")]
    [ApiController]
    [Authorize]
    public class PatientController : BaseAPIController
    {
        IPatientService _patientService;

        /// <summary>
        /// Used to initialize controller and inject patient Service
        /// </summary>
        /// <param name="patientService"></param>
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// To Get all Case entries of a doctor
        /// </summary>
        /// <returns></returns>
        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(PatientModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCases(long UserId)
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();
            try
            {
                var patientModelList = _patientService.GetCases(UserId, ref errorResponseModel);

                if (patientModelList.Count != 0)
                {
                    return Ok(patientModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Create new Patient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(PatientModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request, please verify details");
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var userModel = _patientService.SavePatient(model, ref errorMessage);
                if (userModel.PatientID != 0)
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        /// <summary>
        /// Get patient 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPatientDetails/{PatientID}/{caseId}")]
        [ProducesResponseType(typeof(PatientModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientDetails(long PatientID,long caseId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientModelList = _patientService.GetPatientDetails(PatientID, caseId, ref errorResponseModel);

                if (patientModelList != null)
                {
                    return Ok(patientModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Save complaints
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveComplaints")]
        public IActionResult SaveComplaints(PatientModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request, please verify details");
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var userModel = _patientService.SaveComplaints(model, ref errorMessage);
                if (userModel != "")
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        /// <summary>
        /// To get GetPatientDetails by patientModel 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("GetPatientDetailsById/{patientId}")]
        [ProducesResponseType(typeof(GetPatientDetailsById), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientDetailsById(long patientId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientModel = _patientService.GetPatientDetailsById(patientId, ref errorResponseModel);

                if (patientModel != null)
                {
                    return Ok(patientModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
                /// To delete Depatient 
                /// </summary>
                /// <param name=""></param>
                /// <returns></returns>
        [HttpPost]
        [Route("Deletepatient")]
        [ProducesResponseType(typeof(PatientModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Deletepatient(int patientId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsModel = _patientService.Deletepatient(patientId, ref errorResponseModel);



                if (newsModel != null)
                {
                    return Ok(newsModel);
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