
--hamed ----------------------------------------------------addExclusionreasonToOfferTable 05-05-2020--------------------------------------------------------------------------

ALTER TABLE [Offer].[Offer] ADD [ExclusionReason] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200505140439_addExclusionreasonToOfferTable', N'3.1.0');

GO


INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (2075, N'ExclusionSupplierFromAwarding', N'عند استبعاد مورد من الترسية', N'When supplier excluded from awarding', 4, 0, 1, 1, 0, 1, N'عميلنا العزيز، يؤسفونا  إبلاغكم أنه لم تتم الترسية عليكم وتم إستبعاد عروضكم  للمنافسة رقم:  {0}', N'عميلنا العزيز، يؤسفونا  إبلاغكم أنه لم تتم الترسية عليكم وتم إستبعاد عروضكم  للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'ExclusionSupplierFromAwarding', N'	<div dir="rtl">   
  <p style="text-align:right"> السلام عليكم ورحمة الله وبركاته</p>
    <p style="text-align:right">
      عزيزنا العميل نحيطكم علما أنه تم ترسية المنافسة {0}
	  علي المتنافس :{1}
	  
        سبب عدم الترسية  : {2}<br><br>
				وتقبلوا وافر تحياتنا،، <br>  
        فريق عمل منافسات
    </p>
	</div>', N'	<div dir="rtl">   
  <p style="text-align:right"> السلام عليكم ورحمة الله وبركاته</p>
    <p style="text-align:right">
      عزيزنا العميل نحيطكم علما أنه تم ترسية المنافسة {0}
	  علي المتنافس :{1}
	  
        سبب عدم الترسية  : {2}<br><br>
				وتقبلوا وافر تحياتنا،، <br>  
        فريق عمل منافسات
    </p>
	</div>', N' لم تتم الترسية عليكم وتم إستبعاد عروضكم  للمنافسة رقم:  {0}', N' لم تتم الترسية عليكم وتم إستبعاد عروضكم  للمنافسة رقم:  {0}', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO


----youssef ---------------------------------------------Init-Announcement 2020-05-07 3.30-----------------------------------------------------------------

IF SCHEMA_ID(N'Announcement') IS NULL EXEC(N'CREATE SCHEMA [Announcement];');

GO

CREATE TABLE [Announcement].[AnnouncementStatus] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(1024) NULL,
    CONSTRAINT [PK_AnnouncementStatus] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Announcement].[Announcement] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [AnnouncementName] nvarchar(1024) NOT NULL,
    [IntroAboutTender] nvarchar(max) NOT NULL,
    [AnnouncementPeriod] int NOT NULL,
    [InsidKsa] bit NOT NULL,
    [Details] nvarchar(max) NULL,
    [ActivityDescription] nvarchar(2000) NULL,
    [StatusId] int NOT NULL,
    CONSTRAINT [PK_Announcement] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Announcement_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementActivity] (
    [TenderActivityId] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [ActivityId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementActivity] PRIMARY KEY ([TenderActivityId]),
    CONSTRAINT [FK_AnnouncementActivity_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementActivity_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementArea] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [AreaId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementArea] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementArea_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementArea_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementConstructionWork] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [ConstructionWorkId] int NOT NULL,
    [AnnouncementId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementConstructionWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementConstructionWork_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementCountry] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [CountryId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementCountry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementCountry_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementCountry_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementHistory] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [StatusId] int NOT NULL,
    [RejectionReason] nvarchar(2000) NULL,
    [AnnouncementId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementHistory_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementMaintenanceRunnigWork] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [MaintenanceRunningWorkId] int NOT NULL,
    [AnnouncementId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementMaintenanceRunnigWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Announcement_StatusId] ON [Announcement].[Announcement] ([StatusId]);

GO

CREATE INDEX [IX_AnnouncementActivity_ActivityId] ON [Announcement].[AnnouncementActivity] ([ActivityId]);

GO

CREATE INDEX [IX_AnnouncementActivity_AnnouncementId] ON [Announcement].[AnnouncementActivity] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementActivity_TenderId] ON [Announcement].[AnnouncementActivity] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementArea_AnnouncementId] ON [Announcement].[AnnouncementArea] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementArea_AreaId] ON [Announcement].[AnnouncementArea] ([AreaId]);

GO

CREATE INDEX [IX_AnnouncementArea_TenderId] ON [Announcement].[AnnouncementArea] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementConstructionWork_AnnouncementId] ON [Announcement].[AnnouncementConstructionWork] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementConstructionWork_ConstructionWorkId] ON [Announcement].[AnnouncementConstructionWork] ([ConstructionWorkId]);

