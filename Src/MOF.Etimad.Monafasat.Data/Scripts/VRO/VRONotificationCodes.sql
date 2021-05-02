
--Purchase Specialist Actions (17)--

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (923, N'sendrelatedvrotender', N'انتظار الاعتماد من مسؤل الاعتماد', N'when tender has been sent to etimad officer to approve', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد', N'Dear user, 
The tender has been sent to etimad officer to approve
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been sent to etimad officer to approve: <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم إعتماد المنافسة رقم:  {0}', N'Tender has been sent to etimad officer to approve: {0}')
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (936, N'approvetenderextenddate', N'اعتماد تمديد تواريخ المنافسه', N'when new attachment added to tender', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للمنافسة رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  
     <p style="text-align:right">        
	  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">            

	  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">   
	  السادة {0}                          
	         السلام عليكم ورحمة الله,             </span>         </span>     </p>     <p style="text-align:right">       
			            إعلان تمديد تواريخ منافسة صادر عن {1} – {2} :                  <br><br>   
						      إسم المنافسة: <strong>{3}</strong><br>         رقم المنافسة: {4}<br>   
							        الغرض من المنافسة: <strong>{5}</strong>          <br><br>     
									    آخر موعد لاستلام الاستفسارات: {6}<br>         آخر موعد لاستلام العروض: {7}<br>     
										             تاريخ فتح المظاريف : {8} الساعة: {9}
   <br>          <br><br>                   <br><br>                 وتقبلوا وافر تحياتنا،،          <br><br>         فريق عمل منافسات    </p>', N'    <p style="text-align:center">&nbsp;</p>   
	  <p style="text-align:right">       
	    <span style="font-family:arial,helvetica,sans-serif;font-size:12px">          
		   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">             
	   Dear ,      {0} 
		      </span>         </span>     </p>     <p style="text-align:right">     
	               The follwing tender has been extended {1} – {2} :       
		       <br><br>         Tender Name: <strong>{3}</strong><br>     
	      Tender No.: {4}<br>         Purpose: <strong>{5</strong>      
		    <br><br>         Last Date to accept vendors questions: {6}br>   
			     Closing Date: {7}<br>         Bid Opening Date: {8} Time: {9} <br>    
				      <br><br>                   <br><br>             
					      Thank You,          <br>', N'إعلان تمديد التواريخ  للمنافسة رقم: {0}', N'Dates of tender number {0} has been extended ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (919, N'publishopenenvelopesbackend', N'بدأ فتح المظاريف', N'when open envelops published', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
{0}', N'عزيزنا المستخدم، تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إعتماد تقرير فتح المظاريف للمنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p> ', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Bid opening for following tender has been approved: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat
    </p>
 ', N'تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
{0}', N'تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (937, N'publishfaqanswerbackend', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 37, 0, 1, 1, 0, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
{0}', N'Dear user, There is a reply on a question of tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

 <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
             
                السادة : {0}
                <br>
                
                
               
        </span>
    </p>
    <p style="text-align:right">
        نود تنبيهكم بأنه تم إرسال إجابة على  إستفسار  على  المنافسة<br />
        <br><br>
        إسم المنافسة: <strong>{1}</strong><br>
        رقم المنافسة: {2}<br>
        الغرض من المنافسة: <strong>{3}</strong> 
        <br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>

', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
               
                Dear : {0}
               
                
                Dear,
            </span>
        </span>
    </p>
    <p style="text-align:right">
        An answer has been published for below tender<br />
        <br><br>
        Tender Name: <strong>{1}</strong><br>
        Tender No.: {2}<br>
        Purpose: <strong>{3}</strong> 
        <br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafasat
    </p>', N'تم الرد على إحدى إستفسارات  المنافسة رقم:
{0}
', N' Ther is a reply on question on tender No: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (920, N'approvetenderawarding', N'عندما يتم ترسية المنافسة', N'when tender awarding approved', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0}
', N' Dear user, Tender Awarding has been approved for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم اعتماد الترسية  للمنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Following tender has been awarded <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat
    </p>', N'تم اعتماد الترسية للمنافسة رقم: {0}', N'Tender Awarding has been approved for tender No: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (921, N'removetenderattachment', N'عند حذف المرفقات', N'when tender attachment removed', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
 ، تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {1}', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />
        <strong> إسم المنافسة: </strong>{1}
        <br>
        <strong> رقم المنافسة: </strong>{2}
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>
 

', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
         The Files has been modified for the tender<br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>
', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {0}', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (922, N'approvedtender', N'عند اعتماد المنافسه', N'when tender approved', 37, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N'Dear user, 
The tender has been approved
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إعتماد المنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been approved: <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم إعتماد المنافسة رقم:  {0}', N'Tender has been approved: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (926, N'approvetoq', N'اعتماد جدول الكميات', N'when toq request approved', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم:  {0} ', N'Dear user, Quantities table has been modified for the tender
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم التعديل على جدول الكميات الخاصة بمستندات المنافسة<br />
        <strong> إسم المنافسة: </strong>{1}
        <br>
        <strong> رقم المنافسة: </strong>{2}
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>', N'<p style="text-align:right">
      
        Table of quantities has been modified to following tender

  <br><br>
                
       <strong> Details </strong>
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br>
        Purpose: {3}<br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>

        <br>
                Thank You, 
        <br>
        EGP Team Monafasat
    </p>', N'تم التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم:  {0}', N'Quantities table has been modified for the tender
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (927, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
       ', N'Dear user, We''re sorry to inform you that the tender No: {0} has been canceled', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center"></p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إلغاء المنافسة: <strong>{1}</strong> <br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        The follwing tender has been canceled: <strong>{1}</strong> <br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat   </p>', N'تم  إلغاء المنافسه رقم: {0}', N'Tender with number : {0} has been canceled')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (928, N'rejectopenenvelope', N'رفض فتح العروض', N'when open envelope rejected', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير فتح المظاريف للمنافسة رقم:
{0}', N'Dear user, Envelope opening report has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض تقرير فتح المظاريف: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
          فريق عمل منافسات
    </p>', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Bid opening has been rejected: <strong>{1}</strong> <br><br>
Reasons:
<br><br>
{2}
<br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {3}<br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم رفض تقرير فتح المظاريف للمنافسة رقم: {0}', N'Bid opening has been rejected for tender No: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (929, N'rejecttechnicalevaluation', N'عند رفض التقييم الفني', N'when technical evaluation rejected', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الفحص الفني للمنافسة رقم: {0}', N'Dear user, Technical evalation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
    السلام عليكم  {0},
</p>
<p style="text-align:right">
    نود تنبيهكم أنه تم رفض تقرير التقييم الفني للمنافسة: <strong>{1}</strong>
    <br><br>
    <strong> تفاصيل المنافسة: </strong>          <br>
    الغرض من المنافسة: {2}<br><br>
    آخر موعد لاستلام الاستفسارات: {3}<br>
    آخر موعد لاستلام العروض: {4}<br>
    تاريخ فتح المظاريف : {5} الساعة: {6} <br>
    <br><br>
    فريق عمل منافسات
</p>', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
    Dear  {0},
</p>
<p style="text-align:right">
    Technical Evalation has been rejected for: {1}</strong> <br><br>
    <strong> Details: </strong>
    <br>
    Purpose: {2}<br><br>
    Last Date to accept vendors questions: {3}<br>
    Closing Date: {4}<br>
    Bid Opening Date: {5} Time: {6} <br>
    <br><br>
    EGP Team Monafsat
</p>', N'تم رفض تقرير الفحص الفني للمنافسة رقم: {0}', N'Technical Evalation has been rejected for tender No: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (930, N'rejecttender', N'عند رفض المنافسة', N'when your tender rejected', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض طلب طرح المنافسة رقم: 
{0}', N' Dear user, Tender has been rejected with No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض المنافسة: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
        فريق عمل منافسات </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Tender has been rjected for: <strong>{1}</strong> <br><br>
Reasons:
<br><br>
{2}
<br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {3}<br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        
        
        <br><br>
        EGP Team Monafsat
    </p>', N' رفض طلب طرح المنافسة رقم: 
{0}
', N' Tender has been rejected with No. {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (931, N'rejecttenderawarding', N'عند رفض الترسية', N'when tender awarding rejected', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N' Dear user, Tender Awarding has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم رفض الترسية  للمنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات   </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Tebder Awarding has been rejected for : <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat    </p>', N' تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N'Tender Awarding has been rejected for tender No: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (932, N'rejecttendercancellation', N'عند رفض إلغاء المنافسة', N'when tender cancellation rejected', 37, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم,  تم رفض طلب إلغاء المنافسة رقم:
 {0}
', N'Dear user, Tender cancelation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض عملية إلغاء المنافسة: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Tender Cancelation has been rejected for: <strong>{1}</strong> <br><br>
Reasons:
<br><br>
{2}
<br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {3}<br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        
        
        <br><br>
        EGP Team Monafsat
        </p>', N'تم رفض طلب إلغاء المنافسة رقم:  {0}', N'Tender cancelation has been rejected for tender No:
{0}
')
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (933, N'rejecttoq', N'جدول الكميات مرفوض', N'when toq rejected', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم:  {0} ', N'Dear user, Quantities table modification request has been rejected for the tender
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض تعديل جدول الكميات للمنافسة: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Tabel of quantities has been rejected for : <strong>{1}</strong> <br><br>
Reasons:
<br><br>
{2}
<br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {3}<br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        
        
        <br><br>
        EGP Team Monafsat
    </p>', N'تم رفض طلب إضافة جدول الكميات للمنافسة رقم:
{0}
', N'Table of quantities has been rejected for tender No.
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (934, N'rejecttenderextenddate', N'عند رفض تمديد التواريخ', N'when extend date rejected', 37, 1, 0, 0, 1, 1, N'عزيزنا المستخدم، تم رفض تمديد التواريخ  للمنافسة رقم:  {0}', N'Dear User, Extend Dates has been rejected for tender No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض تمديد تواريخ المنافسة :: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
          فريق عمل منافسات
    </p>', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض تمديد تواريخ المنافسة :: <strong>{1}</strong> <br><br>
للأسباب:
<br><br>
{2}
<br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        
        
        <br><br>
          فريق عمل منافسات
    </p>', N'تم رفض تمديد التواريخ  للمنافسة رقم:  {0}', N'Extend Dates has been rejected for tender No. {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (935, N'joinrequest', N'طلب انضمام', N'Join Request', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اضافة طلب انضمام للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم اضافة طلب انضمام للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم اضافة طلب انضمام للمنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Join Request for following tender has been sent: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم اضافة طلب انضمام للمنافسة رقم: {0}', N'تم اضافة طلب انضمام للمنافسة رقم: {0}')
GO
  INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (925, N'approvetenderattachment', N'في إنتظار اعتماد المرفقات', N'when tender attachment waiting approval', 37, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
 ، تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {1}', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />
        <strong> إسم المنافسة: </strong>{1}
        <br>
        <strong> رقم المنافسة: </strong>{2}
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>
 

', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
         The Files has been modified for the tender<br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>
', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {0}', N'The attachments has been modified for the tender No.: {0}')
GO 

--Etimad officer Actions (16)--

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (924, N'sendrelatedvrotender', N'انتظار الاعتماد من مسؤل الاعتماد', N'when tender has been sent to etimad officer to approve', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد', N'Dear user, 
The tender has been sent to etimad officer to approve
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been sent to etimad officer to approve: <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم إعتماد المنافسة رقم:  {0}', N'Tender has been sent to etimad officer to approve: {0}')
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (909, N'approvedtender', N'عند اعتماد المنافسه', N'when tender approved', 36, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N'Dear user, 
The tender has been approved
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إعتماد المنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been approved: <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم إعتماد المنافسة رقم:  {0}', N'Tender has been approved: {0}')
GO


INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (910, N'tenderwaitingcancelation', N'عند انتظار الغاء المنافسه', N'When tender waiting cancelation', 36, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لمنافسة رقم: 
{0}', N'Dear user, there is a pending cancellation request for tender number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال المنافسة لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        The follwing tender has been sent for canceling: <strong>{1}</strong> <br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat    </p>


', N'
هنالك طلب إلغاء بإنتظار الإعتماد لمنافسة رقم: 
{0}', N'There is a pending cancellation request for tender number: {0}')

GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (911, N'sendforcancelationtoq', N'عند إرسال جدول الكمية للإلغاء', N'when table of quantity send for cancellation', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب إلغاء جدول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}', N'عزيزنا المستخدم، هنالك طلب إلغاء جدول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال جدول كميات المنافسةالمنافسة لإعتماد الإلغاء من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
 ', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
        Table of quantities cancelation is waiting your approval for: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat
    </p>
 ', N'هنالك طلب إلغاء جدول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}', N'هنالك طلب إلغاء جدول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (912, N'sendforapprovaltoq', N'عند ارسال جدول الكميات للاعتماد', N'when Table of quantity send for approval', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب إضافة تعديل على جداول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}', N'Table of quantities for below tender is pending approval
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال جدول كميات المنافسةالمنافسة للإعتماد من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض:{4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Table of quantities is waiting your approval for : <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'عزيزنا المستخدم، هنالك طلب إضافة تعديل على جداول الكميات بانتظار الاعتماد للمنافسة رقم:
{0}', N'Table of quantities for below tender is pending approval
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (913, N'publishtenderfile', N'عند نشر ملف منافسه جديده', N'when new tender file published', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
 ، تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {1}', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'
 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بانه يوجد طلب تعديل (بانتظار الاعتماد) على الملفات الخاصة بمستندات المنافسة <br />
        <strong> إسم المنافسة: </strong>{1}
        <br>
         <strong> رقم المنافسة: </strong>{2}
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
         The Files has been modified for the tender<br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>
', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {0}', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (914, N'approvetenderextenddate', N'اعتماد تمديد تواريخ المنافسه', N'when tender extended', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للمنافسة رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  
     <p style="text-align:right">        
	  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">            

	  <span style="font-family:arial,helvetica,sans-serif;font-size:12px">   
	  السادة {0}                          
	         السلام عليكم ورحمة الله,             </span>         </span>     </p>     <p style="text-align:right">       
			            إعلان تمديد تواريخ منافسة صادر عن {1} – {2} :                  <br><br>   
						      إسم المنافسة: <strong>{3}</strong><br>         رقم المنافسة: {4}<br>   
							        الغرض من المنافسة: <strong>{5}</strong>          <br><br>     
									    آخر موعد لاستلام الاستفسارات: {6}<br>         آخر موعد لاستلام العروض: {7}<br>     
										             تاريخ فتح المظاريف : {8} الساعة: {9}
   <br>          <br><br>                   <br><br>                 وتقبلوا وافر تحياتنا،،          <br><br>         فريق عمل منافسات    </p>', N'    <p style="text-align:center">&nbsp;</p>   
	  <p style="text-align:right">       
	    <span style="font-family:arial,helvetica,sans-serif;font-size:12px">          
		   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">             
	   Dear ,      {0} 
		      </span>         </span>     </p>     <p style="text-align:right">     
	               The follwing tender has been extended {1} – {2} :       
		       <br><br>         Tender Name: <strong>{3}</strong><br>     
	      Tender No.: {4}<br>         Purpose: <strong>{5</strong>      
		    <br><br>         Last Date to accept vendors questions: {6}br>   
			     Closing Date: {7}<br>         Bid Opening Date: {8} Time: {9} <br>    
				      <br><br>                   <br><br>             
					      Thank You,          <br>', N'إعلان تمديد التواريخ  للمنافسة رقم: {0}', N'Dates of tender number {0} has been extended ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (915, N'removetenderattachment', N'عند حذف ملحقات المنافسه', N'when tender attachment removed', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
 ، تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {1}', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N' 

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />
        <strong> إسم المنافسة: </strong>{1}
        <br>
        <strong> رقم المنافسة: </strong>{2}
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>
 

', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
         The Files has been modified for the tender<br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>
', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {0}', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (916, N'approvetenderattachment', N'عند انتظار اعتماد ملحقات المنافسه', N'when tender attachment waiting approval', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل على المرفقات جديد و بانتظار الاعتماد للمنافسة رقم:{0}', N'Dear User: {0}, There is a request to modifiy the attachments needs your approval for tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">

    السلام عليكم ورحمة الله  {0},

</p>
<p style="text-align:right">

    نود تنبيهكم بانه يوجد طلب تعديل (بانتظار الاعتماد) على الملفات الخاصة بمستندات المنافسة <br />
    <strong> إسم المنافسة: </strong>{1}
    <br>
    <strong> رقم المنافسة: </strong>{2}
    <br><br>
    وتقبلوا وافر تحياتنا،،
    <br>
    فريق عمل منافسات
</p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
         There is a request to modifiy the attachments needs your approval for tender <br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2} 
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>', N'هنالك طلب إضافة مرفق جديد و بانتظار الاعتماد للمنافسة رقم:{0}', N'The attachments has been modified for the tender No.: {0}')
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (905, N'approvaloperation', N'إعتماد', N'Approval Operation', 36, 0, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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

								      <br><br>     EGP Team Monafasat      </p>  ', NULL, NULL)
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (939, N'tenderwaitingdateextend', N'بانتظار اعتماد تديد تواريخ', N'when tender waiting date extend', 36, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}', N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأن هنالك طلب تمديد بانتظار الإعتماد {1} <br />
        <strong> للمنافسة: </strong>{2} <br><br>
       <strong> بحيث ستصبح مواعيد المنافسة كالتالي: </strong>
        <br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Extend date for the follwing tender ia waiting your approval {1} <br />
        <strong> Tender: </strong>{2} <br><br>
       <strong> The new date for the tender will be: </strong>
        <br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>
', N'طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}', N'طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (940, N'sendopenenvelopesreportforapproval', N'بانتظار اعتماد فتح العروض', N'when new open envelopes report waiting approval', 36, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال تقرير فتح مظاريف للإعتماد من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل {7}
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        Bid Opening is waiting your approval: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team {7}
    </p>
', N'تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (941, N'submittenderforapproval', N'بانتظار اعتماد المنافسة', N'when new tender waiting approval', 36, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم إرسال المنافسة للإعتماد من قبلكم: <strong>{1}</strong> <br><br>            <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {2}<br><br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  ', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Tender Publishing  is waiting your approval for : <strong>{1}</strong> <br><br>            <strong> Details: </strong>          <br>          Purpose: {2}<br><br>          Last Date to accept vendors questions: {3}<br>          Closing Date: {4}<br>          Bid Opening Date: {5} Time: {6} <br>                              <br><br>          EGP Team Monafsat      </p>  ', N'طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (938, N'publishfaqanswerbackend', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 36, 0, 1, 1, 0, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
{0}', N'Dear user, There is a reply on a question of tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

 <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
             
                السادة : {0}
                <br>
                
                
               
        </span>
    </p>
    <p style="text-align:right">
        نود تنبيهكم بأنه تم إرسال إجابة على  إستفسار  على  المنافسة<br />
        <br><br>
        إسم المنافسة: <strong>{1}</strong><br>
        رقم المنافسة: {2}<br>
        الغرض من المنافسة: <strong>{3}</strong> 
        <br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>

', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
               
                Dear : {0}
               
                
                Dear,
            </span>
        </span>
    </p>
    <p style="text-align:right">
        An answer has been published for below tender<br />
        <br><br>
        Tender Name: <strong>{1}</strong><br>
        Tender No.: {2}<br>
        Purpose: <strong>{3}</strong> 
        <br><br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafasat
    </p>', N'تم الرد على إحدى إستفسارات  المنافسة رقم:
{0}
', N' Ther is a reply on question on tender No: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (917, N'vendorsubmitoffer', N'ارسال عرض ', N'Submit Offer', 36, 0, 1, 1, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
 <p style="text-align:right"> 
   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">  
     <span style="font-family:arial,helvetica,sans-serif;font-size:12px">   
 السلام عليكم {0},    </span>   </span>  </p>  <p style="text-align:right"> 
  نود  إعلامكم انه تم استلام عرض  على المنافسة : <strong>{1}</strong> <br><br> 
	<strong> التفاصيل: </strong>   <br>   {2}<br>   الرقم: {3}<br>   الغرض: {4}<br><br> 
	 آخر موعد لاستلام الاستفسارات: {5}<br>   آخر موعد لاستلام العروض: {6}<br> 
	تاريخ فتح المظاريف : {7} الساعة: {8} <br>   <br><br> 
   <br><br>   شكراً لكم,   <br><br>   فريق عمل {9}  </p>  ', N'    <p style="text-align:center">&nbsp;</p> 
	 <p style="text-align:right">
	 <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
	 <span style="font-family:arial,helvetica,sans-serif;font-size:12px"> 
	 Dear {0},
	 
	  </span>  </span>  </p> <p style="text-align:right"> 
	 an offer has been recived for : <strong>{1}</strong> <br><br>  
	  <strong> Details </strong>   <br>      {2}<br>    No.: {3}<br>   
	   Purpose: {4}<br><br>          Last Date to accept vendors questions: {5}<br> 
	      Closing Date: {6}<br>          Bid Opening Date: {7} Time: {8} <br> <br><br>    
     
		      <br><br>       
			     Thank You,          <br><br>     EGP Team {9}      </p>  ', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (918, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 36, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
       ', N'Dear user, We''re sorry to inform you that the tender No: {0} has been canceled', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center"></p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إلغاء المنافسة: <strong>{1}</strong> <br><br>
       <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        The follwing tender has been canceled: <strong>{1}</strong> <br><br>
       <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafsat    </p>', N'تم  إلغاء المنافسه رقم: {0}', N'Tender with number : {0} has been canceled')
GO

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (908, N'SendOffersPostponeRequest', N'ارسال طلب تأجيل العروض', N'Send Offers Postpone Request', 36, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}', N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}')
GO
