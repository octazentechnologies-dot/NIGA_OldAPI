using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class ModalitiesDetailsModel
    {
        public ModalitiesDetailsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            ModalitiesRubricDetails = new List<ModalitiesRubricDetailsModel>();
        }

        public int ModalitiesDetailsId { get; set; }
        public string ModalitiesDetailsKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }
        public List<ModalitiesRubricDetailsModel> ModalitiesRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
