CREATE TABLE [FormConfig].[Column] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NOT NULL,
    [IsHeaderRepeated]    BIT            NOT NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [DisplayOrder]        INT            NOT NULL,
    [FieldTypeId]         INT            NOT NULL,
    [ValidationTypeId]    INT            NOT NULL,
    [DataSource]          NVARCHAR (MAX) NULL,
    [FormId]              BIGINT         NOT NULL,
    [DisplayValidationId] INT            NOT NULL,
    [ColumnTypeId]        INT            NULL,
    CONSTRAINT [PK_FormConfig.Column] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Column_ColumnType] FOREIGN KEY ([ColumnTypeId]) REFERENCES [LookUps].[ColumnType] ([Id]),
    CONSTRAINT [FK_FormConfig.Column_FormConfig.DisplayValidation_DisplayValidationId] FOREIGN KEY ([DisplayValidationId]) REFERENCES [FormConfig].[DisplayValidation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormConfig.Column_FormConfig.Form_FormId] FOREIGN KEY ([FormId]) REFERENCES [FormConfig].[Form] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormConfig.Column_FormConfig.ValidationType_ValidationTypeId] FOREIGN KEY ([ValidationTypeId]) REFERENCES [FormConfig].[ValidationType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormConfig.Column_Lookups.ColumnType_ColumnTypeId] FOREIGN KEY ([ColumnTypeId]) REFERENCES [LookUps].[ColumnType] ([Id]),
    CONSTRAINT [FK_FormConfig.Column_Lookups.FieldType_FieldTypeId] FOREIGN KEY ([FieldTypeId]) REFERENCES [LookUps].[FieldType] ([Id]) ON DELETE CASCADE
);

