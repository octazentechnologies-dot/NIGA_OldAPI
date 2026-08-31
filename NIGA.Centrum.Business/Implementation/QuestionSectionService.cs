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
    /// This is implementation  for the question section operations 
    /// </summary>
   public class QuestionSectionService : IQuestionSectionService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public QuestionSectionService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get question section by questionsectionId
        /// </summary>
        /// <param name="questionsectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public QuestionSectionModel GetQuestionSectionById(long questionsectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questionsectionEntity = context.QuestionSectionMaster.FirstOrDefault(x => x.QuestionSectionId == questionsectionId && !x.DeleteStatus);
            if (questionsectionEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Section not found";
            }
            return new QuestionSectionModel
            {
                QuestionSectionId = questionsectionEntity.QuestionSectionId,
                QuestionSectionName = questionsectionEntity.QuestionSectionName,
                Description = questionsectionEntity.Desciption,
                EnteredDate = questionsectionEntity.EnteredDate,
                EnteredBy = questionsectionEntity.EnteredBy,
                ChangedBy = questionsectionEntity.ChangedBy,
                ChangedDate = questionsectionEntity.ChangedDate,
                DeleteStatus = questionsectionEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the questionsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<QuestionSectionModel> GetQuestionSections(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questionsectionModelList = new List<QuestionSectionModel>();
            var questionsectionEntityList = context.QuestionSectionMaster.Where(x => x.DeleteStatus == false).ToList();
            if (questionsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Section not found";
            }
            questionsectionEntityList.ForEach(item =>
            {
                questionsectionModelList.Add(new QuestionSectionModel
                {
                    QuestionSectionId = item.QuestionSectionId,
                    QuestionSectionName = item.QuestionSectionName,
                    Description = item.Desciption,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return questionsectionModelList;
        }

        /// <summary>
        /// Method implementation for saving new Question Section
        /// </summary>
        /// <param name="questionSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveQuestionSection(QuestionSectionModel questionSectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (questionSectionModel.QuestionSectionId == 0)
            {
                QuestionSectionMaster questionsectionEntity = new QuestionSectionMaster();
                questionsectionEntity.QuestionSectionName = questionSectionModel.QuestionSectionName;
                questionsectionEntity.Desciption = questionSectionModel.Description;
                questionsectionEntity.EnteredBy = questionSectionModel.EnteredBy;
                questionsectionEntity.EnteredDate = DateTime.Now;
                context.QuestionSectionMaster.Add(questionsectionEntity);
                context.SaveChanges();
                Message = "Question Section Saved Successfully";
            }
            else
            {
                var questionsectionEntity = context.QuestionSectionMaster.FirstOrDefault(x => x.QuestionSectionId == questionSectionModel.QuestionSectionId);
                if (questionsectionEntity != null)
                {
                    questionsectionEntity.QuestionSectionName = questionSectionModel.QuestionSectionName;
                    questionsectionEntity.Desciption = questionSectionModel.Description;
                    questionsectionEntity.ChangedBy = questionSectionModel.EnteredBy;
                    questionsectionEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Question Section Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete question Section.
        /// </summary>
        /// <param name="diagnosisModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteQuestionSection(QuestionSectionModel questionSectionModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var questionsectionEntity = context.QuestionSectionMaster.FirstOrDefault(x => x.QuestionSectionId == questionSectionModel.QuestionSectionId);
            if (questionsectionEntity != null)
            {
                questionsectionEntity.DeleteStatus = questionSectionModel.DeleteStatus;
                questionsectionEntity.ChangedBy = questionSectionModel.EnteredBy;
                questionsectionEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Question Section Deleted Successfully";
            }
            return Message;
        }
    }
}
