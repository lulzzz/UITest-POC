ALTER TABLE [Tender].[Tender] ADD [NationalProductPercentage] decimal(18,2) NULL;

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7396807+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397924+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397951+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397953+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397954+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397956+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397958+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397959+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397961+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397962+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397963+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397964+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-29T10:52:57.7397966+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200129075303_AddNationalProductPercentageToTender', N'3.1.0');

GO

