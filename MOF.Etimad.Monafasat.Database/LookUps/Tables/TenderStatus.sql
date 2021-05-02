CREATE TABLE [LookUps].[TenderStatus] (
    [TenderStatusId] INT            NOT NULL,
    [NameAr]         NVARCHAR (100) NULL,
    [NameEn]         NVARCHAR (100) NULL,
    CONSTRAINT [PK_TenderStatus] PRIMARY KEY CLUSTERED ([TenderStatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of tender status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderStatus', @level2type = N'COLUMN', @level2name = N'TenderStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the arabic name of tender status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderStatus', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the english name of tender status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderStatus', @level2type = N'COLUMN', @level2name = N'NameEn';

