using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IAppointmentHistoryNoteService
    {
        /// <summary>
        /// Method is used for to get AppointmentHistoryNote by HistoryId
        /// </summary>
        /// <param name="HistoryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AppointmentHistoryNoteModel GetAppointmentHistoryNoteById(long HistoryId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save AppointmentHistoryNote
        /// </summary>
        /// <param name="appointmentHistoryNote"></param>
        /// <param name="errorResponseModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string SaveUpdateAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote, ref ErrorResponseModel errorResponseModel, int userId);

        /// <summary>
        /// Interface is used to deactivate AppointmentHistoryNote.
        /// </summary>
        /// <param name="appointmentHistoryNote"></param>
        /// <param name="errorResponseModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string DeleteAppointmentHistoryNote(AppointmentHistoryNoteModel appointmentHistoryNote, ref ErrorResponseModel errorResponseModel, int userId);

        /// <summary>
        /// Method is used to get all AppointmentHistoryNotes with pagination
        /// </summary>
        /// <param name="appointmentId">Optional: Filter by AppointmentId. If 0 or null, returns all appointment history notes</param>
        /// <param name="nigaParameters"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PaginationResult GetAllAppointmentHistoryNotes(int? appointmentId, NigaParameters nigaParameters, ref ErrorResponseModel errorResponseModel);

    }
}
