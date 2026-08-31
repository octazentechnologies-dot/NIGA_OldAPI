using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class AccompaniedDetails
    {
        public AccompaniedDetails()
        {
            AccompaniedRubricDetails = new HashSet<AccompaniedRubricDetails>();
        }

        public int AccompaniedDetailsId { get; set; }
        public string AccompaniedDetailsSystem { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<AccompaniedRubricDetails> AccompaniedRubricDetails { get; set; }
    }
}
