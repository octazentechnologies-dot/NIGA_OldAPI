using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class QuestionGroupMaster
    {
        public QuestionGroupMaster()
        {
            ClinicalQuestions = new HashSet<ClinicalQuestions>();
        }

        public int QuestionGroupId { get; set; }
        public string QuestionGroupName { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? QuestionSectionId { get; set; }
        public int? SectionId { get; set; }

        public virtual SectionMaster Section { get; set; }
        public virtual ICollection<ClinicalQuestions> ClinicalQuestions { get; set; }
    }
}
