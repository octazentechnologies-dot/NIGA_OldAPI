using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisCausation
    {
        public DiagnosisCausation()
        {
            DiagnosisCausationRubricDetails = new HashSet<DiagnosisCausationRubricDetails>();
        }

        public int CausationId { get; set; }
        public int? DiagnosisId { get; set; }
        public string CausationName { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<DiagnosisCausationRubricDetails> DiagnosisCausationRubricDetails { get; set; }
    }
}
