using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ThermalMaster
    {
        public ThermalMaster()
        {
            RemedyMaster = new HashSet<RemedyMaster>();
        }

        public int ThermalId { get; set; }
        public string ThermalName { get; set; }
        public string Color { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<RemedyMaster> RemedyMaster { get; set; }
    }
}
