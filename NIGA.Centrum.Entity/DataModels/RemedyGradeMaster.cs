using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RemedyGradeMaster
    {
        public RemedyGradeMaster()
        {
            RubricRemedyDetails = new HashSet<RubricRemedyDetails>();
        }

        public int GradeId { get; set; }
        public int GradeNo { get; set; }
        public string Description { get; set; }
        public string FontName { get; set; }
        public string FontStyle { get; set; }
        public string FontColor { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<RubricRemedyDetails> RubricRemedyDetails { get; set; }
    }
}
