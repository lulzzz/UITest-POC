IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] ON;
INSERT INTO [Verification].[VerificationType] ([VerificationTypeId], [VerificationTypeName])
VALUES (7, N'الإعلان');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'VerificationTypeId', N'VerificationTypeName') AND [object_id] = OBJECT_ID(N'[Verification].[VerificationType]'))
    SET IDENTITY_INSERT [Verification].[VerificationType] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200529091632_Add-Announcement-In-VerificationType', N'3.1.0');