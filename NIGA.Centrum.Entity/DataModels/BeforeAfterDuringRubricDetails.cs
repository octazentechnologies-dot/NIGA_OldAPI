using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class BeforeAfterDuringRubricDetails
    {
        public int BeforeAfterDuringRubricDetailsId { get; set; }
        public int BeforeAfterDuringDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual BeforeAfterDuringDetails BeforeAfterDuringDetails { get; set; }
    }
}
