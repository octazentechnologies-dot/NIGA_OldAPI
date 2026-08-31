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
    public class PathologyService : IPathologyService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PathologyService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }
        public string DeletePathology(PathologyModel pathologyModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var pathologyEntity = context.Pathology.FirstOrDefault(x => x.PathologyId == pathologyModel.PathologyId);
            if (pathologyEntity != null)
            {
                pathologyEntity.DeleteStatus = true;
                // context.Remove(authorEntity);
                context.SaveChanges();
                Message = "Pathology Deleted Successfully";

            }
            return Message;
        }

        public List<PathologyModel> GetPathology(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var ModelList = new List<PathologyModel>();
            var pathologyEntityList = context.Pathology.Where(x => x.DeleteStatus == false).ToList();
            if (pathologyEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Pathology not found";
            }
            pathologyEntityList.ForEach(item =>
            {
                ModelList.Add(new PathologyModel
                {
                    PathologyId = item.PathologyId,
                   PathologyName = item.PathologyName,
                    Description = item.Description,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return ModelList;
        }

        public PathologyModel GetPathologyById(long pathologyId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var pathologyEntity = context.Pathology.Where(x => x.DeleteStatus == false).FirstOrDefault(x => x.PathologyId == pathologyId);
            if (pathologyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Pathology not found";
            }
            return new PathologyModel
            {
                PathologyId = pathologyEntity.PathologyId,
                PathologyName = pathologyEntity.PathologyName,
                Description = pathologyEntity.Description,
                DeleteStatus = pathologyEntity.DeleteStatus,
            };
        }

        public string SavePathology(PathologyModel pathologyModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (pathologyModel.PathologyId == 0)
            {
                Pathology pathologyEntity = new Pathology();
                pathologyEntity.PathologyId = pathologyModel.PathologyId;
                pathologyEntity.PathologyName= pathologyModel.PathologyName;
                pathologyEntity.Description = pathologyModel.Description;
                pathologyEntity.DeleteStatus = false;
                context.Pathology.Add(pathologyEntity);
                context.SaveChanges();
                Message = "Pathology Saved Successfully";
            }
            else
            {
                var pathologyEntity = context.Pathology.FirstOrDefault(x => x.PathologyId == pathologyModel.PathologyId);
                if (pathologyEntity != null)
                {


                    pathologyEntity.PathologyName = pathologyModel.PathologyName;
                    pathologyEntity.Description = pathologyModel.Description;
                    pathologyEntity.DeleteStatus = pathologyModel.DeleteStatus;
                    context.SaveChanges();
                    Message = "Pathology Updated Successfully";
                }
            }
            return Message;
        }
    }
}
