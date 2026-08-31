using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DiagnosisSymptomsModel
    {
        public DiagnosisSymptomsModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();

            DiagnosisSymptomRubric = new List<DiagnosisSymptomRubricModel>();
        }
        public int DiagnosisSymptomId { get; set; }
        public int? DiagnosisId { get; set; }
        public string Symptom { get; set; }
        public int? EnteredBy { get; set; }
        public List<DiagnosisSymptomRubricModel> DiagnosisSymptomRubric { get; set; }

        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }

    }
}
