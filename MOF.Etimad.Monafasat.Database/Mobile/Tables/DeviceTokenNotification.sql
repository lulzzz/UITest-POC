CREATE TABLE [Mobile].[DeviceTokenNotification] (
    [CreatedAt]                 DATETIME2 (7)  NOT NULL,
    [CreatedBy]                 NVARCHAR (256) NULL,
    [UpdatedAt]                 DATETIME2 (7)  NULL,
    [UpdatedBy]                 NVARCHAR (256) NULL,
    [IsActive]                  BIT            NULL,
    [DeviceTokenNotificationId] INT            IDENTITY (1, 1) NOT NULL,
    [DeviceId]                  INT            NOT NULL,
    [ActivityId]                INT            NOT NULL,
    [Status]                    BIT            NOT NULL,
    CONSTRAINT [PK_DeviceTokenNotification] PRIMARY KEY CLUSTERED ([DeviceTokenNotificationId] ASC),
    CONSTRAINT [FK_DeviceTokenNotification_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_DeviceTokenNotification_DeviceToken_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Mobile].[DeviceToken] ([DeviceId])
);


GO
CREATE NONCLUSTERED INDEX [IX_DeviceTokenNotification_ActivityId]
    ON [Mobile].[DeviceTokenNotification]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DeviceTokenNotification_DeviceId]
    ON [Mobile].[DeviceTokenNotification]([DeviceId] ASC);

