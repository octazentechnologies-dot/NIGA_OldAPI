using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class ObservationsDetailsModel
    {
        public ObservationsDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            ObservationsRubricDetails = new List<ObservationsRubricDetailsModel>();
        }

        public int ObservationsDetailsId { get; set; }
        public string ObservationsDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public List<ObservationsRubricDetailsModel> ObservationsRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