GO

CREATE INDEX [IX_AnnouncementCountry_AnnouncementId] ON [Announcement].[AnnouncementCountry] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementCountry_CountryId] ON [Announcement].[AnnouncementCountry] ([CountryId]);

GO

CREATE INDEX [IX_AnnouncementCountry_TenderId] ON [Announcement].[AnnouncementCountry] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementHistory_AnnouncementId] ON [Announcement].[AnnouncementHistory] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementHistory_StatusId] ON [Announcement].[AnnouncementHistory] ([StatusId]);

GO

CREATE INDEX [IX_AnnouncementMaintenanceRunnigWork_AnnouncementId] ON [Announcement].[AnnouncementMaintenanceRunnigWork] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWorkId] ON [Announcement].[AnnouncementMaintenanceRunnigWork] ([MaintenanceRunningWorkId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200505152310_Init-Announcement', N'3.1.0');

GO



----youssef ---------------------------------------------Init-Announcement-2  2020-05-07  3.30--------------------------------

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

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[Announcement] DROP COLUMN [Id];

GO

ALTER TABLE [Announcement].[Announcement] ADD [AnnouncementId] int NOT NULL IDENTITY;

GO

ALTER TABLE [Announcement].[Announcement] ADD [PublishedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Announcement].[Announcement] ADD [TenderTypeId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [PK_Announcement] PRIMARY KEY ([AnnouncementId]);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Announcement].[AnnouncementStatus]'))
    SET IDENTITY_INSERT [Announcement].[AnnouncementStatus] ON;
INSERT INTO [Announcement].[AnnouncementStatus] ([Id], [Name])
VALUES (1, N'تحت الإنشاء'),
(2, N'بإنتظار الإعتماد'),
(3, N'معتمد'),
(4, N'مرفوض'),
(5, N'منهي');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Announcement].[AnnouncementStatus]'))
    SET IDENTITY_INSERT [Announcement].[AnnouncementStatus] OFF;

GO

CREATE INDEX [IX_Announcement_TenderTypeId] ON [Announcement].[Announcement] ([TenderTypeId]);

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_TenderType_TenderTypeId] FOREIGN KEY ([TenderTypeId]) REFERENCES [LookUps].[TenderType] ([TenderTypeId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementArea] ADD CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementConstructionWork] ADD CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementCountry] ADD CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementHistory] ADD CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[AnnouncementMaintenanceRunnigWork] ADD CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200505221945_Init-Announcement-2', N'3.1.0');

GO


-----ismael----------------------------------------Update_Announcement 2020-05-08 04.36.12------------------------------------------------------

ALTER TABLE [Announcement].[Announcement] ADD [AgencyCode] nvarchar(20) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ApprovedBy] nvarchar(200) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [BranchId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ReferenceNumber] nvarchar(100) NULL;

GO

CREATE INDEX [IX_Announcement_AgencyCode] ON [Announcement].[Announcement] ([AgencyCode]);

GO

CREATE INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement] ([BranchId]);

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [BranchId] int NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200508043612_Update_Announcement', N'3.1.0');

GO

--youssef -----------------------------------------------------Update_Announcement  2020-05-08  6.55-------------------------------------------------------------
ALTER TABLE [Announcement].[Announcement] ADD [AgencyCode] nvarchar(20) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ApprovedBy] nvarchar(200) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [BranchId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ReferenceNumber] nvarchar(100) NULL;

GO

CREATE INDEX [IX_Announcement_AgencyCode] ON [Announcement].[Announcement] ([AgencyCode]);

GO

CREATE INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement] ([BranchId]);

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [BranchId] int NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200508043612_Update_Announcement', N'3.1.0');

GO

--ismael -----------------------------------------------------Update_Announcement_2 2020-05-10 23.45.05-------------------------------------------------------------

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'PublishedDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [PublishedDate] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200510234505_Update_Announcement2', N'3.1.0');

GO


----youssef ---------------------------------Update_Announcement_2 - 2020-05-11 4.50-----------------------------------------------------------------

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'PublishedDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [PublishedDate] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200510234505_Update_Announcement2', N'3.1.0');

GO

----youssef ---------------------------------Update_Announcement_3  2020-05-12 10.00-----------------------------------------------------------------

ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [FK_AnnouncementActivity_Tender_TenderId];

GO

ALTER TABLE [Announcement].[AnnouncementArea] DROP CONSTRAINT [FK_AnnouncementArea_Tender_TenderId];

GO

