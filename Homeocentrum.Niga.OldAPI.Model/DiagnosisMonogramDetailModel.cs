using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class DiagnosisMonogramDetailModel
    {
        public DiagnosisMonogramDetailModel()
        {
            DiagnosisMonogramRubricDetails = new List<DiagnosisMonogramRubricDetailsModel>();
        }

        public int DiagnosisMonogramDetailsId { get; set; }
        public string DiagnosisMonogramKeyword { get; set; }
        public int DiagnosisId { get; set; }
        public bool? DeletedStatus { get; set; }

        public List<DiagnosisMonogramRubricDetailsModel> DiagnosisMonogramRubricDetails { get; set; }

    }
}
