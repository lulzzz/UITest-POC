DROP TABLE [Tender].[TenderQuantitiyItemsJson];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200629114015_Create_QuantitiyItemsJson';

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200810094313_Remove_SupplierTenderQuantityTableItemJson_QuantityItemJson';

GO

DROP TABLE [Offer].[SupplierTenderQuantityTableItemJson];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200721131611_applyofferJSON';

GO

DROP TABLE [CommunicationRequest].[NegotiationQuantityItemJson];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200721114821_Negotiation-QT-Convert-Json';

GO

DROP TABLE [Tender].[TenderQuantitiyItemsChangeJson];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200701125236_Add_TenderQuantitiyItemsChangeJson';

GO

