using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class Patient
    {
        public Patient()
        {
            CaseEntryDetails = new HashSet<CaseEntryDetails>();
            PatientAppointment = new HashSet<PatientAppointment>();
        }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public string Email { get; set; }

        public virtual CountryMaster Country { get; set; }
        public virtual StateMaster State { get; set; }
        public virtual ICollection<CaseEntryDetails> CaseEntryDetails { get; set; }
        public virtual ICollection<PatientAppointment> PatientAppointment { get; set; }
    }
}
