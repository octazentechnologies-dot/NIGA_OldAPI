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
    /// This is implementation  for the intensity operations 
    /// </summary>
   public class IntensityService : IIntensityService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public IntensityService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get intensity by IntensityId
        /// </summary>
        /// <param name="intensityId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public IntensityModel GetIntensityById(long intensityId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var intensityEntity = context.IntensityMaster.FirstOrDefault(x => x.IntensityId == intensityId && !x.DeleteStatus);
            if (intensityEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Intensity not found";
            }
            return new IntensityModel
            {
                IntensityId = intensityEntity.IntensityId,
                IntensityNo = intensityEntity.IntensityNo,
                Description = intensityEntity.Description,
                EnteredDate = intensityEntity.EnteredDate,
                EnteredBy = intensityEntity.EnteredBy,
                ChangedBy = intensityEntity.ChangedBy,
                ChangedDate = intensityEntity.ChangedDate,
                DeleteStatus = intensityEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the Intensities
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<IntensityModel> GetIntensities( ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var intensityModelList = new List<IntensityModel>();
            var intensityEntityList = context.IntensityMaster.Where(x => x.DeleteStatus == false).ToList();

            if (intensityEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Intensity not found";
            }
            intensityEntityList.ForEach(item =>
            {
                intensityModelList.Add(new IntensityModel
                {
                    IntensityId = item.IntensityId,
                    IntensityNo = item.IntensityNo,                  
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return intensityModelList;
        }

        /// <summary>
        /// Method implementation for saving new Intensity
        /// </summary>
        /// <param name="intensityModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveIntensity(IntensityModel intensityModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (intensityModel.IntensityId == 0)
            {
                IntensityMaster intensityEntity = new IntensityMaster();

                intensityEntity.IntensityNo = intensityModel.IntensityNo;
                intensityEntity.Description = intensityModel.Description;
                intensityEntity.EnteredBy = intensityModel.EnteredBy;
                intensityEntity.EnteredDate = DateTime.Now;
                context.IntensityMaster.Add(intensityEntity);
                context.SaveChanges();
                Message = "Intensity Saved Successfully";
            }
            else
            {
                var intensityEntity = context.IntensityMaster.FirstOrDefault(x => x.IntensityId == intensityModel.IntensityId);
                if (intensityEntity != null)
                {


                    intensityEntity.IntensityNo = intensityModel.IntensityNo;
                    intensityEntity.Description = intensityModel.Description;
                    intensityEntity.ChangedBy = intensityModel.EnteredBy;
                    intensityEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Intensity Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete intensity.
        /// </summary>
        /// <param name="intensityModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteIntensity(IntensityModel intensityModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var intensityEntity = context.IntensityMaster.FirstOrDefault(x => x.IntensityId == intensityModel.IntensityId);
            if (intensityEntity != null)
            {
                intensityEntity.DeleteStatus = intensityModel.DeleteStatus;
                intensityEntity.ChangedBy = intensityModel.EnteredBy;
                intensityEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Intensity Deleted Successfully";
            }
            return Message;
        }
    }
}
