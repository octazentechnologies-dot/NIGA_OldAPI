using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class BeforeAfterDuringDetailsModel
    {
        public BeforeAfterDuringDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            BeforeAfterDuringRubricDetails = new List<BeforeAfterDuringRubricDetailsModel>();
        }

        public int BeforeAfterDuringDetailsId { get; set; }
        public string BeforeAfterDuringDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public List<BeforeAfterDuringRubricDetailsModel> BeforeAfterDuringRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
