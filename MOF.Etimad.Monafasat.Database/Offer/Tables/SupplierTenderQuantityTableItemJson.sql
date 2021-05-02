CREATE TABLE [Offer].[SupplierTenderQuantityTableItemJson] (
    [Id]                               BIGINT         IDENTITY (1, 1) NOT NULL,
    [SupplierTenderQuantityTableItems] NVARCHAR (MAX) NULL,
    [SupplierTenderQuantityTableId]    BIGINT         NOT NULL,
    CONSTRAINT [PK_SupplierTenderQuantityTableItemJson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTable_SupplierTenderQuantityTableId] FOREIGN KEY ([SupplierTenderQuantityTableId]) REFERENCES [Offer].[SupplierTenderQuantityTable] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTableId]
    ON [Offer].[SupplierTenderQuantityTableItemJson]([SupplierTenderQuantityTableId] ASC);

