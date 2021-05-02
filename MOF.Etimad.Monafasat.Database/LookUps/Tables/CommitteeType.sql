CREATE TABLE [LookUps].[CommitteeType] (
    [CommitteeTypeId] INT            NOT NULL,
    [NameAr]          NVARCHAR (500) NULL,
    [NameEn]          NVARCHAR (500) NULL,
    CONSTRAINT [PK_CommitteeType] PRIMARY KEY CLUSTERED ([CommitteeTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of committee type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'CommitteeType', @level2type = N'COLUMN', @level2name = N'CommitteeTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define arabic name of committee type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'CommitteeType', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define english name of committee type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'CommitteeType', @level2type = N'COLUMN', @level2name = N'NameEn';

