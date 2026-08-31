using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisSystemDetails
    {
        public int DiagnosisSystemDetailId { get; set; }
        public int DiagnosisId { get; set; }
        public int DiagnosisSystemId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual DiagnosisSystem DiagnosisSystem { get; set; }
    }
}
