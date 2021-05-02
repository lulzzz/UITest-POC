CREATE TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] (
    [CreatedAt]                           DATETIME2 (7)  NOT NULL,
    [CreatedBy]                           NVARCHAR (256) NULL,
    [UpdatedAt]                           DATETIME2 (7)  NULL,
    [UpdatedBy]                           NVARCHAR (256) NULL,
    [IsActive]                            BIT            NULL,
    [Id]                                  INT            IDENTITY (1, 1) NOT NULL,
    [OfferId]                             INT            NOT NULL,
    [ExtendOffersValidityId]              INT            NOT NULL,
    [SupplierCR]                          NVARCHAR (20)  NULL,
    [SupplierExtendOfferValidityStatusId] INT            NULL,
    [PeriodStartDateTime]                 DATETIME2 (7)  NULL,
    [IsReported]                          BIT            NOT NULL,
    CONSTRAINT [PK_ExtendOffersValiditySupplier] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExtendOffersValiditySupplier_ExtendOffersValidity_ExtendOffersValidityId] FOREIGN KEY ([ExtendOffersValidityId]) REFERENCES [CommunicationRequest].[ExtendOffersValidity] ([ExtendOffersValidityId]),
    CONSTRAINT [FK_ExtendOffersValiditySupplier_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]),
    CONSTRAINT [FK_ExtendOffersValiditySupplier_Supplier_SupplierCR] FOREIGN KEY ([SupplierCR]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_ExtendOffersValiditySupplier_SupplierExtendOffersValidityStatus_SupplierExtendOfferValidityStatusId] FOREIGN KEY ([SupplierExtendOfferValidityStatusId]) REFERENCES [LookUps].[SupplierExtendOffersValidityStatus] ([SupplierExtendOffersValidityStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExtendOffersValiditySupplier_ExtendOffersValidityId]
    ON [CommunicationRequest].[ExtendOffersValiditySupplier]([ExtendOffersValidityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExtendOffersValiditySupplier_OfferId]
    ON [CommunicationRequest].[ExtendOffersValiditySupplier]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExtendOffersValiditySupplier_SupplierCR]
    ON [CommunicationRequest].[ExtendOffersValiditySupplier]([SupplierCR] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExtendOffersValiditySupplier_SupplierExtendOfferValidityStatusId]
    ON [CommunicationRequest].[ExtendOffersValiditySupplier]([SupplierExtendOfferValidityStatusId] ASC);

