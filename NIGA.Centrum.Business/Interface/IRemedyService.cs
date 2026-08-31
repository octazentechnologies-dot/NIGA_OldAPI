using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for remedy related operations
    /// </summary>
   public interface IRemedyService
    {
        /// <summary>
        /// Method is used for to get remedy by remedyId
        /// </summary>
        /// <param name="remedyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RemedyModel GetRemedyById(long remedyId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Remedies
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<SearchRemedyModel> GetRemedies(string search,ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Remedie
        /// </summary>
        /// <param name="remedyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveRemedy(RemedyModel remedyModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Remedie.
        /// </summary>
        /// <param name="remedyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteRemedy(RemedyModel remedyModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get Remedies by subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<RemedyModel> GetRemedyBySection(long subSectionId, ref ErrorResponseModel errorResponseModel);

        // Added by Vikas More

        /// <summary>
        /// Get Remedies by subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RemedyCommonUncommonModel GetCommonUnCommonRemedyBySection(long subSectionId, ref ErrorResponseModel errorResponseModel);


    }
}
