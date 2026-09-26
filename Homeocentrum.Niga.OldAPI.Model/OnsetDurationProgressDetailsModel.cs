using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class OnsetDurationProgressDetailsModel
    {
        public OnsetDurationProgressDetailsModel() {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            OnsetDurationProgressRubricDetails = new List<OnsetDurationProgressRubricDetailsModel>();
        }
        public int OnsetDetailId { get; set; }
        public string OnsetKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public List<OnsetDurationProgressRubricDetailsModel> OnsetDurationProgressRubricDetails { get; set; }


        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
