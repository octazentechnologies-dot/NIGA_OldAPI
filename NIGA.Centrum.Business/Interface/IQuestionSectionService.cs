using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for question section related operations
    /// </summary>
    public interface IQuestionSectionService
    {
        /// <summary>
        /// Method is used for to get question section by questionsectionId
        /// </summary>
        /// <param name="questionsectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        QuestionSectionModel GetQuestionSectionById(long questionsectionId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the questionsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<QuestionSectionModel> GetQuestionSections(ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Interface is used to save Question Section
        /// </summary>
        /// <param name="questionSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveQuestionSection(QuestionSectionModel questionSectionModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Question Section.
        /// </summary>
        /// <param name="questionSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteQuestionSection(QuestionSectionModel questionSectionModel, ref ErrorResponseModel errorResponseModel);
    }
}

