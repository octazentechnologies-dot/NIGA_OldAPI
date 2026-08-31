using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatientAppointment
    {
        public PatientAppointment()
        {
            AppointmentHistoryNote = new HashSet<AppointmentHistoryNote>();
            PrescriptionRemedyDetail = new HashSet<PrescriptionRemedyDetail>();
            PrescriptionRubricDetail = new HashSet<PrescriptionRubricDetail>();
        }

        public int PatientAppId { get; set; }
        public int PatientId { get; set; }
        public string AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string Status { get; set; }
        public bool? DeleteStatus { get; set; }
        public long UserId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<AppointmentHistoryNote> AppointmentHistoryNote { get; set; }
        public virtual ICollection<PrescriptionRemedyDetail> PrescriptionRemedyDetail { get; set; }
        public virtual ICollection<PrescriptionRubricDetail> PrescriptionRubricDetail { get; set; }
    }
}
