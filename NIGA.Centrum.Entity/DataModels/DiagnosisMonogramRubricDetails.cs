using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisMonogramRubricDetails
    {
        public int DiagnosisMonogramRubricDetailsId { get; set; }
        public int DiagnosisMonogramDetailsId { get; set; }
        public int Subsections { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual DiagnosisMonogramDetails DiagnosisMonogramDetails { get; set; }
    }
}
