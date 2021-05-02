CREATE TABLE [FormConfig].[ValidationType] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NOT NULL,
    [Regx]               NVARCHAR (MAX) NOT NULL,
    [IsMandatory]        BIT            NOT NULL,
    [ValidationMessage]  NVARCHAR (MAX) NULL,
    [DefaulteValue]      NVARCHAR (MAX) NULL,
    [CanIgnoreMandatory] BIT            NULL,
    CONSTRAINT [PK_FormConfig.ValidationType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

