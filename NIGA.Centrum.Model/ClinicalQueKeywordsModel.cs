using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class ClinicalQueKeywordsModel
    {
        public int ClinicalQueKeywordId { get; set; }
        public int? QuestionsId { get; set; }
        public string KeywordQuestion { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
