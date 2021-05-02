

---------- Nawaf 31/03/2021 ----------

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'OfferPriceAfterLocalContent');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [OfferPriceAfterLocalContent];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'OfferPriceAfterPreference');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [OfferPriceAfterPreference];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'PricePreferancePercentage');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [PricePreferancePercentage];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'isBindedToMandatoryList');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [isBindedToMandatoryList];

GO

EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive';

GO

EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail';

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'TenderReferenceNumber');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [TenderReferenceNumber] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'TenderReferenceNumber';

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'TenderId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'TenderId';

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'SupplierNumber');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [SupplierNumber] nvarchar(20) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'SupplierNumber';

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'InvitationId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [InvitationId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'InvitationId';

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'ConditionsBookletID');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [ConditionsBookletID] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'ConditionsBookletID';

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'BillInvoiceNumber');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [BillInvoiceNumber] nvarchar(50) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'BillInvoiceNumber';

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'AgencyCode');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [AgencyCode] nvarchar(50) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'AgencyCode';

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'Id');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'Id';

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsZakatValidDate');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsZakatValidDate] bit NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsZakatValidDate';

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsZakatAttached');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsZakatAttached] bit NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsZakatAttached';

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSpecificationValidDate');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSpecificationValidDate] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSpecificationValidDate';

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSpecificationAttached');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSpecificationAttached] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSpecificationAttached';

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSocialInsuranceAttached');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSocialInsuranceAttached] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSocialInsuranceAttached';

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSaudizationValidDate');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSaudizationValidDate] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSaudizationValidDate';

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSaudizationAttached');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSaudizationAttached] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSaudizationAttached';

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsCommercialRegisterValid');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsCommercialRegisterValid] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsCommercialRegisterValid';

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsCommercialRegisterAttached');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsCommercialRegisterAttached] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsCommercialRegisterAttached';

GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsChamberJoiningValid');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsChamberJoiningValid] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsChamberJoiningValid';

GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsChamberJoiningAttached');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsChamberJoiningAttached] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsChamberJoiningAttached';

GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'CombinedId');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [CombinedId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'CombinedId';

GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'CombinedDetailId');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [CombinedDetailId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'CombinedDetailId';

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7671134+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672487+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672520+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672522+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672524+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672525+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672526+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672527+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672528+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672529+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672530+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672531+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-23T16:21:19.7672532+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[UserRole] SET [IsCombinedRole] = CAST(1 AS bit)
WHERE [UserRoleId] = 100;
SELECT @@ROWCOUNT;


GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20210331112621_Offer_LocalContentPricesFields';

GO




------------------------------- add Offer Local Content Details In New Table ----- 2021-04-05-----------Youssef

ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [FK_Offer_OfferlocalContentDetails_OfferLocalContentId];

GO

DROP TABLE [OfferlocalContentDetails];

GO

DROP INDEX [IX_Offer_OfferLocalContentId] ON [Offer].[Offer];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'OfferFinalPrice');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [OfferFinalPrice];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'OfferLocalContentId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [OfferLocalContentId];

GO

ALTER TABLE [Offer].[Offer] ADD [OfferPriceAfterLocalContent] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [OfferPriceAfterPreference] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [PricePreferancePercentage] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [isBindedToMandatoryList] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20210405102550_add-table-offer-local-content-details';

GO


------------------------------- Update Offer Local Content Details  ----- 2021-04-06-----------Youssef


ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [FK_OfferlocalContentDetails_Offer_OfferId];

GO

DROP INDEX [IX_OfferlocalContentDetails_OfferId] ON [Offer].[OfferlocalContentDetails];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'OfferId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] DROP COLUMN [OfferId];

GO

DECLARE @defaultSchema sysname = SCHEMA_NAME();
EXEC(N'ALTER SCHEMA [' + @defaultSchema + N'] TRANSFER [Offer].[OfferlocalContentDetails];');

GO

ALTER TABLE [Offer].[Offer] ADD [OfferLocalContentId] int NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OfferlocalContentDetails]') AND [c].[name] = N'IsSmallOrMediumCorporation');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OfferlocalContentDetails] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [OfferlocalContentDetails] ALTER COLUMN [IsSmallOrMediumCorporation] bit NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OfferlocalContentDetails]') AND [c].[name] = N'IsIncludedInMoneyMarket');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [OfferlocalContentDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [OfferlocalContentDetails] ALTER COLUMN [IsIncludedInMoneyMarket] bit NOT NULL;

GO

CREATE INDEX [IX_Offer_OfferLocalContentId] ON [Offer].[Offer] ([OfferLocalContentId]);

GO

ALTER TABLE [Offer].[Offer] ADD CONSTRAINT [FK_Offer_OfferlocalContentDetails_OfferLocalContentId] FOREIGN KEY ([OfferLocalContentId]) REFERENCES [OfferlocalContentDetails] ([OfferLocalContentId]) ON DELETE NO ACTION;

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20210406101308_Update-offer-details';

GO



---------------------------------- Youssef  2021-04-14---------------


DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'BaseLineUpdatedDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] DROP COLUMN [BaseLineUpdatedDate];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'CorporationSizeUpdatedDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] DROP COLUMN [CorporationSizeUpdatedDate];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'IsIncludedInMoneyMarketUpdatedDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] DROP COLUMN [IsIncludedInMoneyMarketUpdatedDate];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'LocalContentDesiredPercentageUpdatedDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] DROP COLUMN [LocalContentDesiredPercentageUpdatedDate];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20210414144748_AddLocalContentUpdatedDate';

GO


---------------------------------- Youssef  2021-04-15--------------- Add Suppliers In Money Market Table

DROP TABLE [Offer].[MoneyMarketSuppliers];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20210415103855_add-money-market-table';

GO

