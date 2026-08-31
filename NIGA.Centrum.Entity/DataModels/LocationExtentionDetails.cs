using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class LocationExtentionDetails
    {
        public LocationExtentionDetails()
        {
            LocationExtentionRubricDetails = new HashSet<LocationExtentionRubricDetails>();
        }

        public int LocationExtentionDetailsId { get; set; }
        public string LocationExtentionDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<LocationExtentionRubricDetails> LocationExtentionRubricDetails { get; set; }
    }
}
