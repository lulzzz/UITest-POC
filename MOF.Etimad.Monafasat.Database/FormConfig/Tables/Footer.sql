CREATE TABLE [FormConfig].[Footer] (
    [Id]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [FormId] BIGINT         NOT NULL,
    CONSTRAINT [PK_FormConfig.Footer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormConfig.Footer_FormConfig.Form_FormId] FOREIGN KEY ([FormId]) REFERENCES [FormConfig].[Form] ([Id]) ON DELETE CASCADE
);

