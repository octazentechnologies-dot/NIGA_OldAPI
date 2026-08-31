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
    /// APIs for doctor dashboard entity 
    /// </summary>
    [Route("api/doctorDashBoard")]
    [ApiController]
    [Authorize]
    public class DoctorDashBoardController : BaseAPIController
    {
        IDoctorDashBoardService _doctorDashBoardService;
        /// <summary>
        /// Used to initialize controller and inject doctor dashboard service
        /// </summary>
        /// <param name="doctorDashBoardService"></param>
        public DoctorDashBoardController(IDoctorDashBoardService doctorDashBoardService)
        {
            _doctorDashBoardService = doctorDashBoardService;
        }

        /// <summary>
        /// To get patient appointment by appointmentDate
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        [HttpPost("GetCountApp")]
        [ProducesResponseType(typeof(DoctorDashBoardModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientAppCount(DoctorDashBoardModel patientAppmodel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientAppModel = _doctorDashBoardService.GetPatientAppCount(patientAppmodel.UserId, patientAppmodel.AppointmentDate, ref errorResponseModel);

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
        /// To get patient appointment by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PatientAppointmentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientAppUser(DoctorDashBoardModel patientAppmodel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var patientAppModel = _doctorDashBoardService.GetPatientAppUserDate(patientAppmodel.UserId, patientAppmodel.AppointmentDate, ref errorResponseModel);

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
    }
}