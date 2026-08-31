using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IQuestionSubGroupService
    {
        /// <summary>
        /// Method is used for to get QuestionSubGroup by questionSubgroupId
        /// </summary>
        /// <param name="questionSubgroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        QuestionSubGroupModel GetQuestionSubGroupById(long questionSubgroupId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Method is used for get all the QuestionSubGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<QuestionSubGroupModel> GetQuestionSubGroup(ref ErrorResponseModel errorResponseModel);

        /// <summary>remedyId
        /// Interface is used to save QuestionSubGroup
        /// </summary>
        /// <param name="questionSubGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate QuestionSubGroup.
        /// </summary>
        /// <param name="questionSubGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteQuestionSubGroup(QuestionSubGroupModel questionSubGroupModel, ref ErrorResponseModel errorResponseModel);
    }
}
