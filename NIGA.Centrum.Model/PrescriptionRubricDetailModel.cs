using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PrescriptionRubricDetailModel
    {
        public int PrescriptionRubricId { get; set; } = 0;
        public int RubricId { get; set; } = 0;
        public int IntensityId { get; set; } = 0;
        public int RemedyCount { get; set; } = 0;
    }

    public class PrescriptionRubricDetailViewModel
    {
        public int PrescriptionRubricId { get; set; } = 0;
        public int AppointmentId { get; set; } = 0;
        public int RubricId { get; set; } = 0;
        public string RubricName { get; set; } = string.Empty;
        public int IntensityId { get; set; } = 0;
        public int RemedyCount { get; set; } = 0;
    }

    public class PrescriptionDetailModel
    {

        public PrescriptionDetailModel() {
            this.PrescriptionRubricDetailList = new List<PrescriptionRubricDetailModel>();
            this.PrescriptionRemedyDetailList = new List<PrescriptionRemedyDetailModel>();
        }
        public int AppointmentId { get; set; } = 0;

        public List<PrescriptionRubricDetailModel> PrescriptionRubricDetailList { get; set; }  
        public List<PrescriptionRemedyDetailModel> PrescriptionRemedyDetailList { get; set; }

    }
}
