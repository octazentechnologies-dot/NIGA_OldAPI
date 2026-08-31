using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for body part related operations
    /// </summary>
   public interface IDiagnosisSystemService
    {
        /// <summary>
        /// Method is used for to get bodypart by bodypartId
        /// </summary>
        /// <param name="DiagnosisSystemId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        DiagnosisSystemModel GetDiagnosisSystemById(long diagnosisSystemId, ref ErrorResponseModel errorResponseModel);
       

        /// <summary>
        /// interface for getting all the bodyparts
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<DiagnosisSystemModel> GetDiagnosisSystem(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save BodyPart
        /// </summary>
        /// <param name="diagnosissystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate bodypart.
        /// </summary>
        /// <param name="diagnosissystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel, ref ErrorResponseModel errorResponseModel);

       
    }
}
