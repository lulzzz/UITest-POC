CREATE TABLE [Notification].[UserNotificationSetting] (
    [CreatedAt]          DATETIME2 (7)  NOT NULL,
    [CreatedBy]          NVARCHAR (256) NULL,
    [UpdatedAt]          DATETIME2 (7)  NULL,
    [UpdatedBy]          NVARCHAR (256) NULL,
    [IsActive]           BIT            NULL,
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserProfileId]      INT            NULL,
    [IsArabic]           BIT            NOT NULL,
    [OperationCode]      NVARCHAR (MAX) NULL,
    [Cr]                 NVARCHAR (20)  NULL,
    [NotificationCodeId] INT            NOT NULL,
    [Sms]                BIT            NOT NULL,
    [Email]              BIT            NOT NULL,
    [UserRoleId]         INT            NOT NULL,
    CONSTRAINT [PK_UserNotificationSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserNotificationSetting_NotificationOperationCode_NotificationCodeId] FOREIGN KEY ([NotificationCodeId]) REFERENCES [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId]),
    CONSTRAINT [FK_UserNotificationSetting_Supplier_Cr] FOREIGN KEY ([Cr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_UserNotificationSetting_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id]),
    CONSTRAINT [FK_UserNotificationSetting_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [LookUps].[UserRole] ([UserRoleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserNotificationSetting_Cr]
    ON [Notification].[UserNotificationSetting]([Cr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserNotificationSetting_NotificationCodeId]
    ON [Notification].[UserNotificationSetting]([NotificationCodeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserNotificationSetting_UserProfileId]
    ON [Notification].[UserNotificationSetting]([UserProfileId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserNotificationSetting_UserRoleId]
    ON [Notification].[UserNotificationSetting]([UserRoleId] ASC);

