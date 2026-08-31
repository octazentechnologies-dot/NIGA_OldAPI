using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class CaseDetailRemedy
    {
        public int CaseDetailRemedyId { get; set; }
        public int? CaseId { get; set; }
        public int? RemedyId { get; set; }
        public int? RemedyIndex { get; set; }

        public virtual CaseEntryDetails Case { get; set; }
    }
}
