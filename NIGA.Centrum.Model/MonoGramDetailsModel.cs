using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class MonoGramDetailsModel
    {
        public int MonogramDetailId { get; set; }
        public int? MonogramId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? IsDelete { get; set; }

    }
    public class MonoGramDetailsListModel
    {
        public int MonogramDetailId { get; set; }
        public int? MonogramId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? IsDelete { get; set; }
        public string SubsectionName { get; set; }

    }
}
