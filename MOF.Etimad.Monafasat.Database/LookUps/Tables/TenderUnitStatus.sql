CREATE TABLE [LookUps].[TenderUnitStatus] (
    [TenderUnitStatusId] INT            NOT NULL,
    [Name]               NVARCHAR (100) NULL,
    CONSTRAINT [PK_TenderUnitStatus] PRIMARY KEY CLUSTERED ([TenderUnitStatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of tender unit status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderUnitStatus', @level2type = N'COLUMN', @level2name = N'TenderUnitStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the name of tender unit status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderUnitStatus', @level2type = N'COLUMN', @level2name = N'Name';

