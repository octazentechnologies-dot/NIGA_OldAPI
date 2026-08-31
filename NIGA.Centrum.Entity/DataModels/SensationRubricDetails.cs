using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SensationRubricDetails
    {
        public int SensationRubricDetailsId { get; set; }
        public int SensationDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual SensationDetails SensationDetails { get; set; }
    }
}
