using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PackageTopupMaster
    {
        public int PackageTopupId { get; set; }
        public string PackageTopupName { get; set; }
        public int CaseCount { get; set; }
        public decimal Amount { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
