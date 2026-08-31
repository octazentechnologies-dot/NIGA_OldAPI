using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DrugGroupMaster
    {
        public DrugGroupMaster()
        {
            AllopathicDrugMaster = new HashSet<AllopathicDrugMaster>();
        }

        public int DrugGroupId { get; set; }
        public int DrugSystemId { get; set; }
        public string DrugGroupName { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual DrugSystemMaster DrugSystem { get; set; }
        public virtual ICollection<AllopathicDrugMaster> AllopathicDrugMaster { get; set; }
    }
}
