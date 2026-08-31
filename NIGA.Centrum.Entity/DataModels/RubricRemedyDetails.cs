using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RubricRemedyDetails
    {
        public RubricRemedyDetails()
        {
            RemedyRubricAuthorDetails = new HashSet<RemedyRubricAuthorDetails>();
        }

        public int RubricRemedyId { get; set; }
        public int? SubSectionId { get; set; }
        public int? RemedyId { get; set; }
        public int? GradeId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public bool? DeletedStatus { get; set; }
        public bool? IsConfirmationRubric { get; set; }
        public bool? IsSmallRubric { get; set; }

        public virtual RemedyGradeMaster Grade { get; set; }
        public virtual RemedyMaster Remedy { get; set; }
        public virtual SubSectionMaster SubSection { get; set; }
        public virtual ICollection<RemedyRubricAuthorDetails> RemedyRubricAuthorDetails { get; set; }
    }
}
