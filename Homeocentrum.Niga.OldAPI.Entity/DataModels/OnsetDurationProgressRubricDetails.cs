using System;
using System.Collections.Generic;

namespace Homeocentrum.Niga.OldAPI.Entity.DataModels
{
    public partial class OnsetDurationProgressRubricDetails
    {
        public int OnsetRubricId { get; set; }
        public int OnsetDetailId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual OnsetDurationProgressDetails OnsetDetail { get; set; }
    }
}
