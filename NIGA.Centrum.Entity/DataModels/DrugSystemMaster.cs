using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DrugSystemMaster
    {
        public DrugSystemMaster()
        {
            DrugGroupMaster = new HashSet<DrugGroupMaster>();
        }

        public int DrugSystemId { get; set; }
        public string DrugSystemName { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<DrugGroupMaster> DrugGroupMaster { get; set; }
    }
}
