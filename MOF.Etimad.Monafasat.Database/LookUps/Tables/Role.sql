CREATE TABLE [LookUps].[Role] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Lookups.Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

