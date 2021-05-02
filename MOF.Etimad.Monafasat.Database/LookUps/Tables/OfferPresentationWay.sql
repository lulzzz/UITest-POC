CREATE TABLE [LookUps].[OfferPresentationWay] (
    [Id]   INT             NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_OfferPresentationWay] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Offer Presentation Way lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferPresentationWay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer Presentation Way lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferPresentationWay', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Name Of Offer Presentation Way lookup', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'OfferPresentationWay', @level2type = N'COLUMN', @level2name = N'Name';

