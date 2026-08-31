using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace NIGA.Centrum.Business.Interface
{
    public interface IPatientAppointmentService
    {

        /// <summary>
        /// Method is used for to get patient appointment by patientAppId
        /// </summary>
        /// <param name="patientAppId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PatientAppointmentModel GetPatientAppById(long patientAppId, ref ErrorResponseModel errorResponseModel);


        PatientAppointmentModel UpdateAppointmentStatus( UpdateAppointmentStatusModel model, ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Interface is used to save Patient Appointment
        /// </summary>
        /// <param name="patientAppointmentModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePatientApp(PatientAppointmentModel patientAppointmentModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method declaration for get all the GetCases by user Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientModel> GetCasesByUser(long UserId, ref ErrorResponseModel errorResponseModel);




        
    }
}
