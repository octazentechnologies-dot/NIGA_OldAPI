using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class MateriaMedicaMaster
    {
        public MateriaMedicaMaster()
        {
            MateriaMedicaDetail = new HashSet<MateriaMedicaDetail>();
        }

        public int MateriaMedicaId { get; set; }
        public int? AuthorId { get; set; }
        public int? RemedyId { get; set; }
        public int? MateriaMedicaHeadId { get; set; }
        public string Dose { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? SeqNo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AuthorMaster Author { get; set; }
        public virtual MateriaMedicaHeadMaster MateriaMedicaHead { get; set; }
        public virtual RemedyMaster Remedy { get; set; }
        public virtual ICollection<MateriaMedicaDetail> MateriaMedicaDetail { get; set; }
    }
}
