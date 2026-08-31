using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for Remedy Grade related operations
    /// </summary>
    public interface IRemedyGradeService
    {
        /// <summary>
        /// Method is used for to get Remedy Grade by RemedyGradeId
        /// </summary>
        /// <param name="remedygradeId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RemedyGradeModel GetRemedyGradeById(long remedygradeId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Remedy Grades
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<RemedyGradeModel> GetRemedyGrades(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Remedy Grade
        /// </summary>
        /// <param name="remedyGradeModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveRemedyGrade(RemedyGradeModel remedyGradeModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Remedy Grade.
        /// </summary>
        /// <param name="remedyGradeModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteRemedyGrade(RemedyGradeModel remedyGradeModel, ref ErrorResponseModel errorResponseModel);
    }
}
