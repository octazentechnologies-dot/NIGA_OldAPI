using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DoctorMaster
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? QualificationId { get; set; }
        public string LocalAddress { get; set; }
        public string PermanantAddress { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int? CasePaperValidity { get; set; }

        public virtual QualificationMaster Qualification { get; set; }
    }
}
