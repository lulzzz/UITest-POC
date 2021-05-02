CREATE TABLE [LookUps].[OfferSolidarityStatus] (
    [CombinedStatusId] INT             NOT NULL,
    [Name]             NVARCHAR (1024) NULL,
    CONSTRAINT [PK_OfferSolidarityStatus] PRIMARY KEY CLUSTERED ([CombinedStatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Offer Solidarity  status lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferSolidarityStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer Solidarity  Status lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferSolidarityStatus', @level2type = N'COLUMN', @level2name = N'CombinedStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the  Name Of  Offer Solidarity Status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferSolidarityStatus', @level2type = N'COLUMN', @level2name = N'Name';

