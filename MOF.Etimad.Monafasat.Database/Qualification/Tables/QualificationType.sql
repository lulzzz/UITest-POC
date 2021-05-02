CREATE TABLE [Qualification].[QualificationType] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [NameEn]    NVARCHAR (50) NULL,
    [IsDeleted] BIT           NULL,
    CONSTRAINT [PK_QualificationType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

