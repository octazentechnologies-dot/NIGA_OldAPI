using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class SensationDetailsModel
    {
        public SensationDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            SensationRubricDetails = new List<SensationRubricDetailsModel>();
        }

        public int SensationDetailsId { get; set; }
        public string SensationDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }
      
        public virtual List<SensationRubricDetailsModel> SensationRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
