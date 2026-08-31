using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PatternRubricDetails
    {
        public int PatternRubricDetailsId { get; set; }
        public int PatternDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual PatternsDetail PatternDetails { get; set; }
    }
}
