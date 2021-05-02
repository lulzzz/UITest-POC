CREATE TABLE [AnnouncementTemplate].[AnnouncementConstructionWorkSupplierTemplate] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]          DATETIME2 (7)  NOT NULL,
    [CreatedBy]          NVARCHAR (256) NULL,
    [UpdatedAt]          DATETIME2 (7)  NULL,
    [UpdatedBy]          NVARCHAR (256) NULL,
    [IsActive]           BIT            NULL,
    [ConstructionWorkId] INT            NOT NULL,
    [AnnouncementId]     INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementConstructionWorkSupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementConstructionWorkSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementConstructionWorkSupplierTemplate_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementConstructionWorkSupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementConstructionWorkSupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementConstructionWorkSupplierTemplate_ConstructionWorkId]
    ON [AnnouncementTemplate].[AnnouncementConstructionWorkSupplierTemplate]([ConstructionWorkId] ASC);

