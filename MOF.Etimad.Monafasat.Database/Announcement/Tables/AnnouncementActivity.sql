CREATE TABLE [Announcement].[AnnouncementActivity] (
    [CreatedAt]              DATETIME2 (7)  NOT NULL,
    [CreatedBy]              NVARCHAR (256) NULL,
    [UpdatedAt]              DATETIME2 (7)  NULL,
    [UpdatedBy]              NVARCHAR (256) NULL,
    [IsActive]               BIT            NULL,
    [ActivityId]             INT            NOT NULL,
    [AnnouncementId]         INT            NOT NULL,
    [AnnouncementActivityId] INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [FK_AnnouncementActivity_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementActivity_ActivityId]
    ON [Announcement].[AnnouncementActivity]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementActivity_AnnouncementId]
    ON [Announcement].[AnnouncementActivity]([AnnouncementId] ASC);

