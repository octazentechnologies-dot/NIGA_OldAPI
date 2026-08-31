using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class PackageMaster
    {
        public PackageMaster()
        {
            Doctor = new HashSet<Doctor>();
            PackageEntryDetails = new HashSet<PackageEntryDetails>();
        }

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int CaseCount { get; set; }
        public int ValidityInDays { get; set; }
        public decimal Amount { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<PackageEntryDetails> PackageEntryDetails { get; set; }
    }
}
