IF SCHEMA_ID(N'MandatoryList') IS NULL EXEC(N'CREATE SCHEMA [MandatoryList];');

GO

CREATE TABLE [LookUps].[MandatoryListStatus] (
    [Id] int NOT NULL,
    [NameAr] nvarchar(100) NULL,
    [NameEn] nvarchar(100) NULL,
    CONSTRAINT [PK_MandatoryListStatus] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [MandatoryList].[MandatoryList] (
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [Id] int NOT NULL IDENTITY,
    [DivisionNameAr] nvarchar(400) NULL,
    [DivisionNameEn] nvarchar(400) NULL,
    [DivisionCode] nvarchar(400) NULL,
    [StatusId] int NOT NULL,
    CONSTRAINT [PK_MandatoryList] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MandatoryList_MandatoryListStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[MandatoryListStatus] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [MandatoryList].[MandatoryListProduct] (
    [Id] int NOT NULL IDENTITY,
    [CSICode] nvarchar(400) NULL,
    [NameAr] nvarchar(400) NULL,
    [NameEn] nvarchar(400) NULL,
    [DescriptionAr] nvarchar(max) NULL,
    [DescriptionEn] nvarchar(max) NULL,
    [PriceCelling] float NOT NULL,
    [MandatoryListId] int NULL,
    CONSTRAINT [PK_MandatoryListProduct] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MandatoryListProduct_MandatoryList_MandatoryListId] FOREIGN KEY ([MandatoryListId]) REFERENCES [MandatoryList].[MandatoryList] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[LookUps].[MandatoryListStatus]'))
    SET IDENTITY_INSERT [LookUps].[MandatoryListStatus] ON;
INSERT INTO [LookUps].[MandatoryListStatus] ([Id], [NameAr], [NameEn])
VALUES (1, N'تحت الإنشاء', NULL),
(2, N'بانتظار الاعتماد', NULL),
(3, N'تم الرفض', NULL),
(4, N'معتمدة', NULL),
(5, N'بانتظار اعتماد الإلغاء', NULL),
(6, N'تم رفض الالغاء', NULL),
(7, N'تم الالغاء', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[LookUps].[MandatoryListStatus]'))
    SET IDENTITY_INSERT [LookUps].[MandatoryListStatus] OFF;

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00', [NameAr] = N'منافسة عامة (النظام القديم)'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00', [NameAr] = N'شراء مباشر (النظام القديم)'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-16T15:06:00.5220000+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserRoleId', N'DisplayNameAr', N'DisplayNameEn', N'Name') AND [object_id] = OBJECT_ID(N'[LookUps].[UserRole]'))
    SET IDENTITY_INSERT [LookUps].[UserRole] ON;
INSERT INTO [LookUps].[UserRole] ([UserRoleId], [DisplayNameAr], [DisplayNameEn], [Name])
VALUES (41, N'مسؤول منتجات القائمة الإلزامية', N'Mandatory List Officer', N'LC_ProductsOfficer'),
(42, N'معتمد منتجات القائمة الإلزامية', N'Mandatory List Approver', N'LC_ProductsApprover');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserRoleId', N'DisplayNameAr', N'DisplayNameEn', N'Name') AND [object_id] = OBJECT_ID(N'[LookUps].[UserRole]'))
    SET IDENTITY_INSERT [LookUps].[UserRole] OFF;

GO

CREATE INDEX [IX_MandatoryList_StatusId] ON [MandatoryList].[MandatoryList] ([StatusId]);

GO

CREATE INDEX [IX_MandatoryListProduct_MandatoryListId] ON [MandatoryList].[MandatoryListProduct] ([MandatoryListId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191216120602_MandatoryListProducts', N'3.1.0');

GO

EXEC sp_rename N'[LookUps].[MandatoryListStatus].[Id]', N'StatusId', N'COLUMN';

GO

ALTER TABLE [MandatoryList].[MandatoryListProduct] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [MandatoryList].[MandatoryListProduct] ADD [CreatedBy] nvarchar(256) NULL;

GO

ALTER TABLE [MandatoryList].[MandatoryListProduct] ADD [IsActive] bit NULL;

GO

ALTER TABLE [MandatoryList].[MandatoryListProduct] ADD [UpdatedAt] datetime2 NULL;

GO

ALTER TABLE [MandatoryList].[MandatoryListProduct] ADD [UpdatedBy] nvarchar(256) NULL;

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6770000+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2019-12-18T12:27:53.6780000+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191218092755_update_mandatorylist', N'3.1.0');

GO

ALTER TABLE [Supplier].[FavouriteSuppliers] DROP CONSTRAINT [FK_FavouriteSuppliers_Supplier_SupplierCRNumber];

GO

ALTER TABLE [Tender].[FavouriteSupplierTenders] DROP CONSTRAINT [AK_FavouriteSupplierTenders_FavouriteSupplierTenderId];

GO

ALTER TABLE [Supplier].[FavouriteSuppliers] DROP CONSTRAINT [AK_FavouriteSuppliers_FavouriteSupplierId];

GO

ALTER TABLE [Supplier].[FavouriteSuppliers] DROP CONSTRAINT [PK_FavouriteSuppliers];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Supplier].[FavouriteSuppliers]') AND [c].[name] = N'SupplierCRNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Supplier].[FavouriteSuppliers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Supplier].[FavouriteSuppliers] ALTER COLUMN [SupplierCRNumber] nvarchar(20) NULL;

GO

ALTER TABLE [Supplier].[FavouriteSuppliers] ADD CONSTRAINT [PK_FavouriteSuppliers] PRIMARY KEY ([FavouriteSupplierId]);

GO

CREATE INDEX [IX_FavouriteSuppliers_FavouriteSupplierListId] ON [Supplier].[FavouriteSuppliers] ([FavouriteSupplierListId]);

GO

ALTER TABLE [Supplier].[FavouriteSuppliers] ADD CONSTRAINT [FK_FavouriteSuppliers_Supplier_SupplierCRNumber] FOREIGN KEY ([SupplierCRNumber]) REFERENCES [IDM].[Supplier] ([SelectedCr]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191221125251_removeFavSupplierListIDPK', N'3.1.0');

GO

ALTER TABLE [MandatoryList].[MandatoryList] ADD [RejectionReason] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191221144704_addedrejectionreason', N'3.1.0');

GO

DROP TABLE [Sadad].[BillsInqueryHistory];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'RejectionDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [RejectionDate];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'RejectionReason');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [RejectionReason];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191222075546_removeBillInqueryHistoryTable', N'3.1.0');

GO

DROP TABLE [Block].[BlockTrackingStatus];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191222081525_removeBlockTrackingStatus', N'3.1.0');

GO

CREATE TABLE [LookUps].[EstablishmentType] (
    [EstablishmentTypeId] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [Name] nvarchar(20) NULL,
    [CommercialRegistrationNo] nvarchar(20) NULL,
    CONSTRAINT [PK_EstablishmentType] PRIMARY KEY ([EstablishmentTypeId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191222134614_addEstablishmentTypes', N'3.1.0');

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'CreatedAt');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [LookUps].[EstablishmentType] DROP COLUMN [CreatedAt];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'CreatedBy');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [LookUps].[EstablishmentType] DROP COLUMN [CreatedBy];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'IsActive');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [LookUps].[EstablishmentType] DROP COLUMN [IsActive];

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'UpdatedAt');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [LookUps].[EstablishmentType] DROP COLUMN [UpdatedAt];

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'UpdatedBy');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [LookUps].[EstablishmentType] DROP COLUMN [UpdatedBy];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191223062058_removeAuditableEntityFromEstablishmentTypes', N'3.1.0');

GO

CREATE TABLE [LookUps].[MandatoryListChangeRequestStatus] (
    [StatusId] int NOT NULL,
    [NameAr] nvarchar(100) NULL,
    [NameEn] nvarchar(100) NULL,
    CONSTRAINT [PK_MandatoryListChangeRequestStatus] PRIMARY KEY ([StatusId])
);

GO

CREATE TABLE [MandatoryList].[MandatoryListChangeRequest] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [DivisionNameAr] nvarchar(400) NULL,
    [DivisionNameEn] nvarchar(400) NULL,
    [DivisionCode] nvarchar(400) NULL,
    [RejectionReason] nvarchar(max) NULL,
    [StatusId] int NOT NULL,
    CONSTRAINT [PK_MandatoryListChangeRequest] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MandatoryListChangeRequest_MandatoryListChangeRequestStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[MandatoryListChangeRequestStatus] ([StatusId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [MandatoryList].[MandatoryListProductChangeRequest] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [CSICode] nvarchar(400) NULL,
    [NameAr] nvarchar(400) NULL,
    [NameEn] nvarchar(400) NULL,
    [DescriptionAr] nvarchar(max) NULL,
    [DescriptionEn] nvarchar(max) NULL,
    [PriceCelling] float NOT NULL,
    [MandatoryListChangeRequestId] int NULL,
    CONSTRAINT [PK_MandatoryListProductChangeRequest] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MandatoryListProductChangeRequest_MandatoryListChangeRequest_MandatoryListChangeRequestId] FOREIGN KEY ([MandatoryListChangeRequestId]) REFERENCES [MandatoryList].[MandatoryListChangeRequest] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StatusId', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[LookUps].[MandatoryListChangeRequestStatus]'))
    SET IDENTITY_INSERT [LookUps].[MandatoryListChangeRequestStatus] ON;
