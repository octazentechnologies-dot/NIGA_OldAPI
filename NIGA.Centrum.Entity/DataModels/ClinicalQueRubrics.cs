using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ClinicalQueRubrics
    {
        public int ClinicalQueRubricId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ClinicalQuestionBodyPartId { get; set; }
        public int? ClinicalQueKeywordId { get; set; }

        public virtual SubSectionMaster Subsection { get; set; }
    }
}
