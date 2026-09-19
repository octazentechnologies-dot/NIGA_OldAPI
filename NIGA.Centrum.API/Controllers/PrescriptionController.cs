using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Common;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [DoctorOnly]
    public class PrescriptionController :  BaseAPIController
    {
        IPrescriptionService _prescriptionService;
        IPatientAppointmentService _patientAppointmentService;

        public PrescriptionController(
            IPrescriptionService prescriptionService,
            IPatientAppointmentService patientAppointmentService)
        {
            _prescriptionService = prescriptionService;
            _patientAppointmentService = patientAppointmentService;
        }

      

        /// <summary>
        /// To get diagnosis by Diagnosis ID 
        /// </summary>
        /// <param name="diagnosisId"></param>
        /// <returns></returns>
        [HttpPost("SavePrescriptionDetail")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePrescriptionDetail(PrescriptionDetailModel prescriptionDetail)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var deny = ForbidAppointmentIfNotOwner(prescriptionDetail?.AppointmentId ?? 0);
                if (deny != null)
                    return deny;

                var diagnosisModel = _prescriptionService.SavePrescriptionDetail(prescriptionDetail, ref errorResponseModel);

                if (diagnosisModel != null)
                {
                    return Ok(diagnosisModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Prescription Rubric Detail by Diagnosis ID 
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpGet("PrescriptionRubricDetail")]
        [ProducesResponseType(typeof(List<PrescriptionRubricDetailViewModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPrescriptionRubricDetail(int appointmentId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var deny = ForbidAppointmentIfNotOwner(appointmentId);
                if (deny != null)
                    return deny;

                var diagnosisModel = _prescriptionService.GetPrescriptionRubricDetail(appointmentId, ref errorResponseModel);

                if (diagnosisModel != null)
                {
                    return Ok(diagnosisModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Prescription Remedy Detail by appointmentId
        /// </summary>
        /// <param name="diagnosisId"></param>
        /// <returns></returns>
        [HttpGet("PrescriptionRemedyDetail")]
        [ProducesResponseType(typeof(DiagnosisModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPrescriptionRemedyDetail(int appointmentId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var deny = ForbidAppointmentIfNotOwner(appointmentId);
                if (deny != null)
                    return deny;

                var diagnosisModel = _prescriptionService.GetPrescriptionRemedyDetail(appointmentId, ref errorResponseModel);

                if (diagnosisModel != null)
                {
                    return Ok(diagnosisModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Prescription Remedy by rubric ID 
        /// </summary>
        /// <param name="diagnosisId"></param>
        /// <returns></returns>
        [HttpPost("GetPrescriptionRemedy")]
        [ProducesResponseType(typeof(List<PrescriptionRemedyViewModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPrescriptionRemedy(List<int?> rubricList)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisModel = _prescriptionService.GetPrescriptionRemedy(rubricList, ref errorResponseModel);

                if (diagnosisModel != null)
                {
                    return Ok(diagnosisModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private IActionResult ForbidAppointmentIfNotOwner(int appointmentId)
        {
            if (appointmentId <= 0)
                return BadRequest("AppointmentId is required.");

            ErrorResponseModel lookupError = null;
            var appointment = _patientAppointmentService.GetPatientAppById(appointmentId, ref lookupError);
            if (appointment == null)
                return ReturnErrorResponse(lookupError);

            return DoctorOwnership.ForbidIfNotOwner(User, appointment.DoctorId);
        }
    }
}
