CREATE TABLE [Announcement].[AnnouncementCountry] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [CountryId]      INT            NOT NULL,
    [AnnouncementId] INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementCountry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementCountry_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementCountry_AnnouncementId]
    ON [Announcement].[AnnouncementCountry]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementCountry_CountryId]
    ON [Announcement].[AnnouncementCountry]([CountryId] ASC);

