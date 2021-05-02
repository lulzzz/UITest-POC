CREATE TABLE [Offer].[SupplierTenderQuantityTable] (
    [CreatedAt]             DATETIME2 (7)   NOT NULL,
    [CreatedBy]             NVARCHAR (256)  NULL,
    [UpdatedAt]             DATETIME2 (7)   NULL,
    [UpdatedBy]             NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [Id]                    BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (MAX)  NOT NULL,
    [OfferId]               INT             NOT NULL,
    [TenederQuantityId]     BIGINT          NULL,
    [AdjustedTotalPrice]    NVARCHAR (MAX)  NULL,
    [AdjustedTotalVAT]      DECIMAL (18, 2) NOT NULL,
    [AdjustedTotalDiscount] NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_SupplierTenderQuantityTable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierTenderQuantityTable_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]),
    CONSTRAINT [FK_SupplierTenderQuantityTable_TenderQuantityTable_TenederQuantityId] FOREIGN KEY ([TenederQuantityId]) REFERENCES [Tender].[TenderQuantityTable] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierTenderQuantityTable_OfferId]
    ON [Offer].[SupplierTenderQuantityTable]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierTenderQuantityTable_TenederQuantityId]
    ON [Offer].[SupplierTenderQuantityTable]([TenederQuantityId] ASC);

