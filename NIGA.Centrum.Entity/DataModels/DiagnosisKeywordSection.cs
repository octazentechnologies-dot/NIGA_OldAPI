using System;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisKeywordSection
    {
        public int DiagnosisKeywordSectionId { get; set; }
        public int DiagnosisId { get; set; }
        public string KeywordType { get; set; }
        public int KeywordDetailId { get; set; }
        public int SectionId { get; set; }
        public bool DeleteStatus { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
