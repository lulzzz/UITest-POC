CREATE TABLE [LookUps].[NotificationOperationCode] (
    [CreatedAt]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                   NVARCHAR (256)  NULL,
    [UpdatedAt]                   DATETIME2 (7)   NULL,
    [UpdatedBy]                   NVARCHAR (256)  NULL,
    [IsActive]                    BIT             NULL,
    [NotificationOperationCodeId] INT             NOT NULL,
    [OperationCode]               NVARCHAR (100)  NULL,
    [ArabicName]                  NVARCHAR (2000) NULL,
    [EnglishName]                 NVARCHAR (2000) NULL,
    [UserRoleId]                  INT             NOT NULL,
    [DefaultSMS]                  BIT             NOT NULL,
    [DefaultEmail]                BIT             NOT NULL,
    [CanEditEmail]                BIT             NOT NULL,
    [CanEditSMS]                  BIT             NOT NULL,
    [NotificationCategoryId]      INT             NULL,
    [SmsTemplateAr]               NVARCHAR (2000) NULL,
    [SmsTemplateEn]               NVARCHAR (2000) NULL,
    [EmailSubjectTemplateAr]      NVARCHAR (2000) NULL,
    [EmailSubjectTemplateEn]      NVARCHAR (2000) NULL,
    [EmailBodyTemplateAr]         NVARCHAR (MAX)  NULL,
    [EmailBodyTemplateEn]         NVARCHAR (MAX)  NULL,
    [PanelTemplateAr]             NVARCHAR (1000) NULL,
    [PanelTemplateEn]             NVARCHAR (1000) NULL,
    CONSTRAINT [PK_NotificationOperationCode] PRIMARY KEY CLUSTERED ([NotificationOperationCodeId] ASC),
    CONSTRAINT [FK_NotificationOperationCode_NotificationCategory_NotificationCategoryId] FOREIGN KEY ([NotificationCategoryId]) REFERENCES [LookUps].[NotificationCategory] ([Id]),
    CONSTRAINT [FK_NotificationOperationCode_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [LookUps].[UserRole] ([UserRoleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_NotificationOperationCode_NotificationCategoryId]
    ON [LookUps].[NotificationOperationCode]([NotificationCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NotificationOperationCode_UserRoleId]
    ON [LookUps].[NotificationOperationCode]([UserRoleId] ASC);

