using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IMenuMasterService
    {
        /// <summary>
        /// Method is used for to get menu by menuid
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        MenuMasterModel GetMenuById(long menuId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the MenuMaster
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<MenuMasterModel> GetMenuMaster(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save MenuMaster
        /// </summary>
        /// <param name="menuMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveMenuMaster(MenuMasterModel menuMasterModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate MenuMaster.
        /// </summary>
        /// <param name="menuMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteMenuMaster(MenuMasterModel menuMasterModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for to get menu by userId
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<MenuMasterResModel> GetMenuByUserId(long userId, int? firmIds, ref ErrorResponseModel errorResponseModel);
    }
}
