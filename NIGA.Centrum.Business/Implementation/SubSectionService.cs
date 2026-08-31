using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace NIGA.Centrum.Business.Implementation
{
  
    /// <summary>
    /// This is implementation  for the subsection operations 
    /// </summary>
    public class SubSectionService : ISubSectionService
    {
        NIGACentrumContext context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<SubSectionService> _logger;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public SubSectionService(NIGACentrumContext centrumContext, IMemoryCache cache, ILogger<SubSectionService> logger)
        {
            context = centrumContext;
            _cache = cache;
            _logger = logger;

        }

        /// <summary>
        /// Methood to get subsection by SubSectionId
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public SubSectionModel GetSubSectionById(long subsectionId, ref ErrorResponseModel errorResponseModel)
        {
            var listSubSectionModel = new SubSectionModel();
            errorResponseModel = new ErrorResponseModel();
            if (subsectionId == 0)
            {
                var listSubsectionEntity = context.SectionMaster.Where(x => x.DeleteStatus == false).ToList();
                if (listSubsectionEntity == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Section not found";
                }


                listSubSectionModel.SubSectionId = 0;
                listSubSectionModel.SubSectionName = listSubSectionModel.SectionName;
                listSubSectionModel.SectionId = listSubSectionModel.SectionId;
                listSubSectionModel.ParentSubSectionId = listSubSectionModel.ParentSubSectionId;
                listSubSectionModel.MainParentSubsection= listSubSectionModel.MainParentSubsection;

            }

            else
            {
                var listSubsectionEntity = context.SubSectionMaster.Where(x => x.DeleteStatus == false).Where((x => x.SubSectionId == subsectionId)).FirstOrDefault();
                List<ReferenceRubricDetailsModel> lstMatMedicaDetails = new List<ReferenceRubricDetailsModel>();
                var materiamedicaremediesEntity = (from sub in context.SubSectionMaster
                                                   join refrub in context.ReferenceRubricDetails
                                                   on sub.SubSectionId equals refrub.RefSubSectionId
                                                   join sect in context.SectionMaster
                                                  on sub.SectionId equals sect.SectionId
                                                   where refrub.SubSectionId == subsectionId && refrub.DeleteStatus == false
                                                   select new ReferenceRubricDetailsModel
                                                   {
                                                        ReferenceRubricId= (int)refrub.ReferenceRubricId,
                                                        SubSectionId=refrub.SubSectionId,
                                                        RefSubSectionId=refrub.RefSubSectionId,
                                                        RefSubSectionName=sub.SubSectionName,
                                                        SectionId=sub.SectionId,
                                                        SectionName=sect.SectionName,
                                                       EnteredBy= refrub.EnteredBy,
                                                       EnteredDate=refrub.EnteredDate,
                                                       ChangedBy = refrub.ChangedBy,
                                                       ChangedDate = refrub.ChangedDate
                                                   }).ToList();






                var subsectionlanguageEntity = (from sub in context.SubSectionMaster
                                                   join sublag in context.SubSectionLanguageDetails
                                                   on sub.SubSectionId equals sublag.SubSectionId
                                                    join lagmst in context.LanguageMaster
                                                   on sublag.LanguageId equals lagmst.LanguageId


                                                where sublag.SubSectionId == subsectionId && sublag.DeleteStatus == false
                                                   select new SubSectionLanguageDetailsModel
                                                   {
                                                      SubSectionId =sublag.SubSectionId,
                                                       SectionName=sub.SubSectionName,
                                                       LanguageId =sublag.LanguageId,
                                                       SubSectionDetails=sublag.SubSectionDetails,
                                                       LanguageName=lagmst.LanguageName,
                                                       SubSectionLanguageId=sublag.SubSectionLanguageId,
                                                       LanguageDescription =lagmst.Description

                                                   }).ToList();

                if (listSubsectionEntity == null)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Section not found";
                }
                else
                {
                    listSubSectionModel.SubSectionId = listSubsectionEntity.SubSectionId;
                    listSubSectionModel.Description = listSubsectionEntity.Description;
                    listSubSectionModel.SubSectionNameAlias = listSubsectionEntity.SubSectionNameAlias;
                    listSubSectionModel.SubSectionName = listSubsectionEntity.SubSectionName;
                    listSubSectionModel.SectionId = listSubsectionEntity.SectionId;
                    listSubSectionModel.ParentSubSectionId = listSubsectionEntity.ParentSubSectionId;
                    listSubSectionModel.ParentSubSectionName = context.SubSectionMaster.Where(x=>x.SubSectionId== listSubsectionEntity.ParentSubSectionId).Select(x=>x.SubSectionName).FirstOrDefault();
                    listSubSectionModel.Referencerubric = materiamedicaremediesEntity;
                    listSubSectionModel.SubSectionLanguageDetails = subsectionlanguageEntity;
                    listSubSectionModel.MainParentSubsection = listSubsectionEntity.MainParentSubsection;


                }
            }

            return listSubSectionModel;
        }

        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
       

        public List<SubSectionModel> GetSubSections(int sectionId, NigaParameters nigaParameters)
        {
            var subsectionModelList = new List<SubSectionModel>();
            var errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList = context.SubSectionMaster
                                            .Where(x => x.DeleteStatus == false
                                            && x.SectionId == sectionId).OrderBy(x => x.SubSectionName)
                                            .Skip((nigaParameters.PageNumber - 1) * nigaParameters.PageSize)
             .Take(nigaParameters.PageSize)
             .ToList();




            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            subsectionEntityList.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias,
                    MainParentSubsection=item.MainParentSubsection,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return subsectionModelList;
        }







        /// <summary>
        /// Method implementation for saving new SubSection
        /// </summary>
        /// <param name="subSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveSubSection(List<SubSectionModel> subSectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            foreach (var items in subSectionModel)
            {
                if(items.SubSectionId == 0)
                {
                    foreach (var item in subSectionModel)
                    {
                        SubSectionMaster subSectionEntity = new SubSectionMaster();
                        if (item.SubSectionId == 0)
                        {
                            subSectionEntity.SectionId = item.SectionId;
                            subSectionEntity.ParentSubSectionId = item.ParentSubSectionId;
                            subSectionEntity.SubSectionName = item.SubSectionName;
                            subSectionEntity.SubSectionNameAlias = item.SubSectionNameAlias;
                            subSectionEntity.Description = item.Description;
                            subSectionEntity.EnteredBy = item.EnteredBy;
                            subSectionEntity.EnteredDate = DateTime.Now;
                            subSectionEntity.DeleteStatus = false;
                            subSectionEntity.MainParentSubsection = item.MainParentSubsection;
                            context.SubSectionMaster.Add(subSectionEntity);
                            context.SaveChanges();
                        }

                        foreach (var item1 in item.Referencerubric)
                        {
                            var modeldetails = new ReferenceRubricDetails();
                            modeldetails.SubSectionId = subSectionEntity.SubSectionId;
                            modeldetails.RefSubSectionId = item1.RefSubSectionId;
                            modeldetails.EnteredBy = item1.EnteredBy;
                            modeldetails.EnteredDate = DateTime.Now;
                            modeldetails.ChangedBy = item1.ChangedBy;
                            modeldetails.ChangedDate = item1.ChangedDate;
                            modeldetails.DeleteStatus = false;
                            context.ReferenceRubricDetails.Add(modeldetails);
                            context.SaveChanges();

                        }



                        foreach (var item1 in item.SubSectionLanguageDetails)
                        {
                            var languagemodeldetails = new SubSectionLanguageDetails();
                            languagemodeldetails.SubSectionId = subSectionEntity.SubSectionId;
                            languagemodeldetails.LanguageId = item1.LanguageId;
                            languagemodeldetails.SubSectionDetails = item1.SubSectionDetails;
                            languagemodeldetails.DeleteStatus=false;
                            context.SubSectionLanguageDetails.Add(languagemodeldetails);
                            context.SaveChanges();

                        }

                        Message = "Rubric Remedy Details Saved Successfully";
                    }

                }
                else
                {
                    foreach (var item in subSectionModel)
                    {
                        SubSectionMaster subSectionEntity = new SubSectionMaster();
                        if (item.SubSectionId > 0)
                        {
                            var subSectionUpdateEntity = context.SubSectionMaster.FirstOrDefault(x => x.SubSectionId == item.SubSectionId);
                                if (subSectionUpdateEntity != null) 
                                {
                                    subSectionUpdateEntity.SectionId = item.SectionId;
                                    subSectionUpdateEntity.ParentSubSectionId = item.ParentSubSectionId;
                                    subSectionUpdateEntity.SubSectionName = item.SubSectionName;
                                    subSectionUpdateEntity.SubSectionNameAlias = item.SubSectionNameAlias;
                                    subSectionUpdateEntity.Description = item.Description;
                                    subSectionUpdateEntity.EnteredBy = item.EnteredBy;
                                    subSectionUpdateEntity.ChangedBy = item.ChangedBy;
                                    subSectionUpdateEntity.ChangedDate = DateTime.Now;
                                    subSectionUpdateEntity.EnteredDate = DateTime.Now;
                                    subSectionEntity.MainParentSubsection = item.MainParentSubsection;
                                context.SaveChanges();
                                }
                        }


                        foreach (var item1 in item.Referencerubric)
                        {
                            var referencerubricUpdateEntity = context.ReferenceRubricDetails.FirstOrDefault(x => x.ReferenceRubricId == item1.ReferenceRubricId && x.DeleteStatus==false);
                            if (referencerubricUpdateEntity != null)
                            {
                                referencerubricUpdateEntity.SubSectionId = item1.SubSectionId;
                                referencerubricUpdateEntity.RefSubSectionId = item1.RefSubSectionId;
                                referencerubricUpdateEntity.EnteredDate = item1.EnteredDate;
                                referencerubricUpdateEntity.EnteredBy = Convert.ToInt32(item.EnteredBy);
                                referencerubricUpdateEntity.ChangedBy = Convert.ToInt32(item.ChangedBy);
                                referencerubricUpdateEntity.ChangedDate = DateTime.Now;
                                referencerubricUpdateEntity.EnteredDate = item1.EnteredDate;
                                referencerubricUpdateEntity.DeleteStatus = false;
                                context.SaveChanges();
                            }
                            else
                            {
                                var modeldetails = new ReferenceRubricDetails();
                                modeldetails.SubSectionId = item.SubSectionId;
                                modeldetails.RefSubSectionId = item1.RefSubSectionId;
                                modeldetails.EnteredDate = DateTime.Now;
                                modeldetails.EnteredBy = Convert.ToInt32(item.EnteredBy);
                                modeldetails.ChangedBy = Convert.ToInt32(item.ChangedBy);
                                modeldetails.DeleteStatus = false;
                                context.ReferenceRubricDetails.Add(modeldetails);
                                context.SaveChanges();

                            }
                        }



                        foreach (var item1 in item.SubSectionLanguageDetails)
                        {
                            var subSectionLanguageDetailsEntity = context.SubSectionLanguageDetails.FirstOrDefault(x => x.SubSectionLanguageId == item1.SubSectionLanguageId && x.DeleteStatus == false);
                            if (subSectionLanguageDetailsEntity != null)
                            {
                                subSectionLanguageDetailsEntity.SubSectionId = item1.SubSectionId;
                                subSectionLanguageDetailsEntity.LanguageId = item1.LanguageId;
                                subSectionLanguageDetailsEntity.SubSectionDetails = item1.SubSectionDetails;
                                subSectionLanguageDetailsEntity.DeleteStatus = false;

                                context.SaveChanges();
                            }
                            else
                            {
                                var languagemodeldetails = new SubSectionLanguageDetails();
                                languagemodeldetails.SubSectionId = item.SubSectionId;
                                languagemodeldetails.LanguageId = item1.LanguageId;
                                languagemodeldetails.SubSectionDetails = item1.SubSectionDetails;
                                languagemodeldetails.DeleteStatus = false;
                                context.SubSectionLanguageDetails.Add(languagemodeldetails);
                                context.SaveChanges();

                            }
                        }

                        Message = "Rubric Remedy Details Update Successfully";
                    }
                }
            }

            


            //if (subSectionModel.SubSectionId == 0)
            //{
            //    SubSectionMaster subSectionEntity = new SubSectionMaster();
            //    subSectionEntity.SectionId = subSectionModel.SectionId;
            //    subSectionEntity.ParentSubSectionId = subSectionModel.ParentSubSectionId;
            //    subSectionEntity.SubSectionName = subSectionModel.SubSectionName;
            //    subSectionEntity.SubSectionNameAlias = subSectionModel.SubSectionNameAlias;
            //    subSectionEntity.Description = subSectionModel.Description;
            //    subSectionEntity.EnteredBy = subSectionModel.EnteredBy;
            //    subSectionEntity.EnteredDate = DateTime.Now;
            //    context.SubSectionMaster.Add(subSectionEntity);
            //    context.SaveChanges();
            //    Message = "SubSection Saved Successfully";
            //}
            //else
            //{
            //    var subSectionEntity = context.SubSectionMaster.FirstOrDefault(x => x.SubSectionId == subSectionModel.SubSectionId);
            //    if (subSectionEntity != null)
            //    {

            //        subSectionEntity.SectionId = subSectionModel.SectionId;
            //        subSectionEntity.ParentSubSectionId = subSectionModel.ParentSubSectionId;
            //        subSectionEntity.SubSectionName = subSectionModel.SubSectionName;
            //        subSectionEntity.SubSectionNameAlias = subSectionModel.SubSectionNameAlias;
            //        subSectionEntity.Description = subSectionModel.Description;
            //        subSectionEntity.ChangedBy = subSectionModel.EnteredBy;
            //        subSectionEntity.ChangedDate = DateTime.Now;
            //        context.SaveChanges();
            //        Message = "SubSection Updated Successfully";
            //    }
            //}
            return Message;
        }


        /// <summary>
        /// Method is used for delete SubSection.
        /// </summary>
        /// <param name="subSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteSubSection(SubSectionModel subSectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var subSectionEntity = context.SubSectionMaster.FirstOrDefault(x => x.SubSectionId == subSectionModel.SubSectionId);
            if (subSectionEntity != null)
            {
                subSectionEntity.DeleteStatus = subSectionModel.DeleteStatus;
                subSectionEntity.ChangedBy = subSectionModel.EnteredBy;
                subSectionEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "SubSection Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Method implementation is used to get subsection as sections from section id
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SectionModel> GetSubSectionsBySection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel)
        {
            var sectionEntity = context.SubSectionMaster.Where(x => x.SectionId == sectionModel.SectionId
                                                              && x.ParentSubSectionId == sectionModel.ParentSubSectionID).ToList();
            var sectionModelList = new List<SectionModel>();
            if (sectionEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }
            else
            {
                sectionEntity.ForEach(item =>
                {
                    sectionModelList.Add(new SectionModel
                    {
                        SectionId = item.SectionId,
                        SectionName = item.SubSectionName,
                        SectionAlias = item.SubSectionNameAlias,
                        ParentSubSectionID = item.SubSectionId,
                        
                    });
                });
            }
            return sectionModelList;
        }

        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionModel> GetSubSections(ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList = context.SubSectionMaster
                                            .Where(x => x.DeleteStatus == false).ToList();
                                            
            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            subsectionEntityList.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias,
                    MainParentSubsection = item.MainParentSubsection,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return subsectionModelList;
        }




        //public List<SubSection> GetSubSectionsByDate(int userId, ref ErrorResponseModel errorResponseModel)
        //{
        //    var subsectionModelList = new List<SubSection>();
        //    errorResponseModel = new ErrorResponseModel();
        //    // var subsectionEntityList = context.SubSectionMaster
        //    // .Where(x => x.DeleteStatus == false
        //    //&& x.SectionId == sectionId ).ToList();

        //    var subsectionEntityList1 = (from rubric in context.RubricRemedyDetails
        //                                 join
        //                                 subsec in context.SubSectionMaster on rubric.SubSectionId equals subsec.SubSectionId
        //                                 join remedy in context.RemedyMaster on rubric.RemedyId equals remedy.RemedyId
        //                                 where rubric.EnteredBy == userId & Convert.ToDateTime(rubric.EnteredDate).Date == DateTime.Now.Date


        //                                 select new
        //                                 {
        //                                     //remedy.RemedyId,
        //                                     //rubric.GradeId,
        //                                     //subsec.SectionId,
        //                                     subsec.SubSectionId,
        //                                     subsec.SubSectionName,
        //                                     //remedy.RemedyName,
        //                                     //rubric.EnteredBy,
        //                                     //rubric.EnteredDate,
        //                                 }
        //                               ).GroupBy(x => new { x.SubSectionName }).Select(g => g.First()).ToList();

        //    if (subsectionEntityList1.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "SubSection not found";
        //    }

        //    subsectionEntityList1.ForEach(item =>
        //    {
        //        subsectionModelList.Add(new SubSection
        //        {
        //            //RemedyId = item.RemedyId,
        //            //GradeId = item.GradeId,
        //            SubSectionId = item.SubSectionId,
        //            SubSectionName = item.SubSectionName,
        //            //EnteredDate = item.EnteredDate,
        //            //EnteredBy = item.EnteredBy,
        //            //RemedyName = item.RemedyName,
        //            //SectionId = item.SectionId,
        //        });
        //    });
        //    return subsectionModelList;
        //}


        //public List<SubSection> GetSubSectionsByDate(int userId, ref ErrorResponseModel errorResponseModel)
        //{
        //    var subsectionModelList = new List<SubSection>();
        //    errorResponseModel = new ErrorResponseModel();
        //    int overallRemedyCount = 0; // Variable to store overall remedy count

        //    var subsectionEntityList1 = (
        //        from rubric in context.RubricRemedyDetails
        //        join subsec in context.SubSectionMaster on rubric.SubSectionId equals subsec.SubSectionId
        //        join remedy in context.RemedyMaster on rubric.RemedyId equals remedy.RemedyId
        //        where rubric.EnteredBy == userId && rubric.DeletedStatus == false &&  Convert.ToDateTime(rubric.EnteredDate).Date == DateTime.Now.Date

        //        group new { subsec, remedy } by subsec into g
        //        select new
        //        {
        //            SubSectionId = g.Key.SubSectionId,
        //            SubSectionName = g.Key.SubSectionName,
        //            RemedyCount = g.Count() // Count of entered remedies for this subsection
        //        }
        //    ).ToList();

        //    // Calculate overall remedy count
        //    overallRemedyCount = subsectionEntityList1.Sum(item => item.RemedyCount);

        //    if (subsectionEntityList1.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "SubSection not found";
        //    }

        //    subsectionEntityList1.ForEach(item =>
        //    {
        //        subsectionModelList.Add(new SubSection
        //        {
        //            SubSectionId = item.SubSectionId,
        //            SubSectionName = item.SubSectionName,
        //            RemedyCount = item.RemedyCount // Assigning the count of remedies for this subsection
        //        });
        //    });

        //    // Now you have the overall remedy count which you can use as needed
        //    // e.g., errorResponseModel.OverallRemedyCount = overallRemedyCount;

        //    return subsectionModelList;
        //}




        public List<SubSection> GetSubSectionsByDate(int userId, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSection>();
            errorResponseModel = new ErrorResponseModel();

            var subsectionEntityList = (
                from rubric in context.RubricRemedyDetails
                join subsec in context.SubSectionMaster on rubric.SubSectionId equals subsec.SubSectionId
                where rubric.EnteredBy == userId &&
                      rubric.DeletedStatus == false &&
                      rubric.EnteredDate.HasValue && // Check if EnteredDate has value
                      rubric.EnteredDate.Value.Date == DateTime.Now.Date // Access the Value and compare Date
                group subsec by new { subsec.SubSectionId, subsec.SubSectionName } into g
                select new SubSection
                {
                    SubSectionId = g.Key.SubSectionId,
                    SubSectionName = g.Key.SubSectionName,
                    RemedyCount = g.Count() // Count of entered remedies for this subsection
                }
            ).ToList();

            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            return subsectionEntityList;
        }














        public string DeleteSubSectionLanguageDetails(SubSectionLanguageDetailsModel subSectionLanguageDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var SubSectionLanguageDetailsEntity = context.SubSectionLanguageDetails.FirstOrDefault(x => x.SubSectionLanguageId == subSectionLanguageDetailsModel.SubSectionLanguageId);
            if (SubSectionLanguageDetailsEntity != null)
            {
                SubSectionLanguageDetailsEntity.DeleteStatus = true;
                // context.Remove(authorEntity);
                context.SaveChanges();
                Message = "Language Details Deleted Successfully";

            }
            return Message;
        }

        public string DeleteReferenceRubricDetails(ReferenceRubricDetailsModel referenceRubricDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var ReferenceRubricDetailsEntity = context.ReferenceRubricDetails.FirstOrDefault(x => x.ReferenceRubricId == referenceRubricDetailsModel.ReferenceRubricId);
            if (ReferenceRubricDetailsEntity != null)
            {
                ReferenceRubricDetailsEntity.DeleteStatus = true;
                // context.Remove(authorEntity);
                context.SaveChanges();
                Message = "Reference Rubric Details Deleted Successfully";

            }
            return Message;
        }

        public PaginationResult GetSubSectionsWithPagination(int sectionId, NigaParameters nigaParameters)
        {
            var subsectionModelList = new List<SubSectionForPageModel>();
            var errorResponseModel = new ErrorResponseModel();

            var pageNumber = (nigaParameters.PageNumber <= 0) ? 1 : nigaParameters.PageNumber;
            var pageSize = nigaParameters.PageSize;
            var totalRecords = 0.0;
            var totalPages = 0.0;
            var skip = 0;

            subsectionModelList = (from subSection in context.SubSectionMaster
                                   where subSection.SectionId == sectionId && subSection.DeleteStatus == false
                                   orderby subSection.SubSectionName
                                   select new SubSectionForPageModel
                                   {
                                       SubSectionId = subSection.SubSectionId,
                                       SubSectionName = subSection.SubSectionName,
                                   }).ToList();

            if (subsectionModelList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Sub section not found";
            }
            totalRecords = subsectionModelList.Count;
            totalPages = Math.Ceiling((double)totalRecords / pageSize);
            skip = (pageNumber - 1) * pageSize;


            var result = new PaginationResult();
            result.TotalPageCount = totalPages;
            result.TotalCount = totalRecords;
            result.ResultObject = subsectionModelList.Skip(skip).Take(pageSize);
            return result;

        }


        //public List<SubSectionLevelModel> GetSubSectionWithChildrenCount(long subsectionId, ref ErrorResponseModel errorResponseModel)
        //{
        //    errorResponseModel = new ErrorResponseModel();

        //    var result = context.SubSectionMaster
        //        .Where(s =>
        //            (s.SubSectionId == subsectionId ||
        //             s.ParentSubSectionId == subsectionId) &&
        //            s.DeleteStatus == false)
        //        .Select(s => new SubSectionLevelModel
        //        {
        //            SubSectionId = s.SubSectionId,
        //            SubSectionName = s.SubSectionName,
        //            ChildCount = context.SubSectionMaster.Count(c =>
        //                c.ParentSubSectionId == s.SubSectionId &&
        //                c.DeleteStatus == false)
        //        })
        //        .OrderBy(x => x.SubSectionId == subsectionId ? 0 : 1)
        //        .ThenBy(x => x.SubSectionName)
        //        .ToList();

        //    if (result == null || result.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "Subsection not found";
        //    }

        //    return result;
        //}

        //    public List<SubSectionLevelModel> GetSubSectionWithChildrenCount(
        //long subsectionId,
        //ref ErrorResponseModel errorResponseModel)
        //    {
        //        errorResponseModel = new ErrorResponseModel();

        //        var result =
        //            (from s in context.SubSectionMaster
        //             where (s.SubSectionId == subsectionId ||
        //                    s.ParentSubSectionId == subsectionId)
        //                   && s.DeleteStatus == false

        //             join c in context.SubSectionMaster
        //                 .Where(x => x.DeleteStatus == false)
        //                 on s.SubSectionId equals c.ParentSubSectionId into childGroup

        //             select new SubSectionLevelModel
        //             {
        //                 SubSectionId = s.SubSectionId,
        //                 SubSectionName = s.SubSectionName,
        //                 ChildCount = childGroup.Count()
        //             })
        //            .OrderBy(x => x.SubSectionId == subsectionId ? 0 : 1)
        //            .ThenBy(x => x.SubSectionName)
        //            .ToList();

        //        if (!result.Any())
        //        {
        //            errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //            errorResponseModel.Message = "Subsection not found";
        //        }

        //        return result;
        //    }

        //its used for if below is not work
        //public List<SubSectionLevelModel> GetSubSectionWithChildrenCount( long subsectionId, ref ErrorResponseModel errorResponseModel)
        //{
        //    errorResponseModel = new ErrorResponseModel();

        //    // 1️⃣ Pre-aggregate child counts
        //    var childCounts =
        //        context.SubSectionMaster
        //            .AsNoTracking()
        //            .Where(c => c.DeleteStatus == false && c.ParentSubSectionId != null)
        //            .GroupBy(c => c.ParentSubSectionId)
        //            .Select(g => new
        //            {
        //                ParentSubSectionId = g.Key,
        //                Count = g.Count()
        //            });

        //    // 2️⃣ Get target + its children (index-friendly)
        //    var targetAndChildren =
        //        context.SubSectionMaster
        //            .AsNoTracking()
        //            .Where(s => s.DeleteStatus == false &&
        //                       (s.SubSectionId == subsectionId ||
        //                        s.ParentSubSectionId == subsectionId));

        //    // 3️⃣ Join with aggregated counts
        //    var result =
        //        (from s in targetAndChildren
        //         join cc in childCounts
        //             on s.SubSectionId equals cc.ParentSubSectionId into ccg
        //         from cc in ccg.DefaultIfEmpty()

        //         select new SubSectionLevelModel
        //         {
        //             SubSectionId = s.SubSectionId,
        //             SubSectionName = s.SubSectionName,
        //             ChildCount = cc == null ? 0 : cc.Count
        //         })
        //        .OrderBy(x => x.SubSectionId == subsectionId ? 0 : 1)
        //        .ThenBy(x => x.SubSectionName)
        //        .ToList();

        //    if (!result.Any())
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "Subsection not found";
        //    }

        //    return result;
        //}

        public List<SubSectionLevelModel> GetSubSectionWithChildrenCount(
    long subsectionId,
    ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            // 1️⃣ Pre-aggregate child counts
            var childCounts =
                context.SubSectionMaster
                    .AsNoTracking()
                    .Where(c => c.DeleteStatus == false && c.ParentSubSectionId != null)
                    .GroupBy(c => c.ParentSubSectionId)
                    .Select(g => new
                    {
                        ParentSubSectionId = g.Key,
                        Count = g.Count()
                    });

            // 2️⃣ ONLY children (exclude main subsection)
            var children =
                context.SubSectionMaster
                    .AsNoTracking()
                    .Where(s =>
                        s.DeleteStatus == false &&
                        s.ParentSubSectionId == subsectionId);

            // 3️⃣ Join with child counts
            var result =
                (from s in children
                 join cc in childCounts
                     on s.SubSectionId equals cc.ParentSubSectionId into ccg
                 from cc in ccg.DefaultIfEmpty()
                 select new SubSectionLevelModel
                 {
                     SubSectionId = s.SubSectionId,
                     SubSectionName = s.SubSectionName,
                     ChildCount = cc == null ? 0 : cc.Count
                 })
                .OrderBy(x => x.SubSectionName)
                .ToList();

            if (!result.Any())
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "No child subsections found";
            }

            return result;
        }



        public List<SubSectionLevelModel> GetMainParentSubSectionsWithChildCount(
    long sectionId,
    ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            // 1️⃣ Pre-aggregate children
            var childCounts =
                context.SubSectionMaster
                    .AsNoTracking()
                    .Where(c => c.DeleteStatus == false && c.ParentSubSectionId != null)
                    .GroupBy(c => c.ParentSubSectionId)
                    .Select(g => new
                    {
                        ParentSubSectionId = g.Key,
                        Count = g.Count()
                    });

            // 2️⃣ Join aggregated result with parents
            var result =
                (from s in context.SubSectionMaster.AsNoTracking()
                 join cc in childCounts
                     on s.SubSectionId equals cc.ParentSubSectionId into ccg
                 from cc in ccg.DefaultIfEmpty()

                 where s.SectionId == sectionId
                       && s.DeleteStatus == false
                       && s.MainParentSubsection == true

                 select new SubSectionLevelModel
                 {
                     SubSectionId = s.SubSectionId,
                     SubSectionName = s.SubSectionName,
                     ChildCount = cc == null ? 0 : cc.Count
                 })
                .OrderBy(x => x.SubSectionName)
                .ToList();

            if (!result.Any())
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "No main parent subsections found";
            }

            return result;
        }

        /// <summary>
        /// Method implementation to update MainParentSubsection against subsectionId
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <param name="mainParentSubsection"></param>
        /// <param name="changedBy"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string UpdateMainParentSubsection(long subsectionId, bool mainParentSubsection, string changedBy, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            errorResponseModel = new ErrorResponseModel();

            if (subsectionId == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.BadRequest;
                errorResponseModel.Message = "SubSectionId is required";
                return Message;
            }

            var subSectionEntity = context.SubSectionMaster.FirstOrDefault(x => x.SubSectionId == subsectionId && x.DeleteStatus == false);
            
            if (subSectionEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
                return Message;
            }

            subSectionEntity.MainParentSubsection = mainParentSubsection;
            subSectionEntity.ChangedBy = changedBy;
            subSectionEntity.ChangedDate = DateTime.Now;
            context.SaveChanges();
            
            Message = "MainParentSubsection updated successfully";
            return Message;
        }

        public async Task<List<SubSectionSearchResponse>> SearchAsync(string query, int top)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return new List<SubSectionSearchResponse>();

                string cacheKey = $"subsection_search_{query}_{top}";

                if (_cache.TryGetValue(cacheKey, out List<SubSectionSearchResponse> cached))
                    return cached;

                var words = GetSearchWords(NormalizeSubSectionSearchText(query));

                if (!words.Any())
                    return new List<SubSectionSearchResponse>();

                var fullTextQuery = string.Join(" OR ",
                    words.Select(w => $"\"{w}*\""));

                var sql = @"
            SELECT TOP (@Top)
                s.SubSectionID AS Id,
                s.SubSectionName AS Name,
                ft.RANK
            FROM dbo.SubSectionMaster s
            INNER JOIN CONTAINSTABLE(dbo.SubSectionMaster, SearchNormalized, @SearchQuery) ft
                ON s.SubSectionID = ft.[KEY]
            WHERE s.DeleteStatus = 0
            ORDER BY ft.RANK DESC";

                var result = await context.Query<SubSectionSearchResponse>()
                    .FromSql(sql,
                        new SqlParameter("@Top", top),
                        new SqlParameter("@SearchQuery", fullTextQuery))
                    .ToListAsync();

                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "SearchAsync failed. Query: {Query}", query);
                return new List<SubSectionSearchResponse>();
            }
        }

        public async Task<List<SubSectionSearchResultModel>> SearchBySectionAsync(long sectionId, string query, int top)
        {
            if (sectionId <= 0)
                return new List<SubSectionSearchResultModel>();

            return await SearchSubSectionsInternalAsync(sectionId, query, top, "section");
        }

        public async Task<List<SubSectionSearchResultModel>> SearchGlobalAsync(string query, int top)
        {
            return await SearchSubSectionsInternalAsync(null, query, top, "global");
        }

        public async Task<SubSectionSearchPagedResultModel> SearchBySectionPagedAsync(long sectionId, string query, int pageNumber, int pageSize)
        {
            if (sectionId <= 0)
                return new SubSectionSearchPagedResultModel();

            return await SearchSubSectionsPagedInternalAsync(sectionId, query, pageNumber, pageSize, "section");
        }

        public async Task<SubSectionSearchPagedResultModel> SearchGlobalPagedAsync(string query, int pageNumber, int pageSize)
        {
            return await SearchSubSectionsPagedInternalAsync(null, query, pageNumber, pageSize, "global");
        }

        private async Task<SubSectionSearchPagedResultModel> SearchSubSectionsPagedInternalAsync(
            long? sectionId,
            string query,
            int pageNumber,
            int pageSize,
            string scope)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new SubSectionSearchPagedResultModel();

            query = query.Trim();
            if (query.Length < 2)
                return new SubSectionSearchPagedResultModel();

            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(10, Math.Min(pageSize, 100));
            var offset = (pageNumber - 1) * pageSize;

            var cacheKey = sectionId.HasValue
                ? $"subsection_search_paged_{scope}_{sectionId.Value}_{query.ToLowerInvariant()}_{pageNumber}_{pageSize}"
                : $"subsection_search_paged_{scope}_{query.ToLowerInvariant()}_{pageNumber}_{pageSize}";

            if (_cache.TryGetValue(cacheKey, out SubSectionSearchPagedResultModel cached))
                return cached;

            var words = GetSearchWords(NormalizeSubSectionSearchText(query));
            if (!words.Any())
                return new SubSectionSearchPagedResultModel();

            var previousTimeout = context.Database.GetCommandTimeout();
            context.Database.SetCommandTimeout(scope == "global" ? 45 : previousTimeout ?? 30);

            try
            {
                int totalCount;
                List<SubSectionSearchMatchRow> rankedMatches;

                var fullTextCount = await TryCountSubSectionsFullTextAsync(sectionId, words);
                if (fullTextCount > 0)
                {
                    totalCount = fullTextCount;
                    rankedMatches = await TrySearchSubSectionsFullTextPagedAsync(sectionId, words, offset, pageSize);
                }
                else
                {
                    totalCount = await CountSubSectionsWithEfLikeAsync(sectionId, words);
                    rankedMatches = await SearchSubSectionsWithEfLikePagedAsync(sectionId, words, offset, pageSize);
                }

                // Full-text count can succeed while OFFSET/FETCH page query returns nothing (EF FromSql param issue).
                if (!rankedMatches.Any())
                {
                    if (totalCount <= 0)
                    {
                        totalCount = await CountSubSectionsWithEfLikeAsync(sectionId, words);
                    }

                    rankedMatches = await SearchSubSectionsWithEfLikePagedAsync(sectionId, words, offset, pageSize);
                }

                var items = await BuildSearchResultsWithAncestorsAsync(rankedMatches);
                var result = new SubSectionSearchPagedResultModel
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    HasMore = pageNumber * pageSize < totalCount
                };

                if (result.Items.Any())
                {
                    _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(
                    ex,
                    "Paged subsection search failed. Scope={Scope}, SectionId={SectionId}, Query={Query}, Page={Page}",
                    scope,
                    sectionId,
                    query,
                    pageNumber);
                return new SubSectionSearchPagedResultModel();
            }
            finally
            {
                context.Database.SetCommandTimeout(previousTimeout);
            }
        }

        private async Task<List<SubSectionSearchResultModel>> SearchSubSectionsInternalAsync(
            long? sectionId,
            string query,
            int top,
            string scope)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<SubSectionSearchResultModel>();

            query = query.Trim();
            if (query.Length < 2)
                return new List<SubSectionSearchResultModel>();

            top = Math.Max(5, Math.Min(top, scope == "global" ? 20 : 100));

            var cacheKey = sectionId.HasValue
                ? $"subsection_search_{scope}_{sectionId.Value}_{query.ToLowerInvariant()}_{top}"
                : $"subsection_search_{scope}_{query.ToLowerInvariant()}_{top}";

            if (_cache.TryGetValue(cacheKey, out List<SubSectionSearchResultModel> cached))
                return cached;

            var words = GetSearchWords(NormalizeSubSectionSearchText(query));
            if (!words.Any())
                return new List<SubSectionSearchResultModel>();

            var previousTimeout = context.Database.GetCommandTimeout();
            context.Database.SetCommandTimeout(scope == "global" ? 45 : previousTimeout ?? 30);

            try
            {
                var rankedMatches = await TrySearchSubSectionsFullTextAsync(sectionId, words, top);
                if (!rankedMatches.Any())
                {
                    rankedMatches = await SearchSubSectionsWithEfLikeAsync(sectionId, words, top);
                }

                var result = await BuildSearchResultsWithAncestorsAsync(rankedMatches);
                if (result.Any())
                {
                    _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(
                    ex,
                    "Subsection search failed. Scope={Scope}, SectionId={SectionId}, Query={Query}",
                    scope,
                    sectionId,
                    query);
                return new List<SubSectionSearchResultModel>();
            }
            finally
            {
                context.Database.SetCommandTimeout(previousTimeout);
            }
        }

        private async Task<List<SubSectionSearchMatchRow>> TrySearchSubSectionsFullTextAsync(
            long? sectionId,
            string[] words,
            int top)
        {
            try
            {
                var fullTextQuery = string.Join(" OR ", words.Select(w => $"\"{w}*\""));
                var sectionFilter = sectionId.HasValue ? " AND s.SectionID = @SectionId" : string.Empty;

                var sql = $@"
            SELECT TOP ({top})
                s.SubSectionID AS SubSectionId,
                s.SubSectionName AS SubSectionName,
                s.ParentSubSectionID AS ParentSubSectionId,
                ft.RANK AS Rank
            FROM dbo.SubSectionMaster s
            INNER JOIN CONTAINSTABLE(dbo.SubSectionMaster, SearchNormalized, @SearchQuery) ft
                ON s.SubSectionID = ft.[KEY]
            WHERE s.DeleteStatus = 0{sectionFilter}
            ORDER BY ft.RANK DESC";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@SearchQuery", fullTextQuery)
                };

                if (sectionId.HasValue)
                {
                    parameters.Add(new SqlParameter("@SectionId", sectionId.Value));
                }

                return await context.Query<SubSectionSearchMatchRow>()
                    .FromSql(sql, parameters.ToArray())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(
                    ex,
                    "Full-text subsection search failed for SectionId={SectionId}. Falling back to LIKE search.",
                    sectionId);
                return new List<SubSectionSearchMatchRow>();
            }
        }

        private async Task<List<SubSectionSearchMatchRow>> SearchSubSectionsWithEfLikeAsync(
            long? sectionId,
            string[] words,
            int top)
        {
            return await SearchSubSectionsWithEfLikePagedAsync(sectionId, words, 0, top);
        }

        private async Task<int> TryCountSubSectionsFullTextAsync(long? sectionId, string[] words)
        {
            try
            {
                var fullTextQuery = string.Join(" OR ", words.Select(w => $"\"{w}*\""));
                var sectionFilter = sectionId.HasValue ? " AND s.SectionID = @SectionId" : string.Empty;

                var sql = $@"
            SELECT COUNT(*)
            FROM dbo.SubSectionMaster s
            INNER JOIN CONTAINSTABLE(dbo.SubSectionMaster, SearchNormalized, @SearchQuery) ft
                ON s.SubSectionID = ft.[KEY]
            WHERE s.DeleteStatus = 0{sectionFilter}";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@SearchQuery", fullTextQuery)
                };

                if (sectionId.HasValue)
                {
                    parameters.Add(new SqlParameter("@SectionId", sectionId.Value));
                }

                await context.Database.OpenConnectionAsync();
                try
                {
                    using (var command = context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = sql;
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        var scalar = await command.ExecuteScalarAsync();
                        return scalar == null || scalar == DBNull.Value ? 0 : Convert.ToInt32(scalar);
                    }
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(
                    ex,
                    "Full-text subsection count failed for SectionId={SectionId}. Falling back to LIKE search.",
                    sectionId);
                return 0;
            }
        }

        private async Task<List<SubSectionSearchMatchRow>> TrySearchSubSectionsFullTextPagedAsync(
            long? sectionId,
            string[] words,
            int offset,
            int pageSize)
        {
            try
            {
                var fullTextQuery = string.Join(" OR ", words.Select(w => $"\"{w}*\""));
                var sectionFilter = sectionId.HasValue ? " AND s.SectionID = @SectionId" : string.Empty;

                // OFFSET/FETCH values must be inlined – EF Core 2.x FromSql does not bind @Offset/@PageSize correctly.
                var sql = $@"
            SELECT
                s.SubSectionID AS SubSectionId,
                s.SubSectionName AS SubSectionName,
                s.ParentSubSectionID AS ParentSubSectionId,
                ft.RANK AS Rank
            FROM dbo.SubSectionMaster s
            INNER JOIN CONTAINSTABLE(dbo.SubSectionMaster, SearchNormalized, @SearchQuery) ft
                ON s.SubSectionID = ft.[KEY]
            WHERE s.DeleteStatus = 0{sectionFilter}
            ORDER BY ft.RANK DESC
            OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@SearchQuery", fullTextQuery)
                };

                if (sectionId.HasValue)
                {
                    parameters.Add(new SqlParameter("@SectionId", sectionId.Value));
                }

                return await context.Query<SubSectionSearchMatchRow>()
                    .FromSql(sql, parameters.ToArray())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(
                    ex,
                    "Paged full-text subsection search failed for SectionId={SectionId}. Falling back to LIKE search.",
                    sectionId);
                return new List<SubSectionSearchMatchRow>();
            }
        }

        private async Task<int> CountSubSectionsWithEfLikeAsync(long? sectionId, string[] words)
        {
            try
            {
                IQueryable<SubSectionMaster> query = context.SubSectionMaster
                    .AsNoTracking()
                    .Where(s => s.DeleteStatus == false);

                if (sectionId.HasValue)
                {
                    query = query.Where(s => s.SectionId == sectionId.Value);
                }

                foreach (var word in words)
                {
                    var pattern = "%" + word + "%";
                    query = query.Where(s =>
                        (s.SearchNormalized != null && EF.Functions.Like(s.SearchNormalized, pattern))
                        || (s.SubSectionName != null && EF.Functions.Like(s.SubSectionName, pattern)));
                }

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "EF LIKE subsection count failed for SectionId={SectionId}.", sectionId);
                return 0;
            }
        }

        private async Task<List<SubSectionSearchMatchRow>> SearchSubSectionsWithEfLikePagedAsync(
            long? sectionId,
            string[] words,
            int offset,
            int pageSize)
        {
            try
            {
                IQueryable<SubSectionMaster> query = context.SubSectionMaster
                    .AsNoTracking()
                    .Where(s => s.DeleteStatus == false);

                if (sectionId.HasValue)
                {
                    query = query.Where(s => s.SectionId == sectionId.Value);
                }

                foreach (var word in words)
                {
                    var pattern = "%" + word + "%";
                    query = query.Where(s =>
                        (s.SearchNormalized != null && EF.Functions.Like(s.SearchNormalized, pattern))
                        || (s.SubSectionName != null && EF.Functions.Like(s.SubSectionName, pattern)));
                }

                return await query
                    .OrderBy(s => s.SubSectionName)
                    .Skip(offset)
                    .Take(pageSize)
                    .Select(s => new SubSectionSearchMatchRow
                    {
                        SubSectionId = s.SubSectionId,
                        SubSectionName = s.SubSectionName,
                        ParentSubSectionId = s.ParentSubSectionId,
                        Rank = 0
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Paged EF LIKE subsection search failed for SectionId={SectionId}.", sectionId);
                return new List<SubSectionSearchMatchRow>();
            }
        }

        private async Task<Dictionary<int, SubSectionSearchMatchRow>> LoadSubSectionTreeNodesAsync(List<int> seedIds)
        {
            if (seedIds == null || seedIds.Count == 0)
                return new Dictionary<int, SubSectionSearchMatchRow>();

            var distinctIds = seedIds.Where(id => id > 0).Distinct().Take(50).ToList();
            if (!distinctIds.Any())
                return new Dictionary<int, SubSectionSearchMatchRow>();

            return await LoadSubSectionTreeNodesBatchedAsync(distinctIds, new Dictionary<int, SubSectionSearchMatchRow>());
        }

        private async Task<Dictionary<int, SubSectionSearchMatchRow>> LoadSubSectionTreeNodesBatchedAsync(
            List<int> seedIds,
            Dictionary<int, SubSectionSearchMatchRow> nodeLookup)
        {
            if (nodeLookup == null)
            {
                nodeLookup = new Dictionary<int, SubSectionSearchMatchRow>();
            }

            var visited = new HashSet<int>(nodeLookup.Keys);
            var pendingParentIds = seedIds
                .Where(id => id > 0)
                .Distinct()
                .Where(id => !visited.Contains(id))
                .ToList();

            var safetyCounter = 0;
            while (pendingParentIds.Count > 0 && safetyCounter < 50)
            {
                safetyCounter++;
                pendingParentIds = pendingParentIds
                    .Where(id => !visited.Contains(id))
                    .Distinct()
                    .ToList();

                if (!pendingParentIds.Any())
                    break;

                var batch = pendingParentIds.Take(200).ToList();
                pendingParentIds = pendingParentIds.Skip(batch.Count).ToList();

                var parents = await context.SubSectionMaster
                    .AsNoTracking()
                    .Where(s => batch.Contains(s.SubSectionId) && s.DeleteStatus == false)
                    .Select(s => new SubSectionSearchMatchRow
                    {
                        SubSectionId = s.SubSectionId,
                        SubSectionName = s.SubSectionName,
                        ParentSubSectionId = s.ParentSubSectionId,
                        Rank = 0
                    })
                    .ToListAsync();

                foreach (var parent in parents)
                {
                    var parentId = (int)parent.SubSectionId;
                    if (visited.Add(parentId))
                    {
                        nodeLookup[parentId] = parent;
                    }

                    if (parent.ParentSubSectionId.HasValue
                        && parent.ParentSubSectionId.Value > 0
                        && !visited.Contains(parent.ParentSubSectionId.Value))
                    {
                        pendingParentIds.Add(parent.ParentSubSectionId.Value);
                    }
                }
            }

            return nodeLookup;
        }

        private async Task<List<SubSectionSearchResultModel>> BuildSearchResultsWithAncestorsAsync(
            List<SubSectionSearchMatchRow> rankedMatches)
        {
            if (rankedMatches == null || rankedMatches.Count == 0)
                return new List<SubSectionSearchResultModel>();

            try
            {
                var seedIds = rankedMatches.Select(m => (int)m.SubSectionId).Distinct().ToList();
                var nodeLookup = await LoadSubSectionTreeNodesAsync(seedIds);

                foreach (var match in rankedMatches)
                {
                    nodeLookup[(int)match.SubSectionId] = match;
                }

                var allIds = nodeLookup.Keys.ToList();
                var childCountRows = await context.SubSectionMaster
                    .AsNoTracking()
                    .Where(c => c.DeleteStatus == false && c.ParentSubSectionId != null && allIds.Contains(c.ParentSubSectionId.Value))
                    .GroupBy(c => c.ParentSubSectionId)
                    .Select(g => new { ParentId = g.Key.Value, Count = g.Count() })
                    .ToListAsync();

                var childCounts = childCountRows.ToDictionary(x => x.ParentId, x => x.Count);

                var results = new List<SubSectionSearchResultModel>();

                foreach (var match in rankedMatches)
                {
                    var ancestors = new List<SubSectionLevelModel>();
                    var currentParentId = match.ParentSubSectionId;
                    var visitedAncestors = new HashSet<int>();

                    while (currentParentId.HasValue && currentParentId.Value > 0)
                    {
                        if (!visitedAncestors.Add(currentParentId.Value))
                            break;

                        if (!nodeLookup.TryGetValue(currentParentId.Value, out var parentNode))
                            break;

                        ancestors.Insert(0, new SubSectionLevelModel
                        {
                            SubSectionId = parentNode.SubSectionId,
                            SubSectionName = parentNode.SubSectionName,
                            ChildCount = childCounts.TryGetValue((int)parentNode.SubSectionId, out var parentChildCount)
                                ? parentChildCount
                                : 0
                        });

                        currentParentId = parentNode.ParentSubSectionId;
                    }

                    results.Add(new SubSectionSearchResultModel
                    {
                        SubSectionId = match.SubSectionId,
                        SubSectionName = match.SubSectionName,
                        ParentSubSectionId = match.ParentSubSectionId,
                        ChildCount = childCounts.TryGetValue((int)match.SubSectionId, out var matchChildCount)
                            ? matchChildCount
                            : 0,
                        Ancestors = ancestors
                    });
                }

                return results;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "Building subsection ancestor tree failed. Returning flat matches.");

                return rankedMatches.Select(match => new SubSectionSearchResultModel
                {
                    SubSectionId = match.SubSectionId,
                    SubSectionName = match.SubSectionName,
                    ParentSubSectionId = match.ParentSubSectionId,
                    ChildCount = 0,
                    Ancestors = new List<SubSectionLevelModel>()
                }).ToList();
            }
        }

        private static readonly HashSet<string> StopWords = new HashSet<string>
{
    "a","an","the","of","in","on","at","for","to","from","and","or","is","are",
    "with","by","as","be","was","were"
};

        private static string[] GetSearchWords(string normalizedQuery)
        {
            return normalizedQuery
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length >= 2 && !StopWords.Contains(w))
                .Distinct()
                .ToArray();
        }

        private static string NormalizeSubSectionSearchText(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var normalized = input.ToLowerInvariant();
            normalized = Regex.Replace(normalized, @"[-,\.:;]", " ");
            normalized = Regex.Replace(normalized, @"(\d)\s*pm\b", "$1 pm");
            normalized = Regex.Replace(normalized, @"(\d)\s*am\b", "$1 am");
            normalized = Regex.Replace(normalized, @"\s+", " ");
            return normalized.Trim();
        }

        private string NormalizeText(string input)
        {
            return NormalizeSubSectionSearchText(input);
        }
    }
}