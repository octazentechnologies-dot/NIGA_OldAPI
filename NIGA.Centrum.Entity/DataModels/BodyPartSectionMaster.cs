using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class BodyPartSectionMaster
    {
        public BodyPartSectionMaster()
        {
            SectionMaster = new HashSet<SectionMaster>();
        }

        public int BodyPartSectionId { get; set; }
        public string BodyPartSectionName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<SectionMaster> SectionMaster { get; set; }
    }
}
