using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PatientAppointmentModel
    {     
        public int PatientAppId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string Status { get; set; }
        public bool? DeleteStatus { get; set; }
        public long UserId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int CaseId { get; set; }

        public string Message { get; set; }

       
    }



    public class PatientAppointmentModel1
    {
        public int PatientAppId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string Status { get; set; }
        public bool? DeleteStatus { get; set; }
        public long UserId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int CaseId { get; set; }
        public int HistoryNoteId { get; set; }


    }
    public class UpdateAppointmentStatusModel
    {
        public long PatientAppId { get; set; }
        public string Status { get; set; }
    }

    

}
