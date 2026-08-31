-- Mapping table: one QuestionSubgroup -> many SectionMaster rows
-- Run this manually against the NIGA database before testing the feature.

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QuestionSubgroupSection')
BEGIN
    CREATE TABLE dbo.QuestionSubgroupSection
    (
        QuestionSubgroupSectionId INT IDENTITY(1,1) NOT NULL,
        QuestionSubgroupId        INT NOT NULL,
        SectionID                 INT NOT NULL,
        DeleteStatus              BIT NOT NULL CONSTRAINT DF_QuestionSubgroupSection_DeleteStatus DEFAULT (0),
        EnteredBy                 NVARCHAR(50) NULL,
        EnteredDate               DATETIME NULL,
        ChangedBy                 NVARCHAR(50) NULL,
        ChangedDate               DATETIME NULL,

        CONSTRAINT PK_QuestionSubgroupSection
            PRIMARY KEY CLUSTERED (QuestionSubgroupSectionId),

        CONSTRAINT FK_QuestionSubgroupSection_QuestionSubgroup
            FOREIGN KEY (QuestionSubgroupId)
            REFERENCES dbo.QuestionSubgroup (QuestionSubgroupId),

        CONSTRAINT FK_QuestionSubgroupSection_SectionMaster
            FOREIGN KEY (SectionID)
            REFERENCES dbo.SectionMaster (SectionID),

        CONSTRAINT UQ_QuestionSubgroupSection_SubGroup_Section
            UNIQUE (QuestionSubgroupId, SectionID)
    );

    CREATE NONCLUSTERED INDEX IX_QuestionSubgroupSection_QuestionSubgroupId
        ON dbo.QuestionSubgroupSection (QuestionSubgroupId)
        INCLUDE (SectionID, DeleteStatus);

    CREATE NONCLUSTERED INDEX IX_QuestionSubgroupSection_SectionID
        ON dbo.QuestionSubgroupSection (SectionID)
        INCLUDE (QuestionSubgroupId, DeleteStatus);
END
GO
