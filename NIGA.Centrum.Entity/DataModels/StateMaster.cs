using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class StateMaster
    {
        public StateMaster()
        {
            Patient = new HashSet<Patient>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CountryId { get; set; }

        public virtual CountryMaster Country { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
