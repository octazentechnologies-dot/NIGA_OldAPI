using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class OtherSideEffectMaster
    {
        public int OtherSideEffectId { get; set; }
        public string OtherSideEffectName { get; set; }
        public int AllopathicDrugId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual AllopathicDrugMaster AllopathicDrug { get; set; }
    }
}
