using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for intensity related operations
    /// </summary>
   public interface IIntensityService
    {
        /// <summary>
        /// Method is used for to get intensity by intensityId
        /// </summary>
        /// <param name="intensityId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        IntensityModel GetIntensityById(long countryId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Intensities
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<IntensityModel> GetIntensities(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Intensity
        /// </summary>
        /// <param name="intensityModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveIntensity(IntensityModel intensityModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Intensity.
        /// </summary>
        /// <param name="intensityModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteIntensity(IntensityModel intensityModel, ref ErrorResponseModel errorResponseModel);
    }
}
