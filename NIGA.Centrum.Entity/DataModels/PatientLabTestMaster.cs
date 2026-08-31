using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatientLabTestMaster
    {
        public PatientLabTestMaster()
        {
            PatientLabEntry = new HashSet<PatientLabEntry>();
            PatientLabOrder = new HashSet<PatientLabOrder>();
        }

        public int PatientLabTestId { get; set; }
        public string LabTestName { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<PatientLabEntry> PatientLabEntry { get; set; }
        public virtual ICollection<PatientLabOrder> PatientLabOrder { get; set; }
    }
}
