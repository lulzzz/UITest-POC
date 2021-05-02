CREATE TABLE [Offer].[OfferSolidarity] (
    [CreatedAt]        DATETIME2 (7)   NOT NULL,
    [CreatedBy]        NVARCHAR (256)  NULL,
    [UpdatedAt]        DATETIME2 (7)   NULL,
    [UpdatedBy]        NVARCHAR (256)  NULL,
    [IsActive]         BIT             NULL,
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [OfferId]          INT             NOT NULL,
    [RejectionReason]  NVARCHAR (1024) NULL,
    [Email]            NVARCHAR (MAX)  NULL,
    [Mobile]           NVARCHAR (MAX)  NULL,
    [StatusId]         INT             NOT NULL,
    [SubmissionDate]   DATETIME2 (7)   NULL,
    [SolidarityTypeId] INT             NOT NULL,
    [CRNumber]         NVARCHAR (20)   NULL,
    CONSTRAINT [PK_OfferSolidarity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OfferSolidarity_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]),
    CONSTRAINT [FK_OfferSolidarity_OfferSolidarityStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[OfferSolidarityStatus] ([CombinedStatusId]),
    CONSTRAINT [FK_OfferSolidarity_Supplier_CRNumber] FOREIGN KEY ([CRNumber]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_OfferSolidarity_CRNumber]
    ON [Offer].[OfferSolidarity]([CRNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OfferSolidarity_OfferId]
    ON [Offer].[OfferSolidarity]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OfferSolidarity_StatusId]
    ON [Offer].[OfferSolidarity]([StatusId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe the Data related to offer solidarity ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer Solidarity Table', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related Offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'OfferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the reason if the supplier rejected the request', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The email for the supplierwho will recieve the request to be partner of the offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Mobile number for the supplierwho will recieve the request to be partner of the offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'Mobile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the status of the request (Not sent , Rejected  or accepted)', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date of Accepting the solidarity Request', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'SubmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The type of Request between Registered Supplier or Forign ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'SolidarityTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the supplier Commercial Number if he was Registered on Monafasat', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferSolidarity', @level2type = N'COLUMN', @level2name = N'CRNumber';

