ALTER TABLE [Tender].[Tender] ADD [IsNotificationSentForStoppingPeriod] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200330200225_addIsNotificationSentForStoppingPeriodTotenderTable', N'3.1.0');

GO