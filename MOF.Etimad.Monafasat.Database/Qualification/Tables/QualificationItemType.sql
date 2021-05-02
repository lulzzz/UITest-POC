CREATE TABLE [Qualification].[QualificationItemType] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NULL,
    [NameEn] NVARCHAR (50) NULL,
    CONSTRAINT [PK_QualificationItemType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

