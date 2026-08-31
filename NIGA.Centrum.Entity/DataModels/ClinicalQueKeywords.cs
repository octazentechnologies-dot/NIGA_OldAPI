using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ClinicalQueKeywords
    {
        public int ClinicalQueKeywordId { get; set; }
        public int? QuestionsId { get; set; }
        public string KeywordQuestion { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ClinicalQuestions Questions { get; set; }
    }
}
