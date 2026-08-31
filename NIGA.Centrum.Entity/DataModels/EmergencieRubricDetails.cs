using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class EmergencieRubricDetails
    {
        public int EmergencieRubricId { get; set; }
        public int EmergencieId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual EmergencieDetails Emergencie { get; set; }
    }
}
