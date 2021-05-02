CREATE TABLE [LookUps].[TenderCreatedByType] (
    [TenderCreatedByTypeId] INT            NOT NULL,
    [NameAr]                NVARCHAR (100) NULL,
    [NameEn]                NVARCHAR (100) NULL,
    CONSTRAINT [PK_TenderCreatedByType] PRIMARY KEY CLUSTERED ([TenderCreatedByTypeId] ASC)
);

