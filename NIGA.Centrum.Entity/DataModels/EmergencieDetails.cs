using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class EmergencieDetails
    {
        public EmergencieDetails()
        {
            EmergencieRubricDetails = new HashSet<EmergencieRubricDetails>();
        }

        public int EmergencieId { get; set; }
        public string EmergencieKeyword { get; set; }
        public int? DiagnosisId { get; set; }
        public bool DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<EmergencieRubricDetails> EmergencieRubricDetails { get; set; }
    }
}
