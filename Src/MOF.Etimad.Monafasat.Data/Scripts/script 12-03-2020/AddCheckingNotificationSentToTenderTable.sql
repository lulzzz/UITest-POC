



ALTER TABLE [Tender].[Tender] ADD [CheckingNotificationSent] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200317122726_AddCheckingNotificationSentToTenderTable', N'3.1.0');

GO

INSERT [LookUps].[NotificationOperationCode] ([CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [IsActive], [NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (CAST(N'2020-03-17T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL, 2060, N'offersWillCheckingTomorrow', N'قبل موعد فحص العروض بيوم', N'Before the checking of the day', 23, 1, 1, 0, 0, 1, N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}', N'تنبيهات منافسات', N'Monafasat Notification', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فحص العروض بتاريخ {0} للمنافسة رقم:  {1}')