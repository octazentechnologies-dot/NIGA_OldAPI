using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ObservationsRubricDetails
    {
        public int ObservationsRubricDetailsId { get; set; }
        public int ObservationsDetailsId { get; set; }
        public int Subsection { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual ObservationsDetails ObservationsDetails { get; set; }
    }
}
