using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class NewsCategory
    {
        public NewsCategory()
        {
            NewsDetails = new HashSet<NewsDetails>();
        }

        public int NewsCategoryId { get; set; }
        public string NewsCategory1 { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<NewsDetails> NewsDetails { get; set; }
    }
}
