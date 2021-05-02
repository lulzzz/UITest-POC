CREATE TABLE [LookUps].[HeaderType] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Lookups.HeaderType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

