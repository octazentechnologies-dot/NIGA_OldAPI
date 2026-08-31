using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface for PatientLabOrder Actions
    /// </summary>
   public interface IPatientLabOrderServices
    {
        /// <summary>
        /// Method declartion for SavePatinetLabOrder
        /// </summary>
        /// <param name="patientLabOrderModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePatinetLabOrder(PatientLabOrderModel patientLabOrderModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        ///  Method declartion for GetAllPatinetLabOrder
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PatientLabOrderModel> GetAllPatinetLabOrder(ref ErrorResponseModel errorResponseModel);

        List<PatientLabOrderModel> GetPatinetLabOrder(int PatientId, ref ErrorResponseModel errorResponseModel);
    }
}
