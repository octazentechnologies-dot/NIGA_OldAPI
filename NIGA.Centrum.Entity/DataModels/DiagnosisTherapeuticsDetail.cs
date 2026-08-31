using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisTherapeuticsDetail
    {
        public int DiagnosisTherapeuticsDetailId { get; set; }
        public int DiagnosisId { get; set; }
        public string DiagnosisTherapeuticsDetail1 { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
    }
}
