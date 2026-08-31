using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for clinical questions related operations
    /// </summary>
    public interface IClinicalQuestionsService
    {
        /// <summary>
        /// Method is used for to get clinical questions by questionsId
        /// </summary>
        /// <param name="questionsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ClinicalQuestionsModel GetClinicalQuestionsById(long questionsId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Clinical Questions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<ClinicalQuestionsModel> GetClinicalQuestions(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Clinical Questions
        /// </summary>
        /// <param name="clinicalquestionsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveClinicalQuestions(List<ClinicalQuestionsModel> clinicalquestionsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Clinical Questions.
        /// </summary>
        /// <param name="clinicalquestionsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteClinicalQuestions(ClinicalQuestionsModel clinicalquestionsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Clinical Questions by Question group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<ClinicalQuestionsModel> GetQuestionsByGroupId(long QuestionGroupId,ref ErrorResponseModel errorResponseModel);


        List<ClinicalQueKeywordModel> GetQuestionsBySelectedId(long QuestionGroupId, long QuestionSectionId, ref ErrorResponseModel errorResponseModel, long QuestionSubgroupId=0, long BodyPartId = 0);

        string AddEditClinicalQuestionsBodyPart(ClinicalQuestionsBodyPartModel clinicalQuestionsBodyPart, ref ErrorResponseModel errorResponseModel);

        List<QuestionKeyWordBodyPartOutputModel> GetClinicalQuestionsKeyWordBodyPart(QuestionKeyWordBodyPartInputModel questionKeyWordBodyPartInput, ref ErrorResponseModel errorResponseModel);

        List<QuestionKeyWordBodyPartRubricOutputModel> GetClinicalRubricData(QuestionKeyWordBodyPartRubricInputModel questionKeyWordBodyPartRubricInput, ref ErrorResponseModel errorResponseModel);

        string DeleteClinicalQuestionBodyPart(int questionId, int userId, ref ErrorResponseModel errorResponseModel);

        string DeleteClinicalRubricData(int clinicalRubricId, int clinicalQuestionBodyPartId, int qbType, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Clinical Questions & Body part for admin
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<ClinicalQuestionViewModel> GetClinicalQuestionBodyPartList(ref ErrorResponseModel errorResponseModel);

        ClinicalQuestionBodyViewModel GetClinicalQuestionBodyPartDataById(int quetionId, int QBType, ref ErrorResponseModel errorResponseModel);

    }
}
