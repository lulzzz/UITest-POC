CREATE TABLE [Mobile].[MobileAlert] (
    [CreatedAt]       DATETIME2 (7)  NOT NULL,
    [CreatedBy]       NVARCHAR (256) NULL,
    [UpdatedAt]       DATETIME2 (7)  NULL,
    [UpdatedBy]       NVARCHAR (256) NULL,
    [IsActive]        BIT            NULL,
    [MobileAlertId]   INT            IDENTITY (1, 1) NOT NULL,
    [Message]         NVARCHAR (MAX) NULL,
    [DeviceId]        INT            NOT NULL,
    [MessageId]       INT            NULL,
    [MessageStatusId] INT            NOT NULL,
    [GroupCode]       NVARCHAR (MAX) NULL,
    [SendDate]        DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_MobileAlert] PRIMARY KEY CLUSTERED ([MobileAlertId] ASC),
    CONSTRAINT [FK_MobileAlert_DeviceToken_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Mobile].[DeviceToken] ([DeviceId]),
    CONSTRAINT [FK_MobileAlert_InvitationStatus_MessageStatusId] FOREIGN KEY ([MessageStatusId]) REFERENCES [LookUps].[InvitationStatus] ([InvitationStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_MobileAlert_DeviceId]
    ON [Mobile].[MobileAlert]([DeviceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MobileAlert_MessageStatusId]
    ON [Mobile].[MobileAlert]([MessageStatusId] ASC);

