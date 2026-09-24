using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Common;
using Homeocentrum.Niga.OldAPI.Model;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    /// <summary>
    /// APIs for Patient Appointment entity 
    /// </summary>
    [Route("api/patientApp")]
    [ApiController]
    [Authorize]
    public class PatientAppointmentController : BaseAPIController
    {
        IPatientAppointmentService _patientAppointmentService;
        /// <summary>
        /// Used to initialize controller and inject patient appointment service
        /// </summary>
        /// <param name="diagnosisService"></param>
        public PatientAppointmentController(IPatientAppointmentService patientAppointmentService)
        {
            _patientAppointmentService = patientAppointmentService;
        }

        /// <summary>
        /// To get patinet Appointment by patientAppId 
        /// </summary>
        /// <param name="patientAppId"></param>
        /// <returns></returns>
        [HttpGet("{patientAppId}")]
        [ProducesResponseType(typeof(PatientAppointmentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientAppById(long patientAppId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientAppModel = _patientAppointmentService.GetPatientAppById(patientAppId, ref errorResponseModel);

                if (patientAppModel != null)
                {
                    var deny = DoctorOwnership.ForbidIfNotOwner(User, patientAppModel.DoctorId);
                    if (deny != null)
                        return deny;
                    return Ok(patientAppModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To add new patient appointment
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PatientAppointmentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePatientApp(PatientAppointmentModel patientAppointmentModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var jwtDoctorId = DoctorOwnership.GetDoctorId(User);
                if (jwtDoctorId.HasValue && patientAppointmentModel.DoctorId <= 0)
                    patientAppointmentModel.DoctorId = jwtDoctorId.Value;

                var deny = DoctorOwnership.ForbidIfNotOwner(User, patientAppointmentModel.DoctorId);
                if (deny != null)
                    return deny;

                var patientAppModel = _patientAppointmentService.SavePatientApp(patientAppointmentModel, ref errorResponseModel);

                if (patientAppModel != null)
                {
                    return Ok(patientAppModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all Case entries of a user
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCasesByUser/{UserId}")]
        [ProducesResponseType(typeof(PatientModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCasesByUser(long UserId)
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();
            try
            {
                var denyUser = DoctorOwnership.ForbidIfNotCallerOrAdmin(User, UserId);
                if (denyUser != null)
                    return denyUser;

                var patientModelList = _patientAppointmentService.GetCasesByUser(UserId, ref errorResponseModel);

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
        /// Update appointment status by appointment id
        /// </summary>
        [HttpPost("UpdateAppointmentStatus")]
        [ProducesResponseType(typeof(PatientAppointmentModel), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UpdateAppointmentStatus(
            [FromBody] UpdateAppointmentStatusModel model)
        {
            ErrorResponseModel errorResponseModel = null;

            try
            {
                if (model == null || model.PatientAppId <= 0 || string.IsNullOrEmpty(model.Status))
                {
                    return BadRequest("Invalid request data");
                }

                var existing = _patientAppointmentService.GetPatientAppById(model.PatientAppId, ref errorResponseModel);
                if (existing == null)
                    return ReturnErrorResponse(errorResponseModel);

                var deny = DoctorOwnership.ForbidIfNotOwner(User, existing.DoctorId);
                if (deny != null)
                    return deny;

                var result = _patientAppointmentService
                    .UpdateAppointmentStatus(model, ref errorResponseModel);

                if (result != null)
                {
                    return Ok(result);
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