ALTER TABLE [Announcement].[AnnouncementCountry] DROP CONSTRAINT [FK_AnnouncementCountry_Tender_TenderId];

GO

DROP INDEX [IX_AnnouncementCountry_TenderId] ON [Announcement].[AnnouncementCountry];

GO

DROP INDEX [IX_AnnouncementArea_TenderId] ON [Announcement].[AnnouncementArea];

GO

ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [PK_AnnouncementActivity];

GO

DROP INDEX [IX_AnnouncementActivity_TenderId] ON [Announcement].[AnnouncementActivity];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementCountry]') AND [c].[name] = N'TenderId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementCountry] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[AnnouncementCountry] DROP COLUMN [TenderId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementArea]') AND [c].[name] = N'TenderId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementArea] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Announcement].[AnnouncementArea] DROP COLUMN [TenderId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementActivity]') AND [c].[name] = N'TenderActivityId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Announcement].[AnnouncementActivity] DROP COLUMN [TenderActivityId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementActivity]') AND [c].[name] = N'TenderId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Announcement].[AnnouncementActivity] DROP COLUMN [TenderId];

GO

DROP INDEX [IX_AnnouncementCountry_AnnouncementId] ON [Announcement].[AnnouncementCountry];
DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementCountry]') AND [c].[name] = N'AnnouncementId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementCountry] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Announcement].[AnnouncementCountry] ALTER COLUMN [AnnouncementId] int NOT NULL;
CREATE INDEX [IX_AnnouncementCountry_AnnouncementId] ON [Announcement].[AnnouncementCountry] ([AnnouncementId]);

GO

DROP INDEX [IX_AnnouncementArea_AnnouncementId] ON [Announcement].[AnnouncementArea];
DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementArea]') AND [c].[name] = N'AnnouncementId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementArea] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Announcement].[AnnouncementArea] ALTER COLUMN [AnnouncementId] int NOT NULL;
CREATE INDEX [IX_AnnouncementArea_AnnouncementId] ON [Announcement].[AnnouncementArea] ([AnnouncementId]);

GO

DROP INDEX [IX_AnnouncementActivity_AnnouncementId] ON [Announcement].[AnnouncementActivity];
DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementActivity]') AND [c].[name] = N'AnnouncementId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementActivity] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Announcement].[AnnouncementActivity] ALTER COLUMN [AnnouncementId] int NOT NULL;
CREATE INDEX [IX_AnnouncementActivity_AnnouncementId] ON [Announcement].[AnnouncementActivity] ([AnnouncementId]);

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD [AnnouncementActivityId] int NOT NULL IDENTITY;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'IntroAboutTender');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [IntroAboutTender] nvarchar(max) NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[Announcement]') AND [c].[name] = N'AnnouncementName');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[Announcement] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [AnnouncementName] nvarchar(1024) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ReasonForSelectingTenderTypeId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Announcement].[AnnouncementActivity] ADD CONSTRAINT [PK_AnnouncementActivity] PRIMARY KEY ([AnnouncementActivityId]);

GO

CREATE INDEX [IX_Announcement_ReasonForSelectingTenderTypeId] ON [Announcement].[Announcement] ([ReasonForSelectingTenderTypeId]);

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId] FOREIGN KEY ([ReasonForSelectingTenderTypeId]) REFERENCES [Lookups].[ReasonForPurchaseTenderType] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200511225117_Updates-In-Announcement', N'3.1.0');

GO

