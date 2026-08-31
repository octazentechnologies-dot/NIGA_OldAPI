using Microsoft.EntityFrameworkCore;
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
    public class DrugSystemService : IDrugSystemService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DrugSystemService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get DrugSystem by drugSystemId
        /// </summary>
        /// <param name="drugSystemId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public DrugSystemModel GetDrugSystemById(long drugSystemId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var drugSystemEntity = context.DrugSystemMaster.FirstOrDefault(x => x.DrugSystemId == drugSystemId && x.DeleteStatus == false);
            if (drugSystemEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DrugSystem not found";
            }
            return new DrugSystemModel
            {
                DrugSystemId = drugSystemEntity.DrugSystemId,
                DrugSystemName = drugSystemEntity.DrugSystemName,
                DeleteStatus = drugSystemEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the DrugSystem
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<DrugSystemModel> GetDrugSystem(ref ErrorResponseModel errorResponseModel)
        {
            var drugSystemModelList = new List<DrugSystemModel>();
            errorResponseModel = new ErrorResponseModel();
            var drugSystemEntityList = context.DrugSystemMaster.Where(x => x.DeleteStatus == false).ToList();
            if (drugSystemEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DrugSystem not found";
            }

            drugSystemEntityList.ForEach(item =>
            {
                drugSystemModelList.Add(new DrugSystemModel
                {
                    DrugSystemId = item.DrugSystemId,
                    DrugSystemName = item.DrugSystemName,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return drugSystemModelList;
        }

        /// <summary>
        /// Method implementation for saving new DrugSystem
        /// </summary>
        /// <param name="DrugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveDrugSystem(DrugSystemModel drugSystemModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (drugSystemModel.DrugSystemId == 0)
            {
                DrugSystemMaster drugSystemEntity = new DrugSystemMaster();
                drugSystemEntity.DrugSystemName = drugSystemModel.DrugSystemName;
                drugSystemEntity.DeleteStatus = false;
                context.DrugSystemMaster.Add(drugSystemEntity);
                context.SaveChanges();
                Message = "DrugSystem Saved Successfully";
            }
            else
            {
                var drugSystemEntity = context.DrugSystemMaster.FirstOrDefault(x => x.DrugSystemId == drugSystemModel.DrugSystemId);
                if (drugSystemEntity != null)
                {

                    drugSystemEntity.DrugSystemName = drugSystemModel.DrugSystemName;
                    drugSystemEntity.DeleteStatus = false;

                    context.SaveChanges();
                    Message = "DrugSystem Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete DrugSystem.
        /// </summary>
        /// <param name="drugSystemModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteDrugSystem(DrugSystemModel drugSystemModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var drugSystemEntity = context.DrugSystemMaster.FirstOrDefault(x => x.DrugSystemId == drugSystemModel.DrugSystemId);
            if (drugSystemEntity != null)
            {
                drugSystemEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "DrugSystem Deleted Successfully";
            }
            return Message;
        }
    }
}
