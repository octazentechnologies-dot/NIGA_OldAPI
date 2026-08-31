using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class CaseEntryChiefComplaint
    {
        public int CaseChiefComplaintId { get; set; }
        public int? CaseId { get; set; }
        public string ChiefComplaintName { get; set; }

        public virtual CaseEntryDetails Case { get; set; }
    }
}
