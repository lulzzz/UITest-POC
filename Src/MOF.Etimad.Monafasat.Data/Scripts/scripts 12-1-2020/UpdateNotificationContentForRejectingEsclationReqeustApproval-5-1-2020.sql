

update LookUps.NotificationOperationCode 
set PanelTemplateAr = N'عزيزي المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم: {0}' 
, PanelTemplateEn = N'عزيزي المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم: {0}'
, SmsTemplateAr = N'عزيزي المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم: {0}'
, SmsTemplateEn = N'عزيزي المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم: {0}'
where NotificationOperationCodeId = 770
