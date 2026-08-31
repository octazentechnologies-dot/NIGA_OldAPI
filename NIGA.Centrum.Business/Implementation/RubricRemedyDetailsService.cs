using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using static System.Collections.Specialized.BitVector32;

/// <summary>
/// Created Date    :   10-March-2020
/// Purpose         :   Class for RubricRemedyDetails
/// </summary>
namespace NIGA.Centrum.Business.Implementation
{
    public class RubricRemedyDetailsService : IRubricRemedyDetailsService
    {
        private static readonly MemoryCacheEntryOptions RubricDetailsCacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        };

        NIGACentrumContext context;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initilize class constructior
        /// </summary>
        /// <param name="centrumContext"></param>
        public RubricRemedyDetailsService(NIGACentrumContext centrumContext, IMemoryCache cache)
        {
            context = centrumContext;
            _cache = cache;
        }

        /// <summary>
        /// Method implementation for saving rubric remedy details.
        /// </summary>
        /// <param name="rubricRemedyDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveRubricRemedyDetails(List<RubricRemedyDetailsModel> rubricRemedyDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";

            ////Delete existing ones//
            //var existingDetails = context.RubricRemedyDetails.Where(x => x.SubSectionId == rubricRemedyDetailsModel.SubSectionId
            //                                                    && x.GradeId == rubricRemedyDetailsModel.GradeId ).ToList();
            //context.RubricRemedyDetails.RemoveRange(existingDetails);
            //context.SaveChanges();

            //var remedyIds = rubricRemedyDetailsModel.RemedyIds.Split(',');
            //foreach (var item in remedyIds)
            //{
            //        var rubricRemedyDetailsEntity = new RubricRemedyDetails();
            //        rubricRemedyDetailsEntity.SubSectionId = rubricRemedyDetailsModel.SubSectionId;
            //         rubricRemedyDetailsEntity.RemedyId = Convert.ToInt32(item);
            //      //  rubricRemedyDetailsEntity.RemedyId = rubricRemedyDetailsModel.RemedyId;
            //        rubricRemedyDetailsEntity.GradeId = rubricRemedyDetailsModel.GradeId;
            //        rubricRemedyDetailsEntity.EnteredDate = rubricRemedyDetailsModel.EnteredDate;
            //        rubricRemedyDetailsEntity.EnteredBy = rubricRemedyDetailsModel.EnteredBy;
            //        context.RubricRemedyDetails.Add(rubricRemedyDetailsEntity);
            //        context.SaveChanges();

            //    }
            //            if(existingDetails.Count > 0)
            //                {
            //                    Message = "Rubric Remedy Details Updated Successfully";
            //                }
            //            else
            //                {
            //                    Message = "Rubric Remedy Details Saved Successfully";
            //                }



            foreach (var item in rubricRemedyDetailsModel)
            {
                var rubricRemedyDetailsEntity = new RubricRemedyDetails();

                if (item.RubricRemedyId == 0)
                {
                    rubricRemedyDetailsEntity.RubricRemedyId = item.RubricRemedyId;
                    rubricRemedyDetailsEntity.SubSectionId = item.SubSectionId;
                    rubricRemedyDetailsEntity.RemedyId = item.RemedyId;
                    rubricRemedyDetailsEntity.GradeId = item.GradeId;
                    rubricRemedyDetailsEntity.EnteredDate = item.EnteredDate;
                    rubricRemedyDetailsEntity.EnteredBy = item.EnteredBy;
                    rubricRemedyDetailsEntity.DeletedStatus = false;
                    context.RubricRemedyDetails.Add(rubricRemedyDetailsEntity);
                    context.SaveChanges();

                    foreach (var item1 in item.Authors)
                    {
                        var modeldetails = new RemedyRubricAuthorDetails();
                        modeldetails.RubricRemedyId = rubricRemedyDetailsEntity.RubricRemedyId;
                        modeldetails.AuthorId = item1.AuthorId;
                        context.RemedyRubricAuthorDetails.Add(modeldetails);
                        context.SaveChanges();

                    }

                }
                else
                {

                    var rubricRemedyDetails = context.RubricRemedyDetails.FirstOrDefault(x => x.RubricRemedyId == item.RubricRemedyId);

                    if (rubricRemedyDetails != null)
                    {
                        rubricRemedyDetails.RubricRemedyId = item.RubricRemedyId;
                        rubricRemedyDetails.SubSectionId = item.SubSectionId;
                        rubricRemedyDetails.RemedyId = item.RemedyId;
                        rubricRemedyDetails.GradeId = item.GradeId;
                        rubricRemedyDetails.EnteredDate = item.EnteredDate;
                        rubricRemedyDetails.EnteredBy = item.EnteredBy;
                        rubricRemedyDetails.DeletedStatus = false;
                        context.SaveChanges();

                        foreach (var item1 in item.Authors)
                        {


                            if (item1.RemedyRubricAuthorId == 0)
                            {
                                var modeldetails = new RemedyRubricAuthorDetails();
                                modeldetails.RubricRemedyId = rubricRemedyDetails.RubricRemedyId;
                                modeldetails.AuthorId = item1.AuthorId;
                                context.RemedyRubricAuthorDetails.Add(modeldetails);
                                context.SaveChanges();
                            }
                            else
                            {
                                var modeldetails = context.RemedyRubricAuthorDetails.FirstOrDefault(x => x.RemedyRubricAuthorId == item1.RemedyRubricAuthorId);
                                if (modeldetails != null)
                                {
                                    modeldetails.RubricRemedyId = rubricRemedyDetails.RubricRemedyId;
                                    modeldetails.AuthorId = item1.AuthorId;
                                    context.SaveChanges();
                                }

                            }


                        }
                    }

                }



                Message = "Rubric Remedy Details Saved Successfully";
            }

