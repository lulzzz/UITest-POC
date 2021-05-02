CREATE TABLE [Qualification].[QualificationYear] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (250) NULL,
    CONSTRAINT [PK_QualificationYear] PRIMARY KEY CLUSTERED ([ID] ASC)
);

