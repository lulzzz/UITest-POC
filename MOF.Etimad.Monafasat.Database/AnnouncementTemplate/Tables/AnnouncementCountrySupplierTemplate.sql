CREATE TABLE [AnnouncementTemplate].[AnnouncementCountrySupplierTemplate] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [CountryId]      INT            NOT NULL,
    [AnnouncementId] INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementCountrySupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementCountrySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementCountrySupplierTemplate_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementCountrySupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementCountrySupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementCountrySupplierTemplate_CountryId]
    ON [AnnouncementTemplate].[AnnouncementCountrySupplierTemplate]([CountryId] ASC);

