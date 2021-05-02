update [LookUps].[NotificationOperationCode] set EmailBodyTemplateAr = N'  <p style="text-align:right">        
  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">     
  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">    
 عزيزنا المستخدم،            
  </span>       
  </span>    
  </p>   
  <p style="text-align:right">                    نود ابلاغكم ان المورد {0} قبل التخفيض للمنافسة رقم {1}                 
   </p> ' where notificationoperationcodeid IN (1044, 1023)
go

