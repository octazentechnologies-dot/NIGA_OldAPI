using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface ILanguageMasterService
    {
        /// <summary>
        /// interface for getting all the Language
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<LanguageMasterModel> GetLanguage(ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// Interface is used to save Language
        /// </summary>
        /// <param name="languagemasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveLanguage(LanguageMasterModel languagemasterModel, ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// Interface is used to deactivate Language
        /// </summary>
        /// <param name="languagemasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteLanguage(LanguageMasterModel languagemasterModel, ref ErrorResponseModel errorResponseModel);




        /// <summary>
        /// Method is used for to get Language by LanguageId
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        LanguageMasterModel GetLanguageById(long languageId, ref ErrorResponseModel errorResponseModel);
    }
}
