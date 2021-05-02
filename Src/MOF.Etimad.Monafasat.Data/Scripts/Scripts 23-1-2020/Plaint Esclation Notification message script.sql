update LookUps.NotificationOperationCode set 

 EmailBodyTemplateAr = N' <p style="text-align:center">&nbsp;</p> <p style="text-align:right">  السلام عليكم  {0},   </p> 
 <p style="text-align:right"> :عزيزينا المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم<strong>{1}</strong> <br><br>  
 <br><br>  فريق عمل منافسات  </p>',
 EmailBodyTemplateEn = N' <p style="text-align:center">&nbsp;</p> <p style="text-align:right">  السلام عليكم  {0},   </p> 
 <p style="text-align:right"> :عزيزينا المستخدم، تم رفض اعتماد قراركم على طلب تظلم الموردين للمنافسة رقم<strong>{1}</strong> <br><br>  
 <br><br>  فريق عمل منافسات  </p>'

where NotificationOperationCodeId = 770

update LookUps.NotificationOperationCode set PanelTemplateAr = N'هنالك طلب تعديل جديد على المرفقات بانتظار الاعتماد للمنافسة رقم:{0}'

where NotificationOperationCodeId = 916