-----ibrahim-----------------------------------------------------Join Request  12-05-2020 20.12  ---------------------------------------------------------------------------------------
GO
/****** Object:  Table [Announcement].[AnnouncementJoinRequest]    Script Date: 5/12/2020 08:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Announcement].[AnnouncementJoinRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](256) NULL,
	[IsActive] [bit] NULL,
	[StatusId] [int] NOT NULL,
	[AnnouncementId] [int] NOT NULL,
	[Cr] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnnouncementJoinRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [LookUps].[AnnouncementJoinRequestStatus]    Script Date: 5/12/2020 08:12:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [LookUps].[AnnouncementJoinRequestStatus](
	[Id] [int] NOT NULL,
	[NameEn] [nvarchar](1024) NULL,
	[NameAr] [nvarchar](1024) NULL,
 CONSTRAINT [PK_AnnouncementJoinRequestStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Announcement].[AnnouncementJoinRequest]  WITH CHECK ADD  CONSTRAINT [FK_AnnouncementJoinRequest_Announcement_AnnouncementId] FOREIGN KEY([AnnouncementId])
REFERENCES [Announcement].[Announcement] ([AnnouncementId])
GO
ALTER TABLE [Announcement].[AnnouncementJoinRequest] CHECK CONSTRAINT [FK_AnnouncementJoinRequest_Announcement_AnnouncementId]
GO
ALTER TABLE [Announcement].[AnnouncementJoinRequest]  WITH CHECK ADD  CONSTRAINT [FK_AnnouncementJoinRequest_AnnouncementJoinRequestStatus_StatusId] FOREIGN KEY([StatusId])
REFERENCES [LookUps].[AnnouncementJoinRequestStatus] ([Id])
GO
ALTER TABLE [Announcement].[AnnouncementJoinRequest] CHECK CONSTRAINT [FK_AnnouncementJoinRequest_AnnouncementJoinRequestStatus_StatusId]
GO


----youssef ---------------------------------Add-Announcement-In-VerifcationType   2020-05-29   5.50-----------------------------------------------------------------

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] ON;
INSERT INTO [Verification].[VerificationType] ([VerificationTypeId], [VerificationTypeName])
VALUES (7, N'الإعلان');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200529091632_Add-Announcement-In-VerificationType', N'3.1.0');

----youssef ---------------------------------Announcement Approval Process Notification  2020-05-31  5.30-----------------------------------------------------------------

-- 1
insert into lookups.NotificationCategory values (19,N'العمليات علي الإعلان','Announcement Operations')

-- 2
INSERT INTO [LookUps].[NotificationOperationCode]
           ([NotificationOperationCodeId]
           ,[OperationCode]
           ,[ArabicName]
           ,[EnglishName]
           ,[UserRoleId]
           ,[DefaultSMS]
           ,[DefaultEmail]
           ,[CanEditEmail]
           ,[CanEditSMS]
           ,[NotificationCategoryId]
           ,[SmsTemplateAr]
           ,[SmsTemplateEn]
           ,[EmailSubjectTemplateAr]
           ,[EmailSubjectTemplateEn]
           ,[EmailBodyTemplateAr]
           ,[EmailBodyTemplateEn]
           ,[PanelTemplateAr]
           ,[PanelTemplateEn]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[IsActive]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           (
		   
		   6000,	'SendAnnouncementForApproval',N'	عند إرسال الإعلان للإعتماد','	When send announcement approved	',3,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})	',
			 N'تنبيهات منافسات',	N'تنبيهات منافسات	 ' , N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 N'	 عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',N' عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 N'	 عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   ),
		        (
		   
		   6100,	'ApproveAnnouncement',N'	عند إعتماد إلاعلان','	When Approve Announcement	',1,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})	',
			 N'تنبيهات منافسات',	N'تنبيهات منافسات	 ' , N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 N'	عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 N'	عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   ),
		          (
		   
		   6200,	'RejectApproveAnnouncement',N'	عند رفض إعتماد إلاعلان','	When Reject To Approve Announcement	',1,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'تنبيهات منافسات',N'تنبيهات منافسات	 ' ,N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   )
GO


-----tag-----------------------------------------------------PreAnnouncement Id in tender table 11-5-2020 ---------------------------------------------------------------------------------------

ALTER TABLE [Tender].[Tender] ADD [PreAnnouncementId] int NULL;

GO

CREATE INDEX [IX_Tender_PreAnnouncementId] ON [Tender].[Tender] ([PreAnnouncementId]);

GO

ALTER TABLE [Tender].[Tender] ADD CONSTRAINT [FK_Tender_Announcement_PreAnnouncementId] FOREIGN KEY ([PreAnnouncementId]) REFERENCES [Announcement].[Announcement] ([AnnouncementId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200511202408_AddAnnouncementIdToTender', N'3.1.0');
 
GO


-----ismael-----------------------------------------------------Update_AnnouncementJoinRequest 2020-06-10 14.46.04 ---------------------------------------------------------------------------------------

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Announcement].[AnnouncementJoinRequest]') AND [c].[name] = N'Cr');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Announcement].[AnnouncementJoinRequest] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Announcement].[AnnouncementJoinRequest] ALTER COLUMN [Cr] nvarchar(20) NULL;

GO

CREATE INDEX [IX_AnnouncementJoinRequest_Cr] ON [Announcement].[AnnouncementJoinRequest] ([Cr]);

GO

ALTER TABLE [Announcement].[AnnouncementJoinRequest] ADD CONSTRAINT [FK_AnnouncementJoinRequest_Supplier_Cr] FOREIGN KEY ([Cr]) REFERENCES [IDM].[Supplier] ([SelectedCr]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200610144604_Update_AnnouncementJoinRequest', N'3.1.0');

GO

