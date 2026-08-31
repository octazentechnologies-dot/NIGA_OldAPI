using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for questions group related operations
    /// </summary>
    public interface IQuestionGroupService
    {
        /// <summary>
        /// Method is used for to get questiongroup by questiongroupId
        /// </summary>
        /// <param name="questiongroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        QuestionGroupModel GetQuestionGroupById(long questiongroupId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Question Group
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<QuestionGroupModel> GetQuestionGroup(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Question Group
        /// </summary>
        /// <param name="questiongroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveQuestionGroup(QuestionGroupModel questiongroupModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Question Group.
        /// </summary>
        /// <param name="questiongroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteQuestionGroup(QuestionGroupModel questiongroupModel, ref ErrorResponseModel errorResponseModel);
        List<QuestionGroupModel1> GetQuestionGroupExistance(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to GetQuestionGroupByExistanceId .
        /// </summary>
        /// <param name="QuestionSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<QuestionGroupModel1> GetQuestionGroupByExistanceId(long QuestionSectionId, ref ErrorResponseModel errorResponseModel);
    }

}
