using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SectionMaster
    {
        public SectionMaster()
        {
            BodyPartMaster = new HashSet<BodyPartMaster>();
            QuestionGroupMaster = new HashSet<QuestionGroupMaster>();
            SubSectionMaster = new HashSet<SubSectionMaster>();
            TypeofSymptomsMaster = new HashSet<TypeofSymptomsMaster>();
        }

        public int SectionId { get; set; }
        public int? BodyPartSectionId { get; set; }
        public string SectionName { get; set; }
        public string SectionAlias { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual BodyPartSectionMaster BodyPartSection { get; set; }
        public virtual ICollection<BodyPartMaster> BodyPartMaster { get; set; }
        public virtual ICollection<QuestionGroupMaster> QuestionGroupMaster { get; set; }
        public virtual ICollection<SubSectionMaster> SubSectionMaster { get; set; }
        public virtual ICollection<TypeofSymptomsMaster> TypeofSymptomsMaster { get; set; }
    }
}
