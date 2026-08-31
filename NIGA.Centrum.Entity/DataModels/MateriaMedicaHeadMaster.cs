using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class MateriaMedicaHeadMaster
    {
        public MateriaMedicaHeadMaster()
        {
            MateriaMedicaMaster = new HashSet<MateriaMedicaMaster>();
        }

        public int MateriaMedicaHeadId { get; set; }
        public int? AuthorId { get; set; }
        public string MateriaMedicaHeadName { get; set; }
        public string Description { get; set; }
        public bool? IsSection { get; set; }
        public int? SeqNo { get; set; }
        public bool? DifferentialMm { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AuthorMaster Author { get; set; }
        public virtual ICollection<MateriaMedicaMaster> MateriaMedicaMaster { get; set; }
    }
}
