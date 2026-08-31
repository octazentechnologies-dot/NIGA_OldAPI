using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisSystem
    {
        public DiagnosisSystem()
        {
            DiagnosisSystemDetails = new HashSet<DiagnosisSystemDetails>();
        }

        public int DiagnosisSystemId { get; set; }
        public string DiagnosisSystemName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DiagnosisSystemDetails> DiagnosisSystemDetails { get; set; }
    }
}
