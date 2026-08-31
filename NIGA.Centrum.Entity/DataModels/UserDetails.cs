using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class UserDetails
    {
        public int RecordId { get; set; }
        public long UserId { get; set; }
        public int MenuId { get; set; }
        public bool? IsView { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public int? FirmId { get; set; }

        public virtual FirmDetails Firm { get; set; }
        public virtual MenuMaster Menu { get; set; }
    }
}
