using Microsoft.EntityFrameworkCore;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the patient appointment operations 
    /// </summary>
    public class PatientAppointmentService : IPatientAppointmentService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public PatientAppointmentService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get patient appointment by patientAppId
        /// </summary>
        /// <param name="patientAppId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PatientAppointmentModel GetPatientAppById(long patientAppId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var patientAppEntity = context.PatientAppointment.Include(x => x.Patient).FirstOrDefault(x => x.PatientAppId == patientAppId && x.DeleteStatus == false);
            if (patientAppEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Patient Appointment not found";
            }
            return new PatientAppointmentModel
            {
                PatientAppId = patientAppEntity.PatientAppId,
                PatientId = patientAppEntity.PatientId,
                DoctorId = patientAppEntity.DoctorId,
                PatientName = patientAppEntity.Patient.PatientName,
                MobileNo = patientAppEntity.Patient.MobileNo,
                AppointmentDate = patientAppEntity.AppointmentDate,
                AppointmentTime = patientAppEntity.AppointmentTime,
                Status = patientAppEntity.Status,
                UserId = patientAppEntity.UserId,
                DeleteStatus = patientAppEntity.DeleteStatus,
               
            };
        }


        public PatientAppointmentModel UpdateAppointmentStatus(UpdateAppointmentStatusModel model,ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var appointment = context.PatientAppointment
                .FirstOrDefault(x =>
                    x.PatientAppId == model.PatientAppId &&
                    x.DeleteStatus == false);

            if (appointment == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Appointment not found";
                return null;
            }

            // Status validation
            var allowedStatus = new List<string>
    {
        "WAITING",
        "WALK-IN",
        "NOT ARRIVED",
        "E-CONSULT",
        "REMAINING",
        "COMPLETED"
    };

            if (!allowedStatus.Contains(model.Status.ToUpper()))
            {
                errorResponseModel.StatusCode = HttpStatusCode.BadRequest;
                errorResponseModel.Message = "Invalid appointment status";
                return null;
            }

            // Update status
            appointment.Status = model.Status.ToUpper();
            context.SaveChanges();

            return new PatientAppointmentModel
            {
                PatientAppId = appointment.PatientAppId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = appointment.Status,
                UserId = appointment.UserId,
                DeleteStatus = appointment.DeleteStatus,
                Message = "Appointment status updated successfully"
            };




        }


        /// <summary>
        /// Method implementation for saving new patient appointment
        /// </summary>
        /// <param name="patientAppointmentModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SavePatientApp(PatientAppointmentModel patientAppointmentModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var patientApp = context.PatientAppointment.
                Where(x=> x.DoctorId== patientAppointmentModel.DoctorId 
                && x.AppointmentDate== patientAppointmentModel.AppointmentDate 
                && x.AppointmentTime== patientAppointmentModel.AppointmentTime).ToList();

            if (patientAppointmentModel.PatientAppId == 0 && patientApp.Count==0)
            {
                PatientAppointment patientAppEntity = new PatientAppointment();

                patientAppEntity.PatientId = patientAppointmentModel.PatientId;
                patientAppEntity.UserId = patientAppointmentModel.UserId;
                patientAppEntity.DoctorId = patientAppointmentModel.DoctorId;
                patientAppEntity.AppointmentDate =patientAppointmentModel.AppointmentDate;
                patientAppEntity.AppointmentTime = patientAppointmentModel.AppointmentTime;
                patientAppEntity.Status = patientAppointmentModel.Status;
                patientAppEntity.DeleteStatus = patientAppointmentModel.DeleteStatus;
                context.PatientAppointment.Add(patientAppEntity);
                context.SaveChanges();
                Message = "Patient Appointment Saved Successfully";
            }
            else
            {
               var patientAppEntity1 = context.PatientAppointment.FirstOrDefault(x => x.PatientAppId == patientAppointmentModel.PatientAppId);
                //var patientAppEntity = GetPatientAppById(patientAppointmentModel.PatientAppId, ref errorResponseModel);
                if (patientAppEntity1 != null)
                {
                    patientAppEntity1.PatientId = patientAppointmentModel.PatientId;
                    patientAppEntity1.UserId = patientAppointmentModel.UserId;
                    patientAppEntity1.DoctorId = patientAppointmentModel.DoctorId;
                    patientAppEntity1.Status = patientAppointmentModel.Status;
                    patientAppEntity1.DeleteStatus = patientAppointmentModel.DeleteStatus;
                    if (patientAppointmentModel.AppointmentDate == null)
                    {
                        patientAppointmentModel.AppointmentDate = patientAppEntity1.AppointmentDate;
                        //  patientAppEntity.AppointmentDate = patientAppointmentModel.AppointmentDate;

                    }
                    else
                    {
                        patientAppEntity1.AppointmentDate = patientAppointmentModel.AppointmentDate;

                    }
                    if (patientAppointmentModel.AppointmentTime == null)
                    {
                        patientAppointmentModel.AppointmentTime = patientAppEntity1.AppointmentTime;
                    }
                    else
                    {
                        patientAppEntity1.AppointmentTime = patientAppointmentModel.AppointmentTime;

                    }
                    if (patientAppointmentModel.DeleteStatus == null)
                    {
                        patientAppointmentModel.DeleteStatus = false;
                    }
                    context.SaveChanges();
                    Message = "Patient Appointment Updated Successfully";
                }
                Message = "This Appointment Already Book Please select another time";
            }
            return Message;
        }

        /// <summary>
        /// Method inpmplementaion for get all the cases by UserId.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientModel> GetCasesByUser(long UserId, ref ErrorResponseModel errorResponseModel)
        {
            var patientModelList = new List<PatientModel>();
            errorResponseModel = new ErrorResponseModel();
            var doctorEntity = context.Doctor.Where(x => x.UserId == UserId).FirstOrDefault();
            if (doctorEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "doctor not found";
               
            }
            var caseEntityList = context.CaseEntryDetails.Include(x => x.Patient).Where(x => x.DoctorId == doctorEntity.DoctorId && x.DeleteStatus==false).ToList();
            if (caseEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "patient not found";
            }
            caseEntityList.ForEach(item =>
            {
                patientModelList.Add(new PatientModel
                {
                    PatientID = item.PatientId,
                    PatientName = item.Patient.PatientName,
                    MobileNo = item.Patient.MobileNo,
                    Gender = item.Patient.Gender,
                    Address = item.Patient.Address,
                    DateOfBirth = item.Patient.DateOfBirth,
                });
            });

            return patientModelList;
        }



    }
}
