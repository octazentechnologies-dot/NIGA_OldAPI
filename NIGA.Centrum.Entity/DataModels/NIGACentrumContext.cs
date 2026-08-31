using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class NIGACentrumContext : DbContext
    {
        public NIGACentrumContext()
        {
        }

        public NIGACentrumContext(DbContextOptions<NIGACentrumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccompaniedDetails> AccompaniedDetails { get; set; }
        public virtual DbSet<AccompaniedRubricDetails> AccompaniedRubricDetails { get; set; }
        public virtual DbSet<AdverseReactionMaster> AdverseReactionMaster { get; set; }
        public virtual DbSet<AllopathicDrugMaster> AllopathicDrugMaster { get; set; }
        public virtual DbSet<AppointmentHistoryNote> AppointmentHistoryNote { get; set; }
        public virtual DbSet<AuthorMaster> AuthorMaster { get; set; }
        public virtual DbSet<BeforeAfterDuringDetails> BeforeAfterDuringDetails { get; set; }
        public virtual DbSet<BeforeAfterDuringRubricDetails> BeforeAfterDuringRubricDetails { get; set; }
        public virtual DbSet<BlogDetails> BlogDetails { get; set; }
        public virtual DbSet<BodyPartMaster> BodyPartMaster { get; set; }
        public virtual DbSet<BodyPartSectionMaster> BodyPartSectionMaster { get; set; }
        public virtual DbSet<CaseDetailRemedy> CaseDetailRemedy { get; set; }
        public virtual DbSet<CaseDetails> CaseDetails { get; set; }
        public virtual DbSet<CaseEntryChiefComplaint> CaseEntryChiefComplaint { get; set; }
        public virtual DbSet<CaseEntryDetails> CaseEntryDetails { get; set; }
        public virtual DbSet<CaseEntryDiagnosis> CaseEntryDiagnosis { get; set; }
        public virtual DbSet<ClinicalQueKeywords> ClinicalQueKeywords { get; set; }
        public virtual DbSet<ClinicalQueRubrics> ClinicalQueRubrics { get; set; }
        public virtual DbSet<ClinicalQuestionBodyPart> ClinicalQuestionBodyPart { get; set; }
        public virtual DbSet<ClinicalQuestions> ClinicalQuestions { get; set; }
        public virtual DbSet<ClipboardRubrics> ClipboardRubrics { get; set; }
        public virtual DbSet<CountryMaster> CountryMaster { get; set; }
        public virtual DbSet<DemoData> DemoData { get; set; }
        public virtual DbSet<DiagnosisCausation> DiagnosisCausation { get; set; }
        public virtual DbSet<DiagnosisCausationRubricDetails> DiagnosisCausationRubricDetails { get; set; }
        public virtual DbSet<DiagnosisDetails> DiagnosisDetails { get; set; }
        public virtual DbSet<DiagnosisKeywordSection> DiagnosisKeywordSection { get; set; }
        public virtual DbSet<DiagnosisGroupMaster> DiagnosisGroupMaster { get; set; }
        public virtual DbSet<DiagnosisMaster> DiagnosisMaster { get; set; }
        public virtual DbSet<DiagnosisMonogramDetails> DiagnosisMonogramDetails { get; set; }
        public virtual DbSet<DiagnosisMonogramRubricDetails> DiagnosisMonogramRubricDetails { get; set; }
        public virtual DbSet<DiagnosisMonograms> DiagnosisMonograms { get; set; }
        public virtual DbSet<DiagnosisPathology> DiagnosisPathology { get; set; }
        public virtual DbSet<DiagnosisPathologyDetails> DiagnosisPathologyDetails { get; set; }
        public virtual DbSet<DiagnosisPathologyRubricDetails> DiagnosisPathologyRubricDetails { get; set; }
        public virtual DbSet<DiagnosisSymptomRubric> DiagnosisSymptomRubric { get; set; }
        public virtual DbSet<DiagnosisSymptoms> DiagnosisSymptoms { get; set; }
        public virtual DbSet<DiagnosisSystem> DiagnosisSystem { get; set; }
        public virtual DbSet<DiagnosisSystemDetails> DiagnosisSystemDetails { get; set; }
        public virtual DbSet<DiagnosisTherapeuticsDetail> DiagnosisTherapeuticsDetail { get; set; }
        public virtual DbSet<DiseaseMaster> DiseaseMaster { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<DoctorReceptionStaff> DoctorReceptionStaff { get; set; }
        public virtual DbSet<DrugGroupMaster> DrugGroupMaster { get; set; }
        public virtual DbSet<DrugSystemMaster> DrugSystemMaster { get; set; }
        public virtual DbSet<EmergencieDetails> EmergencieDetails { get; set; }
        public virtual DbSet<EmergencieRubricDetails> EmergencieRubricDetails { get; set; }
        public virtual DbSet<EnquiryDetails> EnquiryDetails { get; set; }
        public virtual DbSet<FirmDetails> FirmDetails { get; set; }
        public virtual DbSet<GenderMaster> GenderMaster { get; set; }
        public virtual DbSet<HumanSystemMaster> HumanSystemMaster { get; set; }
        public virtual DbSet<IntensityMaster> IntensityMaster { get; set; }
        public virtual DbSet<LabTestMaster> LabTestMaster { get; set; }
        public virtual DbSet<LanguageMaster> LanguageMaster { get; set; }
        public virtual DbSet<LanguageVersion> LanguageVersion { get; set; }
        public virtual DbSet<LocationExtentionDetails> LocationExtentionDetails { get; set; }
        public virtual DbSet<LocationExtentionRubricDetails> LocationExtentionRubricDetails { get; set; }
        public virtual DbSet<MateriaMedicaDetail> MateriaMedicaDetail { get; set; }
        public virtual DbSet<MateriaMedicaHeadMaster> MateriaMedicaHeadMaster { get; set; }
        public virtual DbSet<MateriaMedicaMaster> MateriaMedicaMaster { get; set; }
        public virtual DbSet<MedicalAstrologyMaster> MedicalAstrologyMaster { get; set; }
        public virtual DbSet<MenuMaster> MenuMaster { get; set; }
        public virtual DbSet<ModalitiesDetails> ModalitiesDetails { get; set; }
        public virtual DbSet<ModalitiesRubricDetails> ModalitiesRubricDetails { get; set; }
        public virtual DbSet<ModuleMaster> ModuleMaster { get; set; }
        public virtual DbSet<Monogram> Monogram { get; set; }
        public virtual DbSet<MonogramDetails> MonogramDetails { get; set; }
        public virtual DbSet<NewsCategory> NewsCategory { get; set; }
        public virtual DbSet<NewsDetails> NewsDetails { get; set; }
        public virtual DbSet<ObservationsDetails> ObservationsDetails { get; set; }
        public virtual DbSet<ObservationsRubricDetails> ObservationsRubricDetails { get; set; }
        public virtual DbSet<OnsetDurationProgressDetails> OnsetDurationProgressDetails { get; set; }
        public virtual DbSet<OnsetDurationProgressRubricDetails> OnsetDurationProgressRubricDetails { get; set; }
        public virtual DbSet<OtherSideEffectMaster> OtherSideEffectMaster { get; set; }
        public virtual DbSet<PackageEntryDetails> PackageEntryDetails { get; set; }
        public virtual DbSet<PackageMaster> PackageMaster { get; set; }
        public virtual DbSet<PackageTopupMaster> PackageTopupMaster { get; set; }
        public virtual DbSet<PartLocationMaster> PartLocationMaster { get; set; }
        public virtual DbSet<Pathology> Pathology { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientAppointment> PatientAppointment { get; set; }
        public virtual DbSet<PatientLabEntry> PatientLabEntry { get; set; }
        public virtual DbSet<PatientLabOrder> PatientLabOrder { get; set; }
        public virtual DbSet<PatientLabTestMaster> PatientLabTestMaster { get; set; }
        public virtual DbSet<PatternRubricDetails> PatternRubricDetails { get; set; }
        public virtual DbSet<PatternsDetail> PatternsDetail { get; set; }
        public virtual DbSet<PrescriptionRemedyDetail> PrescriptionRemedyDetail { get; set; }
        public virtual DbSet<PrescriptionRubricDetail> PrescriptionRubricDetail { get; set; }
        public virtual DbSet<PsChangeDate> PsChangeDate { get; set; }
        public virtual DbSet<QualificationMaster> QualificationMaster { get; set; }
        public virtual DbSet<QuestionGroupMaster> QuestionGroupMaster { get; set; }
        public virtual DbSet<QuestionSectionMaster> QuestionSectionMaster { get; set; }
        public virtual DbSet<QuestionSubgroup> QuestionSubgroup { get; set; }
        public virtual DbSet<QuestionSubgroupSection> QuestionSubgroupSection { get; set; }
        public virtual DbSet<ReferenceRubricDetails> ReferenceRubricDetails { get; set; }
        public virtual DbSet<RemedyGradeMaster> RemedyGradeMaster { get; set; }
        public virtual DbSet<RemedyMaster> RemedyMaster { get; set; }
        public virtual DbSet<RemedyRubricAuthorDetails> RemedyRubricAuthorDetails { get; set; }
        public virtual DbSet<ReportSettings> ReportSettings { get; set; }
        public virtual DbSet<RoleDetails> RoleDetails { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<RubricRemedyDetails> RubricRemedyDetails { get; set; }
        public virtual DbSet<SearchPageSettings> SearchPageSettings { get; set; }
        public virtual DbSet<SectionGroupMaster> SectionGroupMaster { get; set; }
        public virtual DbSet<SectionMaster> SectionMaster { get; set; }
        public virtual DbSet<SensationDetails> SensationDetails { get; set; }
        public virtual DbSet<SensationRubricDetails> SensationRubricDetails { get; set; }
        public virtual DbSet<SeriousSideEffectMaster> SeriousSideEffectMaster { get; set; }
        public virtual DbSet<StateMaster> StateMaster { get; set; }
        public virtual DbSet<SubSectionLanguageDetails> SubSectionLanguageDetails { get; set; }
        public virtual DbSet<SubSectionMaster> SubSectionMaster { get; set; }
        public virtual DbSet<ThermalMaster> ThermalMaster { get; set; }
        public virtual DbSet<TypeofSymptomsGroupMaster> TypeofSymptomsGroupMaster { get; set; }
        public virtual DbSet<TypeofSymptomsMaster> TypeofSymptomsMaster { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserLoginStatus> UserLoginStatus { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<YearMaster> YearMaster { get; set; }

        // Unable to generate entity type for table 'dbo.ChestDataMig'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.chestdatamigfive'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ChestDataMiglevFive'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.chestdatamigtemp'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SampleTable'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=103.154.184.104;Database=Homeo;User Id=sa;Password=Homeo@niga19;TrustServerCertificate=True;Trusted_Connection=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AccompaniedDetails>(entity =>
            {
                entity.Property(e => e.AccompaniedDetailsSystem).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.AccompaniedDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccompaniedDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<AccompaniedRubricDetails>(entity =>
            {
                entity.HasOne(d => d.AccompaniedDetails)
                    .WithMany(p => p.AccompaniedRubricDetails)
                    .HasForeignKey(d => d.AccompaniedDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccompaniedRubricDetails_AccompaniedDetails");
            });

            modelBuilder.Entity<AdverseReactionMaster>(entity =>
            {
                entity.HasKey(e => e.AdverseReactionId);

                entity.Property(e => e.AdverseReactionName).IsRequired();

                entity.HasOne(d => d.AllopathicDrug)
                    .WithMany(p => p.AdverseReactionMaster)
                    .HasForeignKey(d => d.AllopathicDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdverseReactionMaster_AllopathicDrugMaster");
            });

            modelBuilder.Entity<AllopathicDrugMaster>(entity =>
            {
                entity.HasKey(e => e.AllopathicDrugId);

                entity.Property(e => e.AllopathicDrugName).IsRequired();

                entity.HasOne(d => d.DrugGroup)
                    .WithMany(p => p.AllopathicDrugMaster)
                    .HasForeignKey(d => d.DrugGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AllopathicDrugMaster_DrugGroupMaster");
            });

            modelBuilder.Entity<AppointmentHistoryNote>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentHistoryNote)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_AppointmentHistoryNote_PatientAppointment");
            });

            modelBuilder.Entity<AuthorMaster>(entity =>
            {
                entity.HasKey(e => e.AuthorId);

                entity.Property(e => e.AuthorAlias).HasMaxLength(20);

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<BeforeAfterDuringDetails>(entity =>
            {
                entity.Property(e => e.BeforeAfterDuringDetailsKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.BeforeAfterDuringDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BeforeAfterDuringDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<BeforeAfterDuringRubricDetails>(entity =>
            {
                entity.HasOne(d => d.BeforeAfterDuringDetails)
                    .WithMany(p => p.BeforeAfterDuringRubricDetails)
                    .HasForeignKey(d => d.BeforeAfterDuringDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BeforeAfterDuringRubricDetails_BeforeAfterDuringDetails");
            });

            modelBuilder.Entity<BlogDetails>(entity =>
            {
                entity.HasKey(e => e.BlogId);

                entity.Property(e => e.BlogDate).HasColumnType("datetime");

                entity.Property(e => e.BlogDetails1).HasColumnName("BlogDetails");

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BodyPartMaster>(entity =>
            {
                entity.HasKey(e => e.BodyPartId);

                entity.Property(e => e.BodyPartName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.BodyPartMaster)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodyPartMaster_BodyPartSectionMaster");
            });

            modelBuilder.Entity<BodyPartSectionMaster>(entity =>
            {
                entity.HasKey(e => e.BodyPartSectionId);

                entity.Property(e => e.BodyPartSectionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CaseDetailRemedy>(entity =>
            {
                entity.HasOne(d => d.Case)
                    .WithMany(p => p.CaseDetailRemedy)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_CaseDetailRemedy_CaseEntryDetails");
            });

            modelBuilder.Entity<CaseDetails>(entity =>
            {
                entity.HasKey(e => e.CaseDetailId);

                entity.HasOne(d => d.Intensity)
                    .WithMany(p => p.CaseDetails)
                    .HasForeignKey(d => d.IntensityId)
                    .HasConstraintName("FK_CaseDetails_IntensityMaster");

                entity.HasOne(d => d.Subsection)
                    .WithMany(p => p.CaseDetails)
                    .HasForeignKey(d => d.SubsectionId)
                    .HasConstraintName("FK_CaseDetails_SubSectionMaster");
            });

            modelBuilder.Entity<CaseEntryChiefComplaint>(entity =>
            {
                entity.HasKey(e => e.CaseChiefComplaintId);

                entity.Property(e => e.ChiefComplaintName).HasMaxLength(500);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.CaseEntryChiefComplaint)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("FK_CaseEntryChiefComplaint_CaseEntryDetails");
            });

            modelBuilder.Entity<CaseEntryDetails>(entity =>
            {
                entity.HasKey(e => e.CaseId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DateodFirstVisit).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.RefBy).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.CaseEntryDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseEntryDetails_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.CaseEntryDetails)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseEntryDetails_Patient");
            });

            modelBuilder.Entity<CaseEntryDiagnosis>(entity =>
            {
                entity.HasKey(e => e.CaseDiagnosisId);

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.CaseEntryDiagnosis)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseEntryDiagnosis_CaseEntryDiagnosis");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.CaseEntryDiagnosis)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseEntryDiagnosis_DiagnosisMaster");
            });

            modelBuilder.Entity<ClinicalQueKeywords>(entity =>
            {
                entity.HasKey(e => e.ClinicalQueKeywordId);

                entity.HasOne(d => d.Questions)
                    .WithMany(p => p.ClinicalQueKeywords)
                    .HasForeignKey(d => d.QuestionsId)
                    .HasConstraintName("FK_ClinicalQueKeywords_ClinicalQuestions");
            });

            modelBuilder.Entity<ClinicalQueRubrics>(entity =>
            {
                entity.HasKey(e => e.ClinicalQueRubricId);

                entity.Property(e => e.ClinicalQuestionBodyPartId).HasColumnName("ClinicalQuestionBodyPartID");

                entity.HasOne(d => d.Subsection)
                    .WithMany(p => p.ClinicalQueRubrics)
                    .HasForeignKey(d => d.SubsectionId)
                    .HasConstraintName("FK_ClinicalQueRubrics_SubSectionMaster");
            });

            modelBuilder.Entity<ClinicalQuestionBodyPart>(entity =>
            {
                entity.Property(e => e.ClinicalQuestionBodyPartId).HasColumnName("ClinicalQuestionBodyPartID");
            });

            modelBuilder.Entity<ClinicalQuestions>(entity =>
            {
                entity.HasKey(e => e.QuestionsId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.ClinicalQuestions)
                    .HasForeignKey(d => d.QuestionGroupId)
                    .HasConstraintName("FK_ClinicalQuestions_QuestionGroupMaster");
            });

            modelBuilder.Entity<ClipboardRubrics>(entity =>
            {
                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.Intensity).HasMaxLength(10);

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.ClipboardRubrics)
                    .HasForeignKey(d => d.SubSectionId)
                    .HasConstraintName("FK_ClipboardRubrics_SubSectionMaster");
            });

            modelBuilder.Entity<CountryMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CountryCode).HasMaxLength(50);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DemoData>(entity =>
            {
                entity.HasKey(e => e.SubSectionLanguageId);

                entity.Property(e => e.SubSectionDetails).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.DemoData)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DemoData_LanguageMaster");

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.DemoData)
                    .HasForeignKey(d => d.SubSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DemoData_SubSectionMaster");
            });

            modelBuilder.Entity<DiagnosisCausation>(entity =>
            {
                entity.HasKey(e => e.CausationId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisCausation)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_DiagnosisCausation_DiagnosisMaster");
            });

            modelBuilder.Entity<DiagnosisCausationRubricDetails>(entity =>
            {
                entity.HasKey(e => e.CausationRubricDetailsId)
                    .HasName("PK_CausationRubricDetails");

                entity.HasOne(d => d.Causation)
                    .WithMany(p => p.DiagnosisCausationRubricDetails)
                    .HasForeignKey(d => d.CausationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CausationRubricDetails_DiagnosisCausation");
            });

            modelBuilder.Entity<DiagnosisDetails>(entity =>
            {
                entity.HasKey(e => e.DiagnosisDetailId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_DiagnosisDetails_DiagnosisMaster");

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.DiagnosisDetails)
                    .HasForeignKey(d => d.SubSectionId)
                    .HasConstraintName("FK_DiagnosisDetails_SubSectionMaster");
            });

            modelBuilder.Entity<DiagnosisKeywordSection>(entity =>
            {
                entity.HasKey(e => e.DiagnosisKeywordSectionId);

                entity.ToTable("DiagnosisKeywordSection");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.KeywordType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DiagnosisGroupMaster>(entity =>
            {
                entity.HasKey(e => e.DiagnosisGroupId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DiagnosisGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DiagnosisMaster>(entity =>
            {
                entity.HasKey(e => e.DiagnosisId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.DiagnosisName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DiagnosisNameAlias).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords).HasMaxLength(100);
            });

            modelBuilder.Entity<DiagnosisMonogramDetails>(entity =>
            {
                entity.Property(e => e.DiagnosisMonogramKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisMonogramDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisMonogramDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<DiagnosisMonogramRubricDetails>(entity =>
            {
                entity.HasOne(d => d.DiagnosisMonogramDetails)
                    .WithMany(p => p.DiagnosisMonogramRubricDetails)
                    .HasForeignKey(d => d.DiagnosisMonogramDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisMonogramRubricDetails_DiagnosisMonogramDetails");
            });

            modelBuilder.Entity<DiagnosisMonograms>(entity =>
            {
                entity.HasKey(e => e.DiagnosisMonogramId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisMonograms)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_DiagnosisMonograms_DiagnosisMaster");

                entity.HasOne(d => d.Monogram)
                    .WithMany(p => p.DiagnosisMonograms)
                    .HasForeignKey(d => d.MonogramId)
                    .HasConstraintName("FK_DiagnosisMonograms_Monogram");
            });

            modelBuilder.Entity<DiagnosisPathology>(entity =>
            {
                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisPathology)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_DiagnosisPathology_DiagnosisMaster");

                entity.HasOne(d => d.Pathology)
                    .WithMany(p => p.DiagnosisPathology)
                    .HasForeignKey(d => d.PathologyId)
                    .HasConstraintName("FK_DiagnosisPathology_Pathology");
            });

            modelBuilder.Entity<DiagnosisPathologyDetails>(entity =>
            {
                entity.Property(e => e.DiagnosisPathologyKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisPathologyDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisPathologyDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<DiagnosisPathologyRubricDetails>(entity =>
            {
                entity.HasOne(d => d.DiagnosisPathologyDetails)
                    .WithMany(p => p.DiagnosisPathologyRubricDetails)
                    .HasForeignKey(d => d.DiagnosisPathologyDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisPathologyRubricDetails_DiagnosisPathologyDetails");
            });

            modelBuilder.Entity<DiagnosisSymptomRubric>(entity =>
            {
                entity.HasOne(d => d.DiagnosisSymptom)
                    .WithMany(p => p.DiagnosisSymptomRubric)
                    .HasForeignKey(d => d.DiagnosisSymptomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisSymptomRubric_DiagnosisSymptoms");
            });

            modelBuilder.Entity<DiagnosisSymptoms>(entity =>
            {
                entity.HasKey(e => e.DiagnosisSymptomId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisSymptoms)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_DiagnosisSymptoms_DiagnosisMaster");
            });

            modelBuilder.Entity<DiagnosisSystemDetails>(entity =>
            {
                entity.HasKey(e => e.DiagnosisSystemDetailId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisSystemDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisSystemDetails_DiagnosisMaster");

                entity.HasOne(d => d.DiagnosisSystem)
                    .WithMany(p => p.DiagnosisSystemDetails)
                    .HasForeignKey(d => d.DiagnosisSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisSystemDetails_DiagnosisSystem");
            });

            modelBuilder.Entity<DiagnosisTherapeuticsDetail>(entity =>
            {
                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.Property(e => e.DiagnosisTherapeuticsDetail1).HasColumnName("DiagnosisTherapeuticsDetail");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisTherapeuticsDetail)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisTherapeuticsDetail_DiagnosisTherapeuticsDetail");
            });

            modelBuilder.Entity<DiseaseMaster>(entity =>
            {
                entity.HasKey(e => e.DiseaseId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.EmailId).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.PassingCertNo).HasMaxLength(50);

                entity.Property(e => e.PassingUniversity).HasMaxLength(500);

                entity.Property(e => e.PermanantAddress).HasMaxLength(500);

                entity.Property(e => e.QualificationId).HasColumnName("QualificationID");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_DoctorMaster_PackageMaster");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK_DoctorMaster_QualificationMaster");
            });

            modelBuilder.Entity<DoctorReceptionStaff>(entity =>
            {
                entity.HasKey(e => e.ReceptionStaffId).HasName("PK_DoctorReceptionStaff");

                entity.ToTable("DoctorReceptionStaff");

                entity.Property(e => e.ReceptionStaffId).HasColumnName("ReceptionStaffID");
                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(500);
                entity.Property(e => e.FullName).HasMaxLength(250);
                entity.Property(e => e.ContactNumber).HasMaxLength(50);
                entity.Property(e => e.EmailId).HasMaxLength(250);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
                entity.Property(e => e.ChangedDate).HasColumnType("datetime");
                entity.Property(e => e.DeleteStatus)
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<DrugGroupMaster>(entity =>
            {
                entity.HasKey(e => e.DrugGroupId);

                entity.Property(e => e.DrugGroupName).IsRequired();

                entity.HasOne(d => d.DrugSystem)
                    .WithMany(p => p.DrugGroupMaster)
                    .HasForeignKey(d => d.DrugSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DrugGroupMaster_DrugSystemMaster");
            });

            modelBuilder.Entity<DrugSystemMaster>(entity =>
            {
                entity.HasKey(e => e.DrugSystemId);
            });

            modelBuilder.Entity<EmergencieDetails>(entity =>
            {
                entity.HasKey(e => e.EmergencieId);

                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.EmergencieDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_EmergencieDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<EmergencieRubricDetails>(entity =>
            {
                entity.HasKey(e => e.EmergencieRubricId);

                entity.HasOne(d => d.Emergencie)
                    .WithMany(p => p.EmergencieRubricDetails)
                    .HasForeignKey(d => d.EmergencieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmergencieRubricDetails_EmergencieDetails");
            });

            modelBuilder.Entity<EnquiryDetails>(entity =>
            {
                entity.HasKey(e => e.EnquiryId);

                entity.Property(e => e.EmailId).HasMaxLength(100);

                entity.Property(e => e.EnquiryDate).HasColumnType("datetime");

                entity.Property(e => e.EnquiryDetails1).HasColumnName("EnquiryDetails");

                entity.Property(e => e.EnquiryName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(15);
            });

            modelBuilder.Entity<FirmDetails>(entity =>
            {
                entity.HasKey(e => e.FirmId);

                entity.Property(e => e.ApplicationLockDate).HasColumnType("date");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DatabaseBackupPath).HasColumnName("DatabaseBackupPAth");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirmBranchName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmBranchNameMarathi).HasMaxLength(250);

                entity.Property(e => e.FirmConnectionPath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmEmailIid).HasMaxLength(50);

                entity.Property(e => e.FirmFaxNumber).HasMaxLength(15);

                entity.Property(e => e.FirmLogo).HasMaxLength(450);

                entity.Property(e => e.FirmName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmNameMarathi).HasMaxLength(250);

                entity.Property(e => e.FirmOfficeAddress)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmOfficeAddressMarathi).HasMaxLength(250);

                entity.Property(e => e.FirmPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.FirmRegDate).HasColumnType("datetime");

                entity.Property(e => e.FirmRegNumber)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LanguageIds)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MailPassword).HasMaxLength(30);

                entity.Property(e => e.ModuleIds).HasMaxLength(50);
            });

            modelBuilder.Entity<GenderMaster>(entity =>
            {
                entity.HasKey(e => e.GenderId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<HumanSystemMaster>(entity =>
            {
                entity.HasKey(e => e.HumanSystemId);

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<IntensityMaster>(entity =>
            {
                entity.HasKey(e => e.IntensityId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LabTestMaster>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.Property(e => e.ChangedBy).HasMaxLength(500);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(500);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.TestName).HasMaxLength(1000);
            });

            modelBuilder.Entity<LanguageMaster>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.Property(e => e.LanguageId).HasColumnName("languageId");

                entity.Property(e => e.LanguageName).HasColumnName("languageName");
            });

            modelBuilder.Entity<LanguageVersion>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.Property(e => e.LanguageLogo)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LocationExtentionDetails>(entity =>
            {
                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.Property(e => e.LocationExtentionDetailsKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.LocationExtentionDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationExtentionDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<LocationExtentionRubricDetails>(entity =>
            {
                entity.HasOne(d => d.LocationExtentionDetails)
                    .WithMany(p => p.LocationExtentionRubricDetails)
                    .HasForeignKey(d => d.LocationExtentionDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationExtentionRubricDetails_LocationExtentionDetails");
            });

            modelBuilder.Entity<MateriaMedicaDetail>(entity =>
            {
                entity.HasKey(e => e.MatriaMedicaDetailId);

                entity.Property(e => e.MateriaMedicaDetail1).HasColumnName("MateriaMedicaDetail");

                entity.HasOne(d => d.MateriaMedica)
                    .WithMany(p => p.MateriaMedicaDetail)
                    .HasForeignKey(d => d.MateriaMedicaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MateriaMedicaDetail_MateriaMedicaMaster");
            });

            modelBuilder.Entity<MateriaMedicaHeadMaster>(entity =>
            {
                entity.HasKey(e => e.MateriaMedicaHeadId);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DifferentialMm).HasColumnName("DifferentialMM");

                entity.Property(e => e.MateriaMedicaHeadName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.MateriaMedicaHeadMaster)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_MateriaMedicaHeadMaster_AuthorMaster");
            });

            modelBuilder.Entity<MateriaMedicaMaster>(entity =>
            {
                entity.HasKey(e => e.MateriaMedicaId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Dose).HasMaxLength(500);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.MateriaMedicaMaster)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_MateriaMedicaMaster_AuthorMaster");

                entity.HasOne(d => d.MateriaMedicaHead)
                    .WithMany(p => p.MateriaMedicaMaster)
                    .HasForeignKey(d => d.MateriaMedicaHeadId)
                    .HasConstraintName("FK_MateriaMedicaMaster_MateriaMedicaHeadMaster");

                entity.HasOne(d => d.Remedy)
                    .WithMany(p => p.MateriaMedicaMaster)
                    .HasForeignKey(d => d.RemedyId)
                    .HasConstraintName("FK_MateriaMedicaMaster_RemedyMaster");
            });

            modelBuilder.Entity<MedicalAstrologyMaster>(entity =>
            {
                entity.HasKey(e => e.AstrologyId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.MedicalAstrologyMaster)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK_MedicalAstrologyMaster_DiseaseMaster");
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.ControllerName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirmIds).IsRequired();

                entity.Property(e => e.IsLeaf)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuIcon)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuNameMarathi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuUrl).HasMaxLength(250);

                entity.Property(e => e.ShowInMainMenu)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.MenuMaster)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuMaster_ModuleMaster");
            });

            modelBuilder.Entity<ModalitiesDetails>(entity =>
            {
                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.Property(e => e.ModalitiesDetailsKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.ModalitiesDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModalitiesDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<ModalitiesRubricDetails>(entity =>
            {
                entity.HasOne(d => d.ModalitiesDetails)
                    .WithMany(p => p.ModalitiesRubricDetails)
                    .HasForeignKey(d => d.ModalitiesDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModalitiesRubricDetails_ModalitiesDetails");
            });

            modelBuilder.Entity<ModuleMaster>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ActionName)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(N'Browse')");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Layout')");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleAreaName).HasMaxLength(20);

                entity.Property(e => e.ModuleIcon).HasMaxLength(250);

                entity.Property(e => e.ModuleMarathiName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModuleUrl).HasMaxLength(250);
            });

            modelBuilder.Entity<Monogram>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.Monogram1)
                    .HasColumnName("Monogram")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<MonogramDetails>(entity =>
            {
                entity.HasKey(e => e.MonogramDetailId);

                entity.HasOne(d => d.Monogram)
                    .WithMany(p => p.MonogramDetails)
                    .HasForeignKey(d => d.MonogramId)
                    .HasConstraintName("FK_MonogramDetails_Monogram");
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.Property(e => e.NewsCategory1)
                    .HasColumnName("NewsCategory")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<NewsDetails>(entity =>
            {
                entity.HasKey(e => e.NewsId);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.NewsDate).HasColumnType("datetime");

                entity.HasOne(d => d.NewsCategory)
                    .WithMany(p => p.NewsDetails)
                    .HasForeignKey(d => d.NewsCategoryId)
                    .HasConstraintName("FK_NewsDetails_NewsCategory");
            });

            modelBuilder.Entity<ObservationsDetails>(entity =>
            {
                entity.Property(e => e.ObservationsDetailsKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.ObservationsDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationsDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<ObservationsRubricDetails>(entity =>
            {
                entity.HasOne(d => d.ObservationsDetails)
                    .WithMany(p => p.ObservationsRubricDetails)
                    .HasForeignKey(d => d.ObservationsDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationsRubricDetails_ObservationsDetails");
            });

            modelBuilder.Entity<OnsetDurationProgressDetails>(entity =>
            {
                entity.HasKey(e => e.OnsetDetailId)
                    .HasName("PK_Onset_Duration_ProgressDetails");

                entity.Property(e => e.OnsetKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.OnsetDurationProgressDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Onset_Duration_ProgressDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<OnsetDurationProgressRubricDetails>(entity =>
            {
                entity.HasKey(e => e.OnsetRubricId)
                    .HasName("PK_Onset_Duration_ProgressRubricDetails");

                entity.HasOne(d => d.OnsetDetail)
                    .WithMany(p => p.OnsetDurationProgressRubricDetails)
                    .HasForeignKey(d => d.OnsetDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Onset_Duration_ProgressRubricDetails_Onset_Duration_ProgressDetails");
            });

            modelBuilder.Entity<OtherSideEffectMaster>(entity =>
            {
                entity.HasKey(e => e.OtherSideEffectId);

                entity.Property(e => e.OtherSideEffectName).IsRequired();

                entity.HasOne(d => d.AllopathicDrug)
                    .WithMany(p => p.OtherSideEffectMaster)
                    .HasForeignKey(d => d.AllopathicDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OtherSideEffectMaster_AllopathicDrugMaster");
            });

            modelBuilder.Entity<PackageEntryDetails>(entity =>
            {
                entity.HasKey(e => e.PackageDetailId);

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasMaxLength(512);

                entity.Property(e => e.PaymentId).HasMaxLength(512);

                entity.Property(e => e.TransactionId).HasMaxLength(512);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PackageEntryDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_PackageEntryDetails_Doctor");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageEntryDetails)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_PackageEntryDetails_PackageMaster");
            });

            modelBuilder.Entity<PackageMaster>(entity =>
            {
                entity.HasKey(e => e.PackageId);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PackageTopupMaster>(entity =>
            {
                entity.HasKey(e => e.PackageTopupId);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.PackageTopupName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PartLocationMaster>(entity =>
            {
                entity.HasKey(e => e.PartLocationId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.PartLocationName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Pathology>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.PathologyName).HasMaxLength(1000);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.PatientName).HasMaxLength(200);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_PatientMaster_CountryMaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_PatientMaster_StateMaster");
            });

            modelBuilder.Entity<PatientAppointment>(entity =>
            {
                entity.HasKey(e => e.PatientAppId);

                entity.Property(e => e.AppointmentDate).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PatientAppointment)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientAppointment_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientAppointment)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientAppointment_PatientAppointment");
            });

            modelBuilder.Entity<PatientLabEntry>(entity =>
            {
                entity.HasKey(e => e.PatientLabId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.LabDate).HasColumnType("datetime");

                entity.Property(e => e.ParameterName).HasMaxLength(500);

                entity.Property(e => e.ParameterValue).HasMaxLength(500);

                entity.HasOne(d => d.PatientLabTest)
                    .WithMany(p => p.PatientLabEntry)
                    .HasForeignKey(d => d.PatientLabTestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientLabEntry_PatientLabTestMaster");
            });

            modelBuilder.Entity<PatientLabOrder>(entity =>
            {
                entity.HasKey(e => e.PatientOrderedTestId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.LabName).HasMaxLength(500);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.PatientLabTest)
                    .WithMany(p => p.PatientLabOrder)
                    .HasForeignKey(d => d.PatientLabTestId)
                    .HasConstraintName("FK_PatientLabOrder_PatientLabTestMaster");
            });

            modelBuilder.Entity<PatientLabTestMaster>(entity =>
            {
                entity.HasKey(e => e.PatientLabTestId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.LabTestName).HasMaxLength(1000);
            });

            modelBuilder.Entity<PatternRubricDetails>(entity =>
            {
                entity.HasOne(d => d.PatternDetails)
                    .WithMany(p => p.PatternRubricDetails)
                    .HasForeignKey(d => d.PatternDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatternRubricDetails_PatternRubricDetails");
            });

            modelBuilder.Entity<PatternsDetail>(entity =>
            {
                entity.HasKey(e => e.PatternDetailsId);

                entity.Property(e => e.PatternsKeywords).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.PatternsDetail)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatternsDetail_DiagnosisMaster");
            });

            modelBuilder.Entity<PrescriptionRemedyDetail>(entity =>
            {
                entity.HasKey(e => e.PrescriptionRemedyId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dose).HasMaxLength(250);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PrescriptionRemedyDetail)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionRemedyDetail_PatientAppointment");
            });

            modelBuilder.Entity<PrescriptionRubricDetail>(entity =>
            {
                entity.HasKey(e => e.PrescriptionRubricId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PrescriptionRubricDetail)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionRubricDetail_PatientAppointment");
            });

            modelBuilder.Entity<PsChangeDate>(entity =>
            {
                entity.HasKey(e => e.ChangeDateId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseMonth).HasColumnType("datetime");

                entity.Property(e => e.CloseYear).HasColumnType("datetime");

                entity.Property(e => e.CurrDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<QualificationMaster>(entity =>
            {
                entity.HasKey(e => e.QualificationId);

                entity.Property(e => e.QualificationId).HasColumnName("QualificationID");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DegreeLevel).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.QualificationAlias).HasMaxLength(20);

                entity.Property(e => e.QualificationName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<QuestionGroupMaster>(entity =>
            {
                entity.HasKey(e => e.QuestionGroupId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.QuestionGroupName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.QuestionGroupMaster)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_QuestionGroupMaster_SectionMaster");
            });

            modelBuilder.Entity<QuestionSectionMaster>(entity =>
            {
                entity.HasKey(e => e.QuestionSectionId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Desciption).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.QuestionSectionName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<QuestionSubgroup>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1500);

                entity.Property(e => e.QuestionSubgroup1)
                    .HasColumnName("QuestionSubgroup")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<QuestionSubgroupSection>(entity =>
            {
                entity.HasKey(e => e.QuestionSubgroupSectionId);

                entity.ToTable("QuestionSubgroupSection");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReferenceRubricDetails>(entity =>
            {
                entity.HasKey(e => e.ReferenceRubricId);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.RefSubSection)
                    .WithMany(p => p.ReferenceRubricDetailsRefSubSection)
                    .HasForeignKey(d => d.RefSubSectionId)
                    .HasConstraintName("FK_ReferenceRubricDetails_SubSectionMaster1");

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.ReferenceRubricDetailsSubSection)
                    .HasForeignKey(d => d.SubSectionId)
                    .HasConstraintName("FK_ReferenceRubricDetails_SubSectionMaster");
            });

            modelBuilder.Entity<RemedyGradeMaster>(entity =>
            {
                entity.HasKey(e => e.GradeId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FontColor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FontName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FontStyle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RemedyMaster>(entity =>
            {
                entity.HasKey(e => e.RemedyId)
                    .HasName("PK_Remedy");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.RemedyAlias).HasMaxLength(100);

                entity.Property(e => e.RemedyName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Thermal)
                    .WithMany(p => p.RemedyMaster)
                    .HasForeignKey(d => d.ThermalId)
                    .HasConstraintName("FK_RemedyMaster_ThermalMaster");
            });

            modelBuilder.Entity<RemedyRubricAuthorDetails>(entity =>
            {
                entity.HasKey(e => e.RemedyRubricAuthorId);

                entity.Property(e => e.DeletedStatus).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.RemedyRubricAuthorDetails)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_RemedyRubricAuthorDetails_AuthorMaster");

                entity.HasOne(d => d.RubricRemedy)
                    .WithMany(p => p.RemedyRubricAuthorDetails)
                    .HasForeignKey(d => d.RubricRemedyId)
                    .HasConstraintName("FK_RemedyRubricAuthorDetails_RubricRemedyDetails");
            });

            modelBuilder.Entity<ReportSettings>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ReportSetting");

                entity.Property(e => e.Applicablefor).HasMaxLength(50);

                entity.Property(e => e.BranchAddressFontSize).HasMaxLength(50);

                entity.Property(e => e.BranchFontSize).HasMaxLength(50);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FilterCriteria).HasMaxLength(450);

                entity.Property(e => e.FirmIds).IsRequired();

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.MultipleIssue)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReportFont).HasMaxLength(50);

                entity.Property(e => e.ReportFontSize).HasMaxLength(50);

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrustFontSize).HasMaxLength(50);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.ReportSettings)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportSetting_MenuMaster");
            });

            modelBuilder.Entity<RoleDetails>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.Property(e => e.IsAdd)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsModify)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsView)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleDetails_MenuMaster");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleDetails)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleDetails_RoleMaster");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirmIds)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RubricRemedyDetails>(entity =>
            {
                entity.HasKey(e => e.RubricRemedyId);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.RubricRemedyDetails)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_RubricRemedyDetails_RemedyGradeMaster");

                entity.HasOne(d => d.Remedy)
                    .WithMany(p => p.RubricRemedyDetails)
                    .HasForeignKey(d => d.RemedyId)
                    .HasConstraintName("FK_RubricRemedyDetails_RemedyMaster");

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.RubricRemedyDetails)
                    .HasForeignKey(d => d.SubSectionId)
                    .HasConstraintName("FK_RubricRemedyDetails_SubSectionMaster");
            });

            modelBuilder.Entity<SearchPageSettings>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_SearchPageSetting");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DataKeyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ExceptTableNames).HasMaxLength(250);

                entity.Property(e => e.FilterCriteria).HasMaxLength(250);

                entity.Property(e => e.FirmIds).IsRequired();

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.SearchPageSettings)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SearchPageSetting_MenuMaster");
            });

            modelBuilder.Entity<SectionGroupMaster>(entity =>
            {
                entity.HasKey(e => e.SectionGroupId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.SectionGroupName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SectionMaster>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.SectionAlias).HasMaxLength(50);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.BodyPartSection)
                    .WithMany(p => p.SectionMaster)
                    .HasForeignKey(d => d.BodyPartSectionId)
                    .HasConstraintName("FK_SectionMaster_BodyPartSectionMaster");
            });

            modelBuilder.Entity<SensationDetails>(entity =>
            {
                entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");

                entity.Property(e => e.SensationDetailsKeyword).IsRequired();

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.SensationDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SensationDetails_DiagnosisMaster");
            });

            modelBuilder.Entity<SensationRubricDetails>(entity =>
            {
                entity.HasOne(d => d.SensationDetails)
                    .WithMany(p => p.SensationRubricDetails)
                    .HasForeignKey(d => d.SensationDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SensationRubricDetails_SensationDetails");
            });

            modelBuilder.Entity<SeriousSideEffectMaster>(entity =>
            {
                entity.HasKey(e => e.SeriousSideEffectId);

                entity.Property(e => e.SeriousSideEffectName).IsRequired();

                entity.HasOne(d => d.AllopathicDrug)
                    .WithMany(p => p.SeriousSideEffectMaster)
                    .HasForeignKey(d => d.AllopathicDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeriousSideEffectMaster_AllopathicDrugMaster");
            });

            modelBuilder.Entity<StateMaster>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateMaster)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_StateMaster_CountryMaster");
            });

            modelBuilder.Entity<SubSectionLanguageDetails>(entity =>
            {
                entity.HasKey(e => e.SubSectionLanguageId);

                entity.Property(e => e.SubSectionDetails).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.SubSectionLanguageDetails)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubSectionLanguageDetails_LanguageMaster");

                entity.HasOne(d => d.SubSection)
                    .WithMany(p => p.SubSectionLanguageDetails)
                    .HasForeignKey(d => d.SubSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubSectionLanguageDetails_SubSectionMaster");
            });

            modelBuilder.Entity<SubSectionMaster>(entity =>
            {
                entity.HasKey(e => e.SubSectionId);

                entity.Property(e => e.SubSectionId).HasColumnName("SubSectionID");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ParentSubSectionId).HasColumnName("ParentSubSectionID");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.SearchNormalized);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SubSectionMaster)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SubSectionMaster_SectionMaster");
            });

            modelBuilder.Entity<ThermalMaster>(entity =>
            {
                entity.HasKey(e => e.ThermalId);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.ThermalName).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeofSymptomsGroupMaster>(entity =>
            {
                entity.HasKey(e => e.TypeofSymptomsGroupId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.TypeofSymptomsGroupName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TypeofSymptomsMaster>(entity =>
            {
                entity.HasKey(e => e.TypeofSymptomsId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.TypeofSymptomsName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.SectionGroup)
                    .WithMany(p => p.TypeofSymptomsMaster)
                    .HasForeignKey(d => d.SectionGroupId)
                    .HasConstraintName("FK_TypeofSymptomsMaster_SectionGroupMaster");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TypeofSymptomsMaster)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_TypeofSymptomsMaster_SectionMaster");

                entity.HasOne(d => d.TypeofSymptomsGroup)
                    .WithMany(p => p.TypeofSymptomsMaster)
                    .HasForeignKey(d => d.TypeofSymptomsGroupId)
                    .HasConstraintName("FK_TypeofSymptomsMaster_TypeofSymptomsGroupMaster");
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_UserDetail");

                entity.Property(e => e.IsAdd).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsModify).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsView).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.FirmId)
                    .HasConstraintName("FK_UserDetail_FirmDetails");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetail_MenuMaster");
            });

            modelBuilder.Entity<UserLoginStatus>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.Property(e => e.LoginId).ValueGeneratedNever();

                entity.Property(e => e.InTime).HasColumnType("datetime");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.MachineNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OutTime).HasColumnType("datetime");

                entity.Property(e => e.Satus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.OldPassword).HasMaxLength(50);

                entity.Property(e => e.PasswordRenewDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhoto).HasMaxLength(250);

                entity.Property(e => e.UserStatus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<YearMaster>(entity =>
            {
                entity.HasKey(e => e.YearId)
                    .HasName("PK_YearMasters");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayYear)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.YearType).HasMaxLength(50);

                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.YearMaster)
                    .HasForeignKey(d => d.FirmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YearMaster_FirmDetails");
            });

            modelBuilder.Query<SubSectionSearchResponse>();
            modelBuilder.Query<SubSectionSearchMatchRow>();
        }
    }
}
