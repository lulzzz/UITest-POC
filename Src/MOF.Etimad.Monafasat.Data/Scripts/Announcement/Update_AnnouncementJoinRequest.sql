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

