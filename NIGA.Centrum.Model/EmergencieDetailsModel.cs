using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class EmergencieDetailsModel
    {
        public EmergencieDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            EmergencieRubricDetails = new List<EmergencieRubricDetailsModel>();
        }
        public int EmergencieId { get; set; }
        public string EmergencieKeyword { get; set; }
        public int? DiagnosisId { get; set; }
        public bool DeletedStatus { get; set; }

        public List<EmergencieRubricDetailsModel> EmergencieRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
