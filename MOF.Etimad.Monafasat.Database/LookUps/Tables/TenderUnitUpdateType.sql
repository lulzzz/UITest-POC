CREATE TABLE [LookUps].[TenderUnitUpdateType] (
    [TenderUnitUpdateTypeId] INT             NOT NULL,
    [Name]                   NVARCHAR (1024) NULL,
    CONSTRAINT [PK_TenderUnitUpdateType] PRIMARY KEY CLUSTERED ([TenderUnitUpdateTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of tender unit type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderUnitUpdateType', @level2type = N'COLUMN', @level2name = N'TenderUnitUpdateTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the name of tender unit type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderUnitUpdateType', @level2type = N'COLUMN', @level2name = N'Name';

