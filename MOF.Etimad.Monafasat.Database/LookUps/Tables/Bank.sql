CREATE TABLE [LookUps].[Bank] (
    [BankId] INT             NOT NULL,
    [NameEn] NVARCHAR (1024) NULL,
    [NameAr] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED ([BankId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of bank', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Bank', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define english name of bank', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Bank', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define arabic name of bank', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'Bank', @level2type = N'COLUMN', @level2name = N'NameAr';

