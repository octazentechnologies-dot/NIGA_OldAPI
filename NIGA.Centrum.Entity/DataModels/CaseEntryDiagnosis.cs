using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class CaseEntryDiagnosis
    {
        public int CaseDiagnosisId { get; set; }
        public int CaseId { get; set; }
        public int DiagnosisId { get; set; }

        public virtual CaseEntryDetails Case { get; set; }
        public virtual DiagnosisMaster Diagnosis { get; set; }
    }
}
