CREATE TABLE [Announcement].[AnnouncementArea] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [AreaId]         INT            NOT NULL,
    [AnnouncementId] INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementArea_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementArea_AnnouncementId]
    ON [Announcement].[AnnouncementArea]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementArea_AreaId]
    ON [Announcement].[AnnouncementArea]([AreaId] ASC);

