using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
     public interface IDoctorDashBoardService
    {

        /// <summary>
        /// Method is used for to get patient appointment by appointmentDate
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        DoctorDashBoardModel GetPatientAppCount(long userId, string appointmentDate, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for to get patient appointment by user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientAppointmentModel> GetPatientAppUserDate(long userId, string appointmentDate,ref ErrorResponseModel errorResponseModel);
    }
}
