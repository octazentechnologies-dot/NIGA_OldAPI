using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class CaseEntryDetails
    {
        public CaseEntryDetails()
        {
            CaseDetailRemedy = new HashSet<CaseDetailRemedy>();
            CaseEntryChiefComplaint = new HashSet<CaseEntryChiefComplaint>();
            CaseEntryDiagnosis = new HashSet<CaseEntryDiagnosis>();
        }

        public int CaseId { get; set; }
        public int PatientId { get; set; }
        public int? UserId { get; set; }
        public int DoctorId { get; set; }
        public DateTime? DateodFirstVisit { get; set; }
        public string RefBy { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<CaseDetailRemedy> CaseDetailRemedy { get; set; }
        public virtual ICollection<CaseEntryChiefComplaint> CaseEntryChiefComplaint { get; set; }
        public virtual ICollection<CaseEntryDiagnosis> CaseEntryDiagnosis { get; set; }
    }
}
