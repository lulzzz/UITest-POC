CREATE TABLE [Mobile].[DeviceToken] (
    [CreatedAt]        DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (256) NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [DeviceId]         INT            IDENTITY (1, 1) NOT NULL,
    [UserProfileId]    INT            NULL,
    [DeviceTokenValue] NVARCHAR (500) NULL,
    [DeviceVersion]    NVARCHAR (15)  NULL,
    [DeviceName]       NVARCHAR (100) NULL,
    [Model]            NVARCHAR (30)  NULL,
    [UserDeviceId]     NVARCHAR (60)  NULL,
    [TimeStamp]        INT            NOT NULL,
    [SourceIP]         NVARCHAR (20)  NULL,
    [Android]          INT            NOT NULL,
    CONSTRAINT [PK_DeviceToken] PRIMARY KEY CLUSTERED ([DeviceId] ASC),
    CONSTRAINT [FK_DeviceToken_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DeviceToken_UserProfileId]
    ON [Mobile].[DeviceToken]([UserProfileId] ASC);

