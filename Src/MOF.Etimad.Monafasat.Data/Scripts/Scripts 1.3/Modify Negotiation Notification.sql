Go
delete from Notification.Notification where NotificationSettingId in


    (select id from Notification.UserNotificationSetting where NotificationCodeId in (1023, 1044))
delete from Notification.UserNotificationSetting where NotificationCodeId in (1023, 1044);

update LookUps.NotificationOperationCode set UserRoleId = 24  where NotificationOperationCodeId = 1044
update LookUps.NotificationOperationCode set UserRoleId = 8  where NotificationOperationCodeId = 1023
update  CommunicationRequest.NegotiationFirstStageSupplier set NegotiationSupplierStatusId = 10, IsReported = 0, PeriodStartDateTime = GETDATE() where id = 85
Go


  update LookUps.NotificationOperationCode set 
  EmailBodyTemplateAr = 'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  EmailBodyTemplateEn = 'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  SmsTemplateAr  =  'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  SmsTemplateEn = 'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  PanelTemplateAr = 'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).',
  PanelTemplateEn = 'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).'
  
  
  where NotificationOperationCodeId in (1023, 1044)
  go
