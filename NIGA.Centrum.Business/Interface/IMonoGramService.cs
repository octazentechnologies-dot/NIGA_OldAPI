using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IMonoGramService
    {
        /// <summary>
        /// Method is used for to get Monogram by Id
        /// </summary>
        /// <param name="MonogramId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        MonoGramModel GetMonoGramById(long MonogramId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Monogram
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<MonoGramModel> GetMonogram(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Monogram
        /// </summary>
        /// <param name="MonoGramModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveMonogram(MonoGramModel monoGramModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Monogram.
        /// </summary>
        /// <param name="MonoGramModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteMonogram(MonoGramModel monoGramModel,ref ErrorResponseModel errorResponseModel);

    }
}
