-- Shared junction: Diagnosis keyword details -> many SectionMaster rows
-- Run manually against the NIGA database before testing.

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'DiagnosisKeywordSection')
BEGIN
    CREATE TABLE dbo.DiagnosisKeywordSection
    (
        DiagnosisKeywordSectionId INT IDENTITY(1,1) NOT NULL,
        DiagnosisId               INT NOT NULL,
        KeywordType               NVARCHAR(50) NOT NULL,
        KeywordDetailId           INT NOT NULL,
        SectionID                 INT NOT NULL,
        DeleteStatus              BIT NOT NULL CONSTRAINT DF_DiagnosisKeywordSection_DeleteStatus DEFAULT (0),
        EnteredBy                 NVARCHAR(50) NULL,
        EnteredDate               DATETIME NULL,
        ChangedBy                 NVARCHAR(50) NULL,
        ChangedDate               DATETIME NULL,

        CONSTRAINT PK_DiagnosisKeywordSection
            PRIMARY KEY CLUSTERED (DiagnosisKeywordSectionId),

        CONSTRAINT FK_DiagnosisKeywordSection_DiagnosisMaster
            FOREIGN KEY (DiagnosisId)
            REFERENCES dbo.DiagnosisMaster (DiagnosisID),

        CONSTRAINT FK_DiagnosisKeywordSection_SectionMaster
            FOREIGN KEY (SectionID)
            REFERENCES dbo.SectionMaster (SectionID),

        CONSTRAINT UQ_DiagnosisKeywordSection_Type_Detail_Section
            UNIQUE (KeywordType, KeywordDetailId, SectionID)
    );

    CREATE NONCLUSTERED INDEX IX_DiagnosisKeywordSection_DiagnosisId
        ON dbo.DiagnosisKeywordSection (DiagnosisId)
        INCLUDE (KeywordType, KeywordDetailId, SectionID, DeleteStatus);

    CREATE NONCLUSTERED INDEX IX_DiagnosisKeywordSection_Keyword
        ON dbo.DiagnosisKeywordSection (KeywordType, KeywordDetailId)
        INCLUDE (SectionID, DeleteStatus);
END
GO
