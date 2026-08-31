using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatientLabOrder
    {
        public int PatientOrderedTestId { get; set; }
        public int PatientId { get; set; }
        public int? PatientLabTestId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string LabName { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual PatientLabTestMaster PatientLabTest { get; set; }
    }
}
