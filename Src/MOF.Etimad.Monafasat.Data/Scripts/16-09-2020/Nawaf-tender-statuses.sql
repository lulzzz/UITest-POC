USE [NewMonafasatDevelopment_DevIteration1.3.3]
GO
UPDATE [LookUps].[TenderStatus] SET NameAr = N'مرحلة تقييم العروض المالية' WHERE TenderStatusId = 31
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (85, N'بانتظار اعتماد تقرير فتح العروض المالية', NULL)
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (86, N'تم فتح العروض المالية', NULL)
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (87, N'تم رفض تقرير فتح العروض المالية', NULL)
