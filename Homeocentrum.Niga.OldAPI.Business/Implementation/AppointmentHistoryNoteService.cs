using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Implementation
{
    public class AppointmentHistoryNoteService : IAppointmentHistoryNoteService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public AppointmentHistoryNoteService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }
       
        public string SaveUpdateAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote, ref ErrorResponseModel errorResponseModel,int userId)
        {
            string Message = "";
            if (appointmentHistoryNote.HistoryId == 0)
            {
                AppointmentHistoryNote appointmentHistoryNoteEntity = new AppointmentHistoryNote();
                appointmentHistoryNoteEntity.AppointmentId = appointmentHistoryNote.AppointmentId;
                appointmentHistoryNoteEntity.HistoryNote = appointmentHistoryNote.HistoryNote;
                appointmentHistoryNoteEntity.NoteType = string.IsNullOrWhiteSpace(appointmentHistoryNote.NoteType) ? "General" : appointmentHistoryNote.NoteType.Trim();
                appointmentHistoryNoteEntity.IsErxExcluded = appointmentHistoryNote.IsErxExcluded;
                appointmentHistoryNoteEntity.DeletedStatus = false;
                appointmentHistoryNoteEntity.CreatedBy = userId;
                appointmentHistoryNoteEntity.CreatedDate = DateTime.Now;
                context.AppointmentHistoryNote.Add(appointmentHistoryNoteEntity);
                context.SaveChanges();
                Message = "Appointment History Note Saved Successfully";
            }
            else
            {
                var appointmentHistoryNoteEntity = context.AppointmentHistoryNote.FirstOrDefault(x => x.HistoryId == appointmentHistoryNote.HistoryId);
                if (appointmentHistoryNoteEntity != null)
                {

                    appointmentHistoryNoteEntity.AppointmentId = appointmentHistoryNote.AppointmentId;
                    appointmentHistoryNoteEntity.HistoryNote = appointmentHistoryNote.HistoryNote;
                    appointmentHistoryNoteEntity.NoteType = string.IsNullOrWhiteSpace(appointmentHistoryNote.NoteType) ? "General" : appointmentHistoryNote.NoteType.Trim();
                    appointmentHistoryNoteEntity.IsErxExcluded = appointmentHistoryNote.IsErxExcluded;
                    appointmentHistoryNoteEntity.ModifyBy = userId;
                    appointmentHistoryNoteEntity.ModifyDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Appointment History Note Updated Successfully";
                }
            }
            return Message;
        }

        public AppointmentHistoryNoteModel GetAppointmentHistoryNoteById(long HistoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var appointmentHistoryNoteEntity = context.AppointmentHistoryNote.FirstOrDefault(x => x.HistoryId == HistoryId && x.DeletedStatus == false);
            if (appointmentHistoryNoteEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Appointment History Note not found";
                return null;
            }
            return new AppointmentHistoryNoteModel
            {
                HistoryId = appointmentHistoryNoteEntity.HistoryId,
                AppointmentId = appointmentHistoryNoteEntity.AppointmentId,
                HistoryNote = appointmentHistoryNoteEntity.HistoryNote,
                NoteType = appointmentHistoryNoteEntity.NoteType,
                IsErxExcluded = appointmentHistoryNoteEntity.IsErxExcluded,
                CreatedDate = appointmentHistoryNoteEntity.CreatedDate.HasValue ? appointmentHistoryNoteEntity.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
            };
        }


        public string DeleteAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote, ref ErrorResponseModel errorResponseModel,int userId)
        {
            string Message = "";
            var appointmentHistoryNoteEntity = context.AppointmentHistoryNote.FirstOrDefault(x => x.HistoryId == appointmentHistoryNote.HistoryId);
            if (appointmentHistoryNoteEntity != null)
            {
                appointmentHistoryNoteEntity.DeletedStatus = true;
                appointmentHistoryNoteEntity.ModifyDate = DateTime.Now;
                appointmentHistoryNoteEntity.ModifyBy = userId;
                context.SaveChanges();
                Message = "Appointment History Note Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Method to get all AppointmentHistoryNotes with pagination
        /// </summary>
        /// <param name="appointmentId">Optional: Filter by AppointmentId. If 0 or null, returns all appointment history notes</param>
        /// <param name="nigaParameters"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PaginationResult GetAllAppointmentHistoryNotes(int? appointmentId, NigaParameters nigaParameters, ref ErrorResponseModel errorResponseModel, int? ownerDoctorId = null)
        {
            errorResponseModel = new ErrorResponseModel();

            var pageNumber = (nigaParameters.PageNumber <= 0) ? 1 : nigaParameters.PageNumber;
            var pageSize = nigaParameters.PageSize;
            var totalRecords = 0.0;
            var totalPages = 0.0;
            var skip = 0;

            var appointmentHistoryNoteList = (from appointmentHistoryNote in context.AppointmentHistoryNote
                                              join appt in context.PatientAppointment on appointmentHistoryNote.AppointmentId equals appt.PatientAppId into apptJoin
                                              from appt in apptJoin.DefaultIfEmpty()
                                              where appointmentHistoryNote.DeletedStatus == false
                                              && (appointmentId == null || appointmentId == 0 || appointmentHistoryNote.AppointmentId == appointmentId)
                                              && (ownerDoctorId == null || ownerDoctorId == 0 || (appt != null && appt.DoctorId == ownerDoctorId.Value))
                                              select new AppointmentHistoryNoteModel
                                              {
                                                  HistoryId = appointmentHistoryNote.HistoryId,
                                                  AppointmentId = appointmentHistoryNote.AppointmentId,
                                                  HistoryNote = appointmentHistoryNote.HistoryNote,
                                                  NoteType = appointmentHistoryNote.NoteType,
                                                  IsErxExcluded = appointmentHistoryNote.IsErxExcluded,
                                                  CreatedDate = appointmentHistoryNote.CreatedDate.HasValue ? appointmentHistoryNote.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                              }).ToList();

            if (appointmentHistoryNoteList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Appointment History Notes not found";
            }

            totalRecords = appointmentHistoryNoteList.Count;
            totalPages = Math.Ceiling(totalRecords / pageSize);
            skip = (pageNumber - 1) * pageSize;

            var result = new PaginationResult();
            result.TotalPageCount = totalPages;
            result.TotalCount = totalRecords;
            result.ResultObject = appointmentHistoryNoteList.Skip(skip).Take(pageSize);
            return result;
        }

    }
}
