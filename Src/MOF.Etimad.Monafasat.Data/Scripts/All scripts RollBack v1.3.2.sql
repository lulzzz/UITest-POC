UPDATE [LookUps].[SupplierExtendOffersValidityStatus] SET [Name] = N'تم الرد (بالموافقة)'
WHERE [SupplierExtendOffersValidityStatusId] = 16;
SELECT @@ROWCOUNT;


GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200810131330_updateExtendOfferValiditySupplierStatue';

GO

ALTER TABLE [Announcement].[AnnouncementJoinRequest] DROP CONSTRAINT [FK_AnnouncementJoinRequest_Supplier_Cr];

GO

DROP INDEX [IX_AnnouncementJoinRequest_Cr] ON [Announcement].[AnnouncementJoinRequest];

GO

DELETE FROM [Announcement].[AnnouncementStatus]
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementJoinRequest]') AND [c].[name] = N'Cr');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementJoinRequest] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[AnnouncementJoinRequest] ALTER COLUMN [Cr] nvarchar(max) NULL;

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200610144604_Update_AnnouncementJoinRequest';

GO

DELETE FROM [Verification].[VerificationType]
WHERE [VerificationTypeId] = 7;
SELECT @@ROWCOUNT;


GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200529091632_Add-Announcement-In-VerificationType';

GO

ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [FK_Announcement_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId];

GO

ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [PK_AnnouncementActivity];

GO

DROP INDEX [IX_Announcement_ReasonForSelectingTenderTypeId] ON [Announcement].[Announcement];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementActivity]') AND [c].[name] = N'AnnouncementActivityId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Announcement].[AnnouncementActivity] DROP COLUMN [AnnouncementActivityId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'ReasonForSelectingTenderTypeId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [ReasonForSelectingTenderTypeId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementCountry]') AND [c].[name] = N'AnnouncementId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementCountry] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Announcement].[AnnouncementCountry] ALTER COLUMN [AnnouncementId] int NULL;

GO

ALTER TABLE [Announcement].[AnnouncementCountry] ADD [TenderId] int NOT NULL DEFAULT 0;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementArea]') AND [c].[name] = N'AnnouncementId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementArea] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Announcement].[AnnouncementArea] ALTER COLUMN [AnnouncementId] int NULL;

GO

ALTER TABLE [Announcement].[AnnouncementArea] ADD [TenderId] int NOT NULL DEFAULT 0;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementActivity]') AND [c].[name] = N'AnnouncementId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Announcement].[AnnouncementActivity] ALTER COLUMN [AnnouncementId] int NULL;

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD [TenderActivityId] int NOT NULL IDENTITY;

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD [TenderId] int NOT NULL DEFAULT 0;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'IntroAboutTender');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [IntroAboutTender] nvarchar(max) NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'AnnouncementName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [AnnouncementName] nvarchar(1024) NOT NULL;

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD CONSTRAINT [PK_AnnouncementActivity] PRIMARY KEY ([TenderActivityId]);

GO

CREATE INDEX [IX_AnnouncementCountry_TenderId] ON [Announcement].[AnnouncementCountry] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementArea_TenderId] ON [Announcement].[AnnouncementArea] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementActivity_TenderId] ON [Announcement].[AnnouncementActivity] ([TenderId]);

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD CONSTRAINT [FK_AnnouncementActivity_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementArea] ADD CONSTRAINT [FK_AnnouncementArea_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementCountry] ADD CONSTRAINT [FK_AnnouncementCountry_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION;

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200511225117_Updates-In-Announcement';

GO

ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [FK_Tender_Announcement_PreAnnouncementId];

GO

DROP INDEX [IX_Tender_PreAnnouncementId] ON [Tender].[Tender];

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'PreAnnouncementId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Tender].[Tender] DROP COLUMN [PreAnnouncementId];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200511202408_AddAnnouncementIdToTender';

GO

DROP TABLE [Announcement].[AnnouncementJoinRequest];

GO

DROP TABLE [LookUps].[AnnouncementJoinRequestStatus];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200511103738_addJoinRequest';

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'PublishedDate');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [PublishedDate] datetime2 NOT NULL;

GO

DROP INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement];
DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'BranchId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [BranchId] int NOT NULL;
CREATE INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement] ([BranchId]);

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200510234505_Update_Announcement2';

GO

ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [FK_Announcement_GovAgency_AgencyCode];

GO

ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [FK_Announcement_Branch_BranchId];

GO

DROP INDEX [IX_Announcement_AgencyCode] ON [Announcement].[Announcement];

GO

DROP INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'AgencyCode');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [AgencyCode];

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'ApprovedBy');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [ApprovedBy];

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'BranchId');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [BranchId];

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'ReferenceNumber');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [ReferenceNumber];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200508043612_Update_Announcement';

GO

ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [FK_Announcement_TenderType_TenderTypeId];

GO

ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[AnnouncementArea] DROP CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[AnnouncementConstructionWork] DROP CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[AnnouncementCountry] DROP CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[AnnouncementHistory] DROP CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[AnnouncementMaintenanceRunnigWork] DROP CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId];

GO

ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [PK_Announcement];

GO

DROP INDEX [IX_Announcement_TenderTypeId] ON [Announcement].[Announcement];

GO

DELETE FROM [Announcement].[AnnouncementStatus]
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

DELETE FROM [Announcement].[AnnouncementStatus]
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


GO

DELETE FROM [Announcement].[AnnouncementStatus]
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


GO

DELETE FROM [Announcement].[AnnouncementStatus]
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'AnnouncementId');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [AnnouncementId];

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'PublishedDate');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [PublishedDate];

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'TenderTypeId');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [TenderTypeId];

GO

ALTER TABLE [Announcement].[Announcement] ADD [Id] int NOT NULL IDENTITY;

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [PK_Announcement] PRIMARY KEY ([Id]);

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementArea] ADD CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementConstructionWork] ADD CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementCountry] ADD CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementHistory] ADD CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementMaintenanceRunnigWork] ADD CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION;

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200505221945_Init-Announcement-2';

GO

DROP TABLE [Announcement].[AnnouncementActivity];

GO

DROP TABLE [Announcement].[AnnouncementArea];

GO

DROP TABLE [Announcement].[AnnouncementConstructionWork];

GO

DROP TABLE [Announcement].[AnnouncementCountry];

GO

DROP TABLE [Announcement].[AnnouncementHistory];

GO

DROP TABLE [Announcement].[AnnouncementMaintenanceRunnigWork];

GO

DROP TABLE [Announcement].[Announcement];

GO

DROP TABLE [Announcement].[AnnouncementStatus];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200505152310_Init-Announcement';

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'ExclusionReason');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Offer].[Offer] DROP COLUMN [ExclusionReason];

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200505140439_addExclusionreasonToOfferTable';

GO

