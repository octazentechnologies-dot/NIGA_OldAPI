using Microsoft.EntityFrameworkCore;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Xunit.Abstractions;

namespace NIGA.Centrum.Business.Implementation
{
    public class AllopathicDrugService : IAllopathicDrugService
    { 
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public AllopathicDrugService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get AllopathicDrug by adverseReactionId
        /// </summary>
        /// <param name="allopathicDrugId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AllopathicDrugModel GetAllopathicDrugById(long allopathicDrugId, ref ErrorResponseModel errorResponseModel)
        {
            var listAllopathicDrugModel = new AllopathicDrugModel();
            errorResponseModel = new ErrorResponseModel();
            //if (allopathicDrugId == 0)
            //{
            //    var listSubsectionEntity = context.SectionMaster.Where(x => x.DeleteStatus == false).ToList();
            //    if (listSubsectionEntity == null)
            //    {
            //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
            //        errorResponseModel.Message = "Section not found";
            //    }


            //    listAllopathicDrugModel.SubSectionId = 0;
            //    listAllopathicDrugModel.SubSectionName = listAllopathicDrugModel.SectionName;
            //    listAllopathicDrugModel.SectionId = listAllopathicDrugModel.SectionId;
            //    listAllopathicDrugModel.ParentSubSectionId = listAllopathicDrugModel.ParentSubSectionId;


            //}

            //else
            //{
                var listSubsectionEntity = context.AllopathicDrugMaster.Include(x=>x.DrugGroup).Where(x => x.DeleteStatus == false).Where((x => x.AllopathicDrugId == allopathicDrugId)).FirstOrDefault();

                //Get all recodrs from AdverseReactionMaster join with AllopathicDrugMaster on allopathicDrugId
                var adverseReactionEntity = (from adverseReactionMaster in context.AdverseReactionMaster
                                                   join allopathicDrug in context.AllopathicDrugMaster
                                                   on adverseReactionMaster.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                                   where allopathicDrug.AllopathicDrugId == allopathicDrugId && adverseReactionMaster.DeleteStatus == false
                                                   select new AdverseReactionModel
                                                   {
                                                       AdverseReactionId = (int)adverseReactionMaster.AdverseReactionId,
                                                       AllopathicDrugId = adverseReactionMaster.AllopathicDrugId,
                                                       AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                       AdverseReactionName = adverseReactionMaster.AdverseReactionName,
                                                       DeleteStatus = adverseReactionMaster.DeleteStatus,
                                                   }).ToList();


                var otherSideEffectEntity = (from otherSideEffect in context.OtherSideEffectMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on otherSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugId && otherSideEffect.DeleteStatus == false
                                             select new OtherSideEffectModel
                                                {
                                                    OtherSideEffectId = (int)otherSideEffect.OtherSideEffectId,
                                                    AllopathicDrugId = otherSideEffect.AllopathicDrugId,
                                                    AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                    OtherSideEffectName = otherSideEffect.OtherSideEffectName,
                                                    DeleteStatus = otherSideEffect.DeleteStatus,

                                             }).ToList();

                var seriousSideEffectEntity = (from seriousSideEffect in context.SeriousSideEffectMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on seriousSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugId && seriousSideEffect.DeleteStatus == false
                                             select new SeriousSideEffectModel
                                             {
                                                 SeriousSideEffectId = (int)seriousSideEffect.SeriousSideEffectId,
                                                 AllopathicDrugId = seriousSideEffect.AllopathicDrugId,
                                                 AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                 SeriousSideEffectName = seriousSideEffect.SeriousSideEffectName,
                                                 DeleteStatus = seriousSideEffect.DeleteStatus,

                                             }).ToList();

                if (listSubsectionEntity == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Section not found";
                }
                else
                {
                    listAllopathicDrugModel.AllopathicDrugId = listSubsectionEntity.AllopathicDrugId;
                    listAllopathicDrugModel.AllopathicDrugName = listSubsectionEntity.AllopathicDrugName;
                    listAllopathicDrugModel.DrugGroupId = listSubsectionEntity.DrugGroupId;
                    listAllopathicDrugModel.DrugGroupName = listSubsectionEntity.DrugGroup.DrugGroupName;
                    listAllopathicDrugModel.DeleteStatus = listSubsectionEntity.DeleteStatus;
                    listAllopathicDrugModel.AdverseReactionModelList = adverseReactionEntity;
                    listAllopathicDrugModel.OtherSideEffectModelList = otherSideEffectEntity;
                    listAllopathicDrugModel.SeriousSideEffectModelList = seriousSideEffectEntity;


                }
            //}

            return listAllopathicDrugModel;
        }

