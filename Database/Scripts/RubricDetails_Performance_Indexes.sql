-- Speed up GetRubricDetails lookups for Patient Board RUBRIC DETAILS panel.
-- Run against the NIGA Centrum database used by api.homeocentrum.com / api1.homeocentrum.com.

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_RubricRemedyDetails_SubSectionId_DeletedStatus'
      AND object_id = OBJECT_ID('dbo.RubricRemedyDetails')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_RubricRemedyDetails_SubSectionId_DeletedStatus
        ON dbo.RubricRemedyDetails (SubSectionId, DeletedStatus)
        INCLUDE (RemedyId, GradeId, RubricRemedyId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_RemedyRubricAuthorDetails_RubricRemedyId_DeletedStatus'
      AND object_id = OBJECT_ID('dbo.RemedyRubricAuthorDetails')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_RemedyRubricAuthorDetails_RubricRemedyId_DeletedStatus
        ON dbo.RemedyRubricAuthorDetails (RubricRemedyId, DeletedStatus)
        INCLUDE (AuthorId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ReferenceRubricDetails_SubSectionId_DeleteStatus'
      AND object_id = OBJECT_ID('dbo.ReferenceRubricDetails')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_ReferenceRubricDetails_SubSectionId_DeleteStatus
        ON dbo.ReferenceRubricDetails (SubSectionId, DeleteStatus)
        INCLUDE (RefSubSectionId, ReferenceRubricId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_SubSectionLanguageDetails_SubSectionId_DeleteStatus'
      AND object_id = OBJECT_ID('dbo.SubSectionLanguageDetails')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_SubSectionLanguageDetails_SubSectionId_DeleteStatus
        ON dbo.SubSectionLanguageDetails (SubSectionId, DeleteStatus)
        INCLUDE (LanguageId, SubSectionDetails, SubSectionLanguageId);
END
GO
