using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class QualificationMaster
    {
        public QualificationMaster()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
        public string QualificationAlias { get; set; }
        public string Description { get; set; }
        public string DegreeLevel { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
