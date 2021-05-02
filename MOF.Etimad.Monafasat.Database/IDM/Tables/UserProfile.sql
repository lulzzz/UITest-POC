CREATE TABLE [IDM].[UserProfile] (
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [Id]              INT             NOT NULL,
    [Mobile]          NVARCHAR (20)   NULL,
    [Email]           NVARCHAR (100)  NULL,
    [UserName]        NVARCHAR (20)   NULL,
    [FullName]        NVARCHAR (200)  NULL,
    [DefaultUserRole] NVARCHAR (200)  NULL,
    [RowVersion]      VARBINARY (MAX) NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

