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
    /// This is implementation  for the bodypart operations 
    /// </summary>
    public class BodyPartService : IBodyPartService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public BodyPartService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get bodypart by bodypartId
        /// </summary>
        /// <param name="bodyPartId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public BodyPartModel GetBodyPartById(long bodyPartId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var bodypartEntity = context.BodyPartMaster.FirstOrDefault(x => x.BodyPartId == bodyPartId && !x.DeleteStatus);
            if (bodypartEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Body Part not found";
            }
            return new BodyPartModel
            {
                BodyPartId = bodypartEntity.BodyPartId,
                SectionId = bodypartEntity.SectionId,
                BodyPartName = bodypartEntity.BodyPartName,
                Description = bodypartEntity.Description,
                EnteredDate = bodypartEntity.EnteredDate,
                EnteredBy = bodypartEntity.EnteredBy,
                ChangedBy = bodypartEntity.ChangedBy,
                ChangedDate = bodypartEntity.ChangedDate,
                DeleteStatus = bodypartEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the bodyparts
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<BodyPartModel> GetBodyParts( ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var bodypartModelList = new List<BodyPartModel>();
            var bodypartEntityList = context.BodyPartMaster.Where(x => x.DeleteStatus == false).ToList();

            if (bodypartEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Body Part not found";
            }
            bodypartEntityList.ForEach(item =>
            {
                bodypartModelList.Add(new BodyPartModel
                {
                    BodyPartId = item.BodyPartId,
                    SectionId = item.SectionId,
                    BodyPartName = item.BodyPartName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return bodypartModelList;
        }

        /// <summary>
        /// Method implementation for saving new BodyPart
        /// </summary>
        /// <param name="bodypartModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveBodyPart(BodyPartModel bodypartModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (bodypartModel.BodyPartId == 0)
            {
                BodyPartMaster bodypartEntity = new BodyPartMaster();
                bodypartEntity.SectionId = bodypartModel.SectionId;
                bodypartEntity.BodyPartName = bodypartModel.BodyPartName;
                bodypartEntity.Description = bodypartModel.Description;
                bodypartEntity.EnteredBy = bodypartModel.EnteredBy;
                bodypartEntity.EnteredDate = DateTime.Now;
                context.BodyPartMaster.Add(bodypartEntity);
                context.SaveChanges();
                Message = "Body Part Saved Successfully";
            }
            else
            {
                var bodypartEntity = context.BodyPartMaster.FirstOrDefault(x => x.BodyPartId == bodypartModel.BodyPartId);
                if (bodypartEntity != null)
                {

                    bodypartEntity.SectionId = bodypartModel.SectionId;
                    bodypartEntity.BodyPartName = bodypartModel.BodyPartName;
                    bodypartEntity.Description = bodypartModel.Description;
                    bodypartEntity.ChangedBy = bodypartModel.EnteredBy;
                    bodypartEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Body Part Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete bodypart.
        /// </summary>
        /// <param name="bodypartModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteBodyPart(BodyPartModel bodypartModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var bodypartEntity = context.BodyPartMaster.FirstOrDefault(x => x.BodyPartId == bodypartModel.BodyPartId);
            if (bodypartEntity != null)
            {
                bodypartEntity.DeleteStatus = bodypartModel.DeleteStatus;
                bodypartEntity.ChangedBy = bodypartModel.EnteredBy;
                bodypartEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Body Part Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Interface is used to deactivate bodypart.
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<BodyPartModel> GetBodyPartBySection(long SectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var bodypartModelList = new List<BodyPartModel>();
            var bodypartEntityList = context.BodyPartMaster.Where(x => x.SectionId == SectionId && x.DeleteStatus==false).ToList();
            if (bodypartEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Body Part not found";
            }
            bodypartEntityList.ForEach(item =>
            {
                bodypartModelList.Add(new BodyPartModel
                {
                    BodyPartId = item.BodyPartId,
                    SectionId = item.SectionId,
                    BodyPartName = item.BodyPartName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return bodypartModelList;
        }
    }
}
