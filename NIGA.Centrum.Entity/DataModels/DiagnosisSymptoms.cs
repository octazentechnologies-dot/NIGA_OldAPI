using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisSymptoms
    {
        public DiagnosisSymptoms()
        {
            DiagnosisSymptomRubric = new HashSet<DiagnosisSymptomRubric>();
        }

        public int DiagnosisSymptomId { get; set; }
        public int? DiagnosisId { get; set; }
        public string Symptom { get; set; }
        public int? EnteredBy { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMaster Diagnosis { get; set; }
        public virtual ICollection<DiagnosisSymptomRubric> DiagnosisSymptomRubric { get; set; }
    }
}
