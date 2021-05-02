CREATE TABLE [LookUps].[NotificationCategory] (
    [Id]          INT             NOT NULL,
    [ArabicName]  NVARCHAR (1024) NULL,
    [EnglishName] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_NotificationCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

