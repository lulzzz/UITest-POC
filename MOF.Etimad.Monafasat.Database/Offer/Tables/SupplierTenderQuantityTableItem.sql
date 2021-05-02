CREATE TABLE [Offer].[SupplierTenderQuantityTableItem] (
    [CreatedAt]                     DATETIME2 (7)  NOT NULL,
    [CreatedBy]                     NVARCHAR (256) NULL,
    [UpdatedAt]                     DATETIME2 (7)  NULL,
    [UpdatedBy]                     NVARCHAR (256) NULL,
    [IsActive]                      BIT            NULL,
    [Id]                            BIGINT         IDENTITY (1, 1) NOT NULL,
    [ColumnId]                      BIGINT         NOT NULL,
    [TenderFormHeaderId]            BIGINT         NULL,
    [ActivityTemplateId]            INT            NULL,
    [TenderQuantityTableItemId]     BIGINT         NULL,
    [SupplierTenderQuantityTableId] BIGINT         NOT NULL,
    [ItemNumber]                    BIGINT         NOT NULL,
    [Value]                         NVARCHAR (MAX) NULL,
    [AlternativeValue]              NVARCHAR (MAX) NULL,
    [IsDefault]                     BIT            NOT NULL,
    CONSTRAINT [PK_SupplierTenderQuantityTableItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierTenderQuantityTableItem_SupplierTenderQuantityTable_SupplierTenderQuantityTableId] FOREIGN KEY ([SupplierTenderQuantityTableId]) REFERENCES [Offer].[SupplierTenderQuantityTable] ([Id]),
    CONSTRAINT [FK_SupplierTenderQuantityTableItem_TenderQuantityTableItem_TenderQuantityTableItemId] FOREIGN KEY ([TenderQuantityTableItemId]) REFERENCES [Tender].[TenderQuantityTableItem] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierTenderQuantityTableItem_SupplierTenderQuantityTableId]
    ON [Offer].[SupplierTenderQuantityTableItem]([SupplierTenderQuantityTableId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierTenderQuantityTableItem_TenderQuantityTableItemId]
    ON [Offer].[SupplierTenderQuantityTableItem]([TenderQuantityTableItemId] ASC);

