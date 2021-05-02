CREATE TABLE [Qualification].[QualificationLookupsNames] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (250) NULL,
    [NameEn] NVARCHAR (250) NULL,
    CONSTRAINT [PK_QualificationLookupsNames] PRIMARY KEY CLUSTERED ([ID] ASC)
);

