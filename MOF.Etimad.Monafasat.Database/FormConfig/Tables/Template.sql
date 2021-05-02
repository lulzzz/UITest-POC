CREATE TABLE [FormConfig].[Template] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FormConfig.Template] PRIMARY KEY CLUSTERED ([Id] ASC)
);

