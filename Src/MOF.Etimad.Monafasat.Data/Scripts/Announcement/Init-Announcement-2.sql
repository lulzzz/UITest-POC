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

