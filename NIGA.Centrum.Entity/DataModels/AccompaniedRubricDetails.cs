using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class AccompaniedRubricDetails
    {
        public int AccompaniedRubricDetailsId { get; set; }
        public int AccompaniedDetailsId { get; set; }
        public int SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual AccompaniedDetails AccompaniedDetails { get; set; }
    }
}
