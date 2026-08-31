using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for diagnosis group related operations
    /// </summary>
   public interface IDiagnosisGroupService
    {
        /// <summary>
        /// Method is used for to get diagnosis group by diagnosisgroupId
        /// </summary>
        /// <param name="diagnosisgroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        DiagnosisGroupModel GetDiagnosisGroupById(long diagnosisgroupId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to return all the DiagnosisGroups.
        /// </summary>
        /// <returns></returns>
        List<DiagnosisGroupModel> GetDiagnosisGroups( ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save DiagnosisGroup
        /// </summary>
        /// <param name="diagnosisGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveDiagnosisGroup(DiagnosisGroupModel diagnosisGroupModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate diagnosis group.
        /// </summary>
        /// <param name="diagnosisGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDiagnosisGroup(DiagnosisGroupModel diagnosisGroupModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate diagnosis group.
        /// </summary>
        /// <param name="diagnosisGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<DiagnosisGroupViewModel> GetDiagnosisGroupViewModels(ref ErrorResponseModel errorResponseModel);
    }
}
