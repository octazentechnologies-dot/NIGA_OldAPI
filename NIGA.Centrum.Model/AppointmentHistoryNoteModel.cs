using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class AppointmentHistoryNoteModel
    {
        public int HistoryId { get; set; } = 0;
        public int? AppointmentId { get; set; } = 0;
        public string HistoryNote { get; set; }= string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
