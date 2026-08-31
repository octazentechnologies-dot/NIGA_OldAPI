using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DiagnosisMonogramDetailsModel
    {
        public DiagnosisMonogramDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            DiagnosisMonogramRubricDetails = new List<DiagnosisMonogramRubricDetailsModel>();
        }

        public int DiagnosisMonogramDetailsId { get; set; }
        public string DiagnosisMonogramKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public List<DiagnosisMonogramRubricDetailsModel> DiagnosisMonogramRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
