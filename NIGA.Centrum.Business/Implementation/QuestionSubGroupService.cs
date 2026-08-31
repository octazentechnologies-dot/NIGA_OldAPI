using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NIGA.Centrum.Business.Implementation
{
 public class QuestionSubGroupService :IQuestionSubGroupService
    {
        /// <summary>
        /// This is implementation for the QuestionSubGroup operations
        /// </summary>



        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public QuestionSubGroupService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }


        /// <summary>        /// Method to get GetQuestionSubGroup by GetQuestionSubGroupById        /// </summary>        /// <param name="questionSubgroupId"></param>        /// <param name="errorResponseModel"></param>        /// <returns></returns>

        public QuestionSubGroupModel GetQuestionSubGroupById(long questionSubgroupId, ref ErrorResponseModel errorResponseModel) 
        { 
            errorResponseModel = new ErrorResponseModel();
            var questionsubEntity = (from questionSubGroup in context.QuestionSubgroup
                                        join questionGroup in context.QuestionGroupMaster on questionSubGroup.QuestionGroupId equals questionGroup.QuestionGroupId
                                        where questionSubGroup.DeleteStatus == false && questionSubGroup.QuestionSubgroupId == questionSubgroupId
                                        select new QuestionSubGroupModel
                                        {
                                            QuestionGroupId = questionSubGroup.QuestionGroupId,
                                            QuestionGroupName = questionGroup.QuestionGroupName,
                                            QuestionSubgroupId = questionSubGroup.QuestionSubgroupId,
                                            QuestionSubGroupName = questionSubGroup.QuestionSubgroup1,
                                            Description = questionSubGroup.Description,
                                            DeleteStatus = questionSubGroup.DeleteStatus,
                                        }).FirstOrDefault();

            if (questionsubEntity == null)
            { 
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "QuestionSubGroup not found"; 
            }
            else
            {
                AttachSections(new List<QuestionSubGroupModel> { questionsubEntity });
            }
            return questionsubEntity;
        }
        // <summary>
            /// Method to get all the QuestionSubGroup
            /// </summary>
            /// <param name="errorResponseModel"></param>
            /// <returns></returns>

        public List<QuestionSubGroupModel> GetQuestionSubGroup(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

           var questionsubList=(from questionSubGroup in context.QuestionSubgroup 
                             join questionGroup in context.QuestionGroupMaster on questionSubGroup.QuestionGroupId equals questionGroup.QuestionGroupId
                             where questionSubGroup.DeleteStatus==false
                             select new QuestionSubGroupModel
                             { 
                                QuestionGroupId=questionSubGroup.QuestionGroupId,
                                QuestionGroupName=questionGroup.QuestionGroupName,
                                QuestionSubgroupId=questionSubGroup.QuestionSubgroupId,
                                QuestionSubGroupName=questionSubGroup.QuestionSubgroup1,
                                Description=questionSubGroup.Description,
                                DeleteStatus=questionSubGroup.DeleteStatus,
                             }
                             ).ToList();
            if (questionsubList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "QuestionSubGroup Not Found";
            }
            else
            {
                AttachSections(questionsubList);
            }
            return questionsubList;
        }
        /// <summary>
        /// Method implementation for saving new QuestionSubGroup
        /// </summary>
        /// <param name="questionSubGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string SaveQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (questionSubGroupModel.QuestionSubgroupId == 0)
            {
                QuestionSubgroup questionsubEntity = new QuestionSubgroup();
                questionsubEntity.QuestionSubgroupId = questionSubGroupModel.QuestionSubgroupId;
                questionsubEntity.QuestionGroupId = questionSubGroupModel.QuestionGroupId;
                questionsubEntity.QuestionSubgroup1 = questionSubGroupModel.QuestionSubGroupName;
                questionsubEntity.Description = questionSubGroupModel.Description;
                questionsubEntity.DeleteStatus = false;
                context.QuestionSubgroup.Add(questionsubEntity);
                context.SaveChanges();

                SyncSections(questionsubEntity.QuestionSubgroupId, questionSubGroupModel.SectionIds);
                Message = "QuestionSubGroup Saved Successfully";
            }
            else
            {
                var questionsubEntity = context.QuestionSubgroup.FirstOrDefault(x => x.QuestionSubgroupId == questionSubGroupModel.QuestionSubgroupId);
                if (questionsubEntity != null)
                {
                    questionsubEntity.QuestionSubgroupId = questionSubGroupModel.QuestionSubgroupId;
                    questionsubEntity.QuestionGroupId = questionSubGroupModel.QuestionGroupId;
                    questionsubEntity.QuestionSubgroup1 = questionSubGroupModel.QuestionSubGroupName;
                    questionsubEntity.Description = questionSubGroupModel.Description;
                    questionsubEntity.DeleteStatus = false;
                    context.SaveChanges();

                    SyncSections(questionsubEntity.QuestionSubgroupId, questionSubGroupModel.SectionIds);
                    Message = "QuestionSubGroup Updated Successfully";
                }
            }
            return Message;
        }
        /// <summary>
        /// Method is used for delete QuestionSubGroup.
        /// </summary>
        /// <param name="questionSubGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string DeleteQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var questionsubEntity = context.QuestionSubgroup.FirstOrDefault(x => x.QuestionSubgroupId == questionSubGroupModel.QuestionSubgroupId);
            if (questionsubEntity != null)
            {
                questionsubEntity.DeleteStatus = true;

                var mappingRows = context.QuestionSubgroupSection
                    .Where(x => x.QuestionSubgroupId == questionsubEntity.QuestionSubgroupId && !x.DeleteStatus)
                    .ToList();
                foreach (var row in mappingRows)
                {
                    row.DeleteStatus = true;
                    row.ChangedDate = DateTime.Now;
                }

                context.SaveChanges();
                Message = "QuestionSubGroup Deleted Successfully";
            }
            return Message;
        }

        private void SyncSections(int questionSubgroupId, List<int> sectionIds)
        {
            var existingRows = context.QuestionSubgroupSection
                .Where(x => x.QuestionSubgroupId == questionSubgroupId)
                .ToList();

            if (existingRows.Any())
            {
                context.QuestionSubgroupSection.RemoveRange(existingRows);
                context.SaveChanges();
            }

            var distinctSectionIds = (sectionIds ?? new List<int>())
                .Where(id => id > 0)
                .Distinct()
                .ToList();

            foreach (var sectionId in distinctSectionIds)
            {
                context.QuestionSubgroupSection.Add(new QuestionSubgroupSection
                {
                    QuestionSubgroupId = questionSubgroupId,
                    SectionId = sectionId,
                    DeleteStatus = false,
                    EnteredDate = DateTime.Now
                });
            }

            if (distinctSectionIds.Any())
            {
                context.SaveChanges();
            }
        }

        private void AttachSections(List<QuestionSubGroupModel> questionSubGroups)
        {
            if (questionSubGroups == null || questionSubGroups.Count == 0)
            {
                return;
            }

            var subGroupIds = questionSubGroups.Select(x => x.QuestionSubgroupId).ToList();

            var sectionLinks = (from map in context.QuestionSubgroupSection
                                join section in context.SectionMaster on map.SectionId equals section.SectionId
                                where subGroupIds.Contains(map.QuestionSubgroupId)
                                      && !map.DeleteStatus
                                      && !section.DeleteStatus
                                select new
                                {
                                    map.QuestionSubgroupId,
                                    Section = new SectionViewModel
                                    {
                                        SectionId = section.SectionId,
                                        SectionName = section.SectionName,
                                        SectionAlias = section.SectionAlias,
                                        Description = section.Description
                                    }
                                }).ToList();

            foreach (var subGroup in questionSubGroups)
            {
                var sections = sectionLinks
                    .Where(x => x.QuestionSubgroupId == subGroup.QuestionSubgroupId)
                    .Select(x => x.Section)
                    .ToList();

                subGroup.Sections = sections;
                subGroup.SectionIds = sections
                    .Where(x => x.SectionId.HasValue)
                    .Select(x => x.SectionId.Value)
                    .ToList();
            }
        }



    }

}
