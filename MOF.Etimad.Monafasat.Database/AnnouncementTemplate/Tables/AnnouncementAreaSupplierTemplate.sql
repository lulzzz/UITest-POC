CREATE TABLE [AnnouncementTemplate].[AnnouncementAreaSupplierTemplate] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [AreaId]         INT            NOT NULL,
    [AnnouncementId] INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementAreaSupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementAreaSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementAreaSupplierTemplate_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementAreaSupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementAreaSupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementAreaSupplierTemplate_AreaId]
    ON [AnnouncementTemplate].[AnnouncementAreaSupplierTemplate]([AreaId] ASC);

