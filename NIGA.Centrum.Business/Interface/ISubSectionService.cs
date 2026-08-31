using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for subsection related operations
    /// </summary>
    public interface ISubSectionService
    {
        /// <summary>
        /// Method is used for to get subsection by subsectionId
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        SubSectionModel GetSubSectionById(long subsectionId, ref ErrorResponseModel errorResponseModel);


        List<SubSectionLevelModel> GetSubSectionWithChildrenCount(long subsectionId, ref ErrorResponseModel errorResponseModel);

        List<SubSectionLevelModel> GetMainParentSubSectionsWithChildCount(long sectionId,ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Method is used for get all the SubSections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<SubSectionModel> GetSubSections(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save SubSection
        /// </summary>
        /// <param name="subSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveSubSection(List<SubSectionModel> subSectionModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate SubSection.
        /// </summary>
        /// <param name="subSectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteSubSection(SubSectionModel subSectionModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to get subsection as sections from section id
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<SectionModel> GetSubSectionsBySection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel);


        /// <summary>
        /// Method is used for get all the SubSections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<SubSectionModel> GetSubSections(int sectionId,NigaParameters nigaParameters);

        List<SubSection> GetSubSectionsByDate(int userId, ref ErrorResponseModel errorResponseModel);






        /// <summary>
        /// Interface is used to deactivate Author.
        /// </summary>
        /// <param name="subSectionLanguageDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteSubSectionLanguageDetails(SubSectionLanguageDetailsModel subSectionLanguageDetailsModel, ref ErrorResponseModel errorResponseModel);





        /// <summary>
        /// Interface is used to deactivate Author.
        /// </summary>
        /// <param name="referenceRubricDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteReferenceRubricDetails(ReferenceRubricDetailsModel referenceRubricDetailsModel, ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// Method is used for get all the SubSections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        PaginationResult GetSubSectionsWithPagination(int sectionId, NigaParameters nigaParameters);

        /// <summary>
        /// Method is used to update MainParentSubsection against subsectionId
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <param name="mainParentSubsection"></param>
        /// <param name="changedBy"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string UpdateMainParentSubsection(long subsectionId, bool mainParentSubsection, string changedBy, ref ErrorResponseModel errorResponseModel);


        Task<List<SubSectionSearchResponse>> SearchAsync(string query, int top);

        Task<List<SubSectionSearchResultModel>> SearchBySectionAsync(long sectionId, string query, int top);

        Task<List<SubSectionSearchResultModel>> SearchGlobalAsync(string query, int top);

        Task<SubSectionSearchPagedResultModel> SearchBySectionPagedAsync(long sectionId, string query, int pageNumber, int pageSize);

        Task<SubSectionSearchPagedResultModel> SearchGlobalPagedAsync(string query, int pageNumber, int pageSize);
    }





}
