using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SectionGroupMaster
    {
        public SectionGroupMaster()
        {
            TypeofSymptomsMaster = new HashSet<TypeofSymptomsMaster>();
        }

        public int SectionGroupId { get; set; }
        public string SectionGroupName { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<TypeofSymptomsMaster> TypeofSymptomsMaster { get; set; }
    }
}
