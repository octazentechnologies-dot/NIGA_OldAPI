using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for DrugSystem related operations
    /// </summary>
    public interface IDrugSystemService
    {
        /// <summary>
        /// Method is used for to get DrugSystem by drugSystemId
        /// </summary>
        /// <param name="drugSystemId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        DrugSystemModel GetDrugSystemById(long drugSystemId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the DrugSystem
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<DrugSystemModel> GetDrugSystem(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save DrugSystem
        /// </summary>
        /// <param name="drugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveDrugSystem(DrugSystemModel drugSystemModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate DrugSystem.
        /// </summary>
        /// <param name="drugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDrugSystem(DrugSystemModel drugSystemModel, ref ErrorResponseModel errorResponseModel);
    }
}
