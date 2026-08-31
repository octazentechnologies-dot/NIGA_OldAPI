using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class TypeofSymptomsGroupMaster
    {
        public TypeofSymptomsGroupMaster()
        {
            TypeofSymptomsMaster = new HashSet<TypeofSymptomsMaster>();
        }

        public int TypeofSymptomsGroupId { get; set; }
        public string TypeofSymptomsGroupName { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<TypeofSymptomsMaster> TypeofSymptomsMaster { get; set; }
    }
}
