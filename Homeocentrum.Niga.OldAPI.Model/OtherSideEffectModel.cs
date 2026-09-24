using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class OtherSideEffectModel
    {
        public int OtherSideEffectId { get; set; }
        public string OtherSideEffectName { get; set; }
        public int AllopathicDrugId { get; set; }
        public string AllopathicDrugName { get; set; }
        public bool? DeleteStatus { get; set; }
    }
}
