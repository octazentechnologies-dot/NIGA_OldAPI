using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class SubSectionMaster
    {
        public SubSectionMaster()
        {
            CaseDetails = new HashSet<CaseDetails>();
            ClinicalQueRubrics = new HashSet<ClinicalQueRubrics>();
            ClipboardRubrics = new HashSet<ClipboardRubrics>();
            DemoData = new HashSet<DemoData>();
            DiagnosisDetails = new HashSet<DiagnosisDetails>();
            ReferenceRubricDetailsRefSubSection = new HashSet<ReferenceRubricDetails>();
            ReferenceRubricDetailsSubSection = new HashSet<ReferenceRubricDetails>();
            RubricRemedyDetails = new HashSet<RubricRemedyDetails>();
            SubSectionLanguageDetails = new HashSet<SubSectionLanguageDetails>();
        }

        public int SubSectionId { get; set; }
        public int? SectionId { get; set; }
        public string SubSectionName { get; set; }
        public string SubSectionNameAlias { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int? ParentSubSectionId { get; set; }
        public int? BodyPartId { get; set; }
        public int? PartLocationId { get; set; }

        public bool? MainParentSubsection { get; set; }

        public string SearchNormalized { get; set; }

        public virtual SectionMaster Section { get; set; }
        public virtual ICollection<CaseDetails> CaseDetails { get; set; }
        public virtual ICollection<ClinicalQueRubrics> ClinicalQueRubrics { get; set; }
        public virtual ICollection<ClipboardRubrics> ClipboardRubrics { get; set; }
        public virtual ICollection<DemoData> DemoData { get; set; }
        public virtual ICollection<DiagnosisDetails> DiagnosisDetails { get; set; }
        public virtual ICollection<ReferenceRubricDetails> ReferenceRubricDetailsRefSubSection { get; set; }
        public virtual ICollection<ReferenceRubricDetails> ReferenceRubricDetailsSubSection { get; set; }
        public virtual ICollection<RubricRemedyDetails> RubricRemedyDetails { get; set; }
        public virtual ICollection<SubSectionLanguageDetails> SubSectionLanguageDetails { get; set; }
    }
}
