using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    /// This is implementation for the remedy operations 
    /// </summary>
    public class RemedyService : IRemedyService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public RemedyService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }
        /// <summary>
        /// Methood to get remedy by RemedyId
        /// </summary>
        /// <param name="remedyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RemedyModel GetRemedyById(long remedyId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var remedyEntity = context.RemedyMaster.FirstOrDefault(x => x.RemedyId == remedyId && !x.DeleteStatus);
            if (remedyEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }
            return new RemedyModel
            {
                RemedyId = remedyEntity.RemedyId,
                RemedyName = remedyEntity.RemedyName,
                RemedyAlias = string.IsNullOrEmpty(remedyEntity.RemedyAlias) ? "Not Available" : remedyEntity.RemedyAlias,
                Description = remedyEntity.Description,
                EnteredDate = remedyEntity.EnteredDate,
                EnteredBy = remedyEntity.EnteredBy,
                ChangedBy = remedyEntity.ChangedBy,
                ChangedDate = remedyEntity.ChangedDate,
                DeleteStatus = remedyEntity.DeleteStatus,
                ThermalId=remedyEntity.ThermalId,
                CommonOrUncommon=remedyEntity.CommonOrUncommon,
                ThemesOrCharacteristics=remedyEntity.ThemesOrCharacteristics,
                Generals=remedyEntity.Generals,
                Modalities=remedyEntity.Modalities,
                Particulars=remedyEntity.Particulars,
            };

        }

        /// <summary>
        /// Method to get all the remedies
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SearchRemedyModel> GetRemedies(string search,ref ErrorResponseModel errorResponseModel)
        {
            var remedyModelList = new List<SearchRemedyModel>();
            errorResponseModel = new ErrorResponseModel();
            var remedyEntityList = context.RemedyMaster.Where(x => x.DeleteStatus == false
            &&(!string.IsNullOrEmpty(search)?(x.RemedyName.StartsWith(search)):true)).ToList();
            if (remedyEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }

            remedyEntityList.ForEach(item =>

            {
               
                remedyModelList.Add(new SearchRemedyModel
                {
                    
                    RemedyId = item.RemedyId,
                    RemedyName = item.RemedyName,
                    Description = item.Description,
                    DeleteStatus = item.DeleteStatus,
                    RemedyAlias = string.IsNullOrEmpty(item.RemedyAlias) ? "Not Available" : item.RemedyAlias
                  //  RemedyAlias =item.RemedyAlias ?? "Not Avabile",

                });
               
            });
            
            return remedyModelList;
        }

        /// <summary>
        /// Method implementation for saving new Remedie
        /// </summary>
        /// <param name="remedyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveRemedy(RemedyModel remedyModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (remedyModel.RemedyId == 0)
            {
                RemedyMaster remedyEntity = new RemedyMaster();
                remedyEntity.RemedyName = remedyModel.RemedyName;
                remedyEntity.RemedyAlias = remedyModel.RemedyAlias;
                remedyEntity.Description = remedyModel.Description;
                remedyEntity.ThermalId = remedyModel.ThermalId;
                remedyEntity.CommonOrUncommon = remedyModel.CommonOrUncommon;
                remedyEntity.ThemesOrCharacteristics = remedyModel.ThemesOrCharacteristics;
                remedyEntity.Generals = remedyModel.Generals;
                remedyEntity.Modalities = remedyModel.Modalities;
                remedyEntity.Particulars = remedyModel.Particulars;
                remedyEntity.EnteredBy = remedyModel.EnteredBy;
                remedyEntity.EnteredDate = DateTime.Now;
                context.RemedyMaster.Add(remedyEntity);
                context.SaveChanges();
                Message = "Remedy Saved Successfully";
            }
            else
            {
                var remedyEntity = context.RemedyMaster.FirstOrDefault(x => x.RemedyId == remedyModel.RemedyId);
                if (remedyEntity != null)
                {

                    remedyEntity.RemedyName = remedyModel.RemedyName;
                    remedyEntity.RemedyAlias = remedyModel.RemedyAlias;
                    remedyEntity.Description = remedyModel.Description;
                    remedyEntity.ThermalId = remedyModel.ThermalId;
                    remedyEntity.CommonOrUncommon = remedyModel.CommonOrUncommon;
                    remedyEntity.ThemesOrCharacteristics = remedyModel.ThemesOrCharacteristics;
                    remedyEntity.Generals = remedyModel.Generals;
                    remedyEntity.Modalities = remedyModel.Modalities;
                    remedyEntity.Particulars = remedyModel.Particulars;
                    remedyEntity.ChangedBy = remedyModel.EnteredBy;
                    remedyEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Remedy Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Remedie.
        /// </summary>
        /// <param name="remedyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteRemedy(RemedyModel remedyModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var remedyEntity = context.RemedyMaster.FirstOrDefault(x => x.RemedyId == remedyModel.RemedyId);
            if (remedyEntity != null)
            {
                remedyEntity.DeleteStatus = remedyModel.DeleteStatus;
                remedyEntity.ChangedBy = remedyModel.EnteredBy;
                remedyEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Remedy Deleted Successfully";
            }
            return Message;
        }

        public List<RemedyModel> GetRemedyBySection(long subSectionId, ref ErrorResponseModel errorResponseModel)
        {
            var remedyModel = new List<RemedyModel>();
            errorResponseModel = new ErrorResponseModel();
            var remedyEntities = (from remedyDetails in context.RubricRemedyDetails
                                  join remedyMaster in context.RemedyMaster
                                  on remedyDetails.RemedyId equals remedyMaster.RemedyId
                                  join gradeMaster in context.RemedyGradeMaster
                                  on remedyDetails.GradeId equals gradeMaster.GradeId
                                  where remedyDetails.SubSectionId == subSectionId && remedyDetails.DeletedStatus==false
                                  select new
                                  {
                                      remedyDetails.RemedyId,
                                      remedyMaster.RemedyName,
                                      remedyMaster.RemedyAlias,
                                      gradeMaster.FontName,
                                      gradeMaster.FontStyle,
                                      gradeMaster.FontColor,
                                      gradeMaster.GradeNo


                                  }).Distinct().ToList();
            if (remedyEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }


            remedyEntities.ForEach(item =>
            {
                remedyModel.Add(new RemedyModel
                {
                    RemedyId = Convert.ToInt32(item.RemedyId),
                    RemedyName = item.RemedyName,
                    RemedyAlias = item.RemedyAlias,
                    FontName=item.FontName,
                    FontStyle=item.FontStyle,
                    FontColor = item.FontColor,
                    GradeNo= item.GradeNo,

                });

            });

            return remedyModel;
        }

        // Added by Vikas More

        public RemedyCommonUncommonModel GetCommonUnCommonRemedyBySection(long subSectionId, ref ErrorResponseModel errorResponseModel)
        {

            RemedyCommonUncommonModel remedyCommonUncommon = new RemedyCommonUncommonModel();
            errorResponseModel = new ErrorResponseModel();
            var remedyEntities = (from remedyDetails in context.RubricRemedyDetails
                                  join remedyMaster in context.RemedyMaster
                                  on remedyDetails.RemedyId equals remedyMaster.RemedyId
                                  join gradeMaster in context.RemedyGradeMaster
                                  on remedyDetails.GradeId equals gradeMaster.GradeId
                                  where remedyDetails.SubSectionId == subSectionId
                                  select new RemediesModel
                                  {
                                      RemedyId = Convert.ToInt32(remedyMaster.RemedyId),
                                      RemedyName = remedyMaster.RemedyName,
                                      RemedyAlias = remedyMaster.RemedyAlias,
                                      FontName = gradeMaster.FontName,
                                      FontStyle = gradeMaster.FontStyle,
                                      FontColor = gradeMaster.FontColor,
                                      GradeNo = gradeMaster.GradeNo,
                                      ThermalId=remedyMaster.ThermalId,
                                      CommonOrUncommon=remedyMaster.CommonOrUncommon,

                                  }).Distinct().ToList();
            if (remedyEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }
            //Get Common & Uncommon remedies list
            remedyCommonUncommon.CommonRemedies=remedyEntities.Where(x=>x.CommonOrUncommon==false).ToList();
            remedyCommonUncommon.UnCommonRemedies=remedyEntities.Where(x=>x.CommonOrUncommon==true).ToList();

            return remedyCommonUncommon;
        }


    }
}
