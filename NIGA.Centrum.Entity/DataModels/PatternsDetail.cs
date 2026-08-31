using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatternsDetail
    {
        public PatternsDetail()
        {
            PatternRubricDetails = new HashSet<PatternRubricDetails>();
        }

        public int PatternDetailsId { get; set; }
        public string PatternsKeywords { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<PatternRubricDetails> PatternRubricDetails { get; set; }
    }
}
