


---------------------------------- Nawaf 31/03/2021 --------------------------------

EXEC sp_addextendedproperty 'MS_Description', N'Descripe the payment history for Conditional Booklet and Invitation', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Descripe the Details for suppliers [Offer owner and Combined]', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail';

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'TenderReferenceNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [TenderReferenceNumber] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Related Tender Reference Number', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'TenderReferenceNumber';

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'TenderId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'TenderId';

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'SupplierNumber');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [SupplierNumber] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Supplier Commercial Number', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'SupplierNumber';

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'InvitationId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [InvitationId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'This refere the invitation ', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'InvitationId';

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'ConditionsBookletID');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [ConditionsBookletID] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the reference to Conditions Booklet', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'ConditionsBookletID';

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'BillInvoiceNumber');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [BillInvoiceNumber] nvarchar(50) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the number of Bill', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'BillInvoiceNumber';

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'AgencyCode');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [AgencyCode] nvarchar(50) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Code for the agency that own the Tender', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'AgencyCode';

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillArchive]') AND [c].[name] = N'Id');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillArchive] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Sadad].[BillArchive] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  the Identity  for the table ', 'SCHEMA', N'Sadad', 'TABLE', N'BillArchive', 'COLUMN', N'Id';

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsZakatValidDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsZakatValidDate] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define   if the Zakat Certificate  is Valid ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsZakatValidDate';

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsZakatAttached');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsZakatAttached] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Zakat Certificate Copy  is Attached with offer papers ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsZakatAttached';

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSpecificationValidDate');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSpecificationValidDate] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define   if the Specification Certificate  is Valid ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSpecificationValidDate';

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSpecificationAttached');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSpecificationAttached] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Specification Copy is Attached with offer papers', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSpecificationAttached';

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSocialInsuranceAttached');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSocialInsuranceAttached] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Specification Insurance Copy  is Attached with offer papers ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSocialInsuranceAttached';

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSaudizationValidDate');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSaudizationValidDate] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define   if the Saudization Copy   Is Still valid', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSaudizationValidDate';

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsSaudizationAttached');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsSaudizationAttached] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Saudization Copy is Attached with offer papers', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsSaudizationAttached';

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsCommercialRegisterValid');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsCommercialRegisterValid] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define   if the Commercial Registeration    is Valid  ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsCommercialRegisterValid';

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsCommercialRegisterAttached');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsCommercialRegisterAttached] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Commercial Registeration number Copy is Attached with offer papers', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsCommercialRegisterAttached';

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsChamberJoiningValid');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsChamberJoiningValid] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define   if the Chamber Join  Is Still valid', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsChamberJoiningValid';

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'IsChamberJoiningAttached');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [IsChamberJoiningAttached] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  if the Chamber Join Request is Attached with offer papers', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'IsChamberJoiningAttached';

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'CombinedId');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [CombinedId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  the supplier who applied for the tender', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'CombinedId';

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierCombinedDetail]') AND [c].[name] = N'CombinedDetailId');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierCombinedDetail] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Offer].[SupplierCombinedDetail] ALTER COLUMN [CombinedDetailId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  the Identity  for the table ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierCombinedDetail', 'COLUMN', N'CombinedDetailId';

GO

ALTER TABLE [Offer].[Offer] ADD [OfferPriceAfterLocalContent] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [OfferPriceAfterPreference] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [PricePreferancePercentage] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [isBindedToMandatoryList] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1872839+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873581+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873602+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873603+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873605+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873606+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873607+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873608+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873610+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873611+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873612+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873613+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2021-03-31T14:26:18.1873615+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[UserRole] SET [IsCombinedRole] = CAST(1 AS bit)
WHERE [UserRoleId] = 100;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210331112621_Offer_LocalContentPricesFields', N'3.1.0');

GO

--------------------------------Reda 31-03-2021 ---------------------------

CREATE TABLE [Settings].[ConfigurationSetting] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NULL,
    [Description] nvarchar(300) NULL,
    CONSTRAINT [PK_ConfigurationSetting] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210331133654_AddConfigurationSettingTable', N'3.1.0');

GO
-- Date shoud be changed
insert into [Settings].[ConfigurationSetting] values('2021-04-04','Local content change')
GO

----------------------Reda 04-04-2021 --------------------------------
DROP TABLE [Settings].[NationalProductSettings];

GO

