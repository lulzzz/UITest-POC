
UPDATE [LookUps].[TenderStatus] SET NameAr = N'مرحلة تقييم العروض المالية' WHERE TenderStatusId = 31
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (85, N'بانتظار اعتماد تقرير فتح العروض المالية', NULL)
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (86, N'تم فتح العروض المالية', NULL)
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (87, N'تم رفض تقرير فتح العروض المالية', NULL)
INSERT [LookUps].[TenderStatus] ([TenderStatusId], [NameAr], [NameEn]) VALUES (90, N'مرحلة فحص العروض الفنية', NULL)
GO

INSERT INTO [LookUps].[TenderStatus]
           ([TenderStatusId]
           ,[NameAr]
           ,[NameEn])
     VALUES
           (78
           ,N'مرحلة فتح العروض الفنية'
           ,null)
         
		 , (80
           ,N'بإنتظار إعتماد تقرير فتح العروض الفنية'
           ,null)
          
		  
		  , (82
           ,N'تم فتح العروض الفنية'
           ,null)
		   
		   , (84
           ,N'تم رفض تقرير فتح العروض الفنية'
           ,null)
GO



insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (302	,N'تاكيد مرحلة فتح العروض الفنية',	'OpenTechnicalEnvelope')
insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (303	,N'ارسال تقرير فتح العروض الفنية للاعتماد',	'OpenTechnicalEnvelope')
insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (304	,N'انهاء عملية فتح العروض المالية وارسالها للاعتماد',	'SendFinancialOpeningToApproval')
insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (305	,N'انهاء اعتماد فتح العروض المالية',	'ApproveFinancialOpening')
insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (306	,N'رفض فتح العروض المالية',	'RejectFinancialOpening')
insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (307	,N'اعادة فتح العروض المالية',	'ReopenFinancialOpening')

GO
insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(
						 
						 
						 
						 
						 7900,'sendopenenvelopesreportforapproval',N'ارسال تقرير فتح المظاريف الفنية للاعتماد','send Open Envelopes Report for Approval'
						 ,5,0,1,0,1,4
						 ,N'عزيزنا المستخدم، تم إرسال تقرير فتح
العروض الفنية للاعتماد للمنافسة رقم:  {0}
',N' عزيزنا المستخدم، تم إرسال تقرير فتح
العروض  للاعتماد للمنافسة رقم:  {0}'
,N'تنبيهات منافسات','Monafasat Notification',
 N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">          عزيزنا المستخدم، تم إرسال تقرير فتح  العروض الفنية للاعتماد للمنافسة رقم:  {0}
فريق منافسات     </p> '
,
 N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           Dear User, Technical openeing Report was Sent to Approve for Tender Number {0}
EGP Team Monafsat      </p> '


,N' عزيزنا المستخدم، تم إرسال تقرير فتح  العروض الفنية للاعتماد للمنافسة رقم:  {0}',

N'Dear User, Technical openeing Report was Sent to Approve for Tender Number {0}',

'0001-01-01 00:00:00.0000000',null,1,null,null)

 go 

 

insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(
						 
						 
						 
						 
						 8000,'rejectopenenvelope'
						 
						 ,N'رفض تقرير فتح العروض'
						 
						 ,'when open Envelope Rejected'
						 
						 ,6,0,1,1,0,1
						 
						 ,N'عزيزنا المستخدم، تم رفض تقرير فتح
العروض الفنية للمنافسة رقم: {0}'
						 
						 
						 ,'Dear user, Envelope technical opening report has been rejected for tender No: {0}'
						
						,N'تنبيهات منافسات','Monafasat Notification'
						 
						
						,N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">
						 عزيزنا المستخدم، تم رفض تقرير فتح
العروض الفنية للمنافسة رقم: {0} 
						فريق عمل منافسات      </p>'
						 
						
						,N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">
						  Dear user, Envelope technical opening report has been rejected for tender No: {0}    
						  
						  </n>
						  Monafasat Team
						  </p>',
						 
						 
						 N'تم رفض تقرير فتح
العروض الفنية للمنافسة رقم: {0}'
						 
						 ,N'Bid Technical opening has been rejected for tender No: {0}'
						 
						 ,'0001-01-01 00:00:00.0000000',null,1,null,null)
						 go
insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(
						 
						 
						 
						 
						 8002,'publishopentechnicalenvelopes',N'عند نشر تقرير فتح فنى  المظاريف','when open Technical envelops published',6,0,1,0,1,1
						 
						 ,N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}'
						 
						 
						 ,N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}'
						
						
						,N'تنبيهات منافسات','Monafasat Notification',
						 N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right"> <br><br> عزيزنا المستخدم، تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}         فريق عمل منافسات      </p>  '
						 
						 ,N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">     عزيزنا المستخدم، تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}  <br><br>          EGP Team Monafsat      </p>  '
						 
						 ,N'تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}'
						 
						 ,N' تم اعتماد تقرير فتح العروض الفنية للمنافسة رقم:{0}'


,'0001-01-01 00:00:00.0000000',null,1,null,null)


go

insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(
						 8004,'publishopentechnicalenvelopes',N'عند نشر تقرير فتح فنى  المظاريف','when open Technical envelops published',4,0,1,0,1,1
						 
						 ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						 
						 
						,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						 
						
						,N'تنبيهات منافسات','Monafasat Notification',
						 N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right"> <br><br> عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}</p>  '
						 
						 ,N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">   عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}<br><br>          EGP Team Monafsat      </p>  '
						 
						  ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						
						 ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						

,'0001-01-01 00:00:00.0000000',null,1,null,null)
go

insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(
						 8004,'publishopentechnicalenvelopes',N'عند نشر تقرير فتح فنى  المظاريف','when open Technical envelops published',4,0,1,0,1,1
						 
						 ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						 
						 
						,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						 
						
						,N'تنبيهات منافسات','Monafasat Notification',
						 N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right"> <br><br> عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}</p>  '
						 
						 ,N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">   عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}<br><br>          EGP Team Monafsat      </p>  '
						 
						  ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						
						 ,N'عميلنا العزيز، نحيطكم علماً أنه تم الانتهاء من فتح العروض الفنية للمنافسة رقم:{0}'
						

,'0001-01-01 00:00:00.0000000',null,1,null,null)

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (8006, N'sendFinancialOpeningForApproval', N'ارسال عملية فتح العروض المالية للاعتماد', N'Open financial offer sent for approval', 5, 1, 1, 0, 1, 4, N'عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}', N'عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <div dir="rtl">
        <p style="text-align:center">
            السلام عليكم ورحمة الله وبركاته
        </p>
        <br />
        <p style="text-align:right">
            عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}
        </p>
        <br />
    </div>', N'    <div dir="rtl">
        <p style="text-align:center">
            السلام عليكم ورحمة الله وبركاته
        </p>
        <br />
        <p style="text-align:right">
            عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}
        </p>
        <br />
    </div>', N'عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}', N'عزيزنا المستخدم، تم ارسال تقرير فتح العروض المالية للاعتماد للمنافسة رقم:{0}', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO

INSERT INTO [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (8008, N'ApproveFinancialOpening', N'اعتماد عملية فتح العروض المالية', N'Open financial offers approved', 6, 1, 1, 1, 1, 1, N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}
    </p>
    <br />
</div>', N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}
    </p>
    <br />
</div>', N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم اعتماد تقرير فتح العروض المالية للمنافسة رقم: {0}', N'0001-01-01 00:00:00', NULL, NULL, NULL, NULL)
GO

INSERT INTO [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy])
VALUES (8010, N'financialOffersOpeningApproved', N'اعتماد فتح العروض المالية', N'Open financial offers approved', 4, 1, 1, 1, 1, 1, N'عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}', N'عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}
    </p>
    <br />
</div>', N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}
    </p>
    <br />
</div>', N'عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}', N'عميلنا العزيز، نحيطكم علما تم الانتهاء من فتح العروض المالية للمنافسة رقم: {0}', N'0001-01-01 00:00:00', NULL, NULL, NULL, NULL)
GO


go
insert into LookUps.TenderStatus values (92,N'معادة للفحص بسبب قبول طلب التظلم','Back to Checking After Plaint Request Accepted')



