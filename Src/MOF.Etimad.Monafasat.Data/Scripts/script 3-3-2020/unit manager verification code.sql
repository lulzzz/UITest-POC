INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (7898, N'SendVerificationCode', N'ارسال رمز تحقق', N'Send verification code', 20, 1, 1, 1, 1, 1, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
hello, please use the following number : {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
 <p style="text-align:right">
 <span style="font-family:arial,helvetica,sans-serif;font-size:12px"> 
 <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
  السلام عليكم {0},    </span>   </span>  </p>  <p style="text-align:right">

فضلاً استخدم الرقم  : <strong>{1}</strong> <br><br>
		 فريق عمل منافسات  </p>  ', N'    <p style="text-align:center">&nbsp;</p>  
	    <p style="text-align:right">   
  <span style="font-family:arial,helvetica,sans-serif;font-size:12px"> 
   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">  
     Dear {0},   </span>   </span>   </p> 
		 <p style="text-align:right">    please use the number : <strong>{1}</strong> <br><br>     

								      <br><br>     EGP Team Monafasat      </p>  ', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
