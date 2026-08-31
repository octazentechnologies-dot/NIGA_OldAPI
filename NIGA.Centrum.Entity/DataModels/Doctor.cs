using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class Doctor
    {
        public Doctor()
        {
            CaseEntryDetails = new HashSet<CaseEntryDetails>();
            PackageEntryDetails = new HashSet<PackageEntryDetails>();
            PatientAppointment = new HashSet<PatientAppointment>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? QualificationId { get; set; }
        public string PermanantAddress { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int? CasePaperValidity { get; set; }
        public int? PackageId { get; set; }
        public string PassingUniversity { get; set; }
        public string PassingCertNo { get; set; }
        public string City { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? UserId { get; set; }

        public virtual PackageMaster Package { get; set; }
        public virtual QualificationMaster Qualification { get; set; }
        public virtual ICollection<CaseEntryDetails> CaseEntryDetails { get; set; }
        public virtual ICollection<PackageEntryDetails> PackageEntryDetails { get; set; }
        public virtual ICollection<PatientAppointment> PatientAppointment { get; set; }
    }
}
