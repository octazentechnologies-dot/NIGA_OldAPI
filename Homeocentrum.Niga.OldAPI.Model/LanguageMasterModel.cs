using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class LanguageMasterModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
