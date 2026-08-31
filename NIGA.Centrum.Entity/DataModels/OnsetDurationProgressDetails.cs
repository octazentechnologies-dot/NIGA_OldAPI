using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class OnsetDurationProgressDetails
    {
        public OnsetDurationProgressDetails()
        {
            OnsetDurationProgressRubricDetails = new HashSet<OnsetDurationProgressRubricDetails>();
        }

        public int OnsetDetailId { get; set; }
        public string OnsetKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<OnsetDurationProgressRubricDetails> OnsetDurationProgressRubricDetails { get; set; }
    }
}
