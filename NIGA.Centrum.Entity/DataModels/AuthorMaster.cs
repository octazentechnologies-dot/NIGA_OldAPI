using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class AuthorMaster
    {
        public AuthorMaster()
        {
            MateriaMedicaHeadMaster = new HashSet<MateriaMedicaHeadMaster>();
            MateriaMedicaMaster = new HashSet<MateriaMedicaMaster>();
            RemedyRubricAuthorDetails = new HashSet<RemedyRubricAuthorDetails>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsForRepertory { get; set; }
        public string AuthorAlias { get; set; }

        public virtual ICollection<MateriaMedicaHeadMaster> MateriaMedicaHeadMaster { get; set; }
        public virtual ICollection<MateriaMedicaMaster> MateriaMedicaMaster { get; set; }
        public virtual ICollection<RemedyRubricAuthorDetails> RemedyRubricAuthorDetails { get; set; }
    }
}
