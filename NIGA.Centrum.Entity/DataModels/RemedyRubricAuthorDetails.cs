using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class RemedyRubricAuthorDetails
    {
        public int RemedyRubricAuthorId { get; set; }
        public int? RubricRemedyId { get; set; }
        public int? AuthorId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual AuthorMaster Author { get; set; }
        public virtual RubricRemedyDetails RubricRemedy { get; set; }
    }
}
