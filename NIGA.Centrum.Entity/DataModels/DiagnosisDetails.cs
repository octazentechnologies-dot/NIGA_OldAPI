using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisDetails
    {
        public int DiagnosisDetailId { get; set; }
        public int? DiagnosisId { get; set; }
        public int? SubSectionId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual SubSectionMaster SubSection { get; set; }
    }
}
