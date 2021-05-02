CREATE TABLE [FormConfig].[DisplayValidationRole] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [CanEdit]             BIT NOT NULL,
    [CanRead]             BIT NOT NULL,
    [RoleId]              INT NOT NULL,
    [DisplayValidationId] INT NOT NULL,
    CONSTRAINT [PK_FormConfig.DisplayValidationRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormConfig.DisplayValidationRole_FormConfig.DisplayValidation_DisplayValidationId] FOREIGN KEY ([DisplayValidationId]) REFERENCES [FormConfig].[DisplayValidation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormConfig.DisplayValidationRole_Lookups.Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [LookUps].[Role] ([Id]) ON DELETE CASCADE
);

