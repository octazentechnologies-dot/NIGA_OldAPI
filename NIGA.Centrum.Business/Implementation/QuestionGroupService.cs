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
    /// This is implementation  for the Question Group operations 
    /// </summary>
    public class QuestionGroupService : IQuestionGroupService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public QuestionGroupService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get Question Group by questiongroupId
        /// </summary>
        /// <param name="questiongroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public QuestionGroupModel GetQuestionGroupById(long questiongroupId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questiongroupEntity = context.QuestionGroupMaster.FirstOrDefault(x => x.QuestionGroupId == questiongroupId && !x.DeleteStatus);
            if (questiongroupEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Group not found";
            }
            return new QuestionGroupModel
            {
                QuestionGroupId = questiongroupEntity.QuestionGroupId,
                QuestionSectionId=questiongroupEntity.QuestionSectionId,
                QuestionGroupName = questiongroupEntity.QuestionGroupName,      
                SectionId = questiongroupEntity.SectionId,
                Description = questiongroupEntity.Description,             
                EnteredDate = questiongroupEntity.EnteredDate,
                EnteredBy = questiongroupEntity.EnteredBy,
                ChangedBy = questiongroupEntity.ChangedBy,
                ChangedDate = questiongroupEntity.ChangedDate,
                DeleteStatus = questiongroupEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the Question Group
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<QuestionGroupModel> GetQuestionGroup(ref ErrorResponseModel errorResponseModel)
        {
            var questiongroupModelList = new List<QuestionGroupModel>();
            errorResponseModel = new ErrorResponseModel();
            var questiongroupEntityList = context.QuestionGroupMaster.Where(x => x.DeleteStatus == false).ToList();
            if (questiongroupEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Group not found";
            }

            questiongroupEntityList.ForEach(item =>
            {
                questiongroupModelList.Add(new QuestionGroupModel
                {
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionSectionId = item.QuestionSectionId,
                    QuestionGroupName = item.QuestionGroupName,
                    SectionId=item.SectionId,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return questiongroupModelList;
        }

        /// <summary>
        /// Method implementation for saving new Question Group
        /// </summary>
        /// <param name="questiongroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveQuestionGroup(QuestionGroupModel questiongroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (questiongroupModel.QuestionGroupId == 0)
            {
                QuestionGroupMaster questiongroupEntity = new QuestionGroupMaster();
                questiongroupEntity.QuestionGroupName = questiongroupModel.QuestionGroupName;     
                questiongroupEntity.QuestionSectionId = questiongroupModel.QuestionSectionId;   
                questiongroupEntity.Description = questiongroupModel.Description;
                questiongroupEntity.SectionId = questiongroupModel.SectionId;
                questiongroupEntity.EnteredBy = questiongroupModel.EnteredBy;
                questiongroupEntity.EnteredDate = DateTime.Now;
                context.QuestionGroupMaster.Add(questiongroupEntity);
                context.SaveChanges();
                Message = "Question Group Saved Successfully";
            }
            else
            {
                var questiongroupEntity = context.QuestionGroupMaster.FirstOrDefault(x => x.QuestionGroupId == questiongroupModel.QuestionGroupId);
                if (questiongroupEntity != null)
                {
                    questiongroupEntity.QuestionGroupName = questiongroupModel.QuestionGroupName;
                    questiongroupEntity.QuestionSectionId= questiongroupModel.QuestionSectionId;
                    questiongroupEntity.SectionId= questiongroupModel.SectionId;
                    questiongroupEntity.Description = questiongroupModel.Description;
                    questiongroupEntity.ChangedBy = questiongroupModel.EnteredBy;
                    questiongroupEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Question Group Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Question Group.
        /// </summary>
        /// <param name="questiongroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteQuestionGroup(QuestionGroupModel questiongroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var questiongroupEntity = context.QuestionGroupMaster.FirstOrDefault(x => x.QuestionGroupId == questiongroupModel.QuestionGroupId);
            if (questiongroupEntity != null)
            {
                questiongroupEntity.DeleteStatus = true;
                questiongroupEntity.ChangedBy = questiongroupModel.EnteredBy;
                questiongroupEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Question Group Deleted Successfully";
            }
            return Message;
        }


        public List<QuestionGroupModel1> GetQuestionGroupExistance(ref ErrorResponseModel errorResponseModel)
        {
            var MateriaMedicaHeadList = new List<QuestionGroupModel1>();
            errorResponseModel = new ErrorResponseModel();
            var materiaMedicaheadEntityList = (from m in context.QuestionGroupMaster
                                               join auth in context.QuestionSectionMaster on m.QuestionSectionId equals auth.QuestionSectionId
                                               where m.DeleteStatus==false 
                                               select new
                                               {
                                                   m.QuestionGroupId,
                                                   m.QuestionSectionId,
                                                   m.QuestionGroupName,
                                                   m.Description,
                                                   m.EnteredDate,
                                                   m.EnteredBy,
                                                   m.ChangedDate,
                                                   m.ChangedBy,
                                                   m.DeleteStatus,
                                                   auth.QuestionSectionName,
                                                   m.SectionId
                                               }).ToList();
            if (materiaMedicaheadEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedicaHead not found";
            }



            materiaMedicaheadEntityList.ForEach(item =>
            {
                MateriaMedicaHeadList.Add(new QuestionGroupModel1
                {
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionGroupName = item.QuestionGroupName,
                    QuestionSectionId = item.QuestionSectionId,
                    Description = item.Description,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    QuestionSectionName = item.QuestionSectionName,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                    SectionId = item.SectionId
                });
            });
            return MateriaMedicaHeadList;
        }

        public List<QuestionGroupModel1> GetQuestionGroupByExistanceId(long QuestionSectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questiongroupModel1 = new List<QuestionGroupModel1>();
            var questiongroupEntity = context.QuestionGroupMaster.Where(x => x.QuestionSectionId==QuestionSectionId && x.DeleteStatus==false).ToList();

            if (questiongroupModel1.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Group not found";
            }
            questiongroupEntity.ForEach(item =>
            {
                questiongroupModel1.Add(new QuestionGroupModel1
                {
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionGroupName = item.QuestionGroupName,
                    QuestionSectionId = item.QuestionSectionId,
                    Description = item.Description,
                    SectionId = item.SectionId,
                    DeleteStatus = item.DeleteStatus,
                    EnteredDate = item.EnteredDate,


                });
            });
            return questiongroupModel1;
        }
    }
}
