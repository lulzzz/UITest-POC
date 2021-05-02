CREATE TABLE [Tender].[TenderCountry] (
    [CreatedAt] DATETIME2 (7)  NOT NULL,
    [CreatedBy] NVARCHAR (256) NULL,
    [UpdatedAt] DATETIME2 (7)  NULL,
    [UpdatedBy] NVARCHAR (256) NULL,
    [IsActive]  BIT            NULL,
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [CountryId] INT            NOT NULL,
    [TenderId]  INT            NOT NULL,
    CONSTRAINT [PK_TenderCountry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderCountry_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId]),
    CONSTRAINT [FK_TenderCountry_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderCountry_CountryId]
    ON [Tender].[TenderCountry]([CountryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderCountry_TenderId]
    ON [Tender].[TenderCountry]([TenderId] ASC);