CREATE TABLE [Settings].[LocalContentSetting] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [NationalProductPercentage] decimal(18,2) NOT NULL,
    [HighValueContractsAmmount] decimal(18,2) NOT NULL,
    [LocalContentMaximumPercentage] decimal(18,2) NOT NULL,
    [PriceWeightAfterAdjustment] decimal(18,2) NOT NULL,
    [LocalContentWeightAndFinancialMarket] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_LocalContentSetting] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Tender].[TenderLocalContentSetting] (
    [Id] int NOT NULL IDENTITY,
    [TenderId] int NOT NULL,
    [NationalProductPercentage] decimal(18,2) NULL,
    [HighValueContractsAmmount] decimal(18,2) NULL,
    [LocalContentMaximumPercentage] decimal(18,2) NULL,
    [PriceWeightAfterAdjustment] decimal(18,2) NULL,
    [LocalContentWeightAndFinancialMarket] decimal(18,2) NULL,
    CONSTRAINT [PK_TenderLocalContentSetting] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenderLocalContentSetting_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_TenderLocalContentSetting_TenderId] ON [Tender].[TenderLocalContentSetting] ([TenderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210404114110_AddLocalContentSettingAndTenderSettingsTables', N'3.1.0');

GO

INSERT INTO Settings.LocalContentSetting VALUES('2021-04-04 00:00:00.0000000', null, null, null, null, 10, 50000000,10,60,40);

GO

DROP TABLE [Tender].[TenderLocalContentSetting];

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [BaselineWeight] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [FinancialMarketListedWeight] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [HighValueContractsAmmount] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [LocalContentMaximumPercentage] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [LocalContentTargetWeight] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [LocalContentWeightAndFinancialMarket] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [NationalProductPercentage] decimal(18,2) NULL;

GO

ALTER TABLE [Tender].[TenderLocalContent] ADD [PriceWeightAfterAdjustment] decimal(18,2) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210404144623_RemoveTenderLocalContentSettingsTable', N'3.1.0');

GO



------------------------------- add Offer Local Content Details In New Table ----- 2021-04-05-----------Youssef

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

ALTER TABLE [Offer].[Offer] ADD [OfferFinalPrice] decimal(18,2) NULL;

GO

ALTER TABLE [Offer].[Offer] ADD [OfferLocalContentId] int NULL;

GO

CREATE TABLE [OfferlocalContentDetails] (
    [OfferLocalContentId] int NOT NULL IDENTITY,
    [LocalContentPercentage] decimal(18,2) NULL,
    [LocalContentDesiredPercentage] decimal(18,2) NULL,
    [IsSmallOrMediumCorporation] bit NOT NULL,
    [IsIncludedInMoneyMarket] bit NOT NULL,
    [OfferPriceAfterLocalContent] decimal(18,2) NULL,
    [PricePreferancePercentage] decimal(18,2) NULL,
    [OfferPriceAfterSmallAndMediumCorporations] decimal(18,2) NULL,
    [IsBindedToMandatoryList] bit NOT NULL,
    [IsBindedToTheLowestBaseLine] bit NOT NULL,
    [IsBindedToTheLowestLocalContent] bit NOT NULL,
    CONSTRAINT [PK_OfferlocalContentDetails] PRIMARY KEY ([OfferLocalContentId])
);

GO

CREATE INDEX [IX_Offer_OfferLocalContentId] ON [Offer].[Offer] ([OfferLocalContentId]);

GO

ALTER TABLE [Offer].[Offer] ADD CONSTRAINT [FK_Offer_OfferlocalContentDetails_OfferLocalContentId] FOREIGN KEY ([OfferLocalContentId]) REFERENCES [OfferlocalContentDetails] ([OfferLocalContentId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210405102550_add-table-offer-local-content-details', N'3.1.0');

GO




------------------------------- Update Offer Local Content Details  ----- 2021-04-06-----------Youssef



Go
ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [FK_Offer_OfferlocalContentDetails_OfferLocalContentId];

GO

DROP INDEX [IX_Offer_OfferLocalContentId] ON [Offer].[Offer];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'OfferLocalContentId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [OfferLocalContentId];

GO

ALTER SCHEMA [Offer] TRANSFER [OfferlocalContentDetails];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'IsSmallOrMediumCorporation');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] ALTER COLUMN [IsSmallOrMediumCorporation] bit NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[OfferlocalContentDetails]') AND [c].[name] = N'IsIncludedInMoneyMarket');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[OfferlocalContentDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Offer].[OfferlocalContentDetails] ALTER COLUMN [IsIncludedInMoneyMarket] bit NULL;

GO

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD [OfferId] int NOT NULL DEFAULT 0;

GO

CREATE UNIQUE INDEX [IX_OfferlocalContentDetails_OfferId] ON [Offer].[OfferlocalContentDetails] ([OfferId]);

GO

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD CONSTRAINT [FK_OfferlocalContentDetails_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210406101308_Update-offer-details', N'3.1.0');

GO 
---------------------------------- Youssef  2021-04-14---------------

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD [BaseLineUpdatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD [CorporationSizeUpdatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD [IsIncludedInMoneyMarketUpdatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Offer].[OfferlocalContentDetails] ADD [LocalContentDesiredPercentageUpdatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210414144748_AddLocalContentUpdatedDate', N'3.1.0');

GO



---------------------------------- Youssef  2021-04-15--------------- Add Suppliers In Money Market Table



CREATE TABLE [Offer].[MoneyMarketSuppliers] (
    [Id] int NOT NULL IDENTITY,
    [SupplierCr] nvarchar(1024) NULL,
    [SupplierName] nvarchar(1024) NULL,
    CONSTRAINT [PK_MoneyMarketSuppliers] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210415103855_add-money-market-table', N'3.1.0');

GO

