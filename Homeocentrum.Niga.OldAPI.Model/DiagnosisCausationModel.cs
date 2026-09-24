using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class DiagnosisCausationModel
    {
        public DiagnosisCausationModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            DiagnosisCausationRubricDetails = new List<DiagnosisCausationRubricDetailsModel>();
        }

        public int CausationId { get; set; }
        public int? DiagnosisId { get; set; }
        public string CausationName { get; set; }

        public List<DiagnosisCausationRubricDetailsModel> DiagnosisCausationRubricDetails { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
