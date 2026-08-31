using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PatternsDetailModel
    {
        public PatternsDetailModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            PatternRubricDetails = new List<PatternRubricDetailsModel>();
        }

        public int PatternDetailsId { get; set; }
        public string PatternsKeywords { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual List<PatternRubricDetailsModel> PatternRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
