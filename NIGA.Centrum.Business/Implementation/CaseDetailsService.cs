using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Internal;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace NIGA.Centrum.Business.Implementation
{
    public class CaseDetailsService : ICaseDetailsService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public CaseDetailsService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

       




        /// <summary>
        /// Method implementation for saving Case details.
        /// </summary>
        /// <param name="casedetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string SaveCaseDetails(List<CaseDetailsModel> casedetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var existingDetails = context.CaseDetails.Where(x => x.CaseId == casedetailsModel[0].CaseId
                                                            ).ToList();

            //foreach (var item in casedetailsModel)
            // {
            //     var caseDetailsEntity = new CaseDetails();
            //    // caseDetailsEntity.CaseDetailId = item.CaseDetailId;
            //     caseDetailsEntity.SubsectionId = item.SubsectionId;
            //     caseDetailsEntity.CaseId = item.CaseId;
            //     caseDetailsEntity.IntensityId = item.IntensityId;
            //     caseDetailsEntity.RemedyCount = item.RemedyCount;
            //     context.CaseDetails.Add(caseDetailsEntity);
            //     context.SaveChanges();

            // }

            
            foreach (var item in casedetailsModel)
            {
                var caseDetailsEntity = new CaseDetails();

                if (item.CaseDetailId == 0)
                {
                   // var caseDetailsEntity = new CaseDetails();
                     caseDetailsEntity.CaseDetailId = item.CaseDetailId;
                    caseDetailsEntity.SubsectionId = item.SubsectionId;
                    caseDetailsEntity.CaseId = item.CaseId;
                    caseDetailsEntity.IntensityId = item.IntensityId;
                    caseDetailsEntity.RemedyCount = item.RemedyCount;
                    context.CaseDetails.Add(caseDetailsEntity);
                    context.SaveChanges();
         
                   
                   
                }
                        if (casedetailsModel.IndexOf(item) == casedetailsModel.Count-1) 
                        { 
                                foreach (var item1 in item.ModelEx)
                                {
                                    var modeldetails = new CaseDetailRemedy();
                                    modeldetails.CaseId = caseDetailsEntity.CaseId;
                                    modeldetails.RemedyId = item1.RemedyId;
                                    modeldetails.RemedyIndex = item1.RemedyIndex;
                                    context.CaseDetailRemedy.Add(modeldetails);
                                    context.SaveChanges();

                                }
                        }

            }
            Message = "Case Details Saved Successfully";

            return Message;


            //if (existingDetails.Count < 0)
            //{
            //    Message = "Case Details Saved Successfully";
            //}
            //Message = "Case Details Saved Successfully";
            //return Message;
        }


        /// <summary>
        /// Methood to get GetPatientBackHostory by patientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientAppointmentModel1> GetPatientBackHostoryById(long patientId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var patienappointmentmodel = new List<PatientAppointmentModel1>();
            var patientEntity = (from patientAppointment in context.PatientAppointment
                                 join caseEntryDetail in context.CaseEntryDetails on patientAppointment.PatientId equals caseEntryDetail.PatientId
                                 join AHN in context.AppointmentHistoryNote on patientAppointment.PatientAppId equals AHN.AppointmentId into AHNGroup
                                 from AHN in AHNGroup.DefaultIfEmpty()
                                 where patientAppointment.PatientId == patientId
                                 select new PatientAppointmentModel1
                                 {
                                     PatientAppId = patientAppointment.PatientAppId,
                                     PatientId = patientAppointment.PatientId,
                                     AppointmentDate = patientAppointment.AppointmentDate,
                                     AppointmentTime = patientAppointment.AppointmentTime,
                                     Status = patientAppointment.Status,
                                     UserId = patientAppointment.UserId,
                                     DoctorId = patientAppointment.DoctorId,
                                     CaseId = caseEntryDetail.CaseId,
                                     HistoryNoteId =AHN!=null?AHN.HistoryId:0,

                                 }).ToList();
            

            if (patientEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Patient Back Hostory not found";
            }
           
            return patientEntity;
        }





        ///// <summary>
        ///// Method implementation for getting Case details.
        ///// </summary>
        ///// <param name="CaseDetailId"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //public List<CaseDetailsModel> GetCaseDetailsById(long CaseDetailId, ref ErrorResponseModel errorResponseModel)
        //{
        //    var casedetailsModel = new List<CaseDetailsModel>();
        //    errorResponseModel = new ErrorResponseModel();
        //    var caseEntities = (from casedetails in context.CaseDetails
        //                          join subSection in context.SubSectionMaster
        //                          on casedetails.SubsectionId equals subSection.SubSectionId
        //                          join intensity in context.IntensityMaster
        //                          on casedetails.IntensityId equals intensity.IntensityId



        //                          where casedetails.CaseDetailId == CaseDetailId
        //                        select new
        //                          {

        //                              subSection.SubSectionId,
        //                              intensity.IntensityId,
        //                              casedetails.CaseDetailId,
        //                              casedetails.CaseId,
        //                              casedetails.SubsectionId,
        //                              casedetails.RemedyCount,


        //                          }).ToList();
        //    if (caseEntities.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "CaseDetails Not Found";
        //    }
        //    caseEntities.ForEach(item =>
        //    {
        //        casedetailsModel.Add(new CaseDetailsModel
        //        {
        //            CaseDetailId = item.CaseDetailId,
        //            CaseId = item.CaseId,
        //            IntensityId = item.IntensityId,
        //            SubsectionId = item.SubsectionId,
        //            RemedyCount = item.RemedyCount,

        //        });
        //    });

        //    return casedetailsModel;
        //}


        ///// <summary>
        ///// Get details to edit Case Details
        ///// </summary>
        ///// <param name="subSectionId"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //public CaseDetailsModel GetCaseDetailsToEdit(int subSectionId, int caseId, ref ErrorResponseModel errorResponseModel)
        //{
        //    throw new NotImplementedException();
        //}

    }
    }
