CREATE TABLE [FormConfig].[Form] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [HeaderTypeId]   INT            NOT NULL,
    [TemplateId]     INT            NOT NULL,
    [SubmitUrl]      NVARCHAR (MAX) NULL,
    [SubmitName]     NVARCHAR (MAX) NULL,
    [CSVTemplet]     NVARCHAR (MAX) NULL,
    [IsCostForm]     BIT            NOT NULL,
    [FormCategoryId] INT            NULL,
    CONSTRAINT [PK_FormConfig.Form] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormConfig.Form_FormConfig.Template_TemplateId] FOREIGN KEY ([TemplateId]) REFERENCES [FormConfig].[Template] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormConfig.Form_Lookups.FormCategory_FormCategoryId] FOREIGN KEY ([FormCategoryId]) REFERENCES [LookUps].[FormCategory] ([Id]),
    CONSTRAINT [FK_FormConfig.Form_Lookups.HeaderType_HeaderTypeId] FOREIGN KEY ([HeaderTypeId]) REFERENCES [LookUps].[HeaderType] ([Id]) ON DELETE CASCADE
);

