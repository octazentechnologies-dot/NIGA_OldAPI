using Microsoft.EntityFrameworkCore;
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
    public class PatientService : IPatientService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PatientService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public PatientModel SavePatient(PatientModel patientModel, ref ErrorResponseModel errorResponseModel)
        {
            if (patientModel.PatientID == 0)
            {
                /*Save record to Petient table*/
                var CurrentDate = DateTime.Now;
                var patientEntity = new Patient();
                patientEntity.PatientName = patientModel.PatientName;
                patientEntity.Address = patientModel.Address;
                patientEntity.StateId = patientModel.StateId;
                patientEntity.CountryId = patientModel.CountryId;
                patientEntity.MobileNo = patientModel.MobileNo;
                patientEntity.DateOfBirth = patientModel.DateOfBirth;
                patientEntity.Gender = patientModel.Gender;
                patientEntity.EnteredBy = patientModel.EnteredBy;
                patientEntity.EnteredDate = CurrentDate;
                patientEntity.DeleteStatus = false;
                context.Patient.Add(patientEntity);
                var doctorEntity = context.Doctor.Where(x => x.UserId == patientModel.LoggedInUser).FirstOrDefault();
                if (doctorEntity != null)
                {
                    var caseEntryDetailsEntity = new CaseEntryDetails();
                    caseEntryDetailsEntity.UserId = patientModel.LoggedInUser;
                    caseEntryDetailsEntity.DoctorId = doctorEntity.DoctorId;
                    caseEntryDetailsEntity.DateodFirstVisit = patientModel.DateodFirstVisit;
                    caseEntryDetailsEntity.RefBy = patientModel.RefBy;
                    caseEntryDetailsEntity.EnteredBy = patientModel.EnteredBy;
                    caseEntryDetailsEntity.EnteredDate = CurrentDate;
                    caseEntryDetailsEntity.DeleteStatus = false;
                    patientEntity.CaseEntryDetails.Add(caseEntryDetailsEntity);
                    if (patientModel.DiagnosisIds != null)
                    {
                        foreach (var item in patientModel.DiagnosisIds.Split(','))
                        {
                            var caseEntryDiagnosis = new CaseEntryDiagnosis();
                            caseEntryDiagnosis.DiagnosisId = Convert.ToInt32(item);
                            caseEntryDetailsEntity.CaseEntryDiagnosis.Add(caseEntryDiagnosis);
                        }
                    }



                    //foreach (var item in patientModel.ChiefComplaintIds.Split(','))
                    //{
                    //    var caseEntryChiefComplaint = new CaseEntryChiefComplaint();
                    //    caseEntryChiefComplaint.ChiefComplaintName = item;
                    //    caseEntryDetailsEntity.CaseEntryChiefComplaint.Add(caseEntryChiefComplaint);
                    //}
                }
                context.SaveChanges();
                patientModel.PatientID = patientEntity.PatientId;
                patientModel.Message = "Patient Saved Successfully";
            }
            return patientModel;
        }

        /// <summary>
        /// Method inpmplementaion for get all the cases.
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientModel> GetCases(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var patientModelList = new List<PatientModel>();
            errorResponseModel = new ErrorResponseModel();
            var doctorEntity = context.Doctor.FirstOrDefault(x => x.UserId == userId);
            var caseEntityList = context.CaseEntryDetails.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.CaseEntryDiagnosis).Where(x => x.DoctorId == doctorEntity.DoctorId && x.DeleteStatus==false).OrderByDescending(x => x.PatientId).ToList();
            if (caseEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Not found";

            }
            foreach (var item in caseEntityList)
            {
                PatientModel p = new PatientModel();
                p.DoctorID = item.DoctorId;
                p.PatientID = item.PatientId;
                p.PatientName = item.Patient.PatientName;
                p.MobileNo = item.Patient.MobileNo;
                p.UserId = (int)item.UserId;
                p.DateodFirstVisit = item.DateodFirstVisit;
                p.Gender = item.Patient.Gender;
                p.Address = item.Patient.Address;
                p.DateOfBirth = item.Patient.DateOfBirth;
                p.CaseId = item.CaseId;
                p.EnteredDate = item.EnteredDate;
                
                List<string> diagnosisList = new List<string>();
                foreach (var caseEntryDiagnosis in item.CaseEntryDiagnosis)
                {
                    var DiagnosisName = context.DiagnosisMaster.Where(x => x.DiagnosisId == caseEntryDiagnosis.DiagnosisId && x.DeleteStatus==false).Select(x => x.DiagnosisName).FirstOrDefault();
                    diagnosisList.Add(DiagnosisName);
                }
                p.DiagnosisIds = string.Join(',', diagnosisList.ToArray());
                patientModelList.Add(p);
            }
            return patientModelList;
        }

        /// <summary>
        /// Method implementation for patient details
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
      public PatientModel GetPatientDetails(long PatientID, long caseId,ref ErrorResponseModel errorResponseModel)
        {
            var patientModelList = new PatientModel();
            errorResponseModel = new ErrorResponseModel();
            var patientEntity = (from p in context.Patient
                                 join
                                 c in context.CaseEntryDetails on p.PatientId equals
                                  c.PatientId
                                 where p.PatientId == PatientID && c.CaseId==caseId
                                 select new
                                 {
                                     c.CaseId,
                                     c.UserId,
                                     c.DoctorId,
                                     p.PatientId,
                                     p.PatientName,
                                     p.Address,
                                     p.StateId,
                                     p.CountryId,
                                     p.MobileNo,
                                     p.PhoneNo,
                                     p.DateOfBirth,
                                     p.Gender,
                                     p.EnteredBy,
                                     p.EnteredDate,
                                     p.ChangedBy,
                                     p.ChangedDate,
                                     p.DeleteStatus,
                                    
                                 }
                                  ).FirstOrDefault();
                //context.Patient.Where(x => x.PatientId == PatientID).FirstOrDefault();
            if (patientEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "patient not found";
                return null;
            }
            return new PatientModel
            {
               
                PatientID = patientEntity.PatientId,
                DoctorID=patientEntity.DoctorId,
                PatientName = patientEntity.PatientName,
                Address = patientEntity.Address,
                StateId = patientEntity.StateId,
                CountryId = patientEntity.CountryId,
                MobileNo = patientEntity.MobileNo,
                PhoneNo = patientEntity.PhoneNo,
                DateOfBirth = patientEntity.DateOfBirth,
                Gender = patientEntity.Gender,
                EnteredBy = patientEntity.EnteredBy,
                EnteredDate = patientEntity.EnteredDate,
                ChangedBy = patientEntity.ChangedBy,
                ChangedDate = patientEntity.ChangedDate,
                DeleteStatus = patientEntity.DeleteStatus,
                UserId = patientEntity.UserId,
                CaseId = patientEntity.CaseId
            };
             
        }

        /// <summary>
        /// Method implementation for Saving new Complaints.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveComplaints(PatientModel patient, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var CaseEntry = context.CaseEntryDetails.Where(x => x.PatientId == patient.PatientID).FirstOrDefault();
            foreach (var item in patient.ChiefComplaintIds.Split(','))
            {
                var caseEntryChiefComplaint = new CaseEntryChiefComplaint();
                caseEntryChiefComplaint.ChiefComplaintName = item;
                caseEntryChiefComplaint.CaseId = CaseEntry.CaseId;
                context.CaseEntryChiefComplaint.Add(caseEntryChiefComplaint);
                context.SaveChanges();
                Message = "Complaints Saved Successfully";
            }
            return Message;
        }


           

        /// <summary>
        /// Methood to get GetPatientDetails by patientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public GetPatientDetailsById GetPatientDetailsById(long patientId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var patientEntity = context.Patient.Where(x => x.PatientId == patientId).FirstOrDefault();
            if (patientEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Patient not found";
            }
            return new GetPatientDetailsById
            {
                PatientID= patientEntity.PatientId,
                PatientName= patientEntity.PatientName,
                
            };

        }

        public string Deletepatient(int patientId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var PatientEntity = context.Patient.FirstOrDefault(x => x.PatientId == patientId);
           var CaseEntryDetails= context.CaseEntryDetails.FirstOrDefault(x=>x.PatientId == patientId);
            if (PatientEntity != null)
            {
                PatientEntity.DeleteStatus = true;
                context.SaveChanges();
                CaseEntryDetails.DeleteStatus = true;
                context.SaveChanges();
              
                Message = " Patient Delete Successfully";
            }
            return Message;
        }
    }
}
