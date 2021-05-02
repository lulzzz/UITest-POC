Go
delete from Notification.Notification where NotificationSettingId in


    (select id from Notification.UserNotificationSetting where NotificationCodeId in (1023, 1044))
delete from Notification.UserNotificationSetting where NotificationCodeId in (1023, 1044);

update LookUps.NotificationOperationCode set UserRoleId = 24  where NotificationOperationCodeId = 1044
update LookUps.NotificationOperationCode set UserRoleId = 8  where NotificationOperationCodeId = 1023
update  CommunicationRequest.NegotiationFirstStageSupplier set NegotiationSupplierStatusId = 10, IsReported = 0, PeriodStartDateTime = GETDATE() where id = 85
Go


  update LookUps.NotificationOperationCode set 
  EmailBodyTemplateAr = N' عزيزنا المستخدم تم إعتماد طلب التخفيض للمنافسة رقم {0} ',
  EmailBodyTemplateEn = N'Dear user, the Negotiation request for the competition has been approved : {0}',
  SmsTemplateAr  = N' عزيزنا المستخدم تم إعتماد طلب التخفيض للمنافسة رقم {0} ',
  SmsTemplateEn = N'Dear user, the Negotiation request for the competition has been approved : {0}',
  PanelTemplateAr =N' عزيزنا المستخدم تم إعتماد طلب التخفيض للمنافسة رقم {0} ',
  PanelTemplateEn = N'Dear user, the Negotiation request for the competition has been approved : {0}'
      
  
  where NotificationOperationCodeId in (  738)
  Go

  update LookUps.NotificationOperationCode set 
  EmailBodyTemplateAr = N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  EmailBodyTemplateEn = N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  SmsTemplateAr  =  N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  SmsTemplateEn = N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  PanelTemplateAr = N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  PanelTemplateEn = N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).'
  
  
  where NotificationOperationCodeId in (1023, 1044)
  go
