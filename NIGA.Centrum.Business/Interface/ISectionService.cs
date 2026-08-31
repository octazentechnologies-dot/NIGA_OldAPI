using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for section related operations
    /// </summary>
    public interface ISectionService
    {
        /// <summary>
        /// Method is used for to get section by sectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        SectionModel GetSectionById(long sectionId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all  sections snd subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<SectionModel> getAllSections(ref ErrorResponseModel errorResponseModel);

        List<SectionModel> getAllRemedyByFilter(string search, int SectionId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Section
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveSection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Section.
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteSection(SectionModel sectionModel, ref ErrorResponseModel errorResponseModel);
    }
}
