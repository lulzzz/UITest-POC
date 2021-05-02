CREATE TABLE [Announcement].[AnnouncementJoinRequest] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [StatusId]       INT            NOT NULL,
    [AnnouncementId] INT            NOT NULL,
    [Cr]             NVARCHAR (20)  NULL,
    CONSTRAINT [PK_AnnouncementJoinRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementJoinRequest_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementJoinRequest_AnnouncementJoinRequestStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[AnnouncementJoinRequestStatus] ([Id]),
    CONSTRAINT [FK_AnnouncementJoinRequest_Supplier_Cr] FOREIGN KEY ([Cr]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementJoinRequest_Cr]
    ON [Announcement].[AnnouncementJoinRequest]([Cr] ASC);

