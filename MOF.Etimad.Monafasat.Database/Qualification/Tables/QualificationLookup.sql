CREATE TABLE [Qualification].[QualificationLookup] (
    [ID]                    INT           IDENTITY (1, 1) NOT NULL,
    [QualificationLookupId] INT           NOT NULL,
    [Name]                  NVARCHAR (50) NULL,
    [NameEn]                NVARCHAR (50) NULL,
    CONSTRAINT [PK_QualificationLookup] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationLookup_QualificationLookupsNames_QualificationLookupId] FOREIGN KEY ([QualificationLookupId]) REFERENCES [Qualification].[QualificationLookupsNames] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationLookup_QualificationLookupId]
    ON [Qualification].[QualificationLookup]([QualificationLookupId] ASC);

