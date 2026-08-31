using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class LocationExtentionDetailsModel
    {
        public LocationExtentionDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            LocationExtentionRubricDetails = new List<LocationExtentionRubricDetailsModel>();
        }

        public int LocationExtentionDetailsId { get; set; }
        public string LocationExtentionDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual List<LocationExtentionRubricDetailsModel> LocationExtentionRubricDetails { get; set; }


        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
