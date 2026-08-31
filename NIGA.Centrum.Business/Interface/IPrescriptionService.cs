using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IPrescriptionService
    {
        /// <summary>
        /// Method is used for get all the Clipboard Rubrics
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 

        string SavePrescriptionDetail(PrescriptionDetailModel prescriptionDetail, ref ErrorResponseModel errorResponseModel);
        List<PrescriptionRubricDetailViewModel> GetPrescriptionRubricDetail(int appointmentId, ref ErrorResponseModel errorResponseModel);
        List<PrescriptionRemedyDetailViewModel> GetPrescriptionRemedyDetail(int appointmentId, ref ErrorResponseModel errorResponseModel);
        List<PrescriptionRemedyViewModel> GetPrescriptionRemedy(List<int?> rubricList, ref ErrorResponseModel errorResponseModel);

    }
}
