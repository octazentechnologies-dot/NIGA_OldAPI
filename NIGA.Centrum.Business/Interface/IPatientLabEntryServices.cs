using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IPatientLabEntryServices
    {
        /// <summary>
        /// Method declartion for SavePatientLabEntry
        /// </summary>
        /// <param name="patientLabOrderModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePatientLabEntry(PatientLabEntryModel patientLabEntryModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        ///  Method declartion for GetAllPatientLabEntry
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientLabEntryModel> GetAllPatientLabEntry(ref ErrorResponseModel errorResponseModel);

        List<PatientLabEntryModel> GetPatientLabEntry(int PatientId, ref ErrorResponseModel errorResponseModel);
    }
}
