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
    /// <summary>
    /// This is implementation  for the doctor dashboard Get operations 
    /// </summary>
    public class DoctorDashBoardService : IDoctorDashBoardService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DoctorDashBoardService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Method is used for to get patient appointment by AppointmentDate
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientAppointmentModel> GetPatientAppUserDate(long userId, string appointmentDate, ref ErrorResponseModel errorResponseModel)
        {
            var patientAppModelList = new List<PatientAppointmentModel>();
            errorResponseModel = new ErrorResponseModel();
            var patientAppEntityList = (from p in context.PatientAppointment 
                                              join c in context.CaseEntryDetails
                                              on p.PatientId equals c.PatientId
                                              join u in context.UserMaster 
                                              on p.UserId equals u.UserId
                                              where p.UserId == userId && p.AppointmentDate == appointmentDate
                select new
                {
                    p.PatientAppId,
                    p.PatientId,
                    p.Patient.PatientName,
                    p.Patient.MobileNo,
                    p.UserId,
                    p.DoctorId,
                    p.AppointmentDate,
                    p.AppointmentTime,
                    p.Status,
                    p.DeleteStatus,
                    c.CaseId,
                    
                }
                
                
                
                ).ToList();
            if (patientAppEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Patient Appointment not found";
            }
            patientAppEntityList.ForEach(item =>
            {
                patientAppModelList.Add(new PatientAppointmentModel
                {
                    PatientAppId = item.PatientAppId,
                    PatientId = item.PatientId,
                    UserId = item.UserId,
                    DoctorId = item.DoctorId,
                    PatientName = item.PatientName,
                    MobileNo = item.MobileNo,
                    AppointmentDate = item.AppointmentDate,
                    AppointmentTime = item.AppointmentTime,
                    Status = item.Status,
                    DeleteStatus = item.DeleteStatus,
                    CaseId = item.CaseId
                   
                   
                });
            });
            return patientAppModelList;
        }

        /// <summary>
        /// Method is used for to get patient appointment by appointmentDate
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public DoctorDashBoardModel GetPatientAppCount(long userId, string appointmentDate, ref ErrorResponseModel errorResponseModel)
        {
            string h = "Not Arrived";
            string a = "Waiting";
            string w = "Completed";
            //DateTime appitmentcurrent = DateTime.Parse(appointmentDate);
            DoctorDashBoardModel appointmentCount = new DoctorDashBoardModel();
            appointmentCount.patientApp = context.PatientAppointment.Where(x => x.AppointmentDate == appointmentDate && x.UserId == userId).Count();
            //appointmentCount.walkInpatientApp = context.PatientAppointment.Where(x => DateTime.Parse(x.AppointmentDate) >= appitmentcurrent && x.UserId == userId).Count();
            appointmentCount.patientAppComplated = context.PatientAppointment.Where(x => x.AppointmentDate == appointmentDate && x.UserId == userId && x.Status == w).Count();
            appointmentCount.patientAppWaiting = context.PatientAppointment.Where(x => x.AppointmentDate == appointmentDate && x.UserId == userId && x.Status == a).Count();
            appointmentCount.patientAppNotArrived = context.PatientAppointment.Where(x => x.AppointmentDate == appointmentDate && x.UserId == userId && x.Status == h).Count();

            return appointmentCount;
        }
    }
}
