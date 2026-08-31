using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisPathologyRubricDetails
    {
        public int DiagnosisPathologyRubricDetailsId { get; set; }
        public int DiagnosisPathologyDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisPathologyDetails DiagnosisPathologyDetails { get; set; }
    }
}
