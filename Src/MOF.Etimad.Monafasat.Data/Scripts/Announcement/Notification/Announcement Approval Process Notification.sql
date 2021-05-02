



-- 1
insert into lookups.NotificationCategory values (19,N'العمليات علي الإعلان','Announcement Operations')




-- 2
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
           ,[CreatedAt]
           ,[CreatedBy]
           ,[IsActive]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           (
		   
		   6000,	'SendAnnouncementForApproval',N'	عند إرسال الإعلان للإعتماد','	When send announcement approved	',3,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})	',
			 N'تنبيهات منافسات',	N'تنبيهات منافسات	 ' , N'عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 N'	 عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',N' عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 N'	 عزيزنا المستخدم، هنالك طلب إعلان لمنافسة بانتظار الاعتماد لإعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   ),
		        (
		   
		   6100,	'ApproveAnnouncement',N'	عند إعتماد إلاعلان','	When Approve Announcement	',1,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})	',
			 N'تنبيهات منافسات',	N'تنبيهات منافسات	 ' , N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 N'	عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',N'عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 N'	عزيزنا المستخدم، تم اعتماد طلب إعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   ),
		          (
		   
		   6200,	'RejectApproveAnnouncement',N'	عند رفض إعتماد إلاعلان','	When Reject To Approve Announcement	',1,0,1,1,0,19,
		   	 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})' ,N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'تنبيهات منافسات',N'تنبيهات منافسات	 ' ,N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 N'عزيزنا المستخدم، تم رفض اعتماد إعلان منافسة رقم: ({0})',
			 	'0001-01-01 00:00:00.0000000'	,NULL	,NULL,	NULL,	NULL
		   )
GO


