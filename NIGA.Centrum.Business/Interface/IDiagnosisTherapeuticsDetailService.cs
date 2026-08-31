using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IDiagnosisTherapeuticsDetailService
    {
        /// <summary>
        /// Method is used for to get Diagnosis Therapeutics Detail by DiagnosisTherapeuticsDetailID
        /// </summary>
        /// <param name="DiagnosisTherapeuticsDetailID"></param>
        /// <returns></returns>
        DiagnosisTherapeuticsDetailModel GetDiagnosisTherapeuticsDetailById(long diagnosisTherapeuticsDetailID, ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// interface for getting all the bodyparts
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<DiagnosisTherapeuticsDetailModel> GetDiagnosisTherapeuticsDetails(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save BodyPart
        /// </summary>
        /// <param name="diagnosisTherapeuticsDetailModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate bodypart.
        /// </summary>
        /// <param name="diagnosisTherapeuticsDetailModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel, ref ErrorResponseModel errorResponseModel);
    }
}
