ALTER TABLE [Tender].[Tender] ADD [QuantityTableVersionId] int NULL;

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7705656+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707166+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707207+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707209+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707211+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707214+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707216+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707217+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707219+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707221+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707222+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707224+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-28T12:07:35.7707226+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200128090740_AddQuantityTableVersionIdInTender', N'3.1.0');

GO


update Tender.Tender set QuantityTableVersionId = 1