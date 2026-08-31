using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RoleDetails
    {
        public int RecordId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool? IsView { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }

        public virtual MenuMaster Menu { get; set; }
        public virtual RoleMaster Role { get; set; }
    }
}
