using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class DiagnosisGroupViewModel
    {
       
        public int DiagnosisGroupId { get; set; }
        public string DiagnosisGroupName { get; set; }
        public List<DiagnosisModel> listDiagnosisModel { get; set; }
    }
}
