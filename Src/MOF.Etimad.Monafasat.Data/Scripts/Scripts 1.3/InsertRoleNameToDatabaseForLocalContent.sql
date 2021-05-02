IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserRoleId', N'DisplayNameAr', N'DisplayNameEn', N'Name') AND [object_id] = OBJECT_ID(N'[LookUps].[UserRole]'))
    SET IDENTITY_INSERT [LookUps].[UserRole] ON;
INSERT INTO [LookUps].[UserRole] ([UserRoleId], [DisplayNameAr], [DisplayNameEn], [Name])
VALUES (43, N'مسؤول متطلبات المحتوى المحلي', N'Local Content Officer', N'NewMonafasat_LocalContentOfficer');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserRoleId', N'DisplayNameAr', N'DisplayNameEn', N'Name') AND [object_id] = OBJECT_ID(N'[LookUps].[UserRole]'))
    SET IDENTITY_INSERT [LookUps].[UserRole] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200130063847_addedLCUserToDatabase', N'3.1.0');

GO

