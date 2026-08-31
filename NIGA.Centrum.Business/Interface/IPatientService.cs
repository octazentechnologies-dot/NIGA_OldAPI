using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface for patient.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Method declarations for Saving new Patient.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PatientModel SavePatient(PatientModel patient, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method declaration for get all the GetCases
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientModel> GetCases(long UserId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method declaration for patient details
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
      PatientModel GetPatientDetails(long PatientID, long CaseId,ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// Method is used for to get Patient by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        GetPatientDetailsById GetPatientDetailsById(long patientId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method declarations for Saving new Complaints.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveComplaints(PatientModel patient, ref ErrorResponseModel errorResponseModel);


        /// <summary>
                /// Interface is used to deactivate patient.
                /// </summary>
                /// <param name="patientId"></param>
                /// <param name="errorResponseModel"></param>
                /// <returns></returns>
        string Deletepatient(int patientId, ref ErrorResponseModel errorResponseModel);

    }
}
