using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class LanguageMaster
    {
        public LanguageMaster()
        {
            DemoData = new HashSet<DemoData>();
            SubSectionLanguageDetails = new HashSet<SubSectionLanguageDetails>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<DemoData> DemoData { get; set; }
        public virtual ICollection<SubSectionLanguageDetails> SubSectionLanguageDetails { get; set; }
    }
}
