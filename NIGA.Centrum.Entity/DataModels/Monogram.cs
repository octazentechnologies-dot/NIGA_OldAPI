using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class Monogram
    {
        public Monogram()
        {
            DiagnosisMonograms = new HashSet<DiagnosisMonograms>();
            MonogramDetails = new HashSet<MonogramDetails>();
        }

        public int MonogramId { get; set; }
        public string Monogram1 { get; set; }
        public string Keywords { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DiagnosisMonograms> DiagnosisMonograms { get; set; }
        public virtual ICollection<MonogramDetails> MonogramDetails { get; set; }
    }
}
