CREATE TABLE [FormConfig].[FooterColumn] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FooterId]   BIGINT         NOT NULL,
    [ColumnId]   BIGINT         NOT NULL,
    [DataSource] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_FormConfig.FooterColumn] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormConfig.FooterColumn_FormConfig.Column_ColumnId] FOREIGN KEY ([ColumnId]) REFERENCES [FormConfig].[Column] ([Id]),
    CONSTRAINT [FK_FormConfig.FooterColumn_FormConfig.Footer_FooterId] FOREIGN KEY ([FooterId]) REFERENCES [FormConfig].[Footer] ([Id])
);

