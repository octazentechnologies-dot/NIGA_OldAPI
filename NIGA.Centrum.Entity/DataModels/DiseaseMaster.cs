using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiseaseMaster
    {
        public DiseaseMaster()
        {
            MedicalAstrologyMaster = new HashSet<MedicalAstrologyMaster>();
        }

        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<MedicalAstrologyMaster> MedicalAstrologyMaster { get; set; }
    }
}
