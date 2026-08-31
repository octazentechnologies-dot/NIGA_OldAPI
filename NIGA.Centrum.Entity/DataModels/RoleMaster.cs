using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            RoleDetails = new HashSet<RoleDetails>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string FirmIds { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<RoleDetails> RoleDetails { get; set; }
    }
}
