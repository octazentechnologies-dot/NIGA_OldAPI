using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface ICaseDetailsService
    {
        ///// <summary>
        ///// Method is used for to get CaseDetails by CaseDetailId
        ///// </summary>
        ///// <param name="CaseDetailId"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //List<CaseDetailsModel> GetCaseDetailsById(long CaseDetailId, ref ErrorResponseModel errorResponseModel);

        ///// <summary>
        ///// Get details to edit rubric remedies
        ///// </summary>
        ///// <param name="subSectionId"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //CaseDetailsModel GetCaseDetailsToEdit(int subSectionId, int caseId, ref ErrorResponseModel errorResponseModel);


        /// <summary>remedyId
        /// Interface is used to save CaseDetails
        /// </summary>
        /// <param name="casedetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveCaseDetails(List<CaseDetailsModel> casedetailsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for to get PatientBackHostory by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientAppointmentModel1> GetPatientBackHostoryById(long patientId, ref ErrorResponseModel errorResponseModel);
    }

}

