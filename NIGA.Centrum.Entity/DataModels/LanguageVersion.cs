using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class LanguageVersion
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageLogo { get; set; }
        public int? SeqNo { get; set; }
    }
}
