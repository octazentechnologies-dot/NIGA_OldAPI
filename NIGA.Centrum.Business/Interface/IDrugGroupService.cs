using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for DrugGroup related operations
    /// </summary>
    public interface IDrugGroupService
    {
        /// <summary>
        /// Method is used for to get DrugGroup by drugGroupId
        /// </summary>
        /// <param name="drugGroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        DrugGroupModel GetDrugGroupById(long drugGroupId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the DrugGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<DrugGroupModel> GetDrugGroup(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save DrugGroup
        /// </summary>
        /// <param name="drugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveDrugGroup(DrugGroupModel drugSystemModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate DrugGroup.
        /// </summary>
        /// <param name="drugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteDrugGroup(DrugGroupModel drugSystemModel, ref ErrorResponseModel errorResponseModel);
    }
}
