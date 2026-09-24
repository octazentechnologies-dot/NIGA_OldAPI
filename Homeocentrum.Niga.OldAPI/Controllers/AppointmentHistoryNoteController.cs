using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Implementation;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Common;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Security.Claims;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [DoctorOnly]
    public class AppointmentHistoryNoteController :BaseAPIController
    {
        IAppointmentHistoryNoteService _appointmentHistoryNote;
        IPatientAppointmentService _patientAppointmentService;

        public AppointmentHistoryNoteController(
            IAppointmentHistoryNoteService appointmentHistoryNote,
            IPatientAppointmentService patientAppointmentService)
        {
            _appointmentHistoryNote = appointmentHistoryNote;
            _patientAppointmentService = patientAppointmentService;
        }

        /// <summary>
        /// To get DiagnosisSystem by diagnosisSystemId 
        /// </summary>
        /// <param name="diagnosisSystemId"></param>
        /// <returns></returns>
        [HttpGet("{historyNoteId}")]
        [ProducesResponseType(typeof(AppointmentHistoryNoteModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAppointmentHistoryNoteById(long historyNoteId)
        {
            ErrorResponseModel errorResponseModel = null;
           
            try
            {
                var appointmentHistoryNote = _appointmentHistoryNote.GetAppointmentHistoryNoteById(historyNoteId, ref errorResponseModel);

                if (appointmentHistoryNote == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = errorResponseModel?.Message ?? "Appointment history note not found."
                    });
                }

                var deny = ForbidAppointmentIfNotOwner(appointmentHistoryNote.AppointmentId);
                if (deny != null)
                    return deny;
                return Ok(appointmentHistoryNote);
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
        [HttpPost("SaveUpdateAppointmentHistoryNote")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveUpdateAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote)
        {
            ErrorResponseModel errorResponseModel = null;
            //int userId = 0;
            //if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //{
            //    if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //    {
            //        userId = Convert.ToInt32(((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
            //    }
            //}
            int userId = 0;

            if (User?.Identity?.IsAuthenticated == true)
            {
                var claim = ((ClaimsIdentity)User.Identity)
                            .FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                    return Unauthorized("User ID not found in token.");

                if (!int.TryParse(claim.Value, out userId))
                    return Unauthorized("Invalid User ID in token.");
            }

            try
            {
                var deny = ForbidAppointmentIfNotOwner(appointmentHistoryNote?.AppointmentId);
                if (deny != null)
                    return deny;

                var appointmentHistoryNoteResult = _appointmentHistoryNote.SaveUpdateAppointmentHistoryNote(appointmentHistoryNote, ref errorResponseModel, userId);

                if (appointmentHistoryNoteResult != null)
                {
                    return Ok(appointmentHistoryNoteResult);
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
        [Route("DeleteAppointmentHistoryNote")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote)
        {
            ErrorResponseModel errorResponseModel = null;
            int userId = 0;
            //if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //{
            //    if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //    {
            //        userId = Convert.ToInt32(((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
            //    }
            //}
            try
            {
                var deny = ForbidAppointmentIfNotOwner(appointmentHistoryNote?.AppointmentId);
                if (deny != null)
                    return deny;

                var appointmentHistoryNoteResult = _appointmentHistoryNote.DeleteAppointmentHistoryNote(appointmentHistoryNote, ref errorResponseModel, userId);

                if (appointmentHistoryNoteResult != null)
                {
                    return Ok(appointmentHistoryNoteResult);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all AppointmentHistoryNotes with pagination
        /// </summary>
        /// <param name="appointmentId">Optional: Filter by AppointmentId. If 0 or null, returns all appointment history notes</param>
        /// <param name="nigaParameters"></param>
        /// <returns></returns>
        [HttpGet("GetAllAppointmentHistoryNotes")]
        [ProducesResponseType(typeof(PaginationResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllAppointmentHistoryNotes(int? appointmentId, [FromQuery] NigaParameters nigaParameters)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                if (appointmentId.HasValue && appointmentId.Value > 0)
                {
                    var deny = ForbidAppointmentIfNotOwner(appointmentId);
                    if (deny != null)
                        return deny;
                }
                else if (!DoctorOwnership.IsAdminPortalUser(User))
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        new { success = false, message = "Access denied for this doctor resource." });
                }

                var appointmentHistoryNoteResult = _appointmentHistoryNote.GetAllAppointmentHistoryNotes(appointmentId, nigaParameters, ref errorResponseModel);

                if (appointmentHistoryNoteResult != null)
                {
                    return Ok(appointmentHistoryNoteResult);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private IActionResult ForbidAppointmentIfNotOwner(int? appointmentId)
        {
            if (!appointmentId.HasValue || appointmentId.Value <= 0)
                return BadRequest("AppointmentId is required.");

            ErrorResponseModel lookupError = null;
            var appointment = _patientAppointmentService.GetPatientAppById(appointmentId.Value, ref lookupError);
            if (appointment == null)
                return ReturnErrorResponse(lookupError);

            return DoctorOwnership.ForbidIfNotOwner(User, appointment.DoctorId);
        }
    }
}
