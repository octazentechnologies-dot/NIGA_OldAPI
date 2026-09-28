using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class AppointmentHistoryNoteModel
    {
        public int HistoryId { get; set; } = 0;
        public int? AppointmentId { get; set; } = 0;
        public string HistoryNote { get; set; }= string.Empty;
        public string NoteType { get; set; } = "General";
        public bool IsErxExcluded { get; set; } = true;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
