using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for package related operations
    /// </summary>
    public interface IPackageService
    {
        /// <summary>
        /// Method is used for to get package by packageId
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PackageModel GetPackageById(long packageId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Packages
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<PackageModel> GetPackages(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Package
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePackage(PackageModel packageModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Package.
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeletePackage(PackageModel packageModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Packages
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<PackageTopupModel> GetPackageTopup(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Package
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePackageTopup(PackageTopupModel packageModel, ref ErrorResponseModel errorResponseModel);

    }
}
