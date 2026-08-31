using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class AccompaniedDetailsModel
    {
        public AccompaniedDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            AccompaniedRubricDetails = new List<AccompaniedRubricDetailsModel>();
        }

        public int AccompaniedDetailsId { get; set; }
        public string AccompaniedDetailsSystem { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public  List<AccompaniedRubricDetailsModel> AccompaniedRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
