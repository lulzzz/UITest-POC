CREATE TABLE [LookUps].[TenderType] (
    [CreatedAt]      DATETIME2 (7)   NOT NULL,
    [CreatedBy]      NVARCHAR (256)  NULL,
    [UpdatedAt]      DATETIME2 (7)   NULL,
    [UpdatedBy]      NVARCHAR (256)  NULL,
    [IsActive]       BIT             NULL,
    [TenderTypeId]   INT             NOT NULL,
    [NameAr]         NVARCHAR (500)  NULL,
    [NameEn]         NVARCHAR (500)  NULL,
    [BuyingCost]     DECIMAL (18, 2) NOT NULL,
    [InvitationCost] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_TenderType] PRIMARY KEY CLUSTERED ([TenderTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of tender type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderType', @level2type = N'COLUMN', @level2name = N'TenderTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the arabic name of tender type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderType', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the english name of tender type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderType', @level2type = N'COLUMN', @level2name = N'NameEn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the cost of buying  of tender type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderType', @level2type = N'COLUMN', @level2name = N'BuyingCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Invitation Cost of tender type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'TenderType', @level2type = N'COLUMN', @level2name = N'InvitationCost';

