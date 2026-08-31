using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisCausationRubricDetails
    {
        public int CausationRubricDetailsId { get; set; }
        public int CausationId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisCausation Causation { get; set; }
    }
}
