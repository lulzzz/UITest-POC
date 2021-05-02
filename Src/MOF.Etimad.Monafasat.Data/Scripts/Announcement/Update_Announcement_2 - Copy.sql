﻿DECLARE @var0 sysname;
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

