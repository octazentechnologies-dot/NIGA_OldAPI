using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class CountryMaster
    {
        public CountryMaster()
        {
            Patient = new HashSet<Patient>();
            StateMaster = new HashSet<StateMaster>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<StateMaster> StateMaster { get; set; }
    }
}
