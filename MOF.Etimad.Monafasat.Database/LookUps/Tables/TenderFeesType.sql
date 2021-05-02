CREATE TABLE [LookUps].[TenderFeesType] (
    [TenderFeesTypeId] INT            NOT NULL,
    [NameEnglish]      NVARCHAR (100) NULL,
    [NameArabic]       NVARCHAR (100) NULL,
    CONSTRAINT [PK_TenderFeesType] PRIMARY KEY CLUSTERED ([TenderFeesTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of tender fees type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderFeesType', @level2type = N'COLUMN', @level2name = N'TenderFeesTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define english name of tender fees type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderFeesType', @level2type = N'COLUMN', @level2name = N'NameEnglish';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define arabic name of tender fees type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderFeesType', @level2type = N'COLUMN', @level2name = N'NameArabic';

