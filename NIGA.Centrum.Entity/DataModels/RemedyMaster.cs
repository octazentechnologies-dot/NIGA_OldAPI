using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RemedyMaster
    {
        public RemedyMaster()
        {
            MateriaMedicaMaster = new HashSet<MateriaMedicaMaster>();
            RubricRemedyDetails = new HashSet<RubricRemedyDetails>();
        }

        public int RemedyId { get; set; }
        public string RemedyName { get; set; }
        public string RemedyAlias { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? ThermalId { get; set; }
        public bool? CommonOrUncommon { get; set; }
        public string ThemesOrCharacteristics { get; set; }
        public string Generals { get; set; }
        public string Modalities { get; set; }
        public string Particulars { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ThermalMaster Thermal { get; set; }
        public virtual ICollection<MateriaMedicaMaster> MateriaMedicaMaster { get; set; }
        public virtual ICollection<RubricRemedyDetails> RubricRemedyDetails { get; set; }
    }
}
