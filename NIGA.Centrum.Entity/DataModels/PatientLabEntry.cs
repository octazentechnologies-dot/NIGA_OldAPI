using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatientLabEntry
    {
        public int PatientLabId { get; set; }
        public int PatientId { get; set; }
        public int PatientLabTestId { get; set; }
        public DateTime? LabDate { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual PatientLabTestMaster PatientLabTest { get; set; }
    }
}
