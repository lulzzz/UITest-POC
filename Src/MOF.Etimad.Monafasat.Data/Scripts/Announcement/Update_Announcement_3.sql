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

