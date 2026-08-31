using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DoctorDashBoardModel
    {
        public int PatientAppId { get; set; }
        public string AppointmentDate { get; set; }
        public string Status { get; set; }
        public long UserId { get; set; }
        public int DoctorId { get; set; }
        public int patientApp { get; set; }
        //public int walkInpatientApp { get; set; }
        public int patientAppComplated { get; set; }
        public int patientAppWaiting { get; set; }
        public int patientAppNotArrived { get; set; }
    }
}
