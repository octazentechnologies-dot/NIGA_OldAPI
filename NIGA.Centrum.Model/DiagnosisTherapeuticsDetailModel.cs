using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DiagnosisTherapeuticsDetailModel
    {
        public int DiagnosisTherapeuticsDetailId { get; set; }
        public int DiagnosisId { get; set; }
        public string DiagnosisName { get; set; }
        public string DiagnosisTherapeuticsDetail1 { get; set; }
        public bool? DeletedStatus { get; set; }

    }
}
