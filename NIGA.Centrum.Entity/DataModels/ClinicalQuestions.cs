using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ClinicalQuestions
    {
        public ClinicalQuestions()
        {
            ClinicalQueKeywords = new HashSet<ClinicalQueKeywords>();
        }

        public int QuestionsId { get; set; }
        public int? QuestionGroupId { get; set; }
        public int? QuestionSectionId { get; set; }
        public int? QuestionSubgroupId { get; set; }
        public int? BodyPartId { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }

        public virtual QuestionGroupMaster QuestionGroup { get; set; }
        public virtual ICollection<ClinicalQueKeywords> ClinicalQueKeywords { get; set; }
    }
}
