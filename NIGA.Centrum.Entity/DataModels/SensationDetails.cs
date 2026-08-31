using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SensationDetails
    {
        public SensationDetails()
        {
            SensationRubricDetails = new HashSet<SensationRubricDetails>();
        }

        public int SensationDetailsId { get; set; }
        public string SensationDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<SensationRubricDetails> SensationRubricDetails { get; set; }
    }
}
