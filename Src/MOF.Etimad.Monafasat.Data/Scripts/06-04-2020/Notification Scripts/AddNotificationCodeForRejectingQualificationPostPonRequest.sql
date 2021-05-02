USE [NewMonafasatDevelopment_Iteration3]
GO

INSERT INTO [LookUps].[NotificationOperationCode]
           ([CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy]
           ,[IsActive]
           ,[NotificationOperationCodeId]
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
           ,[PanelTemplateEn])
     VALUES
           

(GETDATE(),NULL,	NULL,NULL,NULL,732,N'RejectQualificationPostponeRequest'	,N'رفض طلب تأجيل تقديم وثائق التأهيل	',N'Reject Upload Qualification Documnets Postpone Request	',4,	0	,1	,0	,1	,16	,
N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}',
	
N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}',
N'	تنبيهات منافسات',	'Monafasat Notification	',
N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}',

	N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}',

	N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}',

	N'عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم وثائق التأهيل رقم:{1}'
	)
GO


