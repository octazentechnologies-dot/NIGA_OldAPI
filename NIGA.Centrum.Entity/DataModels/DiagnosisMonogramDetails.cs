using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisMonogramDetails
    {
        public DiagnosisMonogramDetails()
        {
            DiagnosisMonogramRubricDetails = new HashSet<DiagnosisMonogramRubricDetails>();
        }

        public int DiagnosisMonogramDetailsId { get; set; }
        public string DiagnosisMonogramKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<DiagnosisMonogramRubricDetails> DiagnosisMonogramRubricDetails { get; set; }
    }
}
