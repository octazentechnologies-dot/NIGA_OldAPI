using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Created Date    :   10-March-2020
/// Purpose         :   Interface for RubricRemedyDetails
/// </summary>
namespace NIGA.Centrum.Business.Interface
{
    public interface IRubricRemedyDetailsService
    {
        /// <summary>
        /// Method defination for saving rubric remedy details.
        /// </summary>
        /// <param name="rubricRemedyDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveRubricRemedyDetails(List<RubricRemedyDetailsModel> rubricRemedyDetailsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method defination for getting rubric remedy details
        /// </summary>
        /// <param name="RemedyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RemedyRubricViewModel GetRubricRemedyDetails(long RemedyId, ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// GetRubricRemedyDetails
        /// </summary>
        /// <param name="subSectionId"></param> 
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RemedyCountsModel GetRemedyCounts(int subSectionId,ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// GetRubricList
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<RubricModel> GetRubricList(int SectionId,NigaParameters nigaParameters,ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Get Grade remedies from subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<GradeRemediesModel> GetGradeRemedies(int subSectionId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get details to edit rubric remedies
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        RubricRemedyDetailsModel GetRemedyDetailsToEdit(int subSectionId,int grade, ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// Method is used for get all the SubSections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<RubricRemedyViewModel1> GetSubSections(int sectionId, ref ErrorResponseModel errorResponseModel);


        RubricRemedyDetailModel GetRubricRemedyBySectionGread(int subSectionId, int greadId, ref ErrorResponseModel errorResponseModel);

        string SaveUpdateRubricRemedy(RubricRemedyDetailModel rubricRemedyDetail, int userId, ref ErrorResponseModel errorResponseModel);

        string DeleteRubricRemedyAuthor(RubricRemedyDeleteModel rubricRemedyDeleteModel, ref ErrorResponseModel errorResponseModel);

        string UpdateIsSmallRubric(int rubricRemedyID, bool isSmallRubric, ref ErrorResponseModel errorResponseModel);

        string UpdateIsConfirmationRubric(int rubricRemedyID, bool isConformationRubric, ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Get Grade remedies from subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<RemediesModel> GetGradeRemedies1(int subSectionId, ref ErrorResponseModel errorResponseModel);

        RubricDetailModel GetRubricDetails(int subSectionId, ref ErrorResponseModel errorResponseModel, bool includeAuthors = true);



    }
}
