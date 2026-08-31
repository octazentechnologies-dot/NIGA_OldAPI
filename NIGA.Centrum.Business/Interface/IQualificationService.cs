using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for qualification related operations
    /// </summary>
    public interface IQualificationService
    {
        /// <summary>
        /// Method is used for to get qualification by qualificationId
        /// </summary>
        /// <param name="qualificationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        QualificationModel GetQualificationById(long qualificationId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the Qualifications
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<QualificationModel> GetQualifications(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Qualification
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveQualification(QualificationModel qualificationModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Qualification.
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteQualification(QualificationModel qualificationModel, ref ErrorResponseModel errorResponseModel);
    }
}
