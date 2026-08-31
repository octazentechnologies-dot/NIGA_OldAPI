using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for part location related operations
    /// </summary>
   public interface IPartLocationService
    {
        /// <summary>
        /// Method is used for to get part location by partlocationId
        /// </summary>
        /// <param name="partlocationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PartLocationModel GetPartLocationById(long partlocationId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the partlocations
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PartLocationModel> GetPartLocations(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Part Location
        /// </summary>
        /// <param name="partLocationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePartLocation(PartLocationModel partLocationModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate PartLocation.
        /// </summary>
        /// <param name="partLocationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeletePartLocation(PartLocationModel partLocationModel, ref ErrorResponseModel errorResponseModel);
    }
}
