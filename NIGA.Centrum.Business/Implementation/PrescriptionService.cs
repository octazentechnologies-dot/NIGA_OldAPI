using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    public class PrescriptionService : IPrescriptionService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PrescriptionService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to save prescription Rubric Detail
        /// </summary>
        /// <param name="prescriptionRubricDetail"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// 

        public string SavePrescriptionDetail(PrescriptionDetailModel prescriptionDetail, ref ErrorResponseModel errorResponseModel)
        {
            string Mesaage = "";

            foreach (var item in prescriptionDetail.PrescriptionRubricDetailList)
            {
                var prescriptionRubricDetailEntity = new PrescriptionRubricDetail();
                prescriptionRubricDetailEntity.AppointmentId = prescriptionDetail.AppointmentId;
                prescriptionRubricDetailEntity.RubricId = item.RubricId;
                prescriptionRubricDetailEntity.IntensityId = item.IntensityId;
                prescriptionRubricDetailEntity.RemedyCount = item.RemedyCount;
                prescriptionRubricDetailEntity.DeletedStatus = false;
                prescriptionRubricDetailEntity.CreatedDate = DateTime.Now;
                context.PrescriptionRubricDetail.Add(prescriptionRubricDetailEntity);
                context.SaveChanges();
            }

            foreach (var item in prescriptionDetail.PrescriptionRemedyDetailList)
            {
                var prescriptionRemedyDetailEntity = new PrescriptionRemedyDetail();
                prescriptionRemedyDetailEntity.AppointmentId = prescriptionDetail.AppointmentId;
                prescriptionRemedyDetailEntity.RemedyId = item.RemedyId;
                prescriptionRemedyDetailEntity.Description = item.Description;
                prescriptionRemedyDetailEntity.DeletedStatus = false;
                prescriptionRemedyDetailEntity.CreatedDate = DateTime.Now;
                context.PrescriptionRemedyDetail.Add(prescriptionRemedyDetailEntity);
                context.SaveChanges();
            }

            Mesaage = "Record Saved Successfully";
            
            return Mesaage;
        }

        /// <summary>
        ///  Method implementation for getting Prescription Rubric Detail
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PrescriptionRubricDetailViewModel> GetPrescriptionRubricDetail(int appointmentId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var prescriptionRubricDetailData = (from prescriptionRubricDetail in context.PrescriptionRubricDetail
                                           join subSection in context.SubSectionMaster on prescriptionRubricDetail.RubricId equals subSection.SubSectionId
                                           where prescriptionRubricDetail.AppointmentId == appointmentId
                                           select new PrescriptionRubricDetailViewModel()
                                           {
                                                PrescriptionRubricId = prescriptionRubricDetail.PrescriptionRubricId,
                                                AppointmentId = prescriptionRubricDetail.AppointmentId,
                                                RubricId = prescriptionRubricDetail.RubricId,
                                                RubricName = subSection.SubSectionName,
                                                IntensityId = prescriptionRubricDetail.IntensityId,
                                                RemedyCount = prescriptionRubricDetail.RemedyCount
                                           }).ToList();


            if (prescriptionRubricDetailData.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }

            return prescriptionRubricDetailData;
        }

        /// <summary>
        ///  Method implementation for getting Prescription Remedy Detail
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PrescriptionRemedyDetailViewModel> GetPrescriptionRemedyDetail(int appointmentId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var prescriptionRemedyDetailData = (from prescriptionRemedyDetail in context.PrescriptionRemedyDetail
                                                join remedy in context.RemedyMaster on prescriptionRemedyDetail.RemedyId equals remedy.RemedyId
                                                where prescriptionRemedyDetail.AppointmentId == appointmentId
                                                select new PrescriptionRemedyDetailViewModel()
                                                {
                                                    PrescriptionRemedyId = prescriptionRemedyDetail.PrescriptionRemedyId,
                                                    AppointmentId = prescriptionRemedyDetail.AppointmentId,
                                                    RemedyId = prescriptionRemedyDetail.RemedyId,
                                                    RemedyName = remedy.RemedyName,
                                                    Description = prescriptionRemedyDetail.Description,
                                                    Dose = prescriptionRemedyDetail.Dose
                                                }).ToList();

            if (prescriptionRemedyDetailData.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }

            return prescriptionRemedyDetailData;
        }

        /// <summary>
        ///  Method implementation for getting Prescription Remedy list
        /// </summary>
        /// <param name="rubricList"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PrescriptionRemedyViewModel> GetPrescriptionRemedy(List<int?> rubricList, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var remedyEntity = (from rubricRemedy in  context.RubricRemedyDetails 
                                          join remedy in context.RemedyMaster on rubricRemedy.RemedyId equals remedy.RemedyId
                                          where rubricList.Contains(rubricRemedy.SubSectionId) && rubricRemedy.DeletedStatus==false
                                          select new PrescriptionRemedyViewModel
                                          {
                                            RemedyId = remedy.RemedyId,
                                            RemedyName = remedy.RemedyName,
                                          }).OrderBy(rem => rem.RemedyId).ToList();

            if (remedyEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }

            return remedyEntity.GroupBy(x => x.RemedyId).Select(x => x.FirstOrDefault()).ToList();
        }
    }
}
