CREATE TABLE [Notification].[Notification] (
    [CreatedAt]                 DATETIME2 (7)  NOT NULL,
    [CreatedBy]                 NVARCHAR (256) NULL,
    [UpdatedAt]                 DATETIME2 (7)  NULL,
    [UpdatedBy]                 NVARCHAR (256) NULL,
    [IsActive]                  BIT            NULL,
    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]                    INT            NULL,
    [CR]                        NVARCHAR (20)  NULL,
    [NotifacationStatusId]      INT            NOT NULL,
    [sendAt]                    DATETIME2 (7)  NULL,
    [Link]                      NVARCHAR (MAX) NULL,
    [Key]                       NVARCHAR (MAX) NULL,
    [NotificationSettingId]     INT            NULL,
    [NotifayByType]             INT            NOT NULL,
    [Title]                     NVARCHAR (MAX) NULL,
    [Content]                   NVARCHAR (MAX) NULL,
    [CurrentEmail]              NVARCHAR (MAX) NULL,
    [NotificationPanel_Title]   NVARCHAR (MAX) NULL,
    [NotificationPanel_Content] NVARCHAR (MAX) NULL,
    [BranchId]                  INT            NULL,
    [CommitteeId]               INT            NULL,
    [NotificationSMS_Content]   NVARCHAR (MAX) NULL,
    [CurrentMobile]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Notification_NotifacationStatusEntity_NotifacationStatusId] FOREIGN KEY ([NotifacationStatusId]) REFERENCES [LookUps].[NotifacationStatusEntity] ([NotifacationStatusEntityId]),
    CONSTRAINT [FK_Notification_Supplier_CR] FOREIGN KEY ([CR]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_Notification_UserNotificationSetting_NotificationSettingId] FOREIGN KEY ([NotificationSettingId]) REFERENCES [Notification].[UserNotificationSetting] ([Id]),
    CONSTRAINT [FK_Notification_UserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Notification_CR]
    ON [Notification].[Notification]([CR] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Notification_NotifacationStatusId]
    ON [Notification].[Notification]([NotifacationStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Notification_NotificationSettingId]
    ON [Notification].[Notification]([NotificationSettingId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Notification_UserId]
    ON [Notification].[Notification]([UserId] ASC);