            return Message;
        }

        /// <summary>
        /// Method implementation for getting rubric remedy details.
        /// </summary>
        /// <param name="rubricRemedyDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RemedyRubricViewModel GetRubricRemedyDetails(long RemedyId, ref ErrorResponseModel errorResponseModel)
        {

            var remadyInfo = context.RemedyMaster.Where(x => x.RemedyId == RemedyId).FirstOrDefault();
            var remedyRubricViewModel = new RemedyRubricViewModel();
            remedyRubricViewModel.RemedyID = remadyInfo.RemedyId;
            remedyRubricViewModel.RemedyName = remadyInfo.RemedyName;
            remedyRubricViewModel.ThemesOrCharacteristics = remadyInfo.ThemesOrCharacteristics;
            remedyRubricViewModel.Generals = remadyInfo.Generals;
            remedyRubricViewModel.Particulars = remadyInfo.Particulars;
            remedyRubricViewModel.Modalities = remadyInfo.Modalities;

            
            errorResponseModel = new ErrorResponseModel();
            var remedyEntities = (from remedyDetails in context.RubricRemedyDetails
                                  join subSection in context.SubSectionMaster on remedyDetails.SubSectionId equals subSection.SubSectionId
                                  join gradeMaster in context.RemedyGradeMaster on remedyDetails.GradeId equals gradeMaster.GradeId
                                  where remedyDetails.RemedyId == RemedyId && subSection.DeleteStatus==false 
                                  && remedyDetails.DeletedStatus==false
                                  select new RubricRemedyViewModel
                                  {
                                      RubricRemedyId=remedyDetails.RubricRemedyId,
                                      SectionId=subSection.SectionId,
                                      SubSectionId=subSection.SubSectionId,
                                      SubSectionName = subSection.SubSectionName,
                                      RemedyId=remedyDetails.RemedyId,
                                      GradeId=gradeMaster.GradeId,
                                      EnteredBy = remedyDetails.EnteredBy,
                                      EnteredDate = remedyDetails.EnteredDate,
                                      FontName = gradeMaster.FontName,
                                      FontColor = gradeMaster.FontColor,
                                      FontStyle = gradeMaster.FontStyle,
                                      RemedyCount = 0,
                                      IsSmallRubric=remedyDetails.IsSmallRubric,
                                      IsConformationRubric=remedyDetails.IsConfirmationRubric,
                                  }).OrderBy(x => x.SectionId).ToList();

            if (remedyEntities.Count > 0)
            {
                var subSectionIds = remedyEntities
                    .Where(x => x.SubSectionId.HasValue)
                    .Select(x => x.SubSectionId.Value)
                    .Distinct()
                    .ToList();

                var remedyCountBySubSection = context.RubricRemedyDetails
                    .Where(x => x.SubSectionId.HasValue
                                && subSectionIds.Contains(x.SubSectionId.Value)
                                && x.DeletedStatus == false)
                    .GroupBy(x => x.SubSectionId.Value)
                    .Select(g => new { SubSectionId = g.Key, Count = g.Count() })
                    .ToDictionary(x => x.SubSectionId, x => x.Count);

                foreach (var item in remedyEntities)
                {
                    if (item.SubSectionId.HasValue
                        && remedyCountBySubSection.TryGetValue(item.SubSectionId.Value, out var count))
                    {
                        item.RemedyCount = count;
                    }
                }
            }

            if (remedyEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy Not Found";
            }
            remedyRubricViewModel.RubricRemedyViewsList = remedyEntities;
            return remedyRubricViewModel;
        }


        /// <summary>
        /// GetRubricRemedyDetails
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RemedyCountsModel GetRemedyCounts(int subSectionId, ref ErrorResponseModel errorResponseModel)
        {
            var remedyCount = context.RubricRemedyDetails
                .Where(x =>
                    x.SubSectionId == subSectionId
                    && x.DeletedStatus == false
                    && x.Remedy != null
                    && x.Remedy.DeleteStatus == false
                    && x.Grade != null
                    && x.RemedyId != null
                )
                .Select(x => x.RemedyId.Value)
                .Distinct()
                .Count();
            var remedyCountModel = new RemedyCountsModel();
            errorResponseModel = new ErrorResponseModel();
            if (remedyCount == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }
            remedyCountModel.SubSectionId = subSectionId;
            remedyCountModel.RemedyCount = remedyCount;
            return remedyCountModel;
        }

        /// <summary>
        /// GetRubricList
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RubricModel> GetRubricList(int SectionId, NigaParameters nigaParameters, ref ErrorResponseModel errorResponseModel)
        {
            var runricModelList = new List<RubricModel>();
            var rubricRemedyGroup = context.RubricRemedyDetails
                                            .Include(x => x.SubSection)
                                            .Include(x => x.Grade).Include(x => x.Remedy).Where(x => x.SubSection.DeleteStatus.Equals(false) && x.SubSection.SectionId == SectionId)
                                            .GroupBy(x => new { x.SubSectionId, x.GradeId }).Skip((nigaParameters.PageNumber - 1) * nigaParameters.PageSize)
             .Take(nigaParameters.PageSize)
             .ToList();


            if (rubricRemedyGroup.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }
            var list = rubricRemedyGroup.Select(x => new RubricModel
            {
                RubricRemedyId = x.Max(item => item.RubricRemedyId),
                SubSectionId = Convert.ToInt32(x.Key.SubSectionId),
                Grade = Convert.ToInt32(x.Key.GradeId),
                SectionId = x.Select(subsection => subsection.SubSection.SectionId).FirstOrDefault(),
                SectionName = context.SectionMaster.Where(s => s.SectionId == SectionId).Select(s => s.SectionName).FirstOrDefault(),
                //SectionName=x.Select(section=>section.Section.SectionName).FirstOrDefault(),
                SubSectionName = x.Select(subsection => subsection.SubSection.SubSectionName).FirstOrDefault(),
            }).Where(x => x.SectionId == SectionId).GroupBy(x => new { x.SubSectionId }).Select(g => g.First()).ToList();
            return list.OrderBy(x=>x.SubSectionName).ToList();
        }

        /// <summary>
        /// Get Grade remedies from subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        //public List<GradeRemediesModel> GetGradeRemedies(int subSectionId, ref ErrorResponseModel errorResponseModel)
        //{
        //    var gradeGroup = context.RubricRemedyDetails
        //                                    .Where(x => x.SubSectionId == subSectionId && x.DeletedStatus==false)
        //                                    .Include(x => x.Grade).Include(x => x.Remedy)
        //                                    .Include(x => x.RemedyRubricAuthorDetails)
        //                                    .GroupBy(x => x.GradeId).ToList();
        //    errorResponseModel = new ErrorResponseModel();
        //    if (gradeGroup.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "Grade details not found";
        //        return new List<GradeRemediesModel>();
        //    }

        //    return gradeGroup.Select(grade => new GradeRemediesModel
        //    {
        //        GradeId = Convert.ToInt32(grade.Key),
        //        GradeNo = grade.Select(x => x.Grade.GradeNo).FirstOrDefault(),
        //        FontName = grade.Select(x => x.Grade.FontName).FirstOrDefault(),
        //        FontStyle = grade.Select(x => x.Grade.FontStyle).FirstOrDefault(),
        //        FontColor = grade.Select(x => x.Grade.FontColor).FirstOrDefault(),
        //        Description = grade.Select(x => x.Grade.Description).FirstOrDefault(),
        //        subSectionId = subSectionId,
        //        remediesModels = grade.Select(remedy => new RemediesModel
        //        {
        //            RemedyId = Convert.ToInt32(remedy.RemedyId),
        //            RemedyName = remedy.Remedy.RemedyName,
        //            RemedyAlias = string.IsNullOrEmpty(remedy.Remedy.RemedyAlias) ? "Not Available" : remedy.Remedy.RemedyAlias,
        //            AuthorId = (int?)remedy.RemedyRubricAuthorDetails
        //                        .FirstOrDefault(x => x.RubricRemedyId == remedy.RubricRemedyId)
        //                        ?.AuthorId ?? 0,
        //            AuthorAlias = GetAuthorAlies(Convert.ToInt32(remedy.RemedyId)) 
        //            //context.AuthorMaster
        //            //.Where(x => x.AuthorId == (remedy.RemedyRubricAuthorDetails
        //            //    .FirstOrDefault(xa => xa.RubricRemedyId == remedy.RubricRemedyId).AuthorId))
        //            //.FirstOrDefault()?.AuthorAlias


        //        }).OrderBy(x => x.RemedyName).ToList()
        //    }).OrderBy(x => x.GradeNo).ToList();
        //}

        public List<GradeRemediesModel> GetGradeRemedies(int subSectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var data = context.RubricRemedyDetails
                .Where(x => x.SubSectionId == subSectionId && x.DeletedStatus == false)
                .Select(x => new
                {
                    x.GradeId,
                    x.Grade.GradeNo,
                    x.Grade.FontName,
                    x.Grade.FontStyle,
                    x.Grade.FontColor,
                    x.Grade.Description,
                    x.RemedyId,
                    x.Remedy.RemedyName,
                    x.Remedy.RemedyAlias,
                    Authors = x.RemedyRubricAuthorDetails
                                .Where(a => a.DeletedStatus == false)
                                .Select(a => a.Author.AuthorAlias)
                })
                .ToList();

            if (!data.Any())
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Grade details not found";
                return new List<GradeRemediesModel>();
            }

            var result = data
                .GroupBy(x => x.GradeId)
                .Select(grade => new GradeRemediesModel
                {
                    GradeId = grade.Key ?? 0,
                    GradeNo = grade.First().GradeNo,
                    FontName = grade.First().FontName,
                    FontStyle = grade.First().FontStyle,
                    FontColor = grade.First().FontColor,
                    Description = grade.First().Description,
                    subSectionId = subSectionId,
                    remediesModels = grade.Select(remedy => new RemediesModel
                    {
                        RemedyId = remedy.RemedyId ?? 0,
                        RemedyName = remedy.RemedyName,
                        RemedyAlias = string.IsNullOrEmpty(remedy.RemedyAlias)
                                        ? "Not Available"
                                        : remedy.RemedyAlias,
                        AuthorAlias = string.Join(",", remedy.Authors.Distinct())
                    })
                    .OrderBy(x => x.RemedyName)
                    .ToList()
                })
                .OrderBy(x => x.GradeNo)
                .ToList();

            return result;
        }

        private string GetAuthorAlies(int remedyId)
        {
            var authorAlies= (from authorMaster in context.AuthorMaster.AsNoTracking()
                              join remedyRubricAuthorDetails in context.RemedyRubricAuthorDetails.AsNoTracking() on authorMaster.AuthorId equals remedyRubricAuthorDetails.AuthorId
                              join rubricRemedyDetail in context.RubricRemedyDetails.AsNoTracking() on remedyRubricAuthorDetails.RubricRemedyId equals rubricRemedyDetail.RubricRemedyId
                              where rubricRemedyDetail.RemedyId== remedyId && rubricRemedyDetail.DeletedStatus==false && remedyRubricAuthorDetails.DeletedStatus==false
                              select new { 
                               authorMaster.AuthorAlias
                              }).ToList();

            return string.Join(",", authorAlies.Distinct().Select(x=>x.AuthorAlias));
            
        }

        private Dictionary<int, string> BuildAuthorAliasMapForSubSection(int subSectionId)
        {
            var authorRows = (
                from rrd in context.RubricRemedyDetails.AsNoTracking()
                join rrau in context.RemedyRubricAuthorDetails.AsNoTracking()
                    on rrd.RubricRemedyId equals rrau.RubricRemedyId
                join author in context.AuthorMaster.AsNoTracking()
                    on rrau.AuthorId equals author.AuthorId
                where
                    rrd.SubSectionId == subSectionId
                    && rrd.DeletedStatus == false
                    && rrau.DeletedStatus == false
                    && rrd.RemedyId != null
                select new { RemedyId = rrd.RemedyId.Value, author.AuthorAlias }
            ).ToList();

            return authorRows
                .Where(x => !string.IsNullOrWhiteSpace(x.AuthorAlias))
                .GroupBy(x => x.RemedyId)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(",", g.Select(x => x.AuthorAlias).Distinct())
                );
        }

        private Dictionary<int, string> BuildGlobalAuthorAliasMapForRemedyIds(IEnumerable<int> remedyIds)
        {
            var ids = remedyIds?.Distinct().ToList() ?? new List<int>();
            if (!ids.Any())
            {
                return new Dictionary<int, string>();
            }

            var authorRows = (
                from rrd in context.RubricRemedyDetails.AsNoTracking()
                join rrau in context.RemedyRubricAuthorDetails.AsNoTracking()
                    on rrd.RubricRemedyId equals rrau.RubricRemedyId
                join author in context.AuthorMaster.AsNoTracking()
                    on rrau.AuthorId equals author.AuthorId
                where
                    rrd.RemedyId != null
                    && ids.Contains(rrd.RemedyId.Value)
                    && rrd.DeletedStatus == false
                    && rrau.DeletedStatus == false
                select new { RemedyId = rrd.RemedyId.Value, author.AuthorAlias }
            ).ToList();

            return authorRows
                .Where(x => !string.IsNullOrWhiteSpace(x.AuthorAlias))
                .GroupBy(x => x.RemedyId)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(",", g.Select(x => x.AuthorAlias).Distinct())
                );
        }

        private static string MergeAuthorAliasStrings(params string[] parts)
        {
            var aliases = parts
                .SelectMany(part =>
                    (part ?? string.Empty).Split(
                        new[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries
                    )
                )
                .Select(alias => alias.Trim())
                .Where(alias => !string.IsNullOrWhiteSpace(alias))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return string.Join(",", aliases);
        }

        /// <summary>
        /// Get details to edit rubric remedies
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RubricRemedyDetailsModel GetRemedyDetailsToEdit(int subSectionId, int grade, ref ErrorResponseModel errorResponseModel)
        {
            var rubricRemedyDetails = context.RubricRemedyDetails
                                             .Where(x => x.SubSectionId == subSectionId
                                                    && x.GradeId == grade).ToList();
            if (rubricRemedyDetails.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Grade details not found";
            }
            var rubricRemedyDetailsModel = new RubricRemedyDetailsModel();
            var sectionMaster = context.SubSectionMaster.Where(x => x.SubSectionId == subSectionId).FirstOrDefault();
            if (sectionMaster != null)
            {
                rubricRemedyDetailsModel.SectionId = sectionMaster.SectionId;

            }
            rubricRemedyDetailsModel.SubSectionId = subSectionId;
            rubricRemedyDetailsModel.RubricRemedyId = rubricRemedyDetails.Select(x => x.RubricRemedyId).FirstOrDefault();
            rubricRemedyDetailsModel.GradeId = rubricRemedyDetails.Select(x => x.GradeId).FirstOrDefault() != null ?
                Convert.ToInt32(rubricRemedyDetails.Select(x => x.GradeId).FirstOrDefault()) : 0;
            var remedies = string.Join(',', rubricRemedyDetails.Select(x => x.RemedyId).ToList());
            rubricRemedyDetailsModel.RemedyIds = remedies;

            return rubricRemedyDetailsModel;


        }

        public List<RubricRemedyViewModel1> GetSubSections(int sectionId, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<RubricRemedyViewModel1>();
            errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList = context.SubSectionMaster
                                            .Where(x => x.DeleteStatus == false
                                            && x.SectionId == sectionId).ToList();
            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            subsectionEntityList.ForEach(item =>
            {
                subsectionModelList.Add(new RubricRemedyViewModel1
                {
                    SubSectionId = item.SubSectionId,
                    SectionId = item.SectionId,
                    SubSectionName = item.SubSectionName,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return subsectionModelList;

        }


        // Method created by Vikas More

        public RubricRemedyDetailModel GetRubricRemedyBySectionGread(int subSectionId, int greadId, ref ErrorResponseModel errorResponseModel)
        {
            var remedyModel = new List<RemedyModel>();
            errorResponseModel = new ErrorResponseModel();
            var remedyEntities = (from rubricRemedyDetails in context.RubricRemedyDetails
                                  join subSectionMaster in context.SubSectionMaster on rubricRemedyDetails.SubSectionId equals subSectionMaster.SubSectionId
                                  join gradeMaster in context.RemedyGradeMaster on rubricRemedyDetails.GradeId equals gradeMaster.GradeId
                                  join remedyMaster in context.RemedyMaster on rubricRemedyDetails.RemedyId equals remedyMaster.RemedyId
                                  where rubricRemedyDetails.SubSectionId == subSectionId && rubricRemedyDetails.GradeId == greadId && rubricRemedyDetails.DeletedStatus == false
                                  select new
                                  {
                                      rubricRemedyDetails.RubricRemedyId,
                                      subSectionMaster.SectionId,
                                      subSectionMaster.SubSectionId,
                                      gradeMaster.GradeId,
                                      rubricRemedyDetails.RemedyId,
                                      remedyMaster.RemedyName
                                  }).ToList();

            if (remedyEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }

            RubricRemedyDetailModel rubricRemedyDetailModel = new RubricRemedyDetailModel();

            var remedyDetail = remedyEntities.FirstOrDefault();

            rubricRemedyDetailModel.SubSectionId = subSectionId;
            rubricRemedyDetailModel.GradeId = greadId;
            rubricRemedyDetailModel.SectionId = Convert.ToInt32(remedyDetail.SectionId);

            List<RubricRemedyAuthorModel> rubricRemedyAuthorsList = new List<RubricRemedyAuthorModel>();

            foreach (var item in remedyEntities)
            {
                RubricRemedyAuthorModel rubricRemedyAuthor = new RubricRemedyAuthorModel();

                rubricRemedyAuthor.RubricRemedyId = item.RubricRemedyId;
                rubricRemedyAuthor.RemedyId = item.RemedyId;
                rubricRemedyAuthor.RemedyName = item.RemedyName;
                var rubricAutorData = (from remedyRubricAuthorDetails in context.RemedyRubricAuthorDetails
                                       join auther in context.AuthorMaster on remedyRubricAuthorDetails.AuthorId equals auther.AuthorId
                                       where remedyRubricAuthorDetails.RubricRemedyId == item.RubricRemedyId && remedyRubricAuthorDetails.DeletedStatus == false
                                       select new RubricAuthorModel
                                       {
                                           RemedyRubricAuthorId = remedyRubricAuthorDetails.RemedyRubricAuthorId,
                                           AuthorId = remedyRubricAuthorDetails.AuthorId,
                                           AuthorName = auther.AuthorName

                                       }).ToList();
                rubricRemedyAuthor.RubricAuthorList = rubricAutorData;
                rubricRemedyAuthorsList.Add(rubricRemedyAuthor);
            }
            rubricRemedyDetailModel.RubricRemedyAuthorList = rubricRemedyAuthorsList;

            return rubricRemedyDetailModel;
        }

        public string SaveUpdateRubricRemedy(RubricRemedyDetailModel rubricRemedyDetail, int userId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";

            foreach (var remedyItem in rubricRemedyDetail.RubricRemedyAuthorList)
            {
                if (remedyItem.RubricRemedyId == 0)
                {
                    var rubricRemedyDetailsEntityData = context.RubricRemedyDetails.FirstOrDefault(x => x.SubSectionId == rubricRemedyDetail.SubSectionId && x.GradeId==rubricRemedyDetail.GradeId && x.RemedyId==remedyItem.RemedyId && x.DeletedStatus==false);

                    if (rubricRemedyDetailsEntityData == null)
                    {

                        RubricRemedyDetails rubricRemedyDetailsEntity = new RubricRemedyDetails();
                        rubricRemedyDetailsEntity.SubSectionId = rubricRemedyDetail.SubSectionId;
                        rubricRemedyDetailsEntity.GradeId = rubricRemedyDetail.GradeId;
                        rubricRemedyDetailsEntity.RemedyId = remedyItem.RemedyId;
                        rubricRemedyDetailsEntity.EnteredBy = userId;
                        rubricRemedyDetailsEntity.EnteredDate = DateTime.Now;
                        rubricRemedyDetailsEntity.DeletedStatus = false;
                        context.RubricRemedyDetails.Add(rubricRemedyDetailsEntity);
                        context.SaveChanges();

                        foreach (var authorItem in remedyItem.RubricAuthorList)
                        {
                            AddRemedyRubricAuthorDetails(rubricRemedyDetailsEntity.RubricRemedyId, authorItem.AuthorId);
                        }
                        
                    }
                    else
                    {
                        foreach (var authorItem in remedyItem.RubricAuthorList)
                        {
                            AddRemedyRubricAuthorDetails(rubricRemedyDetailsEntityData.RubricRemedyId, authorItem.AuthorId);
                        }

                    }
                    Message = "Remedy Saved Successfully";
                }
                else
                {
                    var rubricRemedyDetailsEntity = context.RubricRemedyDetails.FirstOrDefault(x => x.RubricRemedyId == remedyItem.RubricRemedyId);
                    if (rubricRemedyDetailsEntity != null)
                    {
                        rubricRemedyDetailsEntity.SubSectionId = rubricRemedyDetail.SubSectionId;
                        rubricRemedyDetailsEntity.GradeId = rubricRemedyDetail.GradeId;
                        rubricRemedyDetailsEntity.RemedyId = remedyItem.RemedyId;
                        rubricRemedyDetailsEntity.DeletedStatus = false;
                        context.SaveChanges();

                        foreach (var authorItem in remedyItem.RubricAuthorList)
                        {
                            if (authorItem.RemedyRubricAuthorId == 0)
                            {
                                AddRemedyRubricAuthorDetails(rubricRemedyDetailsEntity.RubricRemedyId, authorItem.AuthorId);
                            }
                            else
                            {
                                var remedyRubricAuthor = context.RemedyRubricAuthorDetails.FirstOrDefault(x => x.RemedyRubricAuthorId == authorItem.RemedyRubricAuthorId);
                                if (remedyRubricAuthor != null)
                                {
                                    remedyRubricAuthor.RubricRemedyId = rubricRemedyDetailsEntity.RubricRemedyId;
                                    remedyRubricAuthor.AuthorId = authorItem.AuthorId;
                                    remedyRubricAuthor.DeletedStatus = false;
                                    context.SaveChanges();
                                }
                            }
                        }

                        Message = "Remedy Updated Successfully";
                    }
                }
            }
            return Message;
        }

        private void AddRemedyRubricAuthorDetails(int rubricRemedyId, int? authorId)
        {
            var remedyRubricAuthorData=context.RemedyRubricAuthorDetails.FirstOrDefault(x=>x.RubricRemedyId == rubricRemedyId && x.AuthorId==authorId);

            if (remedyRubricAuthorData == null)
            {
                RemedyRubricAuthorDetails remedyRubricAuthor = new RemedyRubricAuthorDetails();
                remedyRubricAuthor.RubricRemedyId = rubricRemedyId;
                remedyRubricAuthor.AuthorId = authorId;
                remedyRubricAuthor.DeletedStatus = false;
                context.RemedyRubricAuthorDetails.Add(remedyRubricAuthor);
                context.SaveChanges();
            }
        }


        public string DeleteRubricRemedyAuthor(RubricRemedyDeleteModel rubricRemedyDeleteModel, ref ErrorResponseModel errorResponseModel)
        {
            string message = string.Empty;

            if (rubricRemedyDeleteModel.RemedyRubricAuthorId > 0)
            {
                var remedyRubricAuthor = context.RemedyRubricAuthorDetails.FirstOrDefault(x => x.RemedyRubricAuthorId == rubricRemedyDeleteModel.RemedyRubricAuthorId);
                if (remedyRubricAuthor != null)
                {
                    remedyRubricAuthor.DeletedStatus = true;
                    context.SaveChanges();
                    message = "Record Deleted successfully";
                }
                else
                {
                    message = "Record not found";
                }
            }
            else if (rubricRemedyDeleteModel.RubricRemedyId > 0)
            {

                var rubricRemedyDetailsEntity = context.RubricRemedyDetails.FirstOrDefault(x => x.RubricRemedyId == rubricRemedyDeleteModel.RubricRemedyId);
                if (rubricRemedyDetailsEntity != null)
                {
                    rubricRemedyDetailsEntity.DeletedStatus = true;
                    context.SaveChanges();
                    message = "Record Deleted successfully";
                }
                else
                {
                    message = "Record not found";
                }
            }
            return message;
        }

        public string UpdateIsSmallRubric(int rubricRemedyID, bool isSmallRubric, ref ErrorResponseModel errorResponseModel)
        {
            string message = string.Empty;
            var rubricRemadyEntity = context.RubricRemedyDetails.Where(x=>x.RubricRemedyId==rubricRemedyID).FirstOrDefault();
            if (rubricRemadyEntity != null)
            {
                rubricRemadyEntity.IsSmallRubric = isSmallRubric;
                context.SaveChanges();
                message = "Update sucessfully isSmall rubric remady";
            }
            else
            {
                message = "Failed to Update isSmall rubric remady";
            }

            return message;
        }

        public string UpdateIsConfirmationRubric(int rubricRemedyID, bool isConformationRubric, ref ErrorResponseModel errorResponseModel)
        {
            string message = string.Empty;
            var rubricRemadyEntity = context.RubricRemedyDetails.Where(x => x.RubricRemedyId == rubricRemedyID).FirstOrDefault();
            if (rubricRemadyEntity != null)
            {
                rubricRemadyEntity.IsConfirmationRubric = isConformationRubric;
                context.SaveChanges();
                message = "Update sucessfully isSmall rubric remady";
            }
            else
            {
                message = "Failed to Update isSmall rubric remady";
            }

            return message;
        }


        /// <summary>
        /// Get Grade remedies from subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RemediesModel> GetGradeRemedies1(int subSectionId, ref ErrorResponseModel errorResponseModel)
        {
            var gradeGroup = context.RubricRemedyDetails
                                            .Where(x => x.SubSectionId == subSectionId && x.DeletedStatus == false)
                                            .Include(x => x.Grade).Include(x => x.Remedy)
                                            .Include(x => x.RemedyRubricAuthorDetails)
                                            .GroupBy(x => x.RemedyId).ToList();
            errorResponseModel = new ErrorResponseModel();
            if (gradeGroup.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Grade details not found";
                return new List<RemediesModel>();
            }

            return gradeGroup.Select(grade => new RemediesModel
            {
                GradeNo = grade.Select(x => x.Grade.GradeNo).FirstOrDefault(),
                FontName = grade.Select(x => x.Grade.FontName).FirstOrDefault(),
                FontStyle = grade.Select(x => x.Grade.FontStyle).FirstOrDefault(),
                FontColor = grade.Select(x => x.Grade.FontColor).FirstOrDefault(),
                RemedyId = grade.Select(x => Convert.ToInt32(x.RemedyId)).FirstOrDefault(),
                RemedyName = grade.Select(x =>x.Remedy.RemedyName).FirstOrDefault(),
                RemedyAlias = grade.Select(x => string.IsNullOrEmpty(x.Remedy.RemedyAlias) ? "Not Available" : x.Remedy.RemedyAlias).FirstOrDefault(),
                AuthorAlias = GetAuthorAlies(grade.Select(x => Convert.ToInt32(x.RemedyId)).FirstOrDefault()),
                //remediesModels = grade.Select(remedy => new RemediesModel
                //{
                //    RemedyId = Convert.ToInt32(remedy.RemedyId),
                //    RemedyName = remedy.Remedy.RemedyName,
                //    RemedyAlias = string.IsNullOrEmpty(remedy.Remedy.RemedyAlias) ? "Not Available" : remedy.Remedy.RemedyAlias,
                //    AuthorId = (int?)remedy.RemedyRubricAuthorDetails
                //                .FirstOrDefault(x => x.RubricRemedyId == remedy.RubricRemedyId)
                //                ?.AuthorId ?? 0,
                //    AuthorAlias = GetAuthorAlies(Convert.ToInt32(remedy.RemedyId))
                //    //context.AuthorMaster
                //    //.Where(x => x.AuthorId == (remedy.RemedyRubricAuthorDetails
                //    //    .FirstOrDefault(xa => xa.RubricRemedyId == remedy.RubricRemedyId).AuthorId))
                //    //.FirstOrDefault()?.AuthorAlias


                //}).OrderBy(x => x.RemedyName).ToList()
            }).OrderBy(x => x.RemedyName).ToList();
        }
      /// <summary>
                 /// Get Grade remedies from subsection
                 /// </summary>
                 /// <param name="subSectionId"></param>
                 /// <param name="errorResponseModel"></param>
                 /// <returns></returns>
        public RubricDetailModel GetRubricDetails(int subSectionId, ref ErrorResponseModel errorResponseModel, bool includeAuthors = true)
        {
            errorResponseModel = new ErrorResponseModel();

            var cacheKey = $"RubricDetails:v9:{subSectionId}:a{(includeAuthors ? 1 : 0)}";
            if (_cache.TryGetValue(cacheKey, out RubricDetailModel cached))
            {
                return cached;
            }

            var rubricDetails = context.SubSectionMaster
                .AsNoTracking()
                .Where(subSection => subSection.SubSectionId == subSectionId && !subSection.DeleteStatus)
                .Select(subSection => new RubricDetailModel
                {
                    SubSectionId = subSection.SubSectionId,
                    Description = subSection.Description,
                    SubSectionNameAlias = subSection.SubSectionNameAlias,
                    SubSectionName = subSection.SubSectionName,
                    SectionId = subSection.SectionId,
                    ParentSubSectionId = subSection.ParentSubSectionId,
                })
                .FirstOrDefault();

            if (rubricDetails == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
                return null;
            }

            var referanceRubricList = (from referanceRubric in context.ReferenceRubricDetails.AsNoTracking()
                                       join subSection in context.SubSectionMaster.AsNoTracking() on referanceRubric.RefSubSectionId equals subSection.SubSectionId
                                       join section in context.SectionMaster.AsNoTracking() on subSection.SectionId equals section.SectionId
                                       where referanceRubric.SubSectionId == subSectionId && referanceRubric.DeleteStatus == false
                                       select new ReferenceRubricDetailsModel
                                       {
                                           SectionId = subSection.SectionId,
                                           SectionName = section.SectionName,
                                           SubSectionId = referanceRubric.SubSectionId,
                                           ReferenceRubricId = (int)referanceRubric.ReferenceRubricId,
                                           RefSubSectionId = referanceRubric.RefSubSectionId,
                                           RefSubSectionName = subSection.SubSectionName,
                                       }).ToList();

            var subsectionlanguageList = (from subSectionLanguage in context.SubSectionLanguageDetails.AsNoTracking()
                                          join languageMaster in context.LanguageMaster.AsNoTracking() on subSectionLanguage.LanguageId equals languageMaster.LanguageId
                                          where subSectionLanguage.SubSectionId == subSectionId && subSectionLanguage.DeleteStatus == false
                                          select new SubSectionLanguageDetailsModel
                                          {
                                              SubSectionId = subSectionLanguage.SubSectionId,
                                              SectionName = rubricDetails.SubSectionName,
                                              LanguageId = subSectionLanguage.LanguageId,
                                              SubSectionDetails = subSectionLanguage.SubSectionDetails,
                                              LanguageName = languageMaster.LanguageName,
                                              SubSectionLanguageId = subSectionLanguage.SubSectionLanguageId,
                                              LanguageDescription = languageMaster.Description
                                          }).ToList();

            var remedyRows = context.RubricRemedyDetails
                .AsNoTracking()
                .Where(x => x.SubSectionId == subSectionId && x.DeletedStatus == false)
                .Where(x => x.Remedy != null && x.Remedy.DeleteStatus == false)
                .Where(x => x.Grade != null && x.RemedyId != null)
                .Select(x => new
                {
                    RemedyId = x.RemedyId.Value,
                    x.Remedy.RemedyName,
                    x.Remedy.RemedyAlias,
                    x.Grade.GradeNo,
                    x.Grade.FontName,
                    x.Grade.FontStyle,
                    x.Grade.FontColor,
                    Authors = x.RemedyRubricAuthorDetails
                        .Where(a => a.DeletedStatus == false)
                        .Select(a => a.Author.AuthorAlias),
                })
                .ToList();

            var remedyIds = remedyRows.Select(x => x.RemedyId).Distinct().ToList();
            Dictionary<int, string> subsectionAuthorMap = includeAuthors
                ? BuildAuthorAliasMapForSubSection(subSectionId)
                : new Dictionary<int, string>();
            Dictionary<int, string> globalAuthorMap = includeAuthors
                ? BuildGlobalAuthorAliasMapForRemedyIds(remedyIds)
                : new Dictionary<int, string>();

            var remediesList = remedyRows
                .GroupBy(x => x.RemedyId)
                .Select(grade =>
                {
                    var remedy = new RemediesModel
                    {
                        RemedyId = grade.Key,
                        GradeNo = grade.Select(x => x.GradeNo).FirstOrDefault(),
                        FontName = grade.Select(x => x.FontName).FirstOrDefault(),
                        FontStyle = grade.Select(x => x.FontStyle).FirstOrDefault(),
                        FontColor = grade.Select(x => x.FontColor).FirstOrDefault(),
                        RemedyName = grade.Select(x => x.RemedyName).FirstOrDefault(),
                        RemedyAlias = grade.Select(x => string.IsNullOrEmpty(x.RemedyAlias) ? x.RemedyName : x.RemedyAlias).FirstOrDefault(),
                    };

                    if (includeAuthors)
                    {
                        var rowAuthors = string.Join(
                            ",",
                            grade
                                .SelectMany(x => x.Authors)
                                .Where(alias => !string.IsNullOrWhiteSpace(alias))
                        );
                        subsectionAuthorMap.TryGetValue(grade.Key, out var subsectionAuthors);
                        globalAuthorMap.TryGetValue(grade.Key, out var globalAuthors);
                        remedy.AuthorAlias = MergeAuthorAliasStrings(
                            rowAuthors,
                            subsectionAuthors,
                            globalAuthors
                        );
                        if (string.IsNullOrWhiteSpace(remedy.AuthorAlias))
                        {
                            remedy.AuthorAlias = GetAuthorAlies(grade.Key);
                        }
                    }

                    return remedy;
                })
                .OrderBy(x => x.RemedyName)
                .ToList();

            rubricDetails.Referencerubric = referanceRubricList;
            rubricDetails.SubSectionLanguageDetails = subsectionlanguageList;
            rubricDetails.RemdeyCount = remediesList.Count;
            rubricDetails.RemediesList = remediesList;

            _cache.Set(cacheKey, rubricDetails, RubricDetailsCacheOptions);

            return rubricDetails;
        }
    }
}
