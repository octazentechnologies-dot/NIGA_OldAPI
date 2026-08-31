using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class BeforeAfterDuringDetails
    {
        public BeforeAfterDuringDetails()
        {
            BeforeAfterDuringRubricDetails = new HashSet<BeforeAfterDuringRubricDetails>();
        }

        public int BeforeAfterDuringDetailsId { get; set; }
        public string BeforeAfterDuringDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<BeforeAfterDuringRubricDetails> BeforeAfterDuringRubricDetails { get; set; }
    }
}
