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
    public class AdverseReactionService : IAdverseReactionService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public AdverseReactionService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get AdverseReaction by adverseReactionId
        /// </summary>
        /// <param name="adverseReactionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AdverseReactionModel GetAdverseReactionnById(long adverseReactionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var adverseReactionEntity = context.AdverseReactionMaster.Include(x => x.AllopathicDrug).FirstOrDefault(x => x.AdverseReactionId == adverseReactionId && x.DeleteStatus==true);
            if (adverseReactionEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "AdverseReaction not found";
            }
            return new AdverseReactionModel
            {
                AdverseReactionId = adverseReactionEntity.AdverseReactionId,
                AdverseReactionName = adverseReactionEntity.AdverseReactionName,
                AllopathicDrugId = adverseReactionEntity.AllopathicDrugId,
                AllopathicDrugName = adverseReactionEntity.AllopathicDrug.AllopathicDrugName,
                DeleteStatus = adverseReactionEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the AdverseReaction
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<AdverseReactionModel> GetAdverseReaction(ref ErrorResponseModel errorResponseModel)
        {
            var adverseReactionModelList = new List<AdverseReactionModel>();
            errorResponseModel = new ErrorResponseModel();
            var adverseReactionEntityList = context.AdverseReactionMaster.Include(x => x.AllopathicDrug).Where(x => x.DeleteStatus == false).ToList();
            if (adverseReactionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "AdverseReaction not found";
            }

            adverseReactionEntityList.ForEach(item =>
            {
                adverseReactionModelList.Add(new AdverseReactionModel
                {
                    AdverseReactionId = item.AdverseReactionId,
                    AdverseReactionName = item.AdverseReactionName,
                    AllopathicDrugId = item.AllopathicDrugId,
                    AllopathicDrugName = item.AllopathicDrug.AllopathicDrugName,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return adverseReactionModelList;
        }

        /// <summary>
        /// Method implementation for saving new AdverseReaction
        /// </summary>
        /// <param name="adverseReactionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveAdverseReaction(AdverseReactionModel adverseReactionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (adverseReactionModel.AdverseReactionId == 0)
            {
                AdverseReactionMaster adverseReactionEntity = new AdverseReactionMaster();
                adverseReactionEntity.AdverseReactionName = adverseReactionModel.AdverseReactionName;
                adverseReactionEntity.AllopathicDrugId = adverseReactionModel.AllopathicDrugId;
                adverseReactionEntity.DeleteStatus = true;
                context.AdverseReactionMaster.Add(adverseReactionEntity);
                context.SaveChanges();
                Message = "Adverse Reaction Saved Successfully";
            }
            else
            {
                var adverseReactionEntity = context.AdverseReactionMaster.FirstOrDefault(x => x.AdverseReactionId == adverseReactionModel.AdverseReactionId);
                if (adverseReactionEntity != null)
                {

                    adverseReactionEntity.AdverseReactionName = adverseReactionModel.AdverseReactionName;
                    adverseReactionEntity.AllopathicDrugId = adverseReactionModel.AllopathicDrugId;
                    adverseReactionEntity.DeleteStatus = true;
                
                    context.SaveChanges();
                    Message = "Adverse Reaction Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete AdverseReaction.
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteAdverseReaction(AdverseReactionModel adverseReactionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var adverseReactionEntity = context.AdverseReactionMaster.FirstOrDefault(x => x.AdverseReactionId == adverseReactionModel.AdverseReactionId);
            if (adverseReactionEntity != null)
            {
                adverseReactionEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "AdverseReaction Deleted Successfully";
            }
            return Message;
        }
    }
}
