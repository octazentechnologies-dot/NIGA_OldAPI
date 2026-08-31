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

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the Clinical Questions operations 
    /// </summary>
    public class ClinicalQuestionsService : IClinicalQuestionsService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public ClinicalQuestionsService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get clinical questions by questionsId
        /// </summary>
        /// <param name="questionsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public ClinicalQuestionsModel GetClinicalQuestionsById(long questionsId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var clinicalquestionsEntity = context.ClinicalQuestions.FirstOrDefault(x => x.QuestionsId == questionsId && x.DeleteStatus==false);
            if (clinicalquestionsEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }
            return new ClinicalQuestionsModel
            {
                QuestionsId = clinicalquestionsEntity.QuestionsId,
                QuestionGroupId = clinicalquestionsEntity.QuestionGroupId,
                QuestionSectionId = clinicalquestionsEntity.QuestionSectionId,
                QuestionSubgroupId = clinicalquestionsEntity.QuestionSubgroupId,
                BodyPartId = clinicalquestionsEntity.BodyPartId,
                EnteredDate = clinicalquestionsEntity.EnteredDate,
                EnteredBy = clinicalquestionsEntity.EnteredBy,
                ChangedBy = clinicalquestionsEntity.ChangedBy,
                ChangedDate = clinicalquestionsEntity.ChangedDate,
                DeleteStatus = clinicalquestionsEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the Clinical Questions
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ClinicalQuestionsModel> GetClinicalQuestions( ref ErrorResponseModel errorResponseModel)
        {
            var clinicalquestionsModelList = new List<ClinicalQuestionsModel>();
            errorResponseModel = new ErrorResponseModel();
            var clinicalquestionsEntityList = context.ClinicalQuestions.Where(x => x.DeleteStatus == false).ToList();

            if (clinicalquestionsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }

            clinicalquestionsEntityList.ForEach(item =>
            {
                clinicalquestionsModelList.Add(new ClinicalQuestionsModel
                {
                    QuestionsId = item.QuestionsId,
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionSectionId=item.QuestionSectionId,
                    QuestionSubgroupId = item.QuestionSubgroupId,
                    BodyPartId = item.BodyPartId,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return clinicalquestionsModelList;
        }

        /// <summary>
        /// Method implementation for saving new Clinical Questions
        /// </summary>
        /// <param name="clinicalquestionsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveClinicalQuestions(List<ClinicalQuestionsModel> clinicalquestionsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            foreach (var item in clinicalquestionsModel)
            {
                DateTime currentDateTime = DateTime.Now;
                if (item.QuestionsId == 0)
                {
                    ClinicalQuestions clinicalquestionsEntity = new ClinicalQuestions();
                    clinicalquestionsEntity.QuestionGroupId = item.QuestionGroupId;
                    clinicalquestionsEntity.QuestionSectionId = item.QuestionSectionId;
                    clinicalquestionsEntity.QuestionSubgroupId = item.QuestionSubgroupId;
                    clinicalquestionsEntity.BodyPartId = item.BodyPartId;
                    clinicalquestionsEntity.EnteredBy = item.EnteredBy;
                    clinicalquestionsEntity.EnteredDate = currentDateTime;
                    context.ClinicalQuestions.Add(clinicalquestionsEntity);
                    context.SaveChanges();

                    foreach (var item1 in item.ModelEx)
                    {
                        var modeldetails = new ClinicalQueKeywords();
                        modeldetails.QuestionsId = clinicalquestionsEntity.QuestionsId;
                        modeldetails.KeywordQuestion = item1.KeywordQuestion;
                        modeldetails.IsDeleted = item1.IsDeleted;
                        context.ClinicalQueKeywords.Add(modeldetails);
                        context.SaveChanges();
                     }
                    foreach (var item2 in item.Model1)
                    {
                        var modeldetails = new ClinicalQueRubrics();
                        modeldetails.SubsectionId = item2.SubsectionId;
                        modeldetails.IsDeleted = item2.IsDeleted;
                        context.ClinicalQueRubrics.Add(modeldetails);
                        context.SaveChanges();
                    }

                    Message = "Clinical Questions Saved Successfully";
                }
                else
                {
                    var clinicalquestionsEntity = context.ClinicalQuestions.FirstOrDefault(x => x.QuestionsId == item.QuestionsId);
                    if (clinicalquestionsEntity != null)
                    {
                        clinicalquestionsEntity.QuestionGroupId = item.QuestionGroupId;
                        clinicalquestionsEntity.QuestionSectionId=item.QuestionSectionId;
                        clinicalquestionsEntity.QuestionSubgroupId=item.QuestionSubgroupId;
                        clinicalquestionsEntity.BodyPartId=item.BodyPartId;
                        clinicalquestionsEntity.ChangedBy = item.EnteredBy;
                        clinicalquestionsEntity.ChangedDate = currentDateTime;
                        context.SaveChanges();
                        Message = "Clinical Questions Update Successfully";
                    }
                }
                
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Clinical Questions.
        /// </summary>
        /// <param name="clinicalquestionsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteClinicalQuestions(ClinicalQuestionsModel clinicalquestionsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var clinicalquestionsEntity = context.ClinicalQuestions.FirstOrDefault(x => x.QuestionsId == clinicalquestionsModel.QuestionsId);
            if (clinicalquestionsEntity != null)
            {
                clinicalquestionsEntity.DeleteStatus = clinicalquestionsModel.DeleteStatus;
                clinicalquestionsEntity.ChangedBy = clinicalquestionsModel.EnteredBy;
                clinicalquestionsEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Clinical Questions Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Method imlementation for getting all the question by Qustion Group.
        /// </summary>
        /// <param name="QuestionGroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ClinicalQuestionsModel> GetQuestionsByGroupId(long QuestionGroupId, ref ErrorResponseModel errorResponseModel)
        {
            var clinicalquestionsModelList = new List<ClinicalQuestionsModel>();
            errorResponseModel = new ErrorResponseModel();
            var clinicalquestionsEntityList = context.ClinicalQuestions.Where(x => x.DeleteStatus == false && x.QuestionGroupId== QuestionGroupId).ToList();
            if (clinicalquestionsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }

            clinicalquestionsEntityList.ForEach(item =>
            {
                clinicalquestionsModelList.Add(new ClinicalQuestionsModel
                {
                    QuestionsId = item.QuestionsId,
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionSectionId = item.QuestionSectionId,
                    QuestionSubgroupId = item.QuestionSubgroupId,
                    BodyPartId = item.BodyPartId,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return clinicalquestionsModelList;
        }

        public List<ClinicalQueKeywordModel> GetQuestionsBySelectedId(long QuestionGroupId, long QuestionSectionId, ref ErrorResponseModel errorResponseModel, long QuestionSubgroupId=0, long BodyPartId = 0)
        {
            errorResponseModel = new ErrorResponseModel();
            var clinicalquestionsModelList = new List<ClinicalQueKeywordModel>();
     
            var clinicalquestionsEntity = (from clinic in context.ClinicalQuestions
                                           join key in context.ClinicalQueKeywords on clinic.QuestionsId equals key.QuestionsId
                                           join rub in context.ClinicalQueRubrics on key.ClinicalQueKeywordId equals rub.ClinicalQueKeywordId
                                           join sub in context.SubSectionMaster on rub.SubsectionId equals sub.SubSectionId
                                           join qsubg in context.QuestionSubgroup on clinic.QuestionSubgroupId equals qsubg.QuestionSubgroupId                            
                                           join bopt in context.BodyPartMaster on clinic.BodyPartId equals bopt.BodyPartId 
                                           into b from bo in b.DefaultIfEmpty()


                                           where clinic.QuestionGroupId == QuestionGroupId && clinic.QuestionSectionId == QuestionSectionId 
                                        

                                           //&&

                                          //(QuestionSubgroupId!=0 && clinic.QuestionSubgroupId == QuestionSubgroupId )
                                          //&& (BodyPartId!=0 && clinic.BodyPartId == BodyPartId)



                                           select new
                                           {
                                               key.QuestionsId,
                                               key.KeywordQuestion,
                                               clinic.DeleteStatus,
                                               rub.SubsectionId,
                                               sub.SubSectionName,
                                               clinic.QuestionSubgroupId, 
                                               clinic.BodyPartId,
                                               qsubg.QuestionSubgroup1,
                                               bo.BodyPartName
                                           }

                      ).ToList();
            if (QuestionSubgroupId != 0 ) 
            {
                clinicalquestionsEntity= clinicalquestionsEntity.Where(x=>x.QuestionSubgroupId == QuestionSubgroupId).ToList();
            }
            if (BodyPartId != 0)
            {
                clinicalquestionsEntity = clinicalquestionsEntity.Where(x => x.BodyPartId == BodyPartId).ToList();
            }



            if (clinicalquestionsEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }

            clinicalquestionsEntity.ForEach(item =>
            {
                clinicalquestionsModelList.Add(new ClinicalQueKeywordModel
                {
                    QuestionsId=item.QuestionsId,   
                    KeywordQuestion = item.KeywordQuestion,
                    SubsectionId=item.SubsectionId,
                    SubSectionName=item.SubSectionName,
                    QuestionSubgroupId=item.QuestionSubgroupId,
                    BodyPartId=item.BodyPartId,
                    QuestionSubgroup1=item.QuestionSubgroup1,
                    BodyPartName=item.BodyPartName,
                    IsDeleted = item.DeleteStatus
                });
            });
            return clinicalquestionsModelList;


        }

        /// <summary>
        /// Method implementation for save and update Clinical Questions
        /// </summary>
        /// <param name="clinicalQuestionsBodyPart"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddEditClinicalQuestionsBodyPart(ClinicalQuestionsBodyPartModel clinicalQuestionsBodyPart, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            // QBType ==1 i.e Questions & QBType == 2 i.e. BodyParts

            if (clinicalQuestionsBodyPart.QuestionsId == 0)
            {
                int questionsId = AddClinicalQuestions(clinicalQuestionsBodyPart.QuestionSectionID, clinicalQuestionsBodyPart.QuestionGroupId, clinicalQuestionsBodyPart.QuestionSubGroupID);
                if (clinicalQuestionsBodyPart.QBType == 1)
                {
                    foreach (var questionItem in clinicalQuestionsBodyPart.ClinicalQuestionList)
                    {
                        int clinicalQueKeywordId = AddClinicalQuestionKeyword(questionItem, questionsId);

                        foreach (var questionRubricItem in questionItem.ClinicalQuestionRubricList)
                        {
                            AddRubricForQuestion(questionRubricItem, clinicalQueKeywordId);
                        }
                    }

                    Message = "Clinical Questions Saved Successfully";
                }
                else
                {
                    foreach (var bodyPartItem in clinicalQuestionsBodyPart.ClinicalBodyPartList)
                    {
                        int clinicalQuestionBodypartId= AddClinicalQuestionBodyPart(bodyPartItem, questionsId);

                        foreach (var bodyPartRubricItem in bodyPartItem.ClinicalBodyPartRubricList)
                        {
                            AddRubricForBodyPart(bodyPartRubricItem, clinicalQuestionBodypartId);
                        }
                    }

                    Message = "Clinical Body Part Saved Successfully";
                }
            }
            else
            {

                if (clinicalQuestionsBodyPart.QBType == 1)
                {
                    foreach (var questionItem in clinicalQuestionsBodyPart.ClinicalQuestionList)
                    {
                        int clinicalQueKeywordId= AddClinicalQuestionKeyword(questionItem, clinicalQuestionsBodyPart.QuestionsId);

                        foreach (var questionRubricItem in questionItem.ClinicalQuestionRubricList)
                        {
                            AddRubricForQuestion(questionRubricItem, clinicalQueKeywordId);
                        }
                    }
                    Message = "Clinical Questions Update Successfully";
                }
                else
                {
                    foreach (var bodyPartItem in clinicalQuestionsBodyPart.ClinicalBodyPartList)
                    {
                        int clinicalQuestionBodypartId = AddClinicalQuestionBodyPart(bodyPartItem, clinicalQuestionsBodyPart.QuestionsId);
                        foreach (var bodyPartRubricItem in bodyPartItem.ClinicalBodyPartRubricList)
                        {
                            AddRubricForBodyPart(bodyPartRubricItem, clinicalQuestionBodypartId);
                        }
                    }

                    Message = "Clinical Bodypart Update Successfully";
                }
            }

            return Message;
        }

        private int AddClinicalQuestionBodyPart(ClinicalBodyPartModel bodyPartItem, int questionsId)
        {
            int resultId = 0;
            if (bodyPartItem.ClinicalQuestionBodyPartID == 0)
            {
                var clinicalBodyPart = new ClinicalQuestionBodyPart();
                clinicalBodyPart.QuestionId = questionsId;
                clinicalBodyPart.BodyPartId = bodyPartItem.BodypartID;
                clinicalBodyPart.DeletedStatus = false;
                context.ClinicalQuestionBodyPart.Add(clinicalBodyPart);
                context.SaveChanges();
                resultId = clinicalBodyPart.ClinicalQuestionBodyPartId;
            }
            else
            {
                var bodypartEntity = context.ClinicalQuestionBodyPart.FirstOrDefault(x => x.ClinicalQuestionBodyPartId == bodyPartItem.ClinicalQuestionBodyPartID);
                if (bodypartEntity != null)
                {
                    bodypartEntity.QuestionId = bodyPartItem.QuestionID;
                    bodypartEntity.DeletedStatus = false;   
                    bodypartEntity.BodyPartId=bodyPartItem.BodypartID;
                    context.SaveChanges();
                    resultId = bodypartEntity.ClinicalQuestionBodyPartId;
                }
            }
            return resultId;
        }

        private int AddClinicalQuestionKeyword(ClinicalQuestionModel questionItem, int questionsId)
        {
            int resultId = 0;
            
            if (questionItem.ClinicalQuestionKeywordID == 0)
            {
                var clinicalQuestionKeyword = new ClinicalQueKeywords();
                clinicalQuestionKeyword.QuestionsId = questionsId;
                clinicalQuestionKeyword.KeywordQuestion = questionItem.KeyWords;
                clinicalQuestionKeyword.IsDeleted = false;
                context.ClinicalQueKeywords.Add(clinicalQuestionKeyword);
                context.SaveChanges();
                resultId = clinicalQuestionKeyword.ClinicalQueKeywordId;
            }
            else
            {
                var clinicalQuestionEntity = context.ClinicalQueKeywords.FirstOrDefault(x => x.ClinicalQueKeywordId == questionItem.ClinicalQuestionKeywordID);
                if (clinicalQuestionEntity != null)
                {
                    clinicalQuestionEntity.QuestionsId = questionItem.QuestionID;
                    clinicalQuestionEntity.KeywordQuestion = questionItem.KeyWords;
                    clinicalQuestionEntity.IsDeleted = false;
                    context.SaveChanges();
                    resultId = clinicalQuestionEntity.ClinicalQueKeywordId;
                }
            }
            return resultId;
        }

        private void AddRubricForQuestion(ClinicalQuestionRubricModel clinicalQuestionBodyPartRubric, int? KeywordBodyPartId)
        {
            if (clinicalQuestionBodyPartRubric.ClinicalQuestionRubricID == 0)
            {
                var clinicalRubrics = new ClinicalQueRubrics();
                clinicalRubrics.SubsectionId = clinicalQuestionBodyPartRubric.SubsectionID;
                clinicalRubrics.ClinicalQueKeywordId = KeywordBodyPartId==0? clinicalQuestionBodyPartRubric.ClinicalQuestionKeywordID: KeywordBodyPartId;
                clinicalRubrics.IsDeleted = false;
                context.ClinicalQueRubrics.Add(clinicalRubrics);
                context.SaveChanges();
            }
            else
            {
                var clinicalQuestionRubricEntity = context.ClinicalQueRubrics.FirstOrDefault(x => x.ClinicalQueRubricId == clinicalQuestionBodyPartRubric.ClinicalQuestionRubricID);
                if (clinicalQuestionRubricEntity != null)
                {
                    clinicalQuestionRubricEntity.SubsectionId = clinicalQuestionBodyPartRubric.SubsectionID;
                    clinicalQuestionRubricEntity.IsDeleted = false;
                    clinicalQuestionRubricEntity.ClinicalQueKeywordId = KeywordBodyPartId;
                    context.SaveChanges();
                }
            }
        }

        private void AddRubricForBodyPart(ClinicalBodyPartRubricModel clinicalQuestionBodyPartRubric, int? KeywordBodyPartId)
        {
            if (clinicalQuestionBodyPartRubric.ClinicalQuestionRubricID == 0)
            {
                var clinicalRubrics = new ClinicalQueRubrics();
                clinicalRubrics.SubsectionId = clinicalQuestionBodyPartRubric.SubsectionID;
                clinicalRubrics.ClinicalQuestionBodyPartId = KeywordBodyPartId == 0 ? clinicalQuestionBodyPartRubric.ClinicalQuestionBodyPartID : KeywordBodyPartId;
                clinicalRubrics.IsDeleted = false;
                context.ClinicalQueRubrics.Add(clinicalRubrics);
                context.SaveChanges();
            }
            else
            {
                var clinicalBodypartRubricEntity = context.ClinicalQueRubrics.FirstOrDefault(x => x.ClinicalQueRubricId == clinicalQuestionBodyPartRubric.ClinicalQuestionRubricID);
                if (clinicalBodypartRubricEntity != null)
                {
                    clinicalBodypartRubricEntity.SubsectionId = clinicalQuestionBodyPartRubric.SubsectionID;
                    clinicalBodypartRubricEntity.IsDeleted = false;
                    clinicalBodypartRubricEntity.ClinicalQuestionBodyPartId = KeywordBodyPartId;
                    context.SaveChanges();
                }
            }
        }

        private int AddClinicalQuestions(int questionSectionID, int? questionGroupId, int? questionSubGroupID)
        {
            var clinicalQuestion = new ClinicalQuestions();
            clinicalQuestion.QuestionSectionId = questionSectionID;
            clinicalQuestion.QuestionGroupId = questionGroupId;
            clinicalQuestion.QuestionSubgroupId = questionSubGroupID;
            clinicalQuestion.DeleteStatus = false;
            context.ClinicalQuestions.Add(clinicalQuestion);
            context.SaveChanges();

            return clinicalQuestion.QuestionsId;
        }

       
        public List<ClinicalQuestionViewModel> GetClinicalQuestionBodyPartList(ref ErrorResponseModel errorResponseModel)
        {
            var clinicalQuestionViewList = new List<ClinicalQuestionViewModel>();
            errorResponseModel = new ErrorResponseModel();

            clinicalQuestionViewList = (from clinicalQuestion in context.ClinicalQuestions
                                        join questionGroup in context.QuestionGroupMaster on clinicalQuestion.QuestionGroupId equals questionGroup.QuestionGroupId
                                        join questionSection in context.QuestionSectionMaster on clinicalQuestion.QuestionSectionId equals questionSection.QuestionSectionId
                                        join questionSubgroup in context.QuestionSubgroup on clinicalQuestion.QuestionSubgroupId equals questionSubgroup.QuestionSubgroupId
                                        where clinicalQuestion.DeleteStatus == false
                                        select new ClinicalQuestionViewModel
                                        {
                                            QuestionsId = clinicalQuestion.QuestionsId,
                                            QuestionGroupId = clinicalQuestion.QuestionGroupId,
                                            QuestionSectionId = clinicalQuestion.QuestionSectionId,
                                            QuestionSubgroupId = clinicalQuestion.QuestionSubgroupId,
                                            QuestionGroupName = questionGroup.QuestionGroupName,
                                            QuestionSectionName = questionSection.QuestionSectionName,
                                            QuestionSubgroupName = questionSubgroup.QuestionSubgroup1,
                                        }).ToList();

            if (clinicalQuestionViewList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }

            return clinicalQuestionViewList;
        }

        public ClinicalQuestionBodyViewModel GetClinicalQuestionBodyPartDataById(int quetionId, int QBType, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var result = new ClinicalQuestionBodyViewModel();

            var clinicalQuestionBodyPart=context.ClinicalQuestions.FirstOrDefault(x=>x.QuestionsId == quetionId);

            result.QuestionsId = clinicalQuestionBodyPart.QuestionsId; 
            result.QuestionGroupId = clinicalQuestionBodyPart.QuestionGroupId;
            result.QuestionSectionId = clinicalQuestionBodyPart.QuestionSectionId;
            result.QuestionSubgroupId = clinicalQuestionBodyPart.QuestionSubgroupId;

            if (QBType == 1)
            {
                var clinicalQuestion = (from clinicalQuestionKey in context.ClinicalQueKeywords
                                        where clinicalQuestionKey.QuestionsId == quetionId && clinicalQuestionKey.IsDeleted==false
                                        select new ClinicalQuestionBodyPartRubricViewModel
                                        {
                                            ClinicalQueKeywordId = clinicalQuestionKey.ClinicalQueKeywordId,
                                            KeywordQuestion = clinicalQuestionKey.KeywordQuestion,
                                            ClinicalRubricViewList= ( from cliniclRubric in context.ClinicalQueRubrics
                                                                      join subsection in context.SubSectionMaster on cliniclRubric.SubsectionId equals subsection.SubSectionId
                                                                      where cliniclRubric.ClinicalQueKeywordId == clinicalQuestionKey.ClinicalQueKeywordId && cliniclRubric.IsDeleted==false
                                                                      select new ClinicalRubricViewModel
                                                                      {
                                                                          ClinicalQuestionRubricID = cliniclRubric.ClinicalQueRubricId,
                                                                          ClinicalQuestionKeywordID = cliniclRubric.ClinicalQueKeywordId,
                                                                          SubsectionID =cliniclRubric.SubsectionId,
                                                                          SubsectionName = subsection.SubSectionName,
                                                                      }).ToList()
                                        }).ToList();
                result.ClinicalQuestionBodyPartViewList = clinicalQuestion;
            }
            else
            {
            
                var clinicalBodyPart = (from clinicalQuestionBodyPart_ in context.ClinicalQuestionBodyPart
                                        join bodyPart in context.BodyPartMaster on clinicalQuestionBodyPart_.BodyPartId equals bodyPart.BodyPartId  
                                        where clinicalQuestionBodyPart_.QuestionId == quetionId && clinicalQuestionBodyPart_.DeletedStatus == false
                                        select new ClinicalQuestionBodyPartRubricViewModel
                                        {
                                            ClinicalQuestionBodyPartId = clinicalQuestionBodyPart_.ClinicalQuestionBodyPartId,
                                            BodyPartId = clinicalQuestionBodyPart_.BodyPartId,
                                            SectionId = bodyPart.SectionId,
                                            BodyPartName = bodyPart.BodyPartName,
                                            ClinicalRubricViewList = (from cliniclRubric in context.ClinicalQueRubrics
                                                                      join subsection in context.SubSectionMaster on cliniclRubric.SubsectionId equals subsection.SubSectionId
                                                                      where cliniclRubric.ClinicalQuestionBodyPartId == clinicalQuestionBodyPart_.ClinicalQuestionBodyPartId && cliniclRubric.IsDeleted == false
                                                                      select new ClinicalRubricViewModel
                                                                      {
                                                                          ClinicalQuestionRubricID = cliniclRubric.ClinicalQueRubricId,
                                                                          ClinicalQuestionBodyPartID = cliniclRubric.ClinicalQuestionBodyPartId,
                                                                          SubsectionID = cliniclRubric.SubsectionId,
                                                                          SubsectionName = subsection.SubSectionName,
                                                                      }).ToList()
                                        }).ToList();
                result.ClinicalQuestionBodyPartViewList = clinicalBodyPart;

            }

            return result;
        }

        /// <summary>
        /// Method is used for delete Clinical Questions.
        /// </summary>
        /// <param name="clinicalquestionsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteClinicalQuestionBodyPart(int questionId,int userId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var clinicalquestionsEntity = context.ClinicalQuestions.FirstOrDefault(x => x.QuestionsId == questionId);
            if (clinicalquestionsEntity != null)
            {
                clinicalquestionsEntity.DeleteStatus = true;
               // clinicalquestionsEntity.ChangedBy = userId;
                clinicalquestionsEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Clinical Questions Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete Clinical rubric.
        /// </summary>
        /// <param name="clinicalRubricId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteClinicalRubricData(int clinicalRubricId,int clinicalQuestionBodyPartId,int qbType, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";

            if (clinicalRubricId > 0)
            {
                var clinicalRubricEntity = context.ClinicalQueRubrics.FirstOrDefault(x => x.ClinicalQueRubricId == clinicalRubricId);
                if (clinicalRubricEntity != null)
                {
                    clinicalRubricEntity.IsDeleted = true;
                    context.SaveChanges();
                    Message = "Clinical Question Rubric Deleted Successfully";
                }
                else
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Clinical Question Rubric Not Found";
                }
            }
            else
            {
                if (qbType == 1)
                {
                    var clinicalQuestionEntity = context.ClinicalQueKeywords.FirstOrDefault(x => x.ClinicalQueKeywordId == clinicalQuestionBodyPartId);
                    if (clinicalQuestionEntity != null)
                    {
                        clinicalQuestionEntity.IsDeleted = true;
                        context.SaveChanges();
                        Message = "Clinical Question Deleted Successfully";
                    }
                    else
                    {
                        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                        errorResponseModel.Message = "Clinical Question Not Found";
                    }
                }
                else
                {
                    var clinicalBodyPartEntity = context.ClinicalQuestionBodyPart.FirstOrDefault(x => x.ClinicalQuestionBodyPartId == clinicalQuestionBodyPartId);
                    if (clinicalBodyPartEntity != null)
                    {
                        clinicalBodyPartEntity.DeletedStatus = true;
                        context.SaveChanges();
                        Message = "Clinical Body Part Deleted Successfully";
                    }
                    else
                    {
                        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                        errorResponseModel.Message = "Clinical Body Part Not Found";
                    }
                }
            
            }


            
            return Message;
        }




        //Doctor Side

        public List<QuestionKeyWordBodyPartOutputModel> GetClinicalQuestionsKeyWordBodyPart(QuestionKeyWordBodyPartInputModel questionKeyWordBodyPartInput, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questionKeyWordBodyPartOutputList = new List<QuestionKeyWordBodyPartOutputModel>();

            if (questionKeyWordBodyPartInput.requestType.Equals("Question"))
            {
                questionKeyWordBodyPartOutputList = (from clinicalQuestion in context.ClinicalQuestions
                                                     join questionKeyword in context.ClinicalQueKeywords on clinicalQuestion.QuestionsId equals questionKeyword.QuestionsId
                                                     where clinicalQuestion.QuestionSectionId == questionKeyWordBodyPartInput.QuestionSectionID &&
                                                     clinicalQuestion.QuestionGroupId == questionKeyWordBodyPartInput.QuestionGroupId &&
                                                     clinicalQuestion.QuestionSubgroupId == questionKeyWordBodyPartInput.QuestionSubGroupId &&
                                                     clinicalQuestion.DeleteStatus == false && questionKeyword.IsDeleted == false
                                                     select new QuestionKeyWordBodyPartOutputModel
                                                     {
                                                         QuestionKeyWordBodyPartID = questionKeyword.ClinicalQueKeywordId,
                                                         QuestionKeyWordBodyPart = questionKeyword.KeywordQuestion
                                                     }

                                                   ).ToList();
            }
            else if (questionKeyWordBodyPartInput.requestType.Equals("Bodypart"))
            {
                questionKeyWordBodyPartOutputList = (from clinicalQuestion in context.ClinicalQuestions
                                                     join clinicalQuestionBodyPart in context.ClinicalQuestionBodyPart on clinicalQuestion.QuestionsId equals clinicalQuestionBodyPart.QuestionId
                                                     join bodyPart in context.BodyPartMaster on clinicalQuestionBodyPart.BodyPartId equals bodyPart.BodyPartId
                                                     where clinicalQuestion.QuestionSectionId == questionKeyWordBodyPartInput.QuestionSectionID &&
                                                     clinicalQuestion.QuestionGroupId == questionKeyWordBodyPartInput.QuestionGroupId &&
                                                     clinicalQuestion.QuestionSubgroupId == questionKeyWordBodyPartInput.QuestionSubGroupId &&
                                                     clinicalQuestion.DeleteStatus == false && clinicalQuestionBodyPart.DeletedStatus == false
                                                     select new QuestionKeyWordBodyPartOutputModel
                                                     {
                                                         QuestionKeyWordBodyPartID = clinicalQuestionBodyPart.ClinicalQuestionBodyPartId,
                                                         BodyPartID = bodyPart.BodyPartId,
                                                         QuestionKeyWordBodyPart = bodyPart.BodyPartName
                                                     }

                                                   ).ToList();
            }

            if (questionKeyWordBodyPartOutputList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = questionKeyWordBodyPartInput.requestType.Equals("Question") ? "Clinical Questions not found" : "Clinical Body Part not found";
            }
            return questionKeyWordBodyPartOutputList;
        }

        public List<QuestionKeyWordBodyPartRubricOutputModel> GetClinicalRubricData(QuestionKeyWordBodyPartRubricInputModel questionKeyWordBodyPartRubricInput, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questionKeyRubricList = new List<QuestionKeyWordBodyPartRubricOutputModel>();

            if (questionKeyWordBodyPartRubricInput.RequestType.Equals("Question"))
            {
                questionKeyRubricList = (from clinicalRubric in context.ClinicalQueRubrics
                                         join subSection in context.SubSectionMaster on clinicalRubric.SubsectionId equals subSection.SubSectionId
                                         where clinicalRubric.ClinicalQueKeywordId == questionKeyWordBodyPartRubricInput.QuestionKeyWordBodyPartID &&
                                         clinicalRubric.IsDeleted == false && subSection.DeleteStatus == false
                                         select new QuestionKeyWordBodyPartRubricOutputModel
                                         {
                                             SubsectionId = clinicalRubric.SubsectionId,
                                             SubsectionName = subSection.SubSectionName
                                         }

                                                   ).ToList();
            }
            else if (questionKeyWordBodyPartRubricInput.RequestType.Equals("Bodypart"))
            {
                questionKeyRubricList = (from clinicalRubric in context.ClinicalQueRubrics
                                         join subSection in context.SubSectionMaster on clinicalRubric.SubsectionId equals subSection.SubSectionId
                                         where clinicalRubric.ClinicalQuestionBodyPartId == questionKeyWordBodyPartRubricInput.QuestionKeyWordBodyPartID &&
                                         clinicalRubric.IsDeleted == false && subSection.DeleteStatus == false
                                         select new QuestionKeyWordBodyPartRubricOutputModel
                                         {
                                             SubsectionId = clinicalRubric.SubsectionId,
                                             SubsectionName = subSection.SubSectionName
                                         }

                                                   ).ToList();
            }

            if (questionKeyRubricList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = questionKeyWordBodyPartRubricInput.RequestType.Equals("Question") ? "Clinical Questions not found" : "Clinical Body Part not found";
            }
            return questionKeyRubricList;
        }

    }
}
