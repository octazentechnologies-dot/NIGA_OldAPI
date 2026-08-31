using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class Pathology
    {
        public Pathology()
        {
            DiagnosisPathology = new HashSet<DiagnosisPathology>();
        }

        public int PathologyId { get; set; }
        public string PathologyName { get; set; }
        public string Description { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<DiagnosisPathology> DiagnosisPathology { get; set; }
    }
}
