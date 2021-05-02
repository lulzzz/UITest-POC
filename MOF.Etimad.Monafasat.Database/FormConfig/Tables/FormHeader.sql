CREATE TABLE [FormConfig].[FormHeader] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [HeaderTypeId] INT            NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_FormConfig.FormHeader] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormConfig.FormHeader_Lookups.HeaderType_HeaderTypeId] FOREIGN KEY ([HeaderTypeId]) REFERENCES [LookUps].[HeaderType] ([Id]) ON DELETE CASCADE
);

