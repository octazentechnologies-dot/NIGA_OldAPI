using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DemoData
    {
        public int SubSectionLanguageId { get; set; }
        public int SubSectionId { get; set; }
        public int LanguageId { get; set; }
        public string SubSectionDetails { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual LanguageMaster Language { get; set; }
        public virtual SubSectionMaster SubSection { get; set; }
    }
}
