CREATE TABLE [Announcement].[AnnouncementConstructionWork] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]          DATETIME2 (7)  NOT NULL,
    [CreatedBy]          NVARCHAR (256) NULL,
    [UpdatedAt]          DATETIME2 (7)  NULL,
    [UpdatedBy]          NVARCHAR (256) NULL,
    [IsActive]           BIT            NULL,
    [ConstructionWorkId] INT            NOT NULL,
    [AnnouncementId]     INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementConstructionWork] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementConstructionWork_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementConstructionWork_AnnouncementId]
    ON [Announcement].[AnnouncementConstructionWork]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementConstructionWork_ConstructionWorkId]
    ON [Announcement].[AnnouncementConstructionWork]([ConstructionWorkId] ASC);

