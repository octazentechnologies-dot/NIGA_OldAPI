using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class AdverseReactionMaster
    {
        public int AdverseReactionId { get; set; }
        public string AdverseReactionName { get; set; }
        public int AllopathicDrugId { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual AllopathicDrugMaster AllopathicDrug { get; set; }
    }
}
