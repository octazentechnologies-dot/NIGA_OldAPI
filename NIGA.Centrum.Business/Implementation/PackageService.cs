using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the package operations 
    /// </summary>
   public class PackageService: IPackageService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PackageService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Method to get package by package id
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PackageModel GetPackageById(long packageId, ref ErrorResponseModel errorResponseModel)
        {           
            errorResponseModel = new ErrorResponseModel();
            var packageEntity = context.PackageMaster.FirstOrDefault(x => x.PackageId == packageId && !x.DeleteStatus);
            if (packageEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Package not found";
            }
            return new PackageModel
            {
                PackageId = packageEntity.PackageId,
                PackageName = packageEntity.PackageName,
                CaseCount = packageEntity.CaseCount,
                ValidityInDays = packageEntity.ValidityInDays,
                Amount = packageEntity.Amount,
                EnteredDate = packageEntity.EnteredDate,
                EnteredBy = packageEntity.EnteredBy,
                ChangedBy = packageEntity.ChangedBy,
                ChangedDate = packageEntity.ChangedDate,
                DeleteStatus = packageEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the Packages
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PackageModel> GetPackages(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var packageModelList = new List<PackageModel>();
            var packageEntityList = context.PackageMaster.Where(x => x.DeleteStatus == false).ToList();
            if (packageEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Package not found";
            }
            packageEntityList.ForEach(item =>
            {
                packageModelList.Add(new PackageModel
                {
                    PackageId = item.PackageId,
                    PackageName = item.PackageName,
                    CaseCount = item.CaseCount,
                    ValidityInDays = item.ValidityInDays,
                    Amount = item.Amount,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return packageModelList;
        }

        /// <summary>
        /// Method implementation for saving new Package
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SavePackage(PackageModel packageModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (packageModel.PackageId == 0)
            {
                PackageMaster packageEntity = new PackageMaster();
                packageEntity.PackageName = packageModel.PackageName;
                packageEntity.CaseCount = packageModel.CaseCount;
                packageEntity.ValidityInDays = packageModel.ValidityInDays;
                packageEntity.Amount = packageModel.Amount;
                packageEntity.EnteredBy = packageModel.EnteredBy;
                packageEntity.EnteredDate = DateTime.Now;
                context.PackageMaster.Add(packageEntity);
                context.SaveChanges();
                Message = "Package Saved Successfully";
            }
            else
            {
                var packageEntity = context.PackageMaster.FirstOrDefault(x => x.PackageId == packageModel.PackageId);
                if (packageEntity != null)
                {

                    packageEntity.PackageName = packageModel.PackageName;
                    packageEntity.CaseCount = packageModel.CaseCount;
                    packageEntity.ValidityInDays = packageModel.ValidityInDays;
                    packageEntity.Amount = packageModel.Amount;
                    packageEntity.ChangedBy = packageModel.EnteredBy;
                    packageEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Package Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Package.
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeletePackage(PackageModel packageModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var packageEntity = context.PackageMaster.FirstOrDefault(x => x.PackageId == packageModel.PackageId);
            if (packageEntity != null)
            {
                packageEntity.DeleteStatus = packageModel.DeleteStatus;
                packageEntity.ChangedBy = packageModel.EnteredBy;
                packageEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Package Deleted Successfully";
            }
            return Message;
        }


        /// <summary>
        /// Method for getting all the Package Topups
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PackageTopupModel> GetPackageTopup(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var packageModelList = new List<PackageTopupModel>();
            packageModelList = ( from packageTopup in context.PackageTopupMaster
                                     where !packageTopup.DeleteStatus
                                     select new PackageTopupModel
                                     {
                                         PackageTopupId = packageTopup.PackageTopupId,
                                         PackageTopupName = packageTopup.PackageTopupName,
                                         CaseCount = packageTopup.CaseCount,
                                         TopupAmount = packageTopup.Amount,
                                     }).ToList();

            if (packageModelList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Package Topup not found";
            }
           
            return packageModelList;
        }

        /// <summary>
        /// Method implementation for saving new Package
        /// </summary>
        /// <param name="packageModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SavePackageTopup(PackageTopupModel packageTopupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";

            PackageTopupMaster packageTopupEntity = new PackageTopupMaster();
            packageTopupEntity.PackageTopupName = packageTopupModel.PackageTopupName;
            packageTopupEntity.CaseCount = packageTopupModel.CaseCount;
            packageTopupEntity.Amount = packageTopupModel.TopupAmount;
            packageTopupEntity.EnteredBy = packageTopupModel.EnteredBy;
            packageTopupEntity.EnteredDate = DateTime.Now;
            packageTopupEntity.DeleteStatus = false;
            context.PackageTopupMaster.Add(packageTopupEntity);
            context.SaveChanges();
            Message = "Package Saved Successfully";


            return Message;
        }
    }
}
