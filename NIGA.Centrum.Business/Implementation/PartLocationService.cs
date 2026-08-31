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
    /// This is implementation  for the part location operations 
    /// </summary>
   public class PartLocationService : IPartLocationService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PartLocationService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get part location by partlocationId
        /// </summary>
        /// <param name="partlocationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PartLocationModel GetPartLocationById(long partlocationId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var partlocationEntity = context.PartLocationMaster.FirstOrDefault(x => x.PartLocationId == partlocationId && !x.DeleteStatus);
            if (partlocationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Part Location not found";
            }
            return new PartLocationModel
            {
                PartLocationId = partlocationEntity.PartLocationId,
                PartLocationName = partlocationEntity.PartLocationName,
                Description = partlocationEntity.Description,
                EnteredDate = partlocationEntity.EnteredDate,
                EnteredBy = partlocationEntity.EnteredBy,
                ChangedBy = partlocationEntity.ChangedBy,
                ChangedDate = partlocationEntity.ChangedDate,
                DeleteStatus = partlocationEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the partlocations
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PartLocationModel> GetPartLocations(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var partlocationModelList = new List<PartLocationModel>();
            var partlocationEntityList = context.PartLocationMaster.Where(x => x.DeleteStatus == false).ToList();
            if (partlocationEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Part Location not found";
            }
            partlocationEntityList.ForEach(item =>
            {
                partlocationModelList.Add(new PartLocationModel
                {
                    PartLocationId = item.PartLocationId,
                    PartLocationName = item.PartLocationName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return partlocationModelList;
        }

        /// <summary>
        /// Method implementation for saving new partlocation
        /// </summary>
        /// <param name="partLocationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SavePartLocation(PartLocationModel partLocationModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (partLocationModel.PartLocationId == 0)
            {
                PartLocationMaster partlocationEntity = new PartLocationMaster();             
                partlocationEntity.PartLocationName = partLocationModel.PartLocationName;
                partlocationEntity.Description = partLocationModel.Description;
                partlocationEntity.EnteredBy = partLocationModel.EnteredBy;
                partlocationEntity.EnteredDate = DateTime.Now;
                context.PartLocationMaster.Add(partlocationEntity);
                context.SaveChanges();
                Message = "Part Location Saved Successfully";
            }
            else
            {
                var partlocationEntity = context.PartLocationMaster.FirstOrDefault(x => x.PartLocationId == partLocationModel.PartLocationId);
                if (partlocationEntity != null)
                {
                    partlocationEntity.PartLocationName = partLocationModel.PartLocationName;
                    partlocationEntity.Description = partLocationModel.Description;
                    partlocationEntity.ChangedBy = partLocationModel.EnteredBy;
                    partlocationEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Part Location Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete partlocation.
        /// </summary>
        /// <param name="partLocationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeletePartLocation(PartLocationModel partLocationModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var partlocationEntity = context.PartLocationMaster.FirstOrDefault(x => x.PartLocationId == partLocationModel.PartLocationId);
            if (partlocationEntity != null)
            {
                partlocationEntity.DeleteStatus = partLocationModel.DeleteStatus;
                partlocationEntity.ChangedBy = partLocationModel.EnteredBy;
                partlocationEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Part Location Deleted Successfully";
            }
            return Message;
        }
    }
}
