CREATE TABLE [LookUps].[OfferStatus] (
    [OfferStatusId] INT            NOT NULL,
    [NameAr]        NVARCHAR (100) NULL,
    [NameEn]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_OfferStatus] PRIMARY KEY CLUSTERED ([OfferStatusId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Offer status lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer Status lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferStatus', @level2type = N'COLUMN', @level2name = N'OfferStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Arabic Name Of  Offer Status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferStatus', @level2type = N'COLUMN', @level2name = N'NameAr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the English Name Of  Offer Status', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferStatus', @level2type = N'COLUMN', @level2name = N'NameEn';

