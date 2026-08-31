using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class DiagnosisMaster
    {
        public DiagnosisMaster()
        {
            AccompaniedDetails = new HashSet<AccompaniedDetails>();
            BeforeAfterDuringDetails = new HashSet<BeforeAfterDuringDetails>();
            CaseEntryDiagnosis = new HashSet<CaseEntryDiagnosis>();
            DiagnosisCausation = new HashSet<DiagnosisCausation>();
            DiagnosisDetails = new HashSet<DiagnosisDetails>();
            DiagnosisMonogramDetails = new HashSet<DiagnosisMonogramDetails>();
            DiagnosisMonograms = new HashSet<DiagnosisMonograms>();
            DiagnosisPathology = new HashSet<DiagnosisPathology>();
            DiagnosisPathologyDetails = new HashSet<DiagnosisPathologyDetails>();
            DiagnosisSymptoms = new HashSet<DiagnosisSymptoms>();
            DiagnosisSystemDetails = new HashSet<DiagnosisSystemDetails>();
            DiagnosisTherapeuticsDetail = new HashSet<DiagnosisTherapeuticsDetail>();
            EmergencieDetails = new HashSet<EmergencieDetails>();
            LocationExtentionDetails = new HashSet<LocationExtentionDetails>();
            ModalitiesDetails = new HashSet<ModalitiesDetails>();
            ObservationsDetails = new HashSet<ObservationsDetails>();
            OnsetDurationProgressDetails = new HashSet<OnsetDurationProgressDetails>();
            PatternsDetail = new HashSet<PatternsDetail>();
            SensationDetails = new HashSet<SensationDetails>();
        }

        public int DiagnosisId { get; set; }
        public int? DiagnosisGroupId { get; set; }
        public string DiagnosisName { get; set; }
        public string DiagnosisNameAlias { get; set; }
        public string Miasm { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Investigations { get; set; }
        public string AllopathicMedicines { get; set; }
        public string Examiniations { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public virtual ICollection<AccompaniedDetails> AccompaniedDetails { get; set; }
        public virtual ICollection<BeforeAfterDuringDetails> BeforeAfterDuringDetails { get; set; }
        public virtual ICollection<CaseEntryDiagnosis> CaseEntryDiagnosis { get; set; }
        public virtual ICollection<DiagnosisCausation> DiagnosisCausation { get; set; }
        public virtual ICollection<DiagnosisDetails> DiagnosisDetails { get; set; }
        public virtual ICollection<DiagnosisMonogramDetails> DiagnosisMonogramDetails { get; set; }
        public virtual ICollection<DiagnosisMonograms> DiagnosisMonograms { get; set; }
        public virtual ICollection<DiagnosisPathology> DiagnosisPathology { get; set; }
        public virtual ICollection<DiagnosisPathologyDetails> DiagnosisPathologyDetails { get; set; }
        public virtual ICollection<DiagnosisSymptoms> DiagnosisSymptoms { get; set; }
        public virtual ICollection<DiagnosisSystemDetails> DiagnosisSystemDetails { get; set; }
        public virtual ICollection<DiagnosisTherapeuticsDetail> DiagnosisTherapeuticsDetail { get; set; }
        public virtual ICollection<EmergencieDetails> EmergencieDetails { get; set; }
        public virtual ICollection<LocationExtentionDetails> LocationExtentionDetails { get; set; }
        public virtual ICollection<ModalitiesDetails> ModalitiesDetails { get; set; }
        public virtual ICollection<ObservationsDetails> ObservationsDetails { get; set; }
        public virtual ICollection<OnsetDurationProgressDetails> OnsetDurationProgressDetails { get; set; }
        public virtual ICollection<PatternsDetail> PatternsDetail { get; set; }
        public virtual ICollection<SensationDetails> SensationDetails { get; set; }
    }
}
