using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ModalitiesRubricDetails
    {
        public int ModalitiesRubricDetailsId { get; set; }
        public int ModalitiesDetailsId { get; set; }
        public int? SubsectionId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual ModalitiesDetails ModalitiesDetails { get; set; }
    }
}
