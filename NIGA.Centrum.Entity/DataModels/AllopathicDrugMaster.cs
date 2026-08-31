using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class AllopathicDrugMaster
    {
        public AllopathicDrugMaster()
        {
            AdverseReactionMaster = new HashSet<AdverseReactionMaster>();
            OtherSideEffectMaster = new HashSet<OtherSideEffectMaster>();
            SeriousSideEffectMaster = new HashSet<SeriousSideEffectMaster>();
        }

        public int AllopathicDrugId { get; set; }
        public int DrugGroupId { get; set; }
        public string AllopathicDrugName { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual DrugGroupMaster DrugGroup { get; set; }
        public virtual ICollection<AdverseReactionMaster> AdverseReactionMaster { get; set; }
        public virtual ICollection<OtherSideEffectMaster> OtherSideEffectMaster { get; set; }
        public virtual ICollection<SeriousSideEffectMaster> SeriousSideEffectMaster { get; set; }
    }
}
