ALTER TABLE [CommunicationRequest].[NegotiationQuantityTable] DROP CONSTRAINT [FK_NegotiationQuantityTable_SupplierQuantityTable_SupplierQuantityTableId];

GO

ALTER TABLE [CommunicationRequest].[NegotiationQuantityTableItem] DROP CONSTRAINT [FK_NegotiationQuantityTableItem_QuantityTableItem_SupplierQuantityTableItemId];

GO

DROP TABLE [Offer].[SupplierQuantityTableItem];

GO

DROP TABLE [Supplier].[SupplierQuantityTableItemsTemp];

GO

DROP TABLE [Tender].[QuantitiesTableItemsChanges];

GO

DROP TABLE [Tender].[QuantityTableItem];

GO

DROP TABLE [Supplier].[SupplierQuantityTableTemp];

GO

DROP TABLE [Tender].[QuantitiesTableChanges];

GO

DROP TABLE [Offer].[SupplierQuantityTable];

GO

DROP TABLE [Tender].[QuantitiesTable];

GO

DROP INDEX [IX_NegotiationQuantityTableItem_SupplierQuantityTableItemId] ON [CommunicationRequest].[NegotiationQuantityTableItem];

GO

DROP INDEX [IX_NegotiationQuantityTable_SupplierQuantityTableId] ON [CommunicationRequest].[NegotiationQuantityTable];

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8758376+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759273+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759281+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759282+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759283+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759284+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759286+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759287+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759288+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759289+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759290+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759292+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-30T10:16:46.8759293+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200130071650_Remove-OldQuantityTables', N'3.1.0');

GO

