CREATE TABLE [AnnouncementTemplate].[LinkedAgenciesAnnouncementTemplate] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [AgencyCode]     NVARCHAR (20)  NULL,
    [AnnouncementId] INT            NOT NULL,
    CONSTRAINT [PK_LinkedAgenciesAnnouncementTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LinkedAgenciesAnnouncementTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_LinkedAgenciesAnnouncementTemplate_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_LinkedAgenciesAnnouncementTemplate_AgencyCode]
    ON [AnnouncementTemplate].[LinkedAgenciesAnnouncementTemplate]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LinkedAgenciesAnnouncementTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[LinkedAgenciesAnnouncementTemplate]([AnnouncementId] ASC);

