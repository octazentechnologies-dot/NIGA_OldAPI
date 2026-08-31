using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SeriousSideEffectMaster
    {
        public int SeriousSideEffectId { get; set; }
        public string SeriousSideEffectName { get; set; }
        public int AllopathicDrugId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual AllopathicDrugMaster AllopathicDrug { get; set; }
    }
}
