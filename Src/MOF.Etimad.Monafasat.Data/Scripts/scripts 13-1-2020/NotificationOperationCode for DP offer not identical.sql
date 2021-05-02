GO
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
           ,[CreatedAt])
     VALUES
           (1993
           ,N'SupplierTechnicalOfferRejected'
           ,N'رفض العرض الفني'
           ,N'Technical offer rejected'
           ,4
           ,1
           ,1
           ,0
           ,0
           ,1
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
           ,N'تنبيهات المنافسة'
           ,N'Monafasat Notification'
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
           ,N'عزيزنا العميل, نحيطم علما انه تم رفض عالعرض الفني لمنافسة:{0}'
		   ,GETDATE()
		   )
GO




