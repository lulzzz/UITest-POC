CREATE TABLE [Announcement].[AnnouncementHistory] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [StatusId]        INT             NOT NULL,
    [RejectionReason] NVARCHAR (2000) NULL,
    [AnnouncementId]  INT             NOT NULL,
    [UserId]          INT             NOT NULL,
    CONSTRAINT [PK_AnnouncementHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementHistory_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementHistory_AnnouncementId]
    ON [Announcement].[AnnouncementHistory]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementHistory_StatusId]
    ON [Announcement].[AnnouncementHistory]([StatusId] ASC);