Go



insert into LookUps.NotificationOperationCode ( NotificationOperationCodeId, OperationCode, ArabicName, EnglishName, UserRoleId, DefaultSMS, DefaultEmail, CanEditEmail, CanEditSMS, NotificationCategoryId, SmsTemplateAr, SmsTemplateEn, 
                         EmailSubjectTemplateAr, EmailSubjectTemplateEn, EmailBodyTemplateAr, EmailBodyTemplateEn, PanelTemplateAr, PanelTemplateEn, CreatedAt, CreatedBy, IsActive, UpdatedAt, UpdatedBy)
						 values(

						 555,'publishtechnicalevaluation'
						 
						 ,N'عند إعتماد التقييم الفني'
						 
						 ,'when publish technical evaluation'
						 
						 ,6,0,1,1,0,1
						 
						 ,N' عزيزنا المستخدم،  نود تنبيهكم  بانه تم اعتماد التقييم الفني للمنافسة رقم: {0}'
						 
						 
						 ,'	Dear user, Technical evalation has been approved for tender No: {0}'
						
						,N'تنبيهات منافسات','Monafasat Notification'
						 
						
						,N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                عزيزنا المستخدم،  {0},
        
    </p>
    <p style="text-align:right">
      
	  نود تنبيهكم  بانه تم اعتماد التقييم الفني للمنافسة رقم: <strong>{1}</strong> <br><br> 

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6}<br>
        

        
        <br><br>
        فريق عمل منافسات   </p>	'
						 
						
						,N' <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Technical Evaluation for following tender has been approved: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat  
		 </p>	',
						 
						 
						 N'  عزيزنا المستخدم،  نود تنبيهكم  بانه تم اعتماد التقييم الفني للمنافسة رقم: {0}'
						 
						 ,N'Technical Offer Report Approved for Tender Number {0}'
						 
						 ,'0001-01-01 00:00:00.0000000',null,1,null,null)					 
Go

INSERT INTO [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (8012, N'RejectFinancialOpening', N'رفض تقرير فتح العروض المالية', N'Open financial offers report rejected', 6, 1, 1, 1, 1, 1, N'عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <div dir="rtl">
        <p style="text-align:center">
            السلام عليكم ورحمة الله وبركاته
        </p>
        <br />
        <p style="text-align:right">
            عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}
        </p>
        <br />
    </div>', N'    <div dir="rtl">
        <p style="text-align:center">
            السلام عليكم ورحمة الله وبركاته
        </p>
        <br />
        <p style="text-align:right">
            عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}
        </p>
        <br />
    </div>', N'عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم رفض تقرير فتح العروض المالية للمنافسة رقم: {0}', N'0001-01-01 00:00:00', NULL, NULL, NULL, NULL)
	GO

ALTER TABLE [Tender].[Tender] ADD [FinancialCheckingDateSet] bit NULL;
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200927092437_added_FinancialCheckingDateSet', N'3.1.0');

GO


update [LookUps].[NotificationOperationCode] 
set SmsTemplateAr = N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
,SmsTemplateEn =N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
,PanelTemplateAr = N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
, PanelTemplateEn = N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
,EmailBodyTemplateAr = N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
,EmailBodyTemplateEn =N'عزيزنا العميل, نحيطم علما انه تم رفض العرض الفني لمنافسة:{0}'
where notificationoperationcodeid = 1993

update [LookUps].[NotificationOperationCode] 
set
EmailBodyTemplateAr = N'    <p style="text-align:center">&nbsp;</p>        <p style="text-align:right">     عزيزنا المستخدم، تم إرسال تقرير الفحص المالى للاعتماد للمنافسة رقم : {1} <br><br>            <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {2}<br><br>          آخر موعد لاستلام الاستفسارات: {3} <br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  '
,EmailBodyTemplateEn =N'    <p style="text-align:center">&nbsp;</p>        <p style="text-align:right">     عزيزنا المستخدم، تم إرسال تقرير الفحص المالى للاعتماد للمنافسة رقم : {1} <br><br>            <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {2}<br><br>          آخر موعد لاستلام الاستفسارات: {3} <br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  '
where notificationoperationcodeid = 2042
