using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DiagnosisMonogramsModel
    {
        public int DiagnosisMonogramId { get; set; }
        public int? MonogramId { get; set; }
        public int? DiagnosisId { get; set; }
        public string Monogram { get; set; }
    }
}
