using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class BlogDetails
    {
        public int BlogId { get; set; }
        public string BlogHead { get; set; }
        public string BlogSubHead { get; set; }
        public DateTime? BlogDate { get; set; }
        public string BlogImage1 { get; set; }
        public string BlogImage2 { get; set; }
        public string BlogDetails1 { get; set; }
        public bool? IsActive { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
