using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for AllopathicDrug related operations
    /// </summary>
    public interface IAllopathicDrugService
    {
        /// <summary>
        /// Method is used for to get allopathicDrug by allopathicDrugId
        /// </summary>
        /// <param name="allopathicDrugId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AllopathicDrugModel GetAllopathicDrugById(long allopathicDrugId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the allopathicDrug
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<AllopathicDrugModel> GetAllopathicDrug(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save allopathicDrug
        /// </summary>
        /// <param name="allopathicDrugModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveAllopathicDrug(AllopathicDrugModel allopathicDrugModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate allopathicDrug.
        /// </summary>
        /// <param name="allopathicDrugModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteAllopathicDrug(AllopathicDrugModel allopathicDrugModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for to get allopathicDrug by allopathicDrugId
        /// </summary>
        /// <param name="allopathicDrugName"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AllopathicDrugModel GetAllopathicDrugByName(string allopathicDrugName, ref ErrorResponseModel errorResponseModel);

        AllopathicDrugModel GetAllopathicDrugByID(int allopathicDrugId, ref ErrorResponseModel errorResponseModel);


        List<AllopathicDrugDDModel> GetAllopathicDrugDDL();
    }
}
