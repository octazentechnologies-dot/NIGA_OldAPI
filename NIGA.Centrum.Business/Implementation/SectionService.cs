using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
    /// <summary>
    /// This is implementation  for the section operations 
    /// </summary>
    public class SectionService : ISectionService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public SectionService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }



        /// <summary>
        /// Methood to get section by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public SectionModel GetSectionById(long sectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var sectionEntity = context.SectionMaster.FirstOrDefault(x => x.SectionId == sectionId && !x.DeleteStatus);
            if (sectionEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
            }
            return new SectionModel
            {
                SectionId = sectionEntity.SectionId,
                SectionName = sectionEntity.SectionName,
                SectionAlias = sectionEntity.SectionAlias,
                Description = sectionEntity.Description,
                EnteredDate = sectionEntity.EnteredDate,
                EnteredBy = sectionEntity.EnteredBy,
                ChangedBy = sectionEntity.ChangedBy,
                ChangedDate = sectionEntity.ChangedDate,
                DeleteStatus = sectionEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// method implementation to get all sections and subsections.
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        //public List<SectionModel> getAllSections( ref ErrorResponseModel errorResponseModel)
        //{
        //    {
        //        errorResponseModel = new ErrorResponseModel(); 
        //        var sectionModelList = new List<SectionModel>();
        //        var listSectionModelEntity = context.SectionMaster.Include(x => x.SubSectionMaster).Where(x => x.DeleteStatus == false ).ToList();
        //       // var listSectionModelEntity = context.SectionMaster.Where(x => x.DeleteStatus == false).ToList();
        //        if (listSectionModelEntity == null)
        //        { errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //            errorResponseModel.Message = "Section not found"; 
        //        }
        //        foreach (var item in listSectionModelEntity)
        //        {
        //            SectionModel sectionModel = new SectionModel();
        //            sectionModel.SectionId = item.SectionId;
        //            sectionModel.SectionName = item.SectionName;

        //            foreach (var subSectionItem in item.SubSectionMaster)
        //            {
        //                if (subSectionItem.DeleteStatus == false)
        //                {
        //                    SubSectionModel subSectionModel = new SubSectionModel();
        //                    subSectionModel.SubSectionId = subSectionItem.SubSectionId;
        //                    subSectionModel.ParentSubSectionId = subSectionItem.ParentSubSectionId;
        //                    subSectionModel.SubSectionName = subSectionItem.SubSectionName;
        //                    sectionModel.listSubSectionModel.Add(subSectionModel);
        //                }
        //            }
        //            sectionModelList.Add(sectionModel);

        //        }
        //        return sectionModelList;
        //    }


        //}

        public List<SectionModel> getAllSections(ref ErrorResponseModel errorResponseModel)
        {
            try
            {
                errorResponseModel = new ErrorResponseModel();
                var sectionModelList = new List<SectionModel>();

                var listSectionModelEntity = context.SectionMaster
                    .Include(x => x.SubSectionMaster)
                    .Where(x => x.DeleteStatus == false)
                    .ToList();

                if (listSectionModelEntity == null || !listSectionModelEntity.Any())
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Section not found";
                    return sectionModelList; // return empty list if not found
                }

                foreach (var item in listSectionModelEntity)
                {
                    SectionModel sectionModel = new SectionModel
                    {
                        SectionId = item.SectionId,
                        SectionName = item.SectionName,
                        listSubSectionModel = new List<SubSectionModel>() // ensure initialized
                    };

                    foreach (var subSectionItem in item.SubSectionMaster)
                    {
                        if (subSectionItem.DeleteStatus == false)
                        {
                            SubSectionModel subSectionModel = new SubSectionModel
                            {
                                SubSectionId = subSectionItem.SubSectionId,
                                ParentSubSectionId = subSectionItem.ParentSubSectionId,
                                SubSectionName = subSectionItem.SubSectionName
                            };
                            sectionModel.listSubSectionModel.Add(subSectionModel);
                        }
                    }

                    sectionModelList.Add(sectionModel);
                }

                return sectionModelList;
            }
            catch (Exception ex)
            {
                errorResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                errorResponseModel.Message = "An error occurred while fetching sections.";
                //errorResponseModel.Details = ex.Message; // you can log ex.StackTrace if needed
                return new List<SectionModel>(); // return empty list on error
            }
        }

        /// <summary>
        /// Method implementation for saving new Section
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveSection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (sectionModel.SectionId == 0)
            {
                SectionMaster sectionEntity = new SectionMaster();
                sectionEntity.SectionName = sectionModel.SectionName;
                sectionEntity.SectionAlias = sectionModel.SectionAlias;
                sectionEntity.Description = sectionModel.Description;
                sectionEntity.EnteredBy = sectionModel.EnteredBy;
                sectionEntity.EnteredDate = DateTime.Now;
                context.SectionMaster.Add(sectionEntity);
                context.SaveChanges();
                Message = "Section Saved Successfully";
            }
            else
            {
                var sectionEntity = context.SectionMaster.FirstOrDefault(x => x.SectionId == sectionModel.SectionId);
                if (sectionEntity != null)
                {

                    sectionEntity.SectionName = sectionModel.SectionName;
                    sectionEntity.SectionAlias = sectionModel.SectionAlias;
                    sectionEntity.Description = sectionModel.Description;
                    sectionEntity.ChangedBy = sectionModel.EnteredBy;
                    sectionEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Section Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Section.
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteSection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var sectionEntity = context.SectionMaster.FirstOrDefault(x => x.SectionId == sectionModel.SectionId);
            if (sectionEntity != null)
            {
                sectionEntity.DeleteStatus = sectionModel.DeleteStatus;
                sectionEntity.ChangedBy = sectionModel.EnteredBy;
                sectionEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Section Deleted Successfully";
            }
            return Message;
        }


        public List<SectionModel> getAllRemedyByFilter(string search, int SectionId, ref ErrorResponseModel errorResponseModel)
        {
                errorResponseModel = new ErrorResponseModel();
                var sectionModelList = new List<SectionModel>();
                //---if sectionId>0 filter by section
                var listSectionModelEntity = context.SectionMaster.Include(x => x.SubSectionMaster)
                    .Where(x => x.DeleteStatus == false && (SectionId > 0 ? x.SectionId == SectionId : true)).ToList();

            
                if (listSectionModelEntity == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Section not found";
                }
                foreach (var item in listSectionModelEntity)
                {
                    SectionModel sectionModel = new SectionModel();
                    sectionModel.SectionId = item.SectionId;
                    sectionModel.SectionName = item.SectionName;
                    if (!string.IsNullOrEmpty(search))
                    {
                        item.SubSectionMaster = item.SubSectionMaster.Where(s => (!string.IsNullOrEmpty(search) ? s.SubSectionName.ToLower().Contains(search.ToLower()) : true)).ToList();
                        foreach (var subSectionItem in item.SubSectionMaster)
                        {


                            SubSectionModel subSectionModel = new SubSectionModel();
                            subSectionModel.SubSectionId = subSectionItem.SubSectionId;
                            subSectionModel.ParentSubSectionId = subSectionItem.ParentSubSectionId;
                            subSectionModel.SubSectionName = subSectionItem.SubSectionName;
                            sectionModel.listSubSectionModel.Add(subSectionModel);
                        }

                    }
                    else
                    {
                        foreach (var subSectionItem in item.SubSectionMaster)
                        {
                            SubSectionModel subSectionModel = new SubSectionModel();
                            subSectionModel.SubSectionId = subSectionItem.SubSectionId;
                            subSectionModel.ParentSubSectionId = subSectionItem.ParentSubSectionId;
                            subSectionModel.SubSectionName = subSectionItem.SubSectionName;
                            sectionModel.listSubSectionModel.Add(subSectionModel);
                        }

                    }
                    sectionModelList.Add(sectionModel);

                }

                return sectionModelList;
            
        }

    }
}
