using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisPathologyDetails
    {
        public DiagnosisPathologyDetails()
        {
            DiagnosisPathologyRubricDetails = new HashSet<DiagnosisPathologyRubricDetails>();
        }

        public int DiagnosisPathologyDetailsId { get; set; }
        public string DiagnosisPathologyKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<DiagnosisPathologyRubricDetails> DiagnosisPathologyRubricDetails { get; set; }
    }
}