INSERT INTO [LookUps].[MandatoryListChangeRequestStatus] ([StatusId], [NameAr], [NameEn])
VALUES (1, N'بانتظار الاعتماد', NULL),
(2, N'تم الرفض', NULL),
(3, N'معتمدة', NULL),
(4, N'مغلقة', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'StatusId', N'NameAr', N'NameEn') AND [object_id] = OBJECT_ID(N'[LookUps].[MandatoryListChangeRequestStatus]'))
    SET IDENTITY_INSERT [LookUps].[MandatoryListChangeRequestStatus] OFF;

GO

CREATE INDEX [IX_MandatoryListChangeRequest_StatusId] ON [MandatoryList].[MandatoryListChangeRequest] ([StatusId]);

GO

CREATE INDEX [IX_MandatoryListProductChangeRequest_MandatoryListChangeRequestId] ON [MandatoryList].[MandatoryListProductChangeRequest] ([MandatoryListChangeRequestId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191223114443_Added_MandatoryList_CR', N'3.1.0');

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'BillCategory');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [BillCategory];

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'BillCycle');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [BillCycle];

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'CreatedOn');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [CreatedOn];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'DistrictCode');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [DistrictCode];

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'EffectDate');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [EffectDate];

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'PaymentNotificationtDate');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [PaymentNotificationtDate];

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'PaymentType');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [PaymentType];

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'ReconciledDt');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [ReconciledDt];

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'ReconciledStatus');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [ReconciledStatus];

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'SadadBillId');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [SadadBillId];

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'ServiceType');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [ServiceType];

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sadad].[BillInfo]') AND [c].[name] = N'UpdatedOn');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Sadad].[BillInfo] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Sadad].[BillInfo] DROP COLUMN [UpdatedOn];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191223130933_removecoulmnsFromBillTable', N'3.1.0');

GO

ALTER TABLE [MandatoryList].[MandatoryListChangeRequest] ADD [OriginalEntityId] int NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_MandatoryListChangeRequest_OriginalEntityId] ON [MandatoryList].[MandatoryListChangeRequest] ([OriginalEntityId]);

GO

ALTER TABLE [MandatoryList].[MandatoryListChangeRequest] ADD CONSTRAINT [FK_MandatoryListChangeRequest_MandatoryList_OriginalEntityId] FOREIGN KEY ([OriginalEntityId]) REFERENCES [MandatoryList].[MandatoryList] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191224054535_Remainging_MandatoryList_CR', N'3.1.0');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] ON;
INSERT INTO [Verification].[VerificationType] ([VerificationTypeId], [VerificationTypeName])
VALUES (6, N'القائوة الالزامية');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191224095942_MandatoryListVerificationType', N'3.1.0');

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[EstablishmentType]') AND [c].[name] = N'Name');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[EstablishmentType] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [LookUps].[EstablishmentType] ALTER COLUMN [Name] nvarchar(500) NULL;

GO

ALTER TABLE [LookUps].[EstablishmentType] ADD [Size] nvarchar(20) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191226101706_addsizeToEstablishmentType', N'3.1.0');

GO

