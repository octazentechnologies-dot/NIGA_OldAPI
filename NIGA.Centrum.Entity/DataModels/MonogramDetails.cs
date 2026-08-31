using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class MonogramDetails
    {
        public int MonogramDetailId { get; set; }
        public int? MonogramId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Monogram Monogram { get; set; }
    }
}
