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
    /// This is implementation for the ClinicalQueKeyword operations 
    /// </summary>
    public class ClinicalQueKeywordService : IClinicalQueKeywordService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public ClinicalQueKeywordService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get ClinicalQueKeyword by ClinicalQueKeywordId
        /// </summary>
        /// <param name="ClinicalQueKeywordId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public ClinicalQueKeywordsModel GetClinicalQueKeywordById(long ClinicalQueKeywordId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var quekeywordEntity = context.ClinicalQueKeywords.FirstOrDefault(x => x.ClinicalQueKeywordId == ClinicalQueKeywordId && x.IsDeleted==false);
            if (quekeywordEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "ClinicalQueKeyword not found";
            }
            return new ClinicalQueKeywordsModel
            {
                ClinicalQueKeywordId = quekeywordEntity.ClinicalQueKeywordId,
                QuestionsId = quekeywordEntity.QuestionsId,
                KeywordQuestion = quekeywordEntity.KeywordQuestion,
                IsDeleted = quekeywordEntity.IsDeleted,
               
            };
        }

        /// <summary>
        /// Method to get all the ClinicalQueKeyword
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public List<ClinicalQueKeywordsModel> GetClinicalQueKeyword(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var quekeywordModelList = new List<ClinicalQueKeywordsModel>();
            var quekeywordEntityList = context.ClinicalQueKeywords.Where(x => x.IsDeleted == false).ToList();

            if (quekeywordEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "ClinicalQueKeywords not found";
            }
            quekeywordEntityList.ForEach(item =>
            {
                quekeywordModelList.Add(new ClinicalQueKeywordsModel
                {
                    ClinicalQueKeywordId = item.ClinicalQueKeywordId,
                    QuestionsId = item.QuestionsId,
                    KeywordQuestion = item.KeywordQuestion,
                    IsDeleted = item.IsDeleted,
                    
                });
            });
            return quekeywordModelList;
        }

        /// <summary>
        /// Method implementation for saving new ClinicalQueKeywords
        /// </summary>
        /// <param name="quekeywordModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (quekeywordModel.ClinicalQueKeywordId == 0)
            {
                ClinicalQueKeywords quekeywordEntity = new ClinicalQueKeywords();
                quekeywordEntity.ClinicalQueKeywordId = quekeywordModel.ClinicalQueKeywordId;
                quekeywordEntity.QuestionsId = quekeywordModel.QuestionsId;
                quekeywordEntity.KeywordQuestion = quekeywordModel.KeywordQuestion;
                quekeywordEntity.IsDeleted = quekeywordModel.IsDeleted;
                context.ClinicalQueKeywords.Add(quekeywordEntity);
                context.SaveChanges();
                Message = "ClinicalQueKeywords Saved Successfully";
            }
            else
            {
                var quekeywordEntity = context.ClinicalQueKeywords.FirstOrDefault(x => x.ClinicalQueKeywordId == quekeywordModel.ClinicalQueKeywordId);
                if (quekeywordEntity != null)
                {

                    quekeywordEntity.ClinicalQueKeywordId = quekeywordModel.ClinicalQueKeywordId;
                    quekeywordEntity.QuestionsId = quekeywordModel.QuestionsId;
                    quekeywordEntity.KeywordQuestion = quekeywordModel.KeywordQuestion;
                    quekeywordEntity.IsDeleted = quekeywordModel.IsDeleted;
                    context.SaveChanges();
                    Message = "ClinicalQueKeywords Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Interface is used to deactivate ClinicalQueKeyword.
        /// </summary>
        /// <param name="quekeywordModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var quekeywordEntity = context.ClinicalQueKeywords.FirstOrDefault(x => x.ClinicalQueKeywordId == quekeywordModel.ClinicalQueKeywordId);
            if (quekeywordEntity != null)
            {
                quekeywordEntity.IsDeleted = quekeywordModel.IsDeleted;
                context.SaveChanges();
                Message = " ClinicalQueKeywords Deleted Successfully";
            }
            return Message;
        }
    }
}