        /// <summary>
        /// Method to get all the AllopathicDrug
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<AllopathicDrugModel> GetAllopathicDrug(ref ErrorResponseModel errorResponseModel)
        {
            var allopathicDrugModelList = new List<AllopathicDrugModel>();
            errorResponseModel = new ErrorResponseModel();
            var allopathicDrugEntity = context.AllopathicDrugMaster.Include(x => x.DrugGroup).Where(x => x.DeleteStatus == false).ToList();
            if (allopathicDrugEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "AllopathicDrug not found";
            }

            allopathicDrugEntity.ForEach(item =>
            {
                List<AdverseReactionModel> adverseReactionModelList=new List<AdverseReactionModel>();
                List<OtherSideEffectModel> otherSideEffectModelList=new List<OtherSideEffectModel>();
                List<SeriousSideEffectModel> seriousSideEffectModelList=new List<SeriousSideEffectModel>();
                var adverseReactionEntity = context.AdverseReactionMaster.Where(x => x.DeleteStatus == false && x.AllopathicDrugId==item.AllopathicDrugId).ToList();
                var otherSideEffectEntity = context.OtherSideEffectMaster.Where(x => x.DeleteStatus == false && x.AllopathicDrugId==item.AllopathicDrugId).ToList();
                var seriousSideEffectEntity = context.SeriousSideEffectMaster.Where(x => x.DeleteStatus == false && x.AllopathicDrugId==item.AllopathicDrugId).ToList();

                adverseReactionEntity.ForEach(adverseReactionItem =>
                {
                    adverseReactionModelList.Add(new AdverseReactionModel() 
                    {
                        AdverseReactionId = (int)adverseReactionItem.AdverseReactionId,
                        AllopathicDrugId = adverseReactionItem.AllopathicDrugId,
                        AllopathicDrugName = item.AllopathicDrugName,
                        AdverseReactionName = adverseReactionItem.AdverseReactionName,
                        DeleteStatus = adverseReactionItem.DeleteStatus,
                    });
                });

                otherSideEffectEntity.ForEach(otherSideEffectItem =>
                {
                    otherSideEffectModelList.Add(new OtherSideEffectModel()
                    {
                        OtherSideEffectId = (int)otherSideEffectItem.OtherSideEffectId,
                        AllopathicDrugId = otherSideEffectItem.AllopathicDrugId,
                        AllopathicDrugName = item.AllopathicDrugName,
                        OtherSideEffectName = otherSideEffectItem.OtherSideEffectName,
                        DeleteStatus = otherSideEffectItem.DeleteStatus,
                    });
                });

                seriousSideEffectEntity.ForEach(seriousSideEffectItem =>
                {
                    seriousSideEffectModelList.Add(new SeriousSideEffectModel()
                    {
                        SeriousSideEffectId = (int)seriousSideEffectItem.SeriousSideEffectId,
                        AllopathicDrugId = seriousSideEffectItem.AllopathicDrugId,
                        AllopathicDrugName = item.AllopathicDrugName,
                        SeriousSideEffectName = seriousSideEffectItem.SeriousSideEffectName,
                        DeleteStatus = seriousSideEffectItem.DeleteStatus,
                    });
                });

                allopathicDrugModelList.Add(new AllopathicDrugModel
                {
                    DrugGroupId = item.DrugGroupId,
                    DrugGroupName = item.DrugGroup.DrugGroupName,
                    AllopathicDrugId = item.AllopathicDrugId,
                    AllopathicDrugName = item.AllopathicDrugName,
                    DeleteStatus = item.DeleteStatus,
                    AdverseReactionModelList = adverseReactionModelList,
                    OtherSideEffectModelList = otherSideEffectModelList,
                    SeriousSideEffectModelList = seriousSideEffectModelList,
                });
            });
            return allopathicDrugModelList;
        }

