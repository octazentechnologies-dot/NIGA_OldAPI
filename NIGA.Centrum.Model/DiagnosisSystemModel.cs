using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DiagnosisSystemModel
    {
        public int DiagnosisSystemId { get; set; }

        public string DiagnosisSystemName { get; set; }
       
        public string Description { get; set; }
        public bool IsActive { get; set; }


    }
}
