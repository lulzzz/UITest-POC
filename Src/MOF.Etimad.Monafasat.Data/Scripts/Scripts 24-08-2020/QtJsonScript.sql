
CREATE TABLE [Tender].[TenderQuantitiyItemsJson] (
    [Id] bigint NOT NULL IDENTITY,
    [TenderQuantityTableId] bigint NOT NULL,
    [TenderQuantityTableItems] nvarchar(max) NULL,
    CONSTRAINT [PK_TenderQuantitiyItemsJson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenderQuantitiyItemsJson_TenderQuantityTable_TenderQuantityTableId] FOREIGN KEY ([TenderQuantityTableId]) REFERENCES [Tender].[TenderQuantityTable] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_TenderQuantitiyItemsJson_TenderQuantityTableId] ON [Tender].[TenderQuantitiyItemsJson] ([TenderQuantityTableId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200629114015_Create_QuantitiyItemsJson', N'3.1.0');



Go

CREATE TABLE [Tender].[TenderQuantitiyItemsChangeJson] (
    [Id] bigint NOT NULL IDENTITY,
    [TenderQuantityTableChangeId] bigint NOT NULL,
    [TenderQuantityTableItemChanges] nvarchar(max) NULL,
    CONSTRAINT [PK_TenderQuantitiyItemsChangeJson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenderQuantitiyItemsChangeJson_TenderQuantityTableChanges_TenderQuantityTableChangeId] FOREIGN KEY ([TenderQuantityTableChangeId]) REFERENCES [Tender].[TenderQuantityTableChanges] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_TenderQuantitiyItemsChangeJson_TenderQuantityTableChangeId] ON [Tender].[TenderQuantitiyItemsChangeJson] ([TenderQuantityTableChangeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200701125236_Add_TenderQuantitiyItemsChangeJson', N'3.1.0');

GO

CREATE TABLE [CommunicationRequest].[NegotiationQuantityItemJson] (
    [Id] bigint NOT NULL IDENTITY,
    [NegotiationSupplierQuantityTableId] bigint NOT NULL,
    [NegotiationSupplierQuantityTableItems] nvarchar(max) NULL,
    CONSTRAINT [PK_NegotiationQuantityItemJson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NegotiationQuantityItemJson_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId] FOREIGN KEY ([NegotiationSupplierQuantityTableId]) REFERENCES [CommunicationRequest].[NegotiationSupplierQuantityTable] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_NegotiationQuantityItemJson_NegotiationSupplierQuantityTableId] ON [CommunicationRequest].[NegotiationQuantityItemJson] ([NegotiationSupplierQuantityTableId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200721114821_Negotiation-QT-Convert-Json', N'3.1.0');

GO

CREATE TABLE [Offer].[SupplierTenderQuantityTableItemJson] (
    [Id] bigint NOT NULL IDENTITY,
    [QuantitiyItemsJson] nvarchar(max) NULL,
    [SupplierTenderQuantityTableItems] nvarchar(max) NULL,
    [SupplierTenderQuantityTableId] bigint NOT NULL,
    CONSTRAINT [PK_SupplierTenderQuantityTableItemJson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTable_SupplierTenderQuantityTableId] FOREIGN KEY ([SupplierTenderQuantityTableId]) REFERENCES [Offer].[SupplierTenderQuantityTable] ([Id]) ON DELETE NO ACTION
);

GO


CREATE UNIQUE INDEX [IX_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTableId] ON [Offer].[SupplierTenderQuantityTableItemJson] ([SupplierTenderQuantityTableId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200721131611_applyofferJSON', N'3.1.0');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierTenderQuantityTableItemJson]') AND [c].[name] = N'QuantitiyItemsJson');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] DROP COLUMN [QuantitiyItemsJson];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200810094313_Remove_SupplierTenderQuantityTableItemJson_QuantityItemJson', N'3.1.0');

GO

