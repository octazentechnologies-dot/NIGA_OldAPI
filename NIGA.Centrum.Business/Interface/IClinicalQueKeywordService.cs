using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for ClinicalQueKeyword related operations
    /// </summary>
    public interface IClinicalQueKeywordService
    {
        /// <summary>
        /// Method is used for to get Author by ClinicalQueKeywordId
        /// </summary>
        /// <param name="ClinicalQueKeywordId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ClinicalQueKeywordsModel GetClinicalQueKeywordById(long ClinicalQueKeywordId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the ClinicalQueKeyword
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ClinicalQueKeywordsModel> GetClinicalQueKeyword(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save ClinicalQueKeyword
        /// </summary>
        /// <param name="quekeywordModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate ClinicalQueKeyword.
        /// </summary>
        /// <param name="quekeywordModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel, ref ErrorResponseModel errorResponseModel);

    }
}
