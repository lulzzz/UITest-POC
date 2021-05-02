CREATE TABLE [LookUps].[FormCategory] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Lookups.FormCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

