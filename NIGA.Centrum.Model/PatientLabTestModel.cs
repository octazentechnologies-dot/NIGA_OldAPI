using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PatientLabTestModel
    {
        public int PatientLabTestId { get; set; } = 0;
        public string LabTestName { get; set; } = string.Empty;

        public string Description { get; set; }= string.Empty;
    }
}
