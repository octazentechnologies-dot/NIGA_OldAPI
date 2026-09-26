using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class PrescriptionRemedyDetailModel
    {
        public int PrescriptionRemedyId { get; set; } = 0;
        public int RemedyId { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public int? PotencyId { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
    }

    public class PrescriptionRemedyDetailViewModel
    {
        public int PrescriptionRemedyId { get; set; }= 0;
        public int AppointmentId { get; set; } = 0;
        public int RemedyId { get; set; } = 0;
        public string RemedyName { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public int? PotencyId { get; set; }
        public string PotencyCode { get; set; } = string.Empty;
    }

    public class PrescriptionRemedyViewModel
    {
        public int RemedyId { get; set; } = 0;
        public string RemedyName { get; set; } = string.Empty;
       
    }
}
