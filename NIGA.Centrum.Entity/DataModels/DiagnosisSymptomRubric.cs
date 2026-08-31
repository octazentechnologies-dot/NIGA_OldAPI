using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisSymptomRubric
    {
        public int DiagnosisSymptomRubricId { get; set; }
        public int DiagnosisSymptomId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisSymptoms DiagnosisSymptom { get; set; }
    }
}
