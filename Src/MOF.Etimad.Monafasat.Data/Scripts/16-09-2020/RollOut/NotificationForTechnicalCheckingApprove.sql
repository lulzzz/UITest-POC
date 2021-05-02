

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
						 go