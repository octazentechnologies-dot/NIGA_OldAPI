using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for EnquiryDetail related operations
    /// </summary>
    public interface IEnquiryDetailService
    {
        /// <summary>
        /// Method is used for to get EnquiryDetail by enquiryId
        /// </summary>
        /// <param name="enquiryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        EnquiryDetailModel GetEnquiryDetailById(long enquiryId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the EnquiryDetails
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<EnquiryDetailModel> GetAllEnquiryDetails(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save/update EnquiryDetails
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveEnquiryDetail(EnquiryDetailModel model, SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate EnquiryDetail.
        /// </summary>
        /// <param name="enquiryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteEnquiryDetail(long enquiryId, ref ErrorResponseModel errorResponseModel);

    }
}
