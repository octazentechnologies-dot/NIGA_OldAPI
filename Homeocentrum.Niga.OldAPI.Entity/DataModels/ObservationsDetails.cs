using System;
using System.Collections.Generic;

namespace Homeocentrum.Niga.OldAPI.Entity.DataModels
{
    public partial class ObservationsDetails
    {
        public ObservationsDetails()
        {
            ObservationsRubricDetails = new HashSet<ObservationsRubricDetails>();
        }

        public int ObservationsDetailsId { get; set; }
        public string ObservationsDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<ObservationsRubricDetails> ObservationsRubricDetails { get; set; }
    }
}
