using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ModalitiesDetails
    {
        public ModalitiesDetails()
        {
            ModalitiesRubricDetails = new HashSet<ModalitiesRubricDetails>();
        }

        public int ModalitiesDetailsId { get; set; }
        public string ModalitiesDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<ModalitiesRubricDetails> ModalitiesRubricDetails { get; set; }
    }
}
