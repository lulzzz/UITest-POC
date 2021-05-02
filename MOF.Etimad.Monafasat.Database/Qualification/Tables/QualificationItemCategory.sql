CREATE TABLE [Qualification].[QualificationItemCategory] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NULL,
    [NameEn] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_QualificationItemCategory] PRIMARY KEY CLUSTERED ([ID] ASC)
);

