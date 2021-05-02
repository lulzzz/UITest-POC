ALTER TABLE [Tender].[Tender] ADD [IsSendToEmarketPlace] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200303120817_AddIsSendToEmarketPlaceToTender', N'3.1.0');

GO

