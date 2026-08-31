using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class UserLoginStatus
    {
        public int LoginId { get; set; }
        public DateTime LogDate { get; set; }
        public long UserId { get; set; }
        public string MachineNo { get; set; }
        public DateTime InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public bool? Satus { get; set; }
    }
}
