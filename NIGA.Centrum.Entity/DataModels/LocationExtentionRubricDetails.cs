using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class LocationExtentionRubricDetails
    {
        public int LocationExtentionRubricDetailsId { get; set; }
        public int LocationExtentionDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual LocationExtentionDetails LocationExtentionDetails { get; set; }
    }
}
