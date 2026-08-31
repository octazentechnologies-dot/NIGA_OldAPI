using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SearchPageSettings
    {
        public int RecordId { get; set; }
        public int MenuId { get; set; }
        public string FilterCriteria { get; set; }
        public string MethodName { get; set; }
        public string DataKeyName { get; set; }
        public string FirmIds { get; set; }
        public string TableName { get; set; }
        public string ExceptTableNames { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual MenuMaster Menu { get; set; }
    }
}