        /// <summary>
        /// Method implementation for saving new AllopathicDrug
        /// </summary>
        /// <param name="AllopathicDrugModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveAllopathicDrug(AllopathicDrugModel allopathicDrugModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (allopathicDrugModel.AllopathicDrugId == 0)
            {
                AllopathicDrugMaster allopathicDrugEntity = new AllopathicDrugMaster();
                allopathicDrugEntity.AllopathicDrugName = allopathicDrugModel.AllopathicDrugName;
                allopathicDrugEntity.DrugGroupId = allopathicDrugModel.DrugGroupId;
                allopathicDrugEntity.DeleteStatus = false;
                context.AllopathicDrugMaster.Add(allopathicDrugEntity);
                context.SaveChanges();

                allopathicDrugModel.AdverseReactionModelList.ForEach(item =>
                {
                    AdverseReactionMaster adverseReactionEntity = new AdverseReactionMaster();
                    adverseReactionEntity.AdverseReactionName = item.AdverseReactionName;
                    adverseReactionEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                    adverseReactionEntity.DeleteStatus = false; 
                    context.AdverseReactionMaster.Add(adverseReactionEntity);
                    context.SaveChanges() ;
                });

                allopathicDrugModel.OtherSideEffectModelList.ForEach(item =>
                {
                    OtherSideEffectMaster otherSideEffectEntity = new OtherSideEffectMaster();
                    otherSideEffectEntity.OtherSideEffectName = item.OtherSideEffectName;
                    otherSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                    otherSideEffectEntity.DeleteStatus = false;
                    context.OtherSideEffectMaster.Add(otherSideEffectEntity);
                    context.SaveChanges();
                });

                allopathicDrugModel.SeriousSideEffectModelList.ForEach(item =>
                {
                    SeriousSideEffectMaster seriousSideEffectEntity = new SeriousSideEffectMaster();
                    seriousSideEffectEntity.SeriousSideEffectName = item.SeriousSideEffectName;
                    seriousSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                    seriousSideEffectEntity.DeleteStatus = false;
                    context.SeriousSideEffectMaster.Add(seriousSideEffectEntity);
                    context.SaveChanges();
                });

                Message = "allopathicDrug Saved Successfully";
            }
            else
            {
                var allopathicDrugEntity = context.AllopathicDrugMaster.FirstOrDefault(x => x.AllopathicDrugId == allopathicDrugModel.AllopathicDrugId);
                if (allopathicDrugEntity != null)
                {

                    allopathicDrugEntity.AllopathicDrugName = allopathicDrugModel.AllopathicDrugName;
                    allopathicDrugEntity.DrugGroupId = allopathicDrugModel.DrugGroupId;
                    allopathicDrugEntity.DeleteStatus = false;
                    context.SaveChanges();

                    allopathicDrugModel.AdverseReactionModelList.ForEach(item =>
                    {
                        var adverseReactionEntity = context.AdverseReactionMaster.FirstOrDefault(x => x.AdverseReactionId == item.AdverseReactionId && x.DeleteStatus == false);
                        if (adverseReactionEntity != null)
                        {
                            adverseReactionEntity.AdverseReactionId = item.AdverseReactionId;
                            adverseReactionEntity.AdverseReactionName = item.AdverseReactionName;
                            adverseReactionEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            adverseReactionEntity.DeleteStatus = false;
                            context.SaveChanges();
                        }
                        else
                        {
                            AdverseReactionMaster _adverseReactionEntity = new AdverseReactionMaster();
                            _adverseReactionEntity.AdverseReactionName = item.AdverseReactionName;
                            _adverseReactionEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            _adverseReactionEntity.DeleteStatus = false;
                            context.AdverseReactionMaster.Add(_adverseReactionEntity);
                            context.SaveChanges();
                        }
                       
                    });

                    allopathicDrugModel.OtherSideEffectModelList.ForEach(item =>
                    {
                        var otherSideEffectEntity = context.OtherSideEffectMaster.FirstOrDefault(x => x.OtherSideEffectId == item.OtherSideEffectId && x.DeleteStatus == false);

                        if (otherSideEffectEntity != null)
                        {
                            otherSideEffectEntity.OtherSideEffectId = item.OtherSideEffectId;
                            otherSideEffectEntity.OtherSideEffectName = item.OtherSideEffectName;
                            otherSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            otherSideEffectEntity.DeleteStatus = false;
                            context.SaveChanges();
                        }
                        else
                        {
                            OtherSideEffectMaster _otherSideEffectEntity = new OtherSideEffectMaster();
                            _otherSideEffectEntity.OtherSideEffectName = item.OtherSideEffectName;
                            _otherSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            _otherSideEffectEntity.DeleteStatus = false;
                            context.OtherSideEffectMaster.Add(_otherSideEffectEntity);
                            context.SaveChanges();
                        }
                     
                    });

                    allopathicDrugModel.SeriousSideEffectModelList.ForEach(item =>
                    {
                        var seriousSideEffectEntity = context.SeriousSideEffectMaster.FirstOrDefault(x => x.SeriousSideEffectId == item.SeriousSideEffectId && x.DeleteStatus == false);

                        if (seriousSideEffectEntity != null)
                        {
                            seriousSideEffectEntity.SeriousSideEffectId = item.SeriousSideEffectId;
                            seriousSideEffectEntity.SeriousSideEffectName = item.SeriousSideEffectName;
                            seriousSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            seriousSideEffectEntity.DeleteStatus = false;
                            context.SaveChanges();
                        }
                        else
                        {
                            SeriousSideEffectMaster _seriousSideEffectEntity = new SeriousSideEffectMaster();
                            _seriousSideEffectEntity.SeriousSideEffectName = item.SeriousSideEffectName;
                            _seriousSideEffectEntity.AllopathicDrugId = allopathicDrugEntity.AllopathicDrugId;
                            _seriousSideEffectEntity.DeleteStatus = false;
                            context.SeriousSideEffectMaster.Add(_seriousSideEffectEntity);
                            context.SaveChanges();
                        }
                       
                    });

                    Message = "allopathicDrug Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete AllopathicDrug.
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteAllopathicDrug(AllopathicDrugModel allopathicDrugModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var allopathicDrugEntity = context.AllopathicDrugMaster.FirstOrDefault(x => x.AllopathicDrugId == allopathicDrugModel.AllopathicDrugId);
            if (allopathicDrugEntity != null)
            {
                allopathicDrugEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "AllopathicDrug Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Methood to get AllopathicDrug by adverseReactionId
        /// </summary>
        /// <param name="allopathicDrugId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AllopathicDrugModel GetAllopathicDrugByName(string allopathicDrugName, ref ErrorResponseModel errorResponseModel)
        {
            var listAllopathicDrugModel = new AllopathicDrugModel();
            errorResponseModel = new ErrorResponseModel();

            var allopathicDrugEntity = (from allopathicDrug in context.AllopathicDrugMaster
                                        join drugGroup in context.DrugGroupMaster on allopathicDrug.DrugGroupId equals drugGroup.DrugGroupId
                                        join drugSystem in context.DrugSystemMaster on drugGroup.DrugSystemId equals drugSystem.DrugSystemId
                                        where allopathicDrug.AllopathicDrugName == allopathicDrugName && allopathicDrug.DeleteStatus == false
                                        select new AllopathicDrugModel
                                        {
                                            AllopathicDrugId = allopathicDrug.AllopathicDrugId,
                                            AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                            DrugGroupId = drugGroup.DrugGroupId,
                                            DrugGroupName = drugGroup.DrugGroupName,
                                            DrugSystemId = drugSystem.DrugSystemId,
                                            DrugSystemName = drugSystem.DrugSystemName,
                                        }).FirstOrDefault();
            if(allopathicDrugEntity != null )
            {
                //Get all recodrs from AdverseReactionMaster join with AllopathicDrugMaster on allopathicDrugId
                var adverseReactionEntity = (from adverseReactionMaster in context.AdverseReactionMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on adverseReactionMaster.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && adverseReactionMaster.DeleteStatus == false
                                             select new AdverseReactionModel
                                             {
                                                 AdverseReactionId = (int)adverseReactionMaster.AdverseReactionId,
                                                 AllopathicDrugId = adverseReactionMaster.AllopathicDrugId,
                                                 AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                 AdverseReactionName = adverseReactionMaster.AdverseReactionName,
                                                 DeleteStatus = adverseReactionMaster.DeleteStatus,
                                             }).ToList();


                var otherSideEffectEntity = (from otherSideEffect in context.OtherSideEffectMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on otherSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && otherSideEffect.DeleteStatus == false
                                             select new OtherSideEffectModel
                                             {
                                                 OtherSideEffectId = (int)otherSideEffect.OtherSideEffectId,
                                                 AllopathicDrugId = otherSideEffect.AllopathicDrugId,
                                                 AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                 OtherSideEffectName = otherSideEffect.OtherSideEffectName,
                                                 DeleteStatus = otherSideEffect.DeleteStatus,

                                             }).ToList();

                var seriousSideEffectEntity = (from seriousSideEffect in context.SeriousSideEffectMaster
                                               join allopathicDrug in context.AllopathicDrugMaster
                                               on seriousSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                               where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && seriousSideEffect.DeleteStatus == false
                                               select new SeriousSideEffectModel
                                               {
                                                   SeriousSideEffectId = (int)seriousSideEffect.SeriousSideEffectId,
                                                   AllopathicDrugId = seriousSideEffect.AllopathicDrugId,
                                                   AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                   SeriousSideEffectName = seriousSideEffect.SeriousSideEffectName,
                                                   DeleteStatus = seriousSideEffect.DeleteStatus,

                                               }).ToList();
                allopathicDrugEntity.AdverseReactionModelList = adverseReactionEntity;
                allopathicDrugEntity.OtherSideEffectModelList = otherSideEffectEntity;
                allopathicDrugEntity.SeriousSideEffectModelList = seriousSideEffectEntity;
            }
            else
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
            }
            return allopathicDrugEntity;
        }

        public List<AllopathicDrugDDModel> GetAllopathicDrugDDL()
        {
            List<AllopathicDrugDDModel> allopathicdrugDDL = new List<AllopathicDrugDDModel>();
            allopathicdrugDDL = (from AllopathicDrugMaster in context.AllopathicDrugMaster
                            where AllopathicDrugMaster.DeleteStatus == false
                            select new AllopathicDrugDDModel
                            {
                              AllopathicDrugName= AllopathicDrugMaster.AllopathicDrugName,
                              AllopathicDrugId= AllopathicDrugMaster.AllopathicDrugId

                            }
                            ).ToList();

            return allopathicdrugDDL;
        }


        /// <summary>
        /// Methood to get AllopathicDrug by adverseReactionId
        /// </summary>
        /// <param name="allopathicDrugId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AllopathicDrugModel GetAllopathicDrugByID(int allopathicDrugId, ref ErrorResponseModel errorResponseModel)
        {
            var listAllopathicDrugModel = new AllopathicDrugModel();
            errorResponseModel = new ErrorResponseModel();

            var allopathicDrugEntity = (from allopathicDrug in context.AllopathicDrugMaster
                                        join drugGroup in context.DrugGroupMaster on allopathicDrug.DrugGroupId equals drugGroup.DrugGroupId
                                        join drugSystem in context.DrugSystemMaster on drugGroup.DrugSystemId equals drugSystem.DrugSystemId
                                        where allopathicDrug.AllopathicDrugId == allopathicDrugId && allopathicDrug.DeleteStatus == false
                                        select new AllopathicDrugModel
                                        {
                                            AllopathicDrugId = allopathicDrug.AllopathicDrugId,
                                            AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                            DrugGroupId = drugGroup.DrugGroupId,
                                            DrugGroupName = drugGroup.DrugGroupName,
                                            DrugSystemId = drugSystem.DrugSystemId,
                                            DrugSystemName = drugSystem.DrugSystemName,
                                        }).FirstOrDefault();
            if (allopathicDrugEntity != null)
            {
                //Get all recodrs from AdverseReactionMaster join with AllopathicDrugMaster on allopathicDrugId
                var adverseReactionEntity = (from adverseReactionMaster in context.AdverseReactionMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on adverseReactionMaster.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && adverseReactionMaster.DeleteStatus == false
                                             select new AdverseReactionModel
                                             {
                                                 AdverseReactionId = (int)adverseReactionMaster.AdverseReactionId,
                                                 AllopathicDrugId = adverseReactionMaster.AllopathicDrugId,
                                                 AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                 AdverseReactionName = adverseReactionMaster.AdverseReactionName,
                                                 DeleteStatus = adverseReactionMaster.DeleteStatus,
                                             }).ToList();


                var otherSideEffectEntity = (from otherSideEffect in context.OtherSideEffectMaster
                                             join allopathicDrug in context.AllopathicDrugMaster
                                             on otherSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                             where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && otherSideEffect.DeleteStatus == false
                                             select new OtherSideEffectModel
                                             {
                                                 OtherSideEffectId = (int)otherSideEffect.OtherSideEffectId,
                                                 AllopathicDrugId = otherSideEffect.AllopathicDrugId,
                                                 AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                 OtherSideEffectName = otherSideEffect.OtherSideEffectName,
                                                 DeleteStatus = otherSideEffect.DeleteStatus,

                                             }).ToList();

                var seriousSideEffectEntity = (from seriousSideEffect in context.SeriousSideEffectMaster
                                               join allopathicDrug in context.AllopathicDrugMaster
                                               on seriousSideEffect.AllopathicDrugId equals allopathicDrug.AllopathicDrugId
                                               where allopathicDrug.AllopathicDrugId == allopathicDrugEntity.AllopathicDrugId && seriousSideEffect.DeleteStatus == false
                                               select new SeriousSideEffectModel
                                               {
                                                   SeriousSideEffectId = (int)seriousSideEffect.SeriousSideEffectId,
                                                   AllopathicDrugId = seriousSideEffect.AllopathicDrugId,
                                                   AllopathicDrugName = allopathicDrug.AllopathicDrugName,
                                                   SeriousSideEffectName = seriousSideEffect.SeriousSideEffectName,
                                                   DeleteStatus = seriousSideEffect.DeleteStatus,

                                               }).ToList();
                allopathicDrugEntity.AdverseReactionModelList = adverseReactionEntity;
                allopathicDrugEntity.OtherSideEffectModelList = otherSideEffectEntity;
                allopathicDrugEntity.SeriousSideEffectModelList = seriousSideEffectEntity;
            }
            else
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
            }
            return allopathicDrugEntity;
        }

    }
}
