using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class GenderMaster
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
