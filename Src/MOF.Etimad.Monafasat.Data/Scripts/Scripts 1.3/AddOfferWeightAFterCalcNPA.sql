ALTER TABLE [Offer].[Offer] ADD [OfferWeightAfterCalcNPA] decimal(18,2) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200212093418_Offer_OfferWeightAfterCalcNPA', N'3.1.0');

GO