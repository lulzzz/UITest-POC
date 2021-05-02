
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (3, N'approvetenderextenddate', N'اعتماد تمديد تواريخ المنافسه', N'when new attachment added to tender', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للمنافسة رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (29, N'approvedtender', N'عند اعتماد المنافسه', N'when tender approved', 3, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N'Dear user, 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (32, N'tenderwaitingcancelation', N'عند انتظار الغاء المنافسه', N'When tender waiting cancelation', 3, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (33, N'sendforcancelationtoq', N'عند إرسال جدول الكمية للإلغاء', N'when table of quantity send for cancellation', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب إلغاء جدول الكميات بانتظار الاعتماد للمنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (34, N'sendforapprovaltoq', N'عند ارسال جدول الكميات للاعتماد', N'when Table of quantity send for approval', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب إضافة تعديل على جداول الكميات بانتظار الاعتماد للمنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (38, N'publishtenderfile', N'عند نشر ملف منافسه جديده', N'when new tender file published', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (39, N'approvetenderextenddate', N'اعتماد تمديد تواريخ المنافسه', N'when tender extended', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للمنافسة رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (43, N'removetenderattachment', N'عند حذف ملحقات المنافسه', N'when tender attachment removed', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (45, N'approvetenderattachment', N'عند انتظار اعتماد ملحقات المنافسه', N'when tender attachment waiting approval', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل على المرفقات جديد و بانتظار الاعتماد للمنافسة رقم:{0}', N'Dear User: {0}, There is a request to modifiy the attachments needs your approval for tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (46, N'publishopenenvelopesbackend', N'بدأ فتح المظاريف', N'when open envelops published', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (51, N'approvetenderextenddate', N'اعتماد تمديد تواريخ المنافسه', N'when new attachment added to tender', 4, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للمنافسة رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (52, N'publishopenenvelopes', N'عند نشر تقرير فتح المظاريف', N'when open envelops published', 4, 0, 1, 0, 1, 1, N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم:{0}
', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم:{0}
', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">

    السلام عليكم {0},

</p>
<p style="text-align:right">

    نود تنبيهكم بأنه تمت عملية فتح المظاريف للمنافسة: <strong>
        {1} <br><br>
        يمكنكم الاطلاع على تقرير فتح المظاريف من خلال تسجيل الدخول إلى حسابكم في الموقع
        <br>
        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات:{3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5}  <br>


        <br><br>
        فريق عمل منافسات
</p>', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">

    Dear  {0},

</p>
<p style="text-align:right">

    Bid opening has been published: <strong>{1}</strong> <br><br>
    You can login into your account to see the details
    <br>
    <strong> Details: </strong>
    <br>
    Purpose: {2}<br><br>
    Last Date to accept vendors questions: {3}<br>
    Closing Date: {4}<br>
    Bid Opening Date: {5} <br>


    <br><br>
    EGP Team Monafsat
</p>', N'نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم:{0}
', N'نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم:{0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (53, N'publishtechnicalevaluation', N'تم اعتماد الفحص الفنى', N'Technical check has been approved', 4, 0, 1, 0, 1, 1, N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم قبول تقرير العرض الفني للمنافسة: <strong>{1}</strong> <br><br>

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
      
        Technical Evaluation for following tender has been approved: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>
', N'تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'تم قبول تقرير العرض الفني للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (54, N'vendorawardingtender', N'عند ترسية منافسة على المورد', N'When tender awarded to supplier', 4, 0, 1, 0, 1, 4, N'عميلنا العزيز، يسعدنا إبلاغكم أنه تمت الترسية عليكم للمنافسة رقم:  {0}', N'عميلنا العزيز، يسعدنا إبلاغكم أنه تمت الترسية عليكم للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم تمت الترسية عليكم  للمنافسة: <strong>{1}</strong> <br><br>

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
      
        You have been awared the tender: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>
', N'تمت الترسية عليكم للمنافسة رقم:  {0}', N'تمت الترسية عليكم للمنافسة رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (55, N'invitevendorstotender', N'عند إستقبال دعوة', N'when vendor recieve a new invitation', 4, 0, 1, 0, 1, 1, N'	عميلنا العزيز، {0} ،لقد تمت دعوتكم للمشاركة في المنافسة : {1}', N'Dear customer {0}, You have been invited to participate in the competition: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السادة : {0}
              
    </p>
    <p style="text-align:right">
        
        تدعوكم {1}– {2}، لتقديم عرضكم للمنافسة التالية:
        
        <br><br>
        إسم المنافسة: <strong>{3}</strong><br>
        رقم المنافسة: {4}<br>
        الغرض من المنافسة: <strong>{5}</strong> 
        <br><br>
        آخر موعد لاستلام الاستفسارات: {6}<br>
        آخر موعد لاستلام العروض:{7}<br>
        تاريخ فتح المظاريف : {8}الساعة: {9} <br>
   
        
        <br><br>
                وتقبلوا وافر تحياتنا،، 
        <br><br>
        فريق عمل منافسات   </p>


', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
	  {1} – {2} is inviting you for below tender:
        <br><br>
        Tender Name: <strong>{3}</strong><br>
        Tender No.: {4}<br>
        Purpose: <strong>{5}</strong> 
        <br><br>
        Last Date to accept vendors questions: {6}<br>
        Closing Date: {7}<br>
        Bid Opening Date: {8} Time: {9} <br>
        <br><br> 
                Thank You, 
        <br><br>
        EGP Team Monafasat
    </p>', N'لقد تمت دعوتكم للمشاركة في المنافسة  رقم : {0}.', N'You have been invited to participate Tender number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (56, N'addtenderattachment', N'عندما ياتم ارفاق ملف لمنافسة', N'when new attachment added to tender', 4, 0, 1, 0, 1, 1, N'عزيزنا المستخدم :{0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (100, N'unblockvendorbecauseofcrnotices', N'إالغاء منع المورد بسبب ملاحظة السجل التجارى', N'un-block vendor because of cr notices', 4, 0, 1, 0, 1, 1, N' يسرنااعلامكم انه تم اعادة تفعيل حسابكم المسجل في نظام منافسات
', N' يسرنااعلامكم انه تم اعادة تفعيل حسابكم المسجل في نظام منافسات
', N'تنبيهات منافسات', N'Monafasat Notification', N'
<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
    <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            السلام عليكم {0},
        </span>
    </span>
</p>
<p style="text-align:right">
    يسرنااعلامكم انه تم اعادة تفعيل حسابكم المسجل في منافسات.
    <br><br>
    يمكنكم تسجيل الدخول للموقع من خلال
    <br>
    <a href="https://monafasat.etimad.sa/users/login/" target="_blank">https://monafasat.etimad.sa/users/login/</a>
    <br>
    بإستخدام رقم السجل التجاري وكلمة المرور.
    <br>
    شكراً لكم,
    <br><br>
    فريق عمل منافسات
</p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                Dear {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
        Your account has been activated on Monafasat.
        <br><br>
       You can login using below link 
        <br>
        <a href="https://monafasat.etimad.sa/users/login/" target="_blank">https://monafasat.etimad.sa/users/login/</a>
        <br>
        using your username and password
        <br>
        Thank You,
        <br><br>
        EGP Team Monafasat
    </p>
', N' يسرنااعلامكم انه تم اعادة تفعيل حسابكم المسجل في نظام منافسات', N' يسرنااعلامكم انه تم اعادة تفعيل حسابكم المسجل في نظام منافسات')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (101, N'approvaloperation', N'إعتماد', N'Approval Operation', 3, 0, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (114, N'canceltenderapproved', N'الغاء نشر منافسة', N'When tender canceled', 4, 0, 1, 0, 1, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}', N'Dear user, We''re sorry to inform you that the tender No: {0} has been canceled', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center"></p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (116, N'rejectvendorrequest', N'رفض طلب انضمام', N'When vendor requested invitation rejeceted', 4, 0, 1, 0, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم رفض طلب انضمامكم للمنافسة رقم: {0}', N'Your request for invitation has been rejected for the tender:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">
        <span style="font-size:14px">
            <strong>
                منافسات
            </strong>
        </span>
    </p>
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                السلام عليكم {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
نود إعلامكم انه تم رفض طلب الانضمام للمنافسة: <strong>{1}</strong> <br><br> 
        <strong> التفاصيل: </strong>
        <br>
         
        رقم المنافسة: {2}<br>
        سبب الرفض: {3}<br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        <br><br>
        شكراً لكم,
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">
        <span style="font-size:14px">
            <strong>
                Monafasat
            </strong>
        </span>
    </p>
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                Dear {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
        Your request for invitation has been rejectedة: <strong>{1}</strong> <br><br>
        <strong> Details </strong>
        <br>
        Tender No.: {2}<br>
        Rejection Reason : {3}<br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        <br><br>
        Thank You,
        <br><br>
        EGP Team Monafasat
    </p>
', N'تم رفض طلب انضمامكم للمنافسة رقم: {0}', N'Your request for invitation has been rejected for the tender:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (129, N'sadadgeneratedbill', N'عندما يكون هناك فاتورة جديد', N'when new bill generated', 4, 0, 1, 1, 1, 1, N'عميلنا العزيز، تم إصدار فاتورة شراء المنافسة على سداد على الرقم: 
{0}', N'SADAD Bill has been generated with No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        السلام عليكم ورحمة الله وبركاته،، {0}
    </p>
    <p style="text-align:right">
        يسعدنا إعلامكم انه تم إصدار فاتورة شراء للمنافسة التالية :
        <br>
        <i>{1}</i>
        <br>
        ويمكنكم دفع الفاتورة من خلال خدمة سداد (الخدمات الحكومية / بوابة المشتريات الحكومية)  على الرقم 
        <br>
		{2}
        
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
		The bill number has been genrated for 
        <br>
        <i>{1}</i>
        <br>
		You can pay this through SADAD channels at any Saudi Bank using the Biller Government E-procurement Portal (Code 141).
        <br>
        {2}
        
        <br><br>
        Thank You, 
        <br>
        EGP Team Monafasat
    </p>
', N'عميلنا العزيز، تم إصدار فاتورة شراء المنافسة على سداد على الرقم: 
{0}', N'SADAD Bill has been generated with No. {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (131, N'approvetoq', N'قبول طلب تعديل جدول كميات', N'when toq request approved', 4, 0, 1, 0, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم: {0}', N'
Dear user, Quantities table has been modified for the tender
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم ورحمة الله  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم التعديل على جدول الكميات الخاصة بمستندات المنافسة
<br />
        و الغاء عرضكم المقدم عليها. لذلك نرجو الدخول على حسابكم و إرسال العرض مرة أخرى
  <br><br>
                
       <strong> التفاصيل: </strong>
        <strong> إسم المنافسة: </strong>{1} 
        <br>
        <strong> رقم المنافسة: </strong>{2} 
        <br>
        الغرض: {3}<br><br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>

        <br>
                وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل  Monafasat
    </p>
', N'<p style="text-align:right">
      
        Table of quantities has been modified to following tender
<br />
        and your offer has been removed, so you have to re-submit your offer again
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (132, N'tenderwaitingdateextend', N'بانتظار اعتماد تديد تواريخ', N'when tender waiting date extend', 3, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}', N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (133, N'sendopenenvelopesreportforapproval', N'بانتظار اعتماد فتح العروض', N'when new open envelopes report waiting approval', 3, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (136, N'submittenderforapproval', N'بانتظار اعتماد المنافسة', N'when new tender waiting approval', 3, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم إرسال المنافسة للإعتماد من قبلكم: <strong>{1}</strong> <br><br>            <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {2}<br><br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  ', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Tender Publishing  is waiting your approval for : <strong>{1}</strong> <br><br>            <strong> Details: </strong>          <br>          Purpose: {2}<br><br>          Last Date to accept vendors questions: {3}<br>          Closing Date: {4}<br>          Bid Opening Date: {5} Time: {6} <br>                              <br><br>          EGP Team Monafsat      </p>  ', N'طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}', N'طلب طرح بانتظار الاعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (137, N'publishfaqanswerbackend', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 3, 0, 1, 1, 0, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (138, N'vendoraskquestion', N'تم اضافة استفسار', N'vendor ask question', 9, 0, 1, 0, 1, 2, N'عزيزنا المستخدم, هنالك استفسار من أحد الموردين على المنافسة رقم:
{0}', N'Dear user ,There is a question on tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">

    السلام عليكم ورحمة الله  {0},

</p>
<p style="text-align:right">

    نود تنبيهكم بأنه تم إرسال إستفسار  على  المنافسة<br />
    <strong> إسم المنافسة: </strong>{1}
    <br>
    <strong> رقم المنافسة: </strong>{2}
    <br>
    <strong> نص السؤال </strong>:
<p>{3}</p>
<br><br>
                وتقبلوا وافر تحياتنا،،
<br>
<p style="text-align:right">
    فريق عمل منافسات
</p>', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
        There are questions for below tender<br />
        <strong> Tender Name: </strong>{1} 
        <br>
        <strong> Tender No.: </strong>{2}
        <br>
        <strong> Question </strong>:
        <p>{3}</p>
        <br><br>
                Thank You, 
        <br>
        EGP Team Monafsat
    </p>', N'هنالك استفسار من أحد الموردين على المنافسة رقم:
{0}
', N'There is a question on tender No: {0')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (139, N'publishfaqanswerbackend', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 9, 0, 1, 0, 1, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (140, N'changetendergatid', N'تم ضم جهة فنية', N'when technical office changed', 9, 0, 1, 0, 1, 2, N'عزيزنا المستخدم, تم تحديث الجهة الفنية للمنافسة رقم : {0}', N'Dear user, You have been invited to reply on questions of  tender {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:right">
    <strong>{0}</strong> <br><br>
    
    <br>
    تتم دعوة الجهة الفنية :{1}<br>
    للاجابة على استفسار المورد    
    <br><br>
    رقم المنافسة: {2}<br>
    الجهة الحكومية: {3} ( {4})<br>
    آخر موعد لاستلام الاستفسارات: {5}<br>
    آخر موعد لاستلام العروض: {6}<br>
    تاريخ فتح المظاريف: {7} الساعة: {8} <br><br>
    للاطلاع على تفاصيل المنافسة <a href="{9}"> انقر هنا </a>  <br>        
</p>
', N'
<p style="text-align:right">
    <strong>{0}</strong> <br><br>
    
    <br>
     Technical department has been invited {1}<br>
    to answer vendors questions 
    <br><br>
    Tender No.: {2}<br>
    Governmnet Agency:{3} ( {4} )<br>
    Last Date to accept vendors questions: {5}<br>
    Closing Date: {6}<br>
    Bid Opening Date: {7} Time: {8}<br><br>
    for more Details: <a href="{9}">Click here</a><br>        
</p>
', N'هناك جهة فنية تابعة لك تم دعوتها للرد على إحدى إستفسارات المنافسة رقم : {0}', N'Technical Commitee have been invited to reply on questions of  tender {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (141, N'publishfaqanswerbackend', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 1, 0, 1, 1, 0, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (142, N'publishfaqanswer', N'تم الرد على استفسار على منافسة', N'when new faq answer published', 4, 0, 1, 1, 0, 2, N'عميلنا العزيز، تم الرد على إحدي الاستفسارات على المنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (143, N'deletevendoroffer', N'عندما يتم سحب العرض', N'when your offer withdrawn', 4, 0, 1, 0, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم سحب العرض المقدم من طرفكم على المنافسة رقم {0}', N'The offer has been withdrawn for the tender   {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                السلام عليكم {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
        نود  إعلامكم انه تم سحب العرض المقدم على المنافسة : <strong>{1}</strong> <br><br>
       <strong> التفاصيل: </strong>
        <br>
        {2}<br>
        الرقم: {3}<br>
        الغرض: {4}<br><br>
        آخر موعد لاستلام الاستفسارات: {5}<br>
        آخر موعد لاستلام العروض: {6}<br>
        تاريخ فتح المظاريف : {7} الساعة: {8} <br>
        
        <br><br>
        شكراً لكم,
        <br><br>
        فريق عمل منافسات
    </p>', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                Dear {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
        The offer for the following teneder has been withdrawn : <strong>{1}</strong> <br><br>
       <strong> Details </strong>
        <br>
        {2}<br>
        No.: {3}<br>
        Purpose: {4}<br><br>
        Last Date to accept vendors questions: {5}<br>
        Closing Date: {6}<br>
        Bid Opening Date: {7} Time: {8} <br>
        
        <br><br>
        Thank You,
        <br><br>
        EGP Team Monafasat

    </p>
', N'تم سحب العرض المقدم من طرفكم على المنافسة رقم {0}', N'The offer has been withdrawn for the tender   {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (144, N'acceptvendoroffer', N'تم قبول عرض مورد', N'when your offer accepted', 4, 0, 1, 1, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم استلام العرض المقدم من طرفكم على المنافسة رقم:{0}', N'Your offer has been recived for the tender:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	<span style="font-family:arial,helvetica,sans-serif;font-size:12px">
		<span style="font-family:arial,helvetica,sans-serif;font-size:12px">
			السلام عليكم {0},
		</span>
	</span>
</p>
<p style="text-align:right">
	نود  إعلامكم انه تم استلام العرض المقدم على المنافسة : <strong>{1}</strong> <br><br>
   <strong> التفاصيل: </strong>
	<br>
	{2}<br>
	الرقم: {3}<br>
	الغرض: {4}<br><br>
	آخر موعد لاستلام الاستفسارات: {5}<br>
	آخر موعد لاستلام العروض: {6}<br>
	تاريخ فتح المظاريف : {7} الساعة: {8} <br>
	<br><br>
<p style="text-align: center;font-weight: bold">
		نود لفت انتباهكم أنه تم ارسال العرض الالكتروني للجهة, يسلم أصل الضمان الابتدائي في ظرف مختوم قبل انتهاء المدة المحددة لتقديم العروض
</p>
	<br><br>
	شكراً لكم,
	<br><br>
	فريق عمل {9}
</p>
', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                Dear {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
        Your offer has been recived for the tender: <strong>{1}</strong> <br><br>
       <strong> Details </strong>
        <br>
        {2}<br>
        No.: {3}<br>
        Purpose: {4}<br><br>
        Last Date to accept vendors questions: {5}<br>
        Closing Date: {6}<br>
        Bid Opening Date: {7} Time: {8} <br>
        <br><br>
    <p style="text-align: center;font-weight: bold">
            Your electronic offer will be submitted to the government agency but you have to send the original to the address mentioned in the tender
    </p>
        <br><br>
        Thank You,
        <br><br>
			EGP Team {9}
    </p>
', N'تم استلام العرض المقدم من طرفكم على المنافسة رقم:{0}', N'Your offer has been recived for the tender:{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (149, N'acceptvendorrequest', N'تم قبول طلب المورد', N'when request accepted', 4, 0, 1, 0, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم الموافقة على قبول دعوة انضمامكم للمنافسة رقم: {0}', N'Your request for invitation has been approved: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">
        <span style="font-size:14px">
            <strong>
                منافسات
            </strong>
        </span>
    </p>
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                السلام عليكم {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
نود إعلامكم انه تم الموافقة على طلب الدعوة على المنافسة : <strong>{1}</strong> <br><br>
        <strong> التفاصيل: </strong>
        <br>
        {2}<br>
        الرقم: {3}<br>
        آخر موعد لاستلام الاستفسارات: {4}<br>
        آخر موعد لاستلام العروض: {5}<br>
        تاريخ فتح المظاريف : {6} الساعة: {7} <br>
        <br><br>
        شكراً لكم,
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">
        <span style="font-size:14px">
            <strong>
                Monafasat
            </strong>
        </span>
    </p>
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
            <span style="font-family:arial,helvetica,sans-serif;font-size:12px">
                Dear {0},
            </span>
        </span>
    </p>
    <p style="text-align:right">
		Your request for invitation has been approved: <strong>{1}</strong> <br><br>
        <strong> Details </strong>
        <br>
        {2}<br>
        No.: {3}<br>
        Last Date to accept vendors questions: {4}<br>
        Closing Date: {5}<br>
        Bid Opening Date: {6} Time: {7} <br>
        <br><br>
        Thank You,
        <br><br>
        EGP Team Monafasat
    </p>
', N'تم الموافقة على قبول دعوة انضمامكم للمنافسة رقم: {0}', N'Your request for invitation has been approved:{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (155, N'sadadpaymentnotification', N'دفع فاتورة سداد', N'Sadad Payment ', 4, 0, 1, 1, 0, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم استلام قيمة فاتورة شراء المنافسة رقم:
{0}', N'Your payement has been recived for tender :{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        السلام عليكم ورحمة الله وبركاته،،{0},
    </p>
    <p style="text-align:right">
        يسعدنا إعلامكم انه تم إستلام قيمة فاتورة شراء المنافسة التالية:
        <br>
        <i>{1}</i>
        <br>
        ويمكنكم الوصول لتفاصيل المنافسة من خلال تسجيل الدخول للموقع من خلال. 
        <br>
        <br>
        بإستخدام رقم السجل التجاري وكلمة المرور.

        
        <br><br>
        وتقبلوا وافر تحياتنا،، 
        <br>
        فريق عمل منافسات
    </p>', N'


    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        Dear {0},
    </p>
    <p style="text-align:right">
		We have recived the payment for the tender :
        <br>
        <i>{1}</i>
        <br>
		and you access the details of the tender by login to your account on 
        <br>
        <a href="https://monafasat.etimad.sa/users/login/" target="_blank">https://monafasat.etimad.sa/users/login/</a>
        <br>
		using your username and password

        
        <br><br>
        Thank You, 
        <br>
        EGP Team Monafasat
    </p>
', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام قيمة فاتورة شراء المنافسة رقم:
{0}', N'Your payement has been recived for tender :{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (158, N'vendorsubmitoffer', N'ارسال عرض ', N'Submit Offer', 3, 0, 1, 1, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (159, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 5, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (160, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 6, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (162, N'publishopenenvelopesbackend', N'بدأ فتح المظاريف', N'when open envelops published', 5, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد تقرير فتح المظاريف للمنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (164, N'approvetenderawarding', N'اعتماد ترسية منافسة', N'when tender awarding approved', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (165, N'approvetenderawarding', N'عندما يتم ترسية المنافسة', N'when tender awarding approved', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (171, N'removetenderattachment', N'عند حذف المرفقات', N'when tender attachment removed', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (175, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 3, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (176, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (178, N'publishopenenvelopesbackend', N'بدأ فتح المظاريف', N'when open envelops published', 6, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم  {0},              </p>     <p style="text-align:right">                نود تنبيهكم بأنه تم إعتماد تقرير فتح المظاريف للمنافسة: <strong>{1}</strong> <br><br>          <strong> تفاصيل المنافسة: </strong>         <br>         الغرض من المنافسة: {2}<br><br>         آخر موعد لاستلام الاستفسارات: {3}<br>         آخر موعد لاستلام العروض: {4}<br>         تاريخ فتح المظاريف : {5} الساعة: {6} <br>                           <br><br>         فريق عمل منافسات     </p>', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          Dear  {0},              </p>     <p style="text-align:right">                Bid opening for following tender has been approved: <strong>{1}</strong> <br><br>          <strong> Details: </strong>         <br>         Purpose: {2}<br><br>         Last Date to accept vendors questions: {3}<br>         Closing Date: {4}<br>         Bid Opening Date: {5} Time: {6} <br>                           <br><br>         EGP Team Monafsat     </p>', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (183, N'rejecttechnicalevaluation', N'رفض فحص العروض', N'Reject Technical Evaluation', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الفحص الفني للمنافسة رقم: {0}', N'Dear user, Technical evalation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (259, N'approvedtender', N'عند اعتماد المنافسه', N'when tender approved', 1, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N'Dear user, 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (263, N'approvetenderattachment', N'في إنتظار اعتماد المرفقات', N'when tender attachment waiting approval', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (265, N'approvetoq', N'اعتماد جدول الكميات', N'when toq request approved', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم:  {0} ', N'Dear user, Quantities table has been modified for the tender
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (266, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (268, N'rejectopenenvelope', N'رفض فتح العروض', N'when open envelope rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير فتح المظاريف للمنافسة رقم:
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (269, N'rejecttechnicalevaluation', N'عند رفض التقييم الفني', N'when technical evaluation rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الفحص الفني للمنافسة رقم: {0}', N'Dear user, Technical evalation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (271, N'rejecttender', N'عند رفض المنافسة', N'when your tender rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض طلب طرح المنافسة رقم: 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (272, N'rejecttenderawarding', N'عند رفض الترسية', N'when tender awarding rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (273, N'rejecttendercancellation', N'عند رفض إلغاء المنافسة', N'when tender cancellation rejected', 1, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (274, N'rejecttoq', N'جدول الكميات مرفوض', N'when toq rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم:  {0} ', N'Dear user, Quantities table modification request has been rejected for the tender
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (278, N'canceltender', N'عند إلغاء المنافسة', N'When tender canceled', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (284, N'rejecttenderawarding', N'عند رفض الترسية', N'when tender awarding rejected', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم:{0}', N' Dear user, Tender Awarding has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (285, N'sendopenenvelopesreportforapproval', N'ارسال تقرير فتح المظاريف للاعتماد', N'send Open Envelopes Report for Approval', 5, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'      <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم إرسال تقرير فتح مظاريف للإعتماد من قبلكم: <strong>{1}</strong> <br><br>            <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {2}<br><br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  ', N'      <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Bid Opening is waiting your approval: <strong>{1}</strong> <br><br>            <strong> Details: </strong>          <br>          Purpose: {2}<br><br>          Last Date to accept vendors questions: {3}<br>          Closing Date: {4}<br>          Bid Opening Date: {5} Time: {6} <br>                              <br><br>          EGP Team Monafsat      </p>  ', N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}', N'عزيزنا المستخدم، تم إرسال تقرير فتح المظاريف للاعتماد للمنافسة رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (287, N'sendtechnicalevaluationreportforapproval', N'عند إرسال تقرير فحص العروض للإعتماد', N'When new technical evaluation report waiting approval', 7, 0, 1, 0, 1, 1, N':عزيزنا المستخدم، تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم: {0}', N'Dear user, Technical evaluation is waiting your appoval for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال تقرير التقييم الفني لاعتماده من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N' <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear {0},
        
    </p>
    <p style="text-align:right">
      
        Technical evaluation is waiting your appoval: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم :  {0}', N'Technical evaluation is waiting your appoval for tender No: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (288, N'tenderawardingwaitingapproval', N'إنتظار اعتماد الترسية ', N'when tender awarding need approval', 7, 0, 1, 1, 0, 4, N'عزيزنا المستخدم، تم ارسال تقرير الترسية للاعتماد للمنافسة رقم: {0}
', N'Dear user, tender awarding sent for approval for tender number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال تقرير الترسية لاعتماده من قبلكم: <strong>{1}</strong> <br><br>

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
      
        Tender Awarding is waiting your approval: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم ارسال تقرير الترسية للاعتماد للمنافسة رقم: {0}
', N'Tender awarding sent for approval for tender number: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (289, N'approvaloperation', N'عملية تحتاج اعتماد', N'when operations need approval', 5, 0, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل:
{0}', N'hello, please use the following number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (290, N'approvaloperation', N'عملية تحتاج اعتماد', N'when operations need approval', 7, 0, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل:
{0}', N'hello, please use the following number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (297, N'publishtechnicalevaluation', N'عند نشر التقييم الفني', N'when publish technical evaluation', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم:{0}', N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم قبول تقرير العرض الفني للمنافسة: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6}<br>
        
        
        <br><br>
        فريق عمل منافسات   </p>', N'    <p style="text-align:center">&nbsp;</p>
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
        EGP Team Monafsat   </p>', N'تم قبول تقرير العرض الفني للمنافسة رقم:{0}', N'Technical Offer Report Approved for Tender Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (302, N'rejectopenenvelope', N'رفض فتح المظاريف', N'when open Envelope Rejected', 6, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير فتح المظاريف للمنافسة رقم: {0}', N'Dear user, Envelope opening report has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (303, N'rejecttendercancellation', N'عند رفض إلغاء المنافسة', N'when tender cancellation rejected', 8, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (317, N'rejecttendercancellation', N'عند رفض إلغاء المنافسة', N'when tender cancellation rejected', 6, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (320, N'approvetoqwithoutoffer', N'قبول طلب تعديل جدول كميات بدون تقديم عرض', N'approve toq request without offer', 4, 0, 1, 0, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم التعديل على جدول الكميات الخاصة بمستندات المنافسة رقم: {0}', N'
Dear user, Quantities table has been modified for the tender
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
<br />
        and your offer has been removed, so you have to re-submit your offer again
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (321, N'ApproveExtendOffers', N'اعتماد طلب تمديد سريان العروض', N'Approve Extend Offers', 4, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (500, N'tenderwaitingcancelation', N'عند انتظار الغاء المنافسه', N'When tender waiting cancelation', 5, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (501, N'tenderwaitingcancelation', N'عند انتظار الغاء المنافسه', N'When tender waiting cancelation', 7, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (502, N'techEvaluationAction', N'ألتقييم الفنى بحاجه لاعتماد', N'When new technical evaluation report waiting approval', 7, 0, 1, 1, 0, 5, N' عزيزنا المستخدم، تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم: {0}
', N' عزيزنا المستخدم، تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم: {0}
', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0}
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال تقرير التقييم الفني لاعتماده من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N' <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0}
        
    </p>
    <p style="text-align:right">
      
        Technical evaluation is waiting your appoval: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>

', N' عزيزنا المستخدم، تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم: {0}
', N' عزيزنا المستخدم، تم إرسال تقرير الفحص الفني للاعتماد للمنافسة رقم: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (503, N'publishopenenvelopesbackend', N'بدأ فتح المظاريف', N'when open envelops published', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم  {0},              </p>     <p style="text-align:right">                نود تنبيهكم بأنه تم إعتماد تقرير فتح المظاريف للمنافسة: <strong>{1}</strong> <br><br>          <strong> تفاصيل المنافسة: </strong>         <br>         الغرض من المنافسة: {2}<br><br>         آخر موعد لاستلام الاستفسارات: {3}<br>         آخر موعد لاستلام العروض: {4}<br>         تاريخ فتح المظاريف : {5} الساعة: {6} <br>                           <br><br>         فريق عمل منافسات     </p>', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          Dear  {0},              </p>     <p style="text-align:right">                Bid opening for following tender has been approved: <strong>{1}</strong> <br><br>          <strong> Details: </strong>         <br>         Purpose: {2}<br><br>         Last Date to accept vendors questions: {3}<br>         Closing Date: {4}<br>         Bid Opening Date: {5} Time: {6} <br>                           <br><br>         EGP Team Monafsat     </p>', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}', N'عزيزنا المستخدم، نحيطكم علماً أنه تمت عملية فتح المظاريف للمنافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (510, N'rejecttenderextenddate', N'عند رفض تمديد التواريخ', N'when extend date rejected', 1, 1, 0, 0, 1, 1, N'عزيزنا المستخدم، تم رفض تمديد التواريخ  للمنافسة رقم:  {0}', N'Dear User, Extend Dates has been rejected for tender No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (666, N'joinrequest', N'طلب انضمام', N'Join Request', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اضافة طلب انضمام للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم اضافة طلب انضمام للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (670, N'submitPrequalificationforapproval', N'بانتظار اعتماد دعوة التأهيل', N'when new PreQualification waiting approval', 3, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لدعوة التأهيل رقم: {0}', N'Dear  User,  PreQualification Publishing  is waiting your approval  Rererence Number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
             <p style="text-align:center">&nbsp;</p>     
 <p style="text-align:right">                        
 عزيزنا المستخدم، هنالك طلب إعلان دعوة تأهيل بانتظار الاعتماد لدعوة التأهيل رقم:       
</n>
{0}. 
 </p>  
<p>    
فريق عمل منافسات    
  </p>  ', N'   <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">                  PreQualification Publishing  is waiting your approval  Rererence Number: <strong>{0}</strong>                              <br><br>          EGP Team Monafsat      </p>  ', N'هنالك طلب طرح بانتظار الاعتماد لدعوة تأهيل رقم: {0}', N'PreQualification Publishing  is waiting your approval  Rererence Number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (675, N'approvedprequalification', N'  عند اعتماد تأهيل', N'when Approve Prequalification', 1, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد طلب إعلان دعوة تأهيل رقم::  {0}', N'Dear user, 
The tender has been approved
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
               {0} عزيزنا المستخدم، تم  اعتماد طلب إعلان دعوة تأهيل رقم:  
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
       Prequalification Request with Reference Number {0} has been approved: <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N' تم اعتماد طلب إعلان دعوة تأهيل رقم::  {0}', N'PreQualification has been approved: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (680, N'RejectPreQualification', N'عند  رفض تأهيل', N'when Reject Prequalification', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض طلب إعلان دعوة تأهيل رقم:: {0}', N'Dear user, 
The tender has been Rejected 
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
               {0} عزيزنا المستخدم، تم  رفض طلب إعلان دعوة تأهيل رقم:  
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
       Prequalification Request with Reference Number {0} has been Rejected : <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N' تم  رفض طلب إعلان دعوة تأهيل رقم::  {0}', N'PreQualification has been  Rejected {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (681, N'sendprequalificationdocs', N'ارسال وثائق التاهيل', N'when sending pre qualification docs', 4, 0, 1, 0, 1, 4, N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل لدعوه التاهيل رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (682, N'sendprequalificationdocs', N'عند ارسال وثائق التاهيل ', N'when sending pre qualification docs', 1, 0, 1, 0, 1, 1, N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}', N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}', N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}', N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}', N'عميلنا العزيز. نحيطكم علما انه تم استلام وثائق التاهيل المقدم من طرفكم على دعوه التاهيل رقم:{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (683, N'rejectPrequalificationCheck', N'عند رفض التأهيل', N'When Reject Pre qualification', 30, 0, 1, 0, 1, 14, N'عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0}', N'عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
     عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0} : <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
     عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0} : <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N'عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0}', N'عزيزنا المستخدم، تم رفض اعتماد نتائج تقييم وثائق التأهيل لطلب التأهيل رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (684, N'returnedtothegovernmentagencyforedit', N'معادة للجهة الحكومية للتعديل', N'returned to the government agency for edit', 1, 1, 0, 0, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تعديل لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، هناك طلب تعديل لمنافسة رقم: {0}', N'عزيزنا المستخدم، هناك طلب تعديل لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (685, N'returnedtothegovernmentagencyforedit', N'معادة للجهة الحكومية للتعديل', N'returned to the government agency for edit', 3, 1, 0, 0, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تعديل لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، هناك طلب تعديل لمنافسة رقم: {0}', N'عزيزنا المستخدم، هناك طلب تعديل لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (686, N'sendToUnitManagerToApprove', N'مرسلة الى مدير الوحدة للاعتماد', N'send To Unit Manager To Approve', 20, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، هنالك طلب اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب اعتماد لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، هناك طلب اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هناك طلب اعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (687, N'approveTenderByUnit', N'تم الموافقة من قبل الوحدة', N'approved by unit', 1, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0}', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (688, N'approveTenderByUnit', N'تم الموافقة من قبل الوحدة', N'approved by unit', 3, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0}', N'عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (689, N'rejectTenderByUnit', N'تم رفض المنافسة من قبل الوحدة', N'reject Tender By Unit', 1, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (690, N'rejectTenderByUnit', N'تم رفض المنافسة من قبل الوحدة', N'reject Tender By Unit', 3, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل الوحدة')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (691, N'rejectTenderFromUnitManager', N'تم رفض المنافسة من قبل مدير الوحدة', N'reject Tender By Unit Manager', 21, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (692, N'rejectTenderFromUnitManager', N'تم رفض المنافسة من قبل مدير الوحدة', N'reject Tender By Unit Manager', 22, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة', N'عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مدير الوحدة')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (693, N'reviewTender', N'مراجعة المنافسة  الوحدة المستوى الثاني', N'review tender unit level 2', 22, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (694, N'approvecheckprequalification', N'مراجعة المنافسة  الوحدة المستوى الثاني', N'review tender unit level 2', 4, 1, 1, 0, 0, 14, N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : 
نتيجة التأهيل : {1}', N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : 
نتيجة التأهيل : {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (695, N'rejectcheckprequalification', N'رفض التأهيل', N'rejectcheckprequalification', 4, 1, 1, 0, 0, 14, N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : ', N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">  عميلنا العزيز,              </p>      <p style="text-align:right">   نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : 
نتيجة التأهيل : {1} سبب الرفض : {2} : 
<br><br>                     <br><br>        فريق عمل منافسات      </p> ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">  عميلنا العزيز,              </p>      <p style="text-align:right">   نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : 
نتيجة التأهيل : {1} سبب الرفض : {2} : 
<br><br>                     <br><br>        فريق عمل منافسات      </p> ', N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : ', N'عميلنا العزيز, نحيطكم علماً أنه تم الإنتهاء من تقييم وثائق التأهيل لدعوة التأهيل ({0}) : ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (696, N'approvecheckprequalificationsecratary', N'اعتماد التأهيل', N'approvecheckprequalificationsecratary', 8, 1, 1, 0, 0, 14, N'عزيزنا المستخدم، تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}).', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">  عميلنا المستخدم,              </p>      <p style="text-align:right"> تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}). : 
نتيجة التأهيل : {1} سبب الرفض : {2} : 
<br><br>                     <br><br>        فريق عمل منافسات      </p> ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">  عميلنا المستخدم,              </p>      <p style="text-align:right"> تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}). : 
نتيجة التأهيل : {1} سبب الرفض : {2} : 
<br><br>                     <br><br>        فريق عمل منافسات      </p> ', N'عزيزنا المستخدم، تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد  نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (700, N'submitPrequalificationforapproval', N'بانتظار اعتماد دعوة التأهيل اللاحق', N'when new Post Qualification waiting approval', 7, 0, 1, 0, 1, 15, N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لدعوة التأهيل اللاحق رقم: {0}', N'Dear  User,  Post Qualification Publishing  is waiting your approval  Rererence Number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
             <p style="text-align:center">&nbsp;</p>     
 <p style="text-align:right">                        
 عزيزنا المستخدم، هنالك طلب إعلان دعوة تأهيل لاحق بانتظار الاعتماد لدعوة التأهيل رقم:       
</n>
{0}. 
 </p>  
<p>    
فريق عمل منافسات    
  </p>  ', N'   <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">
   Dear  User,                </p>
   <p style="text-align:right">
   Post Qualification Publishing  is waiting your approval  Rererence Number: <strong>{0}</strong>
   <br><br>          EGP Team Monafsat      </p>  ', N'هنالك طلب طرح بانتظار الاعتماد لدعوة تأهيل لاحق رقم: {0}', N'Post Qualification Publishing  is waiting your approval  Rererence Number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (701, N'ApprovePostQualification', N'  عند اعتماد تأهيل لاحق', N'when Approve Post Qualification', 8, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد طلب إعلان دعوة تأهيل لاحق رقم::  {0}', N'Dear user, 
Post Qualification Number {0} has been approved
', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
               {0} عزيزنا المستخدم، تم  اعتماد طلب إعلان دعوة تأهيل لاحق رقم:  
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
       Post Qualification Request with Reference Number {0} has been approved: <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N' تم اعتماد طلب إعلان دعوة تأهيل لاحق رقم::  {0}', N'Post Qualification has been approved: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (702, N'RejectingApprovePostQualification', N'عند  رفض دعوة تأهيل لاحق', N'when Reject Post Qualification', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض طلب إعلان دعوة تأهيل لاحق رقم:: {0}', N'Dear user, 
Post Qualification Number 
{0} has been Rejected ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
               {0} عزيزنا المستخدم، تم  رفض طلب إعلان دعوة تأهيل لاحق رقم:  
        
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  User,
        
    </p>
    <p style="text-align:right">
      
       Post Qualification Request with Reference Number {0} has been Rejected : <br><br>

       
        <br><br>
        EGP Team Monafasat
    </p>
', N' تم  رفض طلب إعلان دعوة التأهيل اللاحق رقم::  {0}', N'Post Qualification has been  Rejected {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (704, N'ApprovePostQualification', N'عند إنشاء تأهيل لاحق', N'When Creating Post Qualification', 4, 0, 1, 0, 1, 15, N'عميلنا العزيز, نود تنبيهكم بوجود دعوة تأهيل لاحق رقم {0}
', N'
Dear Customer , There is a Post Qualification Has Been Created On Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز, نود تنبيهكم بوجود دعوة تأهيل لاحق رقم {0}
', N'
Dear Customer , There is a Post Qualification Has Been Created On Number {0}', N'عميلنا العزيز, نود تنبيهكم بوجود دعوة تأهيل لاحق رقم {0}
', N'
Dear Customer , There is a Post Qualification Has Been Created On Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (705, N'ApprovePostQualificationChecking', N'إعتماد الفحص الفنى لوثائق التاهيل اللاحق', N'Approve Post Qualification Document Checking', 8, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Approved  Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (706, N'RejectingApprovePostQualificationChecking', N'رفض إعتماد الفحص الفنى لوثائق التاهيل اللاحق', N'Reject Approve Post Qualification Document Checking', 8, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Rejected On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Rejected On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Checking Has Been Rejected Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (707, N'SubmitPostQualificationForCheckingApproval', N'ارسال وثائق التاهيل اللاحق لاعتماد التقييم الفنى', N'when sending Post Qualification For Checking Approval', 7, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (708, N'ApplyPostQualificationDocuments', N'ارسال وثائق التاهيل اللاحق', N'when sending Post Qualification docs', 4, 0, 1, 0, 1, 15, N'عميلنا العزيز نود تنبيهكم بانه تم تقديم وثائق التاهيل اللاحق  لدعوه التاهيل رقم {0}', N'
Dear Customer , Your Post Qualification Docs Has Been Sent Successfully On Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز, نود تنبيهكم بانه تم تقديم وثائق التاهيل اللاحق  لدعوه التاهيل رقم {0}

', N'
Dear Customer , Your Post Qualification Docs Has Been Sent Successfully On Qualification Number {0}', N'عميلنا المستخدم, نود تنبيهكم بانه تم تقديم وثائق التاهيل اللاحق  لدعوه التاهيل رقم {0}', N'
Post Qualification Docs Has Been Sent Successfully On Qualification Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (709, N'AcceptingPostQualificationDocuments', N'عند قبول الفحص الفنى لوثائق التاهيل اللاحق', N'When Post Qualification Document Checking Accepted', 4, 0, 1, 0, 1, 15, N'عملينا العزيز , نود تنبيهكم انه تم قبول التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}
', N'
Dear Customer , Post Qualification Documents Checking Has Been Accepted Number  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عملينا العزيز , نود تنبيهكم انه تم قبول التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}
', N'
Dear Customer , Post Qualification Documents Checking Has Been Accepted Number  {0}', N'عملينا العزيز , نود تنبيهكم انه تم قبول التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}
', N'
Dear Customer , Post Qualification Documents Checking Has Been Accepted Number  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (710, N'RejectingPostQualificationDocuments', N'عند رفض الفحص الفنى لوثائق التاهيل اللاحق', N'When Post Qualification Document Checking Rejected', 4, 0, 1, 0, 1, 15, N'عملينا العزيز , نود تنبيهكم انه تم رفض التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}', N'
Dear Customer , Post Qualification Documents Checking Has Been Rejected Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عملينا العزيز , نود تنبيهكم انه تم رفض  التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}
', N'
Dear Customer , Post Qualification Documents Checking Has Been Rejected Number {0}', N'عملينا العزيز , نود تنبيهكم انه تم رفض  التقييم الفنى  لدعوة التاهيل اللاحق رقم {0}
', N'
Dear Customer , Post Qualification Documents Checking Has Been Rejected Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (711, N'SendExtendOffersToApprove', N'ارسال طلب تمديد سريان العروض للاعتماد', N'Send extend offers validity to approve', 7, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'
Dear User, the request to extend the validity of offers for accreditation to Competition {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'
Dear User, the request to extend the validity of offers for accreditation to Competition {0}', N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'
Dear User, the request to extend the validity of offers for accreditation to Competition {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (712, N'ApproveExtendOffers', N'اعتماد طلب تمديد سريان العروض', N'Approve Extend Offers', 8, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (720, N'RejectExtendOffers ', N'رفض طلب تمديد سريان العروض', N'Reject Extend Offers ', 8, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'
Dear User, the request to extend the validity of offers for Contest No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'
Dear User, the request to extend the validity of offers for Contest No. {0}', N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'
Dear User, the request to extend the validity of offers for Contest No. {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (721, N'SendExtendOffersToSuppliers', N'ارسال طلب تمديد سريان العروض للمورد', N'SendExtend Offers To Suppliers', 4, 0, 1, 0, 1, 16, N'عميلنا العزيز، {0}  يوجد لديكم طلب تمديد سريان العروض للمنافسة "{1}" ، في حال عدم الرد على الطلب خلال 10 أيام عمل , سيعد ذلك رفضا.', N'Dear customer, {0} You have a request to extend the validity of offers to the competition "{1}", if the request is not answered within 10 working days, it will be rejected.', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز، {0}  يوجد لديكم طلب تمديد سريان العروض للمنافسة "{1}" ، في حال عدم الرد على الطلب خلال 10 أيام عمل , سيعد ذلك رفضا.', N'Dear customer, {0} You have a request to extend the validity of offers to the competition "{1}", if the request is not answered within 10 working days, it will be rejected.', N'عميلنا العزيز، {0}  يوجد لديكم طلب تمديد سريان العروض للمنافسة "{1}" ، في حال عدم الرد على الطلب خلال 10 أيام عمل , سيعد ذلك رفضا.', N'Dear customer, {0} You have a request to extend the validity of offers to the competition "{1}", if the request is not answered within 10 working days, it will be rejected.')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (724, N'AcceptExtendOffersValidityBySupplier', N'الموافقة على طلب تمديد سريان العروض', N'Accept Extend Offers Validity ', 8, 1, 1, 1, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'
Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'
Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'
Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (725, N'RejectExtendOffersValidityBySupplier', N'رفض طلب تمديد سريان العروض', N'Reject Extend Offers Validity ', 8, 1, 1, 1, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (729, N'SendInitialWarantyCopy', N'أرسال نسخة الضمان الأبتدائى', N'Send Initial Waranty Copy', 8, 1, 1, 1, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" ارسل نسخة الضمان الإبتدائى لتمديد سريان العروض للمنافسة رقم: ({1})', N'Dear user, we would like to inform you that the supplier "{0}" has sent the initial waranty copy to extend the validity of the offers to the competition number: ({1})', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" ارسل نسخة الضمان الإبتدائى لتمديد سريان العروض للمنافسة رقم: ({1})', N'Dear user, we would like to inform you that the supplier "{0}" has sent the initial waranty copy to extend the validity of the offers to the competition number: ({1})', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" ارسل نسخة الضمان الإبتدائى لتمديد سريان العروض للمنافسة رقم: ({1})', N'Dear user, we would like to inform you that the supplier "{0}" has sent the initial waranty copy to extend the validity of the offers to the competition number: ({1})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (730, N'SendOffersPostponeRequest', N'ارسال طلب تأجيل العروض', N'Send Offers Postpone Request', 3, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}', N'عزيزنا المستخدم، هنالك طلب تأجيل تقديم العرض من أحد الموردين على المنافسة رقم: .{0}', N'Dear user, there is a request to defer submission of the offer from a supplier to the competition No: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (733, N'RejectOffersPostponeRequest', N'رفض طلب تأجيل العروض', N'Reject Offers Postpone Request', 4, 0, 1, 0, 1, 16, N'
عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم العروض للمنافسة رقم:{1}
', N'Dear Customer, Please note that the request to defer bids for the competition was rejected: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم العروض للمنافسة رقم:{1}
', N'Dear Customer, Please note that the request to defer bids for the competition was rejected: {0}', N'
عميلنا العزيز {0}, نحيطكم علماً أنه تم رفض طلب تأجيل تقديم العروض للمنافسة رقم:{1}
', N'Dear Customer, Please note that the request to defer bids for the competition was rejected: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (734, N'SendFirstNegotitionToApprove', N'ارسال طلب التفاوض المرحلة الأولى للاعتماد', N'Send first negotiation to approve', 7, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، هنالك طلب  تفاوض بانتظار الاعتماد لمنافسة رقم: {0}', N'
Dear user, there is a pending Negotiation request to compete for the number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، هنالك طلب  تفاوض بانتظار الاعتماد لمنافسة رقم: {0}', N'
Dear user, there is a pending Negotiation request to compete for the number: {0}', N'عزيزنا المستخدم، هنالك طلب  تفاوض بانتظار الاعتماد لمنافسة رقم: {0}', N'Dear user, there is a pending Negotiation request to compete for the number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (735, N'AgreeNegotiationFirstSuppliers', N' موافقة المورد   على التفاوض مرحلة اولى', N'Agree Negotiation First Stage ', 7, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (738, N'ApproveNegotiationFirstSecratary', N' اعتماد التفاوض مرحلة اولى سكرتير', N'Approve Negotiation First Secratary', 8, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب  التفاوض للمنافسة رقم:{0}', N'Dear user, the Negotiation request for the competition has been approved : {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب  التفاوض للمنافسة رقم:{0}', N'Dear user, the Negotiation request for the competition has been approved : {0}', N'عزيزنا المستخدم، تم اعتماد طلب  التفاوض للمنافسة رقم:{0}', N'Dear user, the Negotiation request for the competition has been approved : {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (739, N'RejectFirstNegotiation', N'رفض اعتماد التفاوض المرحلة الاولى', N'Reject Negotiation First Suppliers', 8, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم رفض اعتماد طلب التخفيض رقم: {0}', N'
Dear user, the reduction request has been disapproved: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم رفض اعتماد طلب التخفيض رقم: {0}', N'
Dear user, the reduction request has been disapproved: {0}', N'عزيزنا المستخدم، تم رفض اعتماد طلب التخفيض رقم: {0}', N'
Dear user, the reduction request has been disapproved: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (744, N'ApproveBlockSecretary', N'عمليات بحاجه إلي الاعتماد', N'Approve Block Secretary', 26, 1, 1, 0, 1, 16, N'هناك طلب منع بحاجه للاعتماد للمورد:{0}', N'هناك طلب منع بحاجه للاعتماد للمورد:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'هناك طلب منع بحاجه للاعتماد للمورد:{0}', N'هناك طلب منع بحاجه للاعتماد للمورد:{0}', N'هناك طلب منع بحاجه للاعتماد للمورد:{0}', N'هناك طلب منع بحاجه للاعتماد للمورد:{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (745, N'ApproveBlockManager', N'عمليات بحاجة الى الاعتماد', N'Approve Block Manager', 11, 1, 1, 0, 1, 16, N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (746, N'SendNegotiationToSupplier', N'إرسال طلب التفاوض للمورد', N'Send Negotiation To Supplier', 4, 1, 1, 1, 1, 2, N'عزيزى العميل يوجد طلب تفاوض على منافسة رقم {0}', N'Dear, you have Negotiation Request on Tender Number {0}', N'عزيزى العميل يوجد طلب تفاوض على منافسة رقم {0}', N'Dear, you have Negotiation Request on Tender Number {0}', N'عزيزى العميل يوجد طلب تفاوض على منافسة رقم {0}', N'Dear, you have Negotiation Request on Tender Number {0}', N'عزيزى العميل يوجد طلب تفاوض على منافسة رقم {0}', N'Dear, you have Negotiation Request on Tender Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (747, N'SupplierAwardedNotification', N'اشعار الترسية', N'Supplier Awarded Notification', 4, 0, 1, 1, 0, 1, N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم', N'Dear, please note awarding has been accepted to tender {0} to you', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة رقم({3}) ({4}).<br><br>
	<br>
	نفيدكم بموافقة ({5}) على ترسية المنافسة عليكم:<br>
	بمبلغ اجمالى: ({6}) ريال.<br>
	بتاريخ: {7}.<br>
	<br><br>
	نأمل منكم تقديم خطاب ضمان بنكى نهائى بنسبة "{8}" {9} بالمائة من القيمة الإجمالية لعرضكم سارى المفعول لمدة ({10})
	شهرا وتسليمه لإدارة المشتريات والمناقصات 
	"وحدة العقود" بالوزارة خلال عشرة أيام من تاريخه، وتفويض من ترون لتوقيع العقد والتنسيق مع ({11}) للبدء فى التنفيذ، على أن يتم تقديم مطالباتكم المالية عبر منصة اعتماد.<br>
	<br>
	*الضمان البنكى النهائى الزامى فى حال تم طلبه من الجهة الحكومية<br>
	<br>
	فريق عمل منصة اعتماد
</p>', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة رقم({3}) ({4}).<br><br>
	<br>
	نفيدكم بموافقة ({5}) على ترسية المنافسة عليكم:<br>
	بمبلغ اجمالى: ({6}) ريال.<br>
	بتاريخ: {7}.<br>
	<br><br>
	نأمل منكم تقديم خطاب ضمان بنكى نهائى بنسبة "{8}" {9} بالمائة من القيمة الإجمالية لعرضكم سارى المفعول لمدة ({10})
	شهرا وتسليمه لإدارة المشتريات والمناقصات 
	"وحدة العقود" بالوزارة خلال عشرة أيام من تاريخه، وتفويض من ترون لتوقيع العقد والتنسيق مع ({11}) للبدء فى التنفيذ، على أن يتم تقديم مطالباتكم المالية عبر منصة اعتماد.<br>
	<br>
	*الضمان البنكى النهائى الزامى فى حال تم طلبه من الجهة الحكومية<br>
	<br>
	فريق عمل منصة اعتماد
</p>', N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم', N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (748, N'SupplierAwardedNotificationNoGurantee', N'اشعار الترسية بدون ضمان', N'Supplier Awarded Notification Without Guarantee', 4, 0, 1, 1, 0, 1, N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم', N'Dear, please note awarding has been accepted to tender {0} to you', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة رقم({3}) ({4}).<br><br>
	<br>
	نفيدكم بموافقة ({5}) على ترسية المنافسة عليكم:<br>
	بمبلغ اجمالى: ({6}) ريال.<br>
	بتاريخ: {7}.<br>
	<br><br>
	فريق عمل منصة اعتماد
</p>', N'<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة رقم({3}) ({4}).<br><br>
	<br>
	نفيدكم بموافقة ({5}) على ترسية المنافسة عليكم:<br>
	بمبلغ اجمالى: ({6}) ريال.<br>
	بتاريخ: {7}.<br>
	<br><br>
	فريق عمل منصة اعتماد
</p>', N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم', N'عزيزى العميل نفيدكم بالموافقة على ترسية المنافسة رقم {0} عليكم')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (752, N'PlaintRejectedToSupplier', N'تم رفض التظلم', N'Plaint request has been rejected', 4, 0, 1, 1, 0, 16, NULL, NULL, N'تنبيهات منافسات', N'Monafasat Notification', NULL, NULL, NULL, NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (754, N'PlaintApprovedToSupplier', N'تم قبول التظلم', N'Plaint request has been approved', 4, 0, 1, 1, 0, 16, N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إعنماد طلب التظلم وحالته : <strong>{1}</strong> المنافسة رقم: <strong>{2}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إعنماد طلب التظلم وحالته : <strong>{1}</strong> المنافسة رقم: <strong>{2}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (755, N'PlaintEsclationApprovedToSupplier', N'تم الرد على طلب تصعيد التظلم', N'Plaint Escelation has a reply', 4, 0, 1, 1, 0, 16, N'عزيزنا العميل، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا العميل، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا العميل, نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم  على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا العميل نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا العميل، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا العميل، تم الرد على طلب تصعيد تظلم على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (757, N'PlaintEsclationRejectedToSupplier', N'تم رفض طلب تصعيد التظلم', N'Plaint Escelation has been rejected', 4, 0, 1, 1, 0, 16, NULL, NULL, N'تنبيهات منافسات', N'Monafasat Notification', NULL, NULL, NULL, NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (759, N'PlaintSentToApproval', N'تم إرسال طلب التظلم للإعتماد', N'Plaint request sent to approved', 7, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأن هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأن هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (761, N'PlaintApproved', N'تم قبول التظلم', N'Plaint request has been approved', 8, 0, 1, 1, 0, 16, N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إعنماد طلب التظلم وحالته : <strong>{1}</strong> المنافسة رقم: <strong>{2}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إعنماد طلب التظلم وحالته : <strong>{1}</strong> المنافسة رقم: <strong>{2}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}', N'عزيزنا المستخدم،  تم إعنماد طلب التظلم وحالته : {0} للمنافسة رقم: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (763, N'PlaintRejected', N'تم رفض التظلم', N'Plaint request has been rejected', 8, 0, 1, 1, 0, 16, N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (764, N'PlaintStoppingPeriodEnd', N'تم إنتهاء فترة التوقف', N'Plaint stopping period has been ended', 8, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إنتهاء فترة  توقف  الترسية المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم إنتهاء فترة  توقف  الترسية المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (765, N'PlaintEsclationSentForApproval', N'تم إرسال طلب تصعيد التظلم للإعتماد', N'Plaint esclation sent for approval', 27, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم بإنتظار الإعنماد على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم بإنتظار الإعنماد على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم بانتظار الاعتماد للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (766, N'PlaintEsclationCreated', N'تم إنشاء طلب تصعيد تظلم', N'Plaint esclation request has been created', 8, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (768, N'PlaintEsclationApproved', N'تم الرد علي طلب تصعيد التظلم', N'Plaint Escelation has been approved', 28, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم  على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (770, N'PlaintEsclationRejected', N'تم الرد علي طلب تصعيد التظلم', N'Plaint Escelation has been rejected', 28, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم  على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم الرد على طلب تصعيد تظلم على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم الرد على طلب تصعيد تظلم على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (771, N'PlaintRequestCreated', N'تم إنشاء طلب تظلم', N'Plaint request has beaan created', 8, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (773, N'PlaintEsclationCreated', N'تم إنشاء طلب تصعيد تظلم', N'Plaint esclation request has been created', 28, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (775, N'ApproveTenderCheck', N'يوجد طلب اعتماد فحص العروض على منافسة', N'Approvment request is waiting your action on tender number {0}', 23, 0, 1, 1, 0, 4, N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (778, N'TenderOffersCheckingApproved', N'تم اعتماد فحص العروض', N'Offers checking has been approved', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم قبول نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج فحص العروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم قبول نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج فحص العروض على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (779, N'TenderOffersCheckingRejected', N'تم رفض فحص العروض', N'Offers checking has been rejected ', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج فحص العروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (780, N'TenderOffersTechnicalCheckingApproved', N'تم اعتماد فحص العروض الفني', N'Offers technical checking has been approved', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص الفني للعروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (781, N'TenderOffersTechnicalCheckingRejected', N'تم رفض فحص العروض الفني', N'Offers technical checking has been rejected ', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص الفني للعروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (782, N'TenderOffersFinancialCheckingApproved', N'تم اعتماد فحص العروض المالي', N'Offers financial checking has been approved', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم قبول نتائج الفحص المالي للعروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (783, N'TenderOffersFinancialCheckingRejected', N'تم رفض فحص العروض المالي', N'Offers financial checking has been rejected ', 24, 0, 1, 1, 0, 4, N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}', N'عزيزينا المتسخدم، تم رفض نتائج الفحص المالي للعروض على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (784, N'ApproveTenderFinanceChecking', N'يوجد طلب اعتماد فحص العروض المالي على منافسة', N'Approvment request is waiting your action on tender number {0}', 23, 0, 1, 1, 0, 4, N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}', N'عزيزنا المستخدم، يوجد طلب اعتماد فحص العروض المالي على منافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (785, N'offerCheckingDateSet', N'تم إدخال تاريخ فحص العروض لمنافسة', N'Offers checking date have been set', 21, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'السلام عليكم ورحمة الله وبركاته

عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}

تاريخ فحص العروض: {1}', N'السلام عليكم ورحمة الله وبركاته

عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}

تاريخ فحص العروض: {1}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (786, N'SendPreQualificationToApprove', N'ارسال طلب التأهيل المسبق للإعتماد', N'Send Pre Qualification To Approve', 29, 0, 1, 1, 0, 14, N'عزيزنا المستخدم، تم إرسال نتائج تقييم وثائق التأهيل للاعتماد لدعوة التأهيل رقم:  {0} ', N'
Dear user, the results of the qualification assessment for accreditation have been sent to the qualification invitation: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم إرسال نتائج تقييم وثائق التأهيل للاعتماد لدعوة التأهيل رقم:  {0} ', N'
Dear user, the results of the qualification assessment for accreditation have been sent to the qualification invitation: {0}', N'عزيزنا المستخدم، تم إرسال نتائج تقييم وثائق التأهيل للاعتماد لدعوة التأهيل رقم:  {0} ', N'
Dear user, the results of the qualification assessment for accreditation have been sent to the qualification invitation: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (789, N'ApprovePreQualificationCheckingForSecrtary', N'عند إعتماد طلب التأهيل', N'Approve Pre qualification checking', 30, 0, 1, 1, 0, 14, N'عزيزنا المستخدم، تم إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
', N'
Dear User, The results of the qualification assessment for qualification invitation number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
', N'
Dear User, The results of the qualification assessment for qualification invitation number: {0}', N'عزيزنا المستخدم، تم إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
', N'
Dear User, The results of the qualification assessment for qualification invitation number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (792, N'SupplierMatchedPreQualification', N'الموردين المطابقين للتأهيل المسبق', N'Supplier Matched Pre qualification', 4, 0, 1, 1, 0, 14, N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
', N'
Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
', N'
Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}', N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
', N'
Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (795, N'SupplierNotMatchedPreQualification', N'الموردين الغير مطابقين للتأهيل المسبق', N'Supplier not Matched Pre qualification', 4, 0, 1, 1, 0, 14, N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
سبب الرفض: {2}', N'Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}
Disapproval reason: {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
سبب الرفض: {2}', N'Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}
Disapproval reason: {2}', N'عميلنا العزيز، تم الإنتهاء من إعتماد نتائج تقييم وثائق التأهيل لدعوة التأهيل رقم:  {0} 
نتيجة التقييم : {1}
سبب الرفض: {2}', N'Dear customer, the results of evaluating the qualification documents for the qualification invitation number: {0}
Result of evaluation: {1}
Disapproval reason: {2}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (801, N'SendToApproval', N'ارسال للاعتماد', N'Send To Approval', 32, 0, 1, 1, 0, 17, N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، يوجد طلب اعتماد تخطيط مسبق على مشروع {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (802, N'ApprovePreplaning', N'عند اعتماد التخطيط المسبق
', N'ApprovePreplaning', 31, 0, 1, 0, 1, 17, N'عزيزنا المستخدم {0}، تم اعتماد طلب تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، تم اعتماد طلب تخطيط مسبق على مشروع {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم {0}، تم اعتماد طلب تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم {0}، تم اعتماد طلب تخطيط مسبق على مشروع {1}', N'عزيزنا المستخدم ، تم اعتماد طلب تخطيط مسبق على مشروع {0}', N'عزيزنا المستخدم ، تم اعتماد طلب تخطيط مسبق على مشروع {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (804, N'RejectPreplaning', N'عند رفض التخطيط المسبق
', N'RejectPreplaning', 31, 0, 1, 1, 0, 17, N'
عزيزنا المستخدم {0}، تم رفض اعتماد طلب تخطيط مسبق على مشروع {1}', N'
عزيزنا المستخدم {0}، تم رفض اعتماد طلب تخطيط مسبق على مشروع {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'
عزيزنا المستخدم {0}، تم رفض اعتماد طلب تخطيط مسبق على مشروع {1}', N'
عزيزنا المستخدم {0}، تم رفض اعتماد طلب تخطيط مسبق على مشروع {1}', N'
عزيزنا المستخدم ، تم رفض اعتماد طلب تخطيط مسبق على مشروع {0}', N'
عزيزنا المستخدم ، تم رفض اعتماد طلب تخطيط مسبق على مشروع {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (805, N'DisApproveNegotiationFirstSuppliers', N' رفض اعتماد التفاوض مرحلة اولى مورد', N'DisApprove Negotiation First Suppliers', 4, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض  تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: {1}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (806, N'DisApproveCombineRequest', N' رفض طلب تضامن', N'DisApprove Combine Request', 4, 0, 1, 0, 1, 16, N'
عميلنا العزيز، {0} لقد تم رفض
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'
عميلنا العزيز، {0} لقد تم رفض
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}', N'
عميلنا العزيز، {0} لقد تم رفض
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', N'Dear user, we would like to inform you that the supplier "{0}" before bidding reduction to competition number: {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (807, N'AgreeCombineRequest', N'  قبول طلب تضامن', N'Agree Combine Request', 4, 0, 1, 0, 1, 16, N'
عميلنا العزيز، {0} لقد تم قبول 
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', NULL, NULL, NULL, N'
عميلنا العزيز، {0} لقد تم قبول 
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', NULL, N'
عميلنا العزيز، {0} لقد تم قبول 
طلب التضامن من المورد {1} للمشاركة في المنافسة  {2}', NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (811, N'AgreeCombineRequest', N'  قبول طلب تضامن', N'Agree Combine Request', 4, 0, 1, 0, 1, 16, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (813, N'SendTenderCheckToApprove', N'ارسال منافسة مكتب تحقيق الرؤية إلي الاعتماد', N'send VRO tender to approve', 4, 1, 1, 0, 0, 4, N'عزيزنا المستخدم، هنالك طلب اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب اعتماد لمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'<p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، هناك طلب اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، هناك طلب اعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (814, N'TenderHasBeenApproved', N'تم اعتماد منافسة مكتب تحقيق الرؤية', N'VRO Tender has been approved', 1, 1, 1, 0, 0, 4, N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N' عزيزنا المستخدم، تم اعتماد المنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                 عزيزي {0},
        
    </p>
    <p style="text-align:right">
      
        منافسة رقم {1} تم اعتمادها
        <br><br>
        EGP Team Monafasat
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been approved
        <br><br>
        EGP Team Monafasat
    </p>', N'عزيزنا المستخدم، تم اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، تم اعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (815, N'TenderHasBeenRejected', N'تم رفض منافسة مكتب تحقيق الرؤية', N'VRO Tender has been rejected', 1, 1, 1, 0, 0, 4, N' عزيزنا المستخدم، تم رفض طلب اعتماد المنافسة رقم:  {0}', N' عزيزنا المستخدم، تم رفض طلب اعتماد المنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                 عزيزي {0},
        
    </p>
    <p style="text-align:right">
      
        منافسة رقم {1} تم رفض اعتمادها
        <br><br>
        EGP Team Monafasat
    </p>', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                Dear  {0},
        
    </p>
    <p style="text-align:right">
      
        tender {1} has been rejected
        <br><br>
        EGP Team Monafasat
    </p>', N'عزيزنا المستخدم، تم رفض طلب اعتماد لمنافسة رقم: {0}', N'عزيزنا المستخدم، تم رفض طلب اعتماد لمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (818, N'OffersWillOpenTomorrow', N'قبل موعد فتح العروض بيوم', N'Before the opening of the day', 6, 1, 1, 0, 0, 1, N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}', N' عزيزنا المستخدم، يرجى العلم انه سوف يحل تاريخ فتح العروض غدا بتاريخ {1} للمنافسة رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (821, N'reviewTender', N'مراجعة المنافسة  الوحدة المستوى الثاني', N'review tender unit level 2', 21, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (822, N'sentToUnitForReview', N'ارسال للوحدة للمراجعة', N'sent To Unit For Review', 1, 0, 1, 0, 0, 1, N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تحتاج الى المراجعة رقم: {0} ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تم ارسالها الى الوحدة للمراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">               عزيزنا المستخدم، منافسة تم ارسالها الى الوحدة للمراجعة رقم: {0} : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N'عزيزنا المستخدم، منافسة تم ارسالها الى الوحدة للمراجعة رقم: {0} ', N'عزيزنا المستخدم، منافسة تم ارسالها الى الوحدة للمراجعة رقم: {0} ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (823, N'TenderAwardingNeedFirstApprove', N'مرسلة الى مدير الفحص لتأكيد اعتماد الترسية المبدأية', N'Sent to awarding manager to approve first awarding', 7, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم إرسال طلب اعتماد الترسية المبدئي  للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم إرسال طلب اعتماد الترسية المبدئي  للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال  طلب اعتماد الترسية المبدئي  للمنافسة رقم:: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال  طلب اعتماد الترسية المبدئي  للمنافسة رقم:: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N' هنالك  طلب اعتماد الترسية المبدئي  للمنافسة رقم: {0}', N' هنالك  طلب اعتماد الترسية المبدئي  للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (825, N'SendInvitationsToSupplierForSolidarity', N'عند ارسال طلب تضامن للمورد', N'send Solidarity Request', 4, 1, 1, 0, 0, 1, N'عزيزنا المستخدم يوجد دعوة تضامن من المورد {0} على المنافسة رقم {1}', N'Dear User Solidarity Invitation Sent to you From :{0} on Tender : {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>      
       </p>     
  <p style="text-align:right">      
  عزيزنا المستخدم يوجد دعوة تضامن من المورد
  {0} 
  على المنافسة رقم 
  {1} .<br><br>        
  <br><br>          فريق منافسات     </p>  
  ', N' 
   <p style="text-align:center">&nbsp;</p>      
       </p>     
  <p style="text-align:right">  
 Dear User Solidarity Invitation Sent to you From :{0} on Tender : {1}
  .<br><br>        
  <br><br>          فريق منافسات     </p>  
  ', N'عزيزنا المستخدم يوجد دعوة تضامن من المورد : {0} على المنافسة رقم :  {1}  ', N' Dear User Solidarity Invitation Sent to you From :{0} on Tender : {1}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (826, N'AcceptSolidarityInvitation', N'قبول دعوة التفاوض', N'Accept SolidarityI nvitation', 4, 1, 1, 0, 0, 1, N'عميلنا العزيز،{0} لقد تم الموافقة على طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 
', N' Dear Customer {0} Solidarity Invitation was agreed from Supplier :{0} on Tender : {2}
', N'تنبيهات منافسات', N'Monafasat Notification', N'
  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                             </p>      <p style="text-align:right">        
عميلنا العزيز،{0} لقد تم الموافقة على طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 


 .<br><br>                     <br><br>          فريق منافسات     </p>  
', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                             </p>      <p style="text-align:right">    Dear Customer {0} Solidarity Invitation was Accepted from Supplier :{0} on Tender : {2}.<br><br>                     <br><br>          فريق منافسات     </p>  
', N'عميلنا العزيز،{0} لقد تم الموافقة على طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 
', N' Dear Customer {0} Solidarity Invitation was agreed from Supplier :{0} on Tender : {2}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (827, N'RejectSolidarityInvitation', N'رفض دعوة التفاوض', N'RejectSolidarityInvitation', 4, 1, 1, 0, 0, 1, N'عميلنا العزيز،{0} لقد تم رفض  طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 
', N'Dear Customer {0} Solidarity Invitation was Refused from Supplier :{0} on Tender : {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'
  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                             </p>      <p style="text-align:right">        
عميلنا العزيز،{0} لقد تم رفض  طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 


 .<br><br>                     <br><br>          فريق منافسات     </p>  
', N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                             </p>      <p style="text-align:right">    Dear Customer {0} Solidarity Invitation was Refused from Supplier :{0} on Tender : {2}.<br><br>                     <br><br>          فريق منافسات     </p>  
', N'عميلنا العزيز،{0} لقد تم رفض  طلب التضامن من المورد  {1} للمشاركة في المنافسة{2}. 
', N'Dear Customer {0} Solidarity Invitation was Refused from Supplier :{0} on Tender : {2}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (900, N'approvaloperation', N'إعتماد', N'Approval Operation', 27, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (901, N'approvaloperation', N'إعتماد', N'Approval Operation', 23, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (902, N'approvaloperation', N'إعتماد', N'Approval Operation', 32, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (903, N'approvaloperation', N'إعتماد', N'Approval Operation', 29, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (904, N'approvaloperation', N'إعتماد', N'Approval Operation', 34, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (905, N'approvaloperation', N'إعتماد', N'Approval Operation', 36, 1, 1, 1, 1, 4, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (906, N'submitPrequalificationforapproval', N'بانتظار اعتماد دعوة التأهيل اللاحق', N'when new Post Qualification waiting approval', 23, 0, 1, 0, 1, 15, N'عزيزنا المستخدم، هنالك طلب طرح بانتظار الاعتماد لدعوة التأهيل اللاحق رقم: {0}', N'Dear  User,  Post Qualification Publishing  is waiting your approval  Rererence Number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
             <p style="text-align:center">&nbsp;</p>     
 <p style="text-align:right">                        
 عزيزنا المستخدم، هنالك طلب إعلان دعوة تأهيل لاحق بانتظار الاعتماد لدعوة التأهيل رقم:       
</n>
{0}. 
 </p>  
<p>    
فريق عمل منافسات    
  </p>  ', N'   <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">
   Dear  User,                </p>
   <p style="text-align:right">
   Post Qualification Publishing  is waiting your approval  Rererence Number: <strong>{0}</strong>
   <br><br>          EGP Team Monafsat      </p>  ', N'هنالك طلب طرح بانتظار الاعتماد لدعوة تأهيل لاحق رقم: {0}', N'Post Qualification Publishing  is waiting your approval  Rererence Number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (907, N'SubmitPostQualificationForCheckingApproval', N'ارسال وثائق التاهيل اللاحق لاعتماد التقييم الفنى', N'when sending Post Qualification For Checking Approval', 23, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking Number {0}')
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
    </p>', N'تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد', N'Tender has been sent to etimad officer to approve: {0}')
GO
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
    </p>', N'تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد', N'Tender has been sent to etimad officer to approve: {0}')
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (942, N'approvaloperation', N'إعتماد', N'Approval Operation', 25, 1, 1, 1, 1, 1, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1001, N'ApprovePostQualification', N'  عند اعتماد تأهيل لاحق', N'when Approve Post Qualification', 24, 0, 1, 1, 0, 1, N' عزيزنا المستخدم، تم اعتماد طلب إعلان دعوة تأهيل لاحق رقم::  {0}', N'Dear user,  Post Qualification Number {0} has been approved ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                         {0} عزيزنا المستخدم، تم  اعتماد طلب إعلان دعوة تأهيل لاحق رقم:                    <br><br>         فريق عمل منافسات     </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">                 Post Qualification Request with Reference Number {0} has been approved: <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N' تم اعتماد طلب إعلان دعوة تأهيل لاحق رقم::  {0}', N'Post Qualification has been approved: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1002, N'RejectingApprovePostQualification', N'عند  رفض دعوة تأهيل لاحق', N'when Reject Post Qualification', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض طلب إعلان دعوة تأهيل لاحق رقم:: {0}', N'Dear user,  Post Qualification Number  {0} has been Rejected ', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                         {0} عزيزنا المستخدم، تم  رفض طلب إعلان دعوة تأهيل لاحق رقم:                    <br><br>         فريق عمل منافسات     </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  User,                </p>      <p style="text-align:right">                 Post Qualification Request with Reference Number {0} has been Rejected : <br><br>                     <br><br>          EGP Team Monafasat      </p>  ', N' تم  رفض طلب إعلان دعوة التأهيل اللاحق رقم::  {0}', N'Post Qualification has been  Rejected {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1003, N'ApprovePostQualificationChecking', N'إعتماد الفحص الفنى لوثائق التاهيل اللاحق', N'Approve Post Qualification Document Checking', 24, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Approved  Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1004, N'RejectingApprovePostQualificationChecking', N'رفض إعتماد الفحص الفنى لوثائق التاهيل اللاحق', N'Reject Approve Post Qualification Document Checking', 24, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Rejected On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Rejected On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم رفض إعتماد التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'  Dear User , Post Qualification Documents Checking Has Been Rejected Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1006, N'ApprovePreQualificationChecking', N'إعتماد الفحص الفنى لوثائق التاهيل  ', N'Approve Pre Qualification Document Checking', 30, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل   رقم {0}', N'
Dear User , Pre Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل   رقم {0}', N'
Dear User , Pre Qualification Documents Checking Has Been Approved On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إعتماد التقييم الفنى للاعتماد لدعوة التاهيل   رقم {0}', N'
Dear User , Pre Qualification Documents Checking Has Been Approved  Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1008, N'SubmitPreQualificationForCheckingApproval', N'ارسال وثائق التاهيل  لاعتماد التقييم الفنى', N'when sending Post Qualification For Checking Approval', 29, 0, 1, 0, 1, 15, N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل   رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On PreQualification Number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل اللاحق رقم {0}', N'
Dear User , Post Qualification Documents Has Been Sent For Checking On Post Qualification Number {0}', N'عزيزنا المستخدم و نود تنبيهكم انه تم إرسال التقييم الفنى للاعتماد لدعوة التاهيل  رقم {0}', N'
Dear User , PreQualification Documents Has Been Sent For Checking Number {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1010, N'Qualificationwaitingdateextend', N'بانتظار اعتماد تمديد تواريخ', N'when  Qualification waiting date extend', 7, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأن هنالك طلب تمديد بانتظار الإعتماد {1} <br />          <strong> للتاهيل: </strong>{2} <br><br>         <strong> بحيث ستصبح مواعيد التاهيل كالتالي: </strong>          <br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  ', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Extend date for the follwing Qualification waiting your approval {1} <br />          <strong> Tender: </strong>{2} <br><br>         <strong> The new date for the Qualification will be: </strong>          <br>          Last Date to accept vendors questions: {3}<br>          Closing Date: {4}<br>          Bid Opening Date: {5} Time: {6} <br>                              <br><br>          EGP Team Monafasat      </p>  ', N'طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1011, N'approveQualificationattachment', N'عند انتظار اعتماد ملحقات التاهيل', N'when Qualification attachment waiting approval', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل على المرفقات جديد و بانتظار الاعتماد للتاهيل رقم:{0}', N'Dear User: {0}, There is a request to modifiy the attachments needs your approval for Qualification No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>  <p style="text-align:right">        السلام عليكم ورحمة الله  {0},    </p>  <p style="text-align:right">        نود تنبيهكم بانه يوجد طلب تعديل (بانتظار الاعتماد) على الملفات الخاصة بمستندات التاهيل <br />      <strong> إسم التاهيل: </strong>{1}      <br>      <strong> رقم التاهيل: </strong>{2}      <br><br>      وتقبلوا وافر تحياتنا،،      <br>      فريق عمل منافسات  </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear {0},                </p>      <p style="text-align:right">                   There is a request to modifiy the attachments needs your approval for Qualification <br />          <strong> Tender Name: </strong>{1}           <br>          <strong> Qualification No.: </strong>{2}           <br><br>                  Thank You,           <br>          EGP Team Monafsat      </p>', N'هنالك طلب إضافة مرفق جديد و بانتظار الاعتماد للتاهيل رقم:{0}', N'The attachments has been modified for the Qualification No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1012, N'RejectQualificationAttachment', N'عند رفض المرفقات', N'when Qualification attachment removed', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}  ، تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم:   {1}', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'       <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم ورحمة الله  {0},              </p>     <p style="text-align:right">                ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />         <strong> إسم المنافسة: </strong>{1}         <br>         <strong> رقم المنافسة: </strong>{2}         <br><br>                 وتقبلوا وافر تحياتنا،،          <br>         فريق عمل منافسات     </p>      ', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          Dear {0},              </p>     <p style="text-align:right">                 The Files has been modified for the tender<br />         <strong> Tender Name: </strong>{1}          <br>         <strong> Tender No.: </strong>{2}          <br><br>                 Thank You,          <br>         EGP Team Monafsat     </p>  ', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم:   {0}', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1013, N'RejectQualificationExtendDate', N'عن رفض تمديد تواريخ التاهيل', N'when Qualification ExtendDate removed', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تمديد التواريخ  للتاهيل رقم:  {0}', N'Dear User, Extend Dates has been rejected for Qualification No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض تمديد تواريخ التاهيل :: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل التاهيل: </strong>          <br>          الغرض من التاهيل: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>            فريق عمل منافسات      </p>', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض تمديد تواريخ التاهيل :: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل التاهيل: </strong>          <br>          الغرض من التاهيل: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>            فريق عمل منافسات      </p>', N'تم رفض تمديد التواريخ  للتاهيل رقم:  {0}', N'تم رفض تمديد التواريخ  للتاهيل رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1014, N'Qualificationwaitingdateextend', N'بانتظار اعتماد تمديد تواريخ', N'when  Qualification waiting date extend', 23, 0, 1, 0, 1, 4, N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'عزيزنا المستخدم، هنالك طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأن هنالك طلب تمديد بانتظار الإعتماد {1} <br />          <strong> للتاهيل: </strong>{2} <br><br>         <strong> بحيث ستصبح مواعيد التاهيل كالتالي: </strong>          <br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فتح المظاريف : {5} الساعة: {6} <br>                              <br><br>          فريق عمل منافسات      </p>  ', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Extend date for the follwing Qualification waiting your approval {1} <br />          <strong> Tender: </strong>{2} <br><br>         <strong> The new date for the Qualification will be: </strong>          <br>          Last Date to accept vendors questions: {3}<br>          Closing Date: {4}<br>          Bid Opening Date: {5} Time: {6} <br>                              <br><br>          EGP Team Monafasat      </p>  ', N'طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}', N'طلب تمديد تواريخ بانتظار اعتمادكم للتاهيل رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1015, N'approveQualificationattachment', N'عند انتظار اعتماد ملحقات التاهيل', N'when Qualification attachment waiting approval', 23, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هنالك طلب تعديل على المرفقات جديد و بانتظار الاعتماد للتاهيل رقم:{0}', N'Dear User: {0}, There is a request to modifiy the attachments needs your approval for Qualification No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p>  <p style="text-align:right">        السلام عليكم ورحمة الله  {0},    </p>  <p style="text-align:right">        نود تنبيهكم بانه يوجد طلب تعديل (بانتظار الاعتماد) على الملفات الخاصة بمستندات التاهيل <br />      <strong> إسم التاهيل: </strong>{1}      <br>      <strong> رقم التاهيل: </strong>{2}      <br><br>      وتقبلوا وافر تحياتنا،،      <br>      فريق عمل منافسات  </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear {0},                </p>      <p style="text-align:right">                   There is a request to modifiy the attachments needs your approval for Qualification <br />          <strong> Tender Name: </strong>{1}           <br>          <strong> Qualification No.: </strong>{2}           <br><br>                  Thank You,           <br>          EGP Team Monafsat      </p>', N'هنالك طلب إضافة مرفق جديد و بانتظار الاعتماد للتاهيل رقم:{0}', N'The attachments has been modified for the Qualification No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1016, N'RejectQualificationAttachment', N'عند رفض المرفقات', N'when Qualification attachment removed', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}  ، تم رفض تعديل  المرفقات الخاصة بمستندات دعوة التاهيل رقم:   {1}
', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'       <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم ورحمة الله  {0},              </p>     <p style="text-align:right">                ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />         <strong> إسم المنافسة: </strong>{1}         <br>         <strong> رقم المنافسة: </strong>{2}         <br><br>                 وتقبلوا وافر تحياتنا،،          <br>         فريق عمل منافسات     </p>      ', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          Dear {0},              </p>     <p style="text-align:right">                 The Files has been modified for the tender<br />         <strong> Tender Name: </strong>{1}          <br>         <strong> Tender No.: </strong>{2}          <br><br>                 Thank You,          <br>         EGP Team Monafsat     </p>  ', N'تم  رفض التعديل على المرفقات الخاصة بمستندات دعوة التأهيل  رقم:   {0}
', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1017, N'RejectQualificationExtendDate', N'عن رفض تمديد تواريخ التاهيل', N'when Qualification ExtendDate removed', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تمديد التواريخ  للتاهيل رقم:  {0}', N'Dear User, Extend Dates has been rejected for Qualification No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض تمديد تواريخ التاهيل :: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل التاهيل: </strong>          <br>          الغرض من التاهيل: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>            فريق عمل منافسات      </p>', N'        <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض تمديد تواريخ التاهيل :: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل التاهيل: </strong>          <br>          الغرض من التاهيل: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>            فريق عمل منافسات      </p>', N'تم رفض تمديد التواريخ  للتاهيل رقم:  {0}', N'تم رفض تمديد التواريخ  للتاهيل رقم:  {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1018, N'approveQualificationextenddate', N'اعتماد تمديد تواريخ التاهيل', N'when new attachment added to tender', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للتاهيل رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Qualification No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>         <p style="text-align:right">             <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">        السادة {0}                                      السلام عليكم ورحمة الله,             </span>         </span>     </p>     <p style="text-align:right">                        إعلان تمديد تواريخ تاهيل صادر عن {1} – {2} :                  <br><br>                 إسم التاهيل: <strong>{3}</strong><br>         رقم التاهيل: {4}<br>                    الغرض من العرض: <strong>{5}</strong>          <br><br>                    آخر موعد لاستلام الاستفسارات: {6}<br>         آخر موعد لاستلام العروض: {7}<br>                              تاريخ فتح المظاريف : {8} الساعة: {9}     <br>          <br><br>                   <br><br>                 وتقبلوا وافر تحياتنا،،          <br><br>         فريق عمل منافسات    </p>', N'    <p style="text-align:center">&nbsp;</p>        <p style="text-align:right">              <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                 <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                   Dear ,      {0}           </span>         </span>     </p>     <p style="text-align:right">                       The follwing Qualification has been extended {1} – {2} :                  <br><br>         Qualification Name: <strong>{3}</strong><br>              Tender No.: {4}<br>         Purpose: <strong>{5</strong>              <br><br>         Last Date to accept vendors questions: {6}br>             Closing Date: {7}<br>         Bid Opening Date: {8} Time: {9} <br>                <br><br>                   <br><br>                          Thank You,          <br>', N'إعلان تمديد التواريخ  للتاهيل رقم: {0}', N'The Qualification Dates een modified for the Qualification No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1019, N'approveQualificationattachment', N'في إنتظار اعتماد المرفقات', N'when tender attachment waiting approval', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  ', N'    <p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1020, N'approveQualificationextenddate', N'اعتماد تمديد تواريخ التاهيل', N'when new attachment added to tender', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، إعلان تمديد التواريخ صادر عن{0} – {1}  للتاهيل رقم: {2}', N'Dear User, Extend Dates has been approved from {0}-{1} for Qualification No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>         <p style="text-align:right">             <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">        السادة {0}                                      السلام عليكم ورحمة الله,             </span>         </span>     </p>     <p style="text-align:right">                        إعلان تمديد تواريخ تاهيل صادر عن {1} – {2} :                  <br><br>                 إسم التاهيل: <strong>{3}</strong><br>         رقم التاهيل: {4}<br>                    الغرض من العرض: <strong>{5}</strong>          <br><br>                    آخر موعد لاستلام الاستفسارات: {6}<br>         آخر موعد لاستلام العروض: {7}<br>                              تاريخ فتح المظاريف : {8} الساعة: {9}     <br>          <br><br>                   <br><br>                 وتقبلوا وافر تحياتنا،،          <br><br>         فريق عمل منافسات    </p>', N'    <p style="text-align:center">&nbsp;</p>        <p style="text-align:right">              <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                 <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                   Dear ,      {0}           </span>         </span>     </p>     <p style="text-align:right">                       The follwing Qualification has been extended {1} – {2} :                  <br><br>         Qualification Name: <strong>{3}</strong><br>              Tender No.: {4}<br>         Purpose: <strong>{5</strong>              <br><br>         Last Date to accept vendors questions: {6}br>             Closing Date: {7}<br>         Bid Opening Date: {8} Time: {9} <br>                <br><br>                   <br><br>                          Thank You,          <br>', N'إعلان تمديد التواريخ  للتاهيل رقم: {0}', N'The Qualification Dates een modified for the Qualification No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1021, N'approveQualificationattachment', N'في إنتظار اعتماد المرفقات', N'when tender attachment waiting approval', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}', N'Dear User, Extend Dates has been approved from {0}-{1} for Tender No. {2}', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center">&nbsp;</p>  ', N'    <p style="text-align:center">&nbsp;</p>', N'تم التعديل على المرفقات الخاصة بمستندات المنافسة رقم: 
 {0}', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1022, N'sendrelatedvrotender', N'انتظار الاعتماد من مسؤل الاعتماد', N'when tender has been sent to etimad officer to approve', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم ارسال المنافسة رقم:  {0} للاعتماد من مسؤل الاعتماد', N'Dear user, 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1023, N'SendNegotiationRequestToApprove', N'سكرتير الفحص', N'SendNegotiationRequestToApprove', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1024, N'SendNegotiationRequestToApprove', N'سكرتير الشراء', N'SendNegotiationRequestToApprove', 23, 0, 1, 1, 0, 1, N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، هنالك طلب تخفيض بانتظار الاعتماد لمنافسة رقم: ({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})', N'

عزيزنا المستخدم، نود تنبيهكم بوجود طلب
تخفيض بحاجة للمراجعة رقم:
({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1025, N'SendNegotiationRequestToApprove', N'إرسال طلب التفاوض لمختص الوحده المستوى الاول ', N'SendNegotiationRequestToApprove', 21, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'تنبيهات منافسات', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1026, N'SendNegotiationRequestToApprove', N'إرسال طلب التفاوض لمختص الوحده المستوى الثاني', N'SendNegotiationRequestToApprove', 22, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'تنبيهات منافسات', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})', N'
عزيزنا المستخدم، نود تنبيهكم بوجود طلب

تخفيض بحاجة للمراجعة رقم:({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1027, N'SendNegotiationRequestApproval', N'ارسال الموافقة على طلب التفاوض', N'SendNegotiationRequestToApprove', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1028, N'SendNegotiationRequestApproval', N'ارسال الموافقة على طلب التفاوض', N'SendNegotiationRequestToApprove', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1029, N'SendRejectionToSecretary', N'ارسال الرفض على طلب التفاوض', N'SendNegotiationRequestToApprove', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1030, N'SendRejectionToSecretary', N'ارسال  الرفض على طلب التفاوض', N'SendNegotiationRequestToApprove', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})', N'عزيزنا المستخدم، تم رفض اعتماد طلب
التخفيض رقم:
({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1031, N'SendNegotiationApproveFromUnit', N'ارسال الموافقة من الوحدة على طلب التفاوض', N'SendNegotiationRequestToApprove', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1032, N'SendNegotiationApproveFromUnit', N'ارسال الموافقة من الوحدة على طلب التفاوض', N'SendNegotiationRequestToApprove', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة   رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1033, N'SendNegotiationRequestToApproveToSupplier', N'ارسال  طلب التفاوض الى المورد', N'SendNegotiationRequestToApprove', 4, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).', N'عزيزنا المستخدم، نود تنبيهكم بوجود طلب تخفيض بحاجة للمراجعة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1034, N'SendNegotiationRequestToApproveToSupplier', N'ارسال  طلب التفاوض الى المورد', N'SendNegotiationRequestToApprove', 4, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})', N'عزيزنا المستخدم، تم اعتماد  طلب التخفيض للمنافسة وتم ارسال تنبية لمختص الوحدة  رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1035, N'RejectUnitSecondNegotition', N'رفض  طلب التفاوض من الوحدة', N'RejectUnitSecondNegotition', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1036, N'RejectUnitSecondNegotition', N'رفض  طلب التفاوض من الوحدة', N'RejectUnitSecondNegotition', 6, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})', N'عزيزنا المستخدم، تم  رفض طلب التخفيض للمنافسة    رقم: ({0})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1037, N'ApproveNegotiationRequestFromSupplier', N'قبول طلب التفاوض من المورد ', N'ApproveNegotiationRequestFromSupplier', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1038, N'RejectNegotiationRequestFromSupplier', N'رفض طلب التفاوض من المورد ', N'RejectNegotiationRequestFromSupplier', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1039, N'ApproveNegotiationRequestFromSupplier', N'قبول طلب التفاوض من المورد ', N'ApproveNegotiationRequestFromSupplier', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1040, N'RejectNegotiationRequestFromSupplier', N'رفض طلب التفاوض من المورد ', N'RejectNegotiationRequestFromSupplier', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}"  رفض تخفيض العرض للمنافسة رقم: ({1}).
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1041, N'AgreeNegotiationFirstSuppliers', N'عند  قبول طلب تخفيض', N'AgreeNegotiationFirstSuppliers', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'ڢزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1044, N'AgreeNegotiationFirstSuppliers', N'عند   قبول طلب تخفيض', N'AgreeNegotiationFirstSuppliers', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1045, N'AgreeNegotiationFirstSuppliers', N'عند   قبول طلب تخفيض', N'AgreeNegotiationFirstSuppliers', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل تخفيض العرض للمنافسة رقم: ({1}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1046, N'TenderOffersFinancialCheckingApproved', N'عند اعتماد التقييم المالى منافسة مكتب تحقيق الرؤية', N'When approve the financial appraisal, the Office of Vision realization', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم المالى للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1047, N'TenderOffersFinancialCheckingRejected', N'عند رفض اعتماد التقييم المالى منافسة مكتب تحقيق الرؤية', N'When rejecting  to approve the financial appraisal, the Office of Vision realization', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم المالى للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1048, N'sendTechnicalEvaluationOfCompetitionToApprove', N'عند ارسال التقييم الفنى لمنافسة مكتب تحقيق الرؤية للإعتماد', N'When sending the technical evaluation of the competition to achieve the vision for accreditation', 34, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم الفني للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1049, N'TenderOffersTechnicalCheckingApproved', N'عند اعتماد التقييم الفنى منافسة مكتب تحقيق الرؤية', N'When approve the technical appraisal, the Office of Vision realization', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم اعتماد التقييم الفنى للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1050, N'TenderOffersTechnicalCheckingRejected', N'عند رفض اعتماد التقييم الفنى منافسة مكتب تحقيق الرؤية', N'When reject the technical appraisal, the Office of Vision realization', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، تم رفض اعتماد التقييم الفنى للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (1051, N'sendFinancialEvaluationOfCompetitionToApprove', N'عند ارسال التقييم المالى لمنافسة مكتب تحقيق الرؤية للإعتماد', N'When send financial evaluation of competition to approve', 34, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'تنبيهات منافسات', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).', N'عزيزنا المستخدم، هناك طلب اعتماد التقييم المالى للمنافسة رقم: ({0}).')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2000, N'RejectedQualificationAttachment', N'عند رفض المرفقات', N'when Qualification attachment removed', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم :{0}  ، تم رفض تعديل  المرفقات الخاصة بمستندات دعوة التاهيل رقم:   {1}
', N'Dear User: {0}, The attachments has been modified for the tender No.: {1}', N'تنبيهات منافسات', N'Monafasat Notification', N'       <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم ورحمة الله  {0},              </p>     <p style="text-align:right">                ننود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات المنافسة<br />         <strong> إسم المنافسة: </strong>{1}         <br>         <strong> رقم المنافسة: </strong>{2}         <br><br>                 وتقبلوا وافر تحياتنا،،          <br>         فريق عمل منافسات     </p>      ', N'    <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          Dear {0},              </p>     <p style="text-align:right">                 The Files has been modified for the tender<br />         <strong> Tender Name: </strong>{1}          <br>         <strong> Tender No.: </strong>{2}          <br><br>                 Thank You,          <br>         EGP Team Monafsat     </p>  ', N'تم  رفض التعديل على المرفقات الخاصة بمستندات دعوة التأهيل  رقم:   {0}
', N'The attachments has been modified for the tender No.: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2001, N'RejectQualificationCancle', N'عند رفض إلغاء دعوة التأهيل', N'when Qualification cancellation rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:
 {0}
', N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:
 {0}
', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
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
        </p>', N'تم رفض طلب إلغاء دعوة التأهيل رقم:  {0}', N'
Qualification cancelation has been rejected for tender No:
{0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2002, N'CancleQualificationConfirm', N'عند الغاء التاهيل', N'CancleQualificationConfirm', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2003, N'CancleQualification', N'عند الغاء التاهيل', N'CancleQualification', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2004, N'CancleQualification', N'عند الغاء التاهيل', N'CancleQualification', 30, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ', N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}
       ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2005, N'CancleQualification', N'عند الغاء التاهيل', N'CancleQualification', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء  دعوة التأهيل رقم:  {0}', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2007, N'Qualificationwaitingcancelation', N'عند انتظار الغاء دعوة التأهيل', N'Qualificationwaitingcancelation', 29, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة التأهيل رقم: 
{0}', N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة التأهيل رقم: 
{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة التأهيل رقم: 
{0}', N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة التأهيل رقم: 
{0}', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2008, N'Qualificationrwaitingcancelation', N'عند انتظار الغاء التاهيل', N'When Qualification waiting cancelation', 7, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة تاهيل رقم: 
{0}', N'Dear user, there is a pending cancellation request for Qualification number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}', N'There is a pending cancellation request for Qualification number: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2009, N'Qualificationrwaitingcancelation', N'عند انتظار الغاء التاهيل', N'When Qualification waiting cancelation', 23, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة تاهيل رقم: 
{0}', N'Dear user, there is a pending cancellation request for Qualification number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2010, N'Qualificationrwaitingcancelation', N'عند انتظار الغاء التاهيل', N'When Qualification waiting cancelation', 3, 0, 1, 1, 0, 1, N'
عزيزنا المستخدم، هنالك طلب إلغاء بانتظار الإعتماد لدعوة تاهيل رقم: 
{0}', N'Dear user, there is a pending cancellation request for Qualification number: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال التاهيل  لإعتماد إلغائها من قبلكم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل التاهيل: </strong>
        <br>
        الغرض من التاهيل: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لفحص التاهيل : {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}', N'
هنالك طلب إلغاء بإنتظار الإعتماد لدعوة تأهيل رقم: 
{0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2011, N'RejectQualificationCancle', N'عند رفض إلغاء دعوة التأهيل', N'when Qualification cancellation rejected', 1, 0, 1, 1, 0, 1, N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض عملية إلغاء المنافسة: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}<br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Tender Cancelation has been rejected for: <strong>{1}</strong> <br><br>  Reasons:  <br><br>  {2}  <br><br>         <strong> Details: </strong>          <br>          Purpose: {3}<br><br>          Last Date to accept vendors questions: {4}<br>          Closing Date: {5}<br>          Bid Opening Date: {6} Time: {7} <br>                              <br><br>          EGP Team Monafsat          </p>', N'تم رفض طلب إلغاء دعوة التأهيل رقم:  {0}', N'  Qualification cancelation has been rejected for tender No:  {0}  ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2012, N'RejectQualificationCancle', N'عند رفض إلغاء دعوة التأهيل', N'when Qualification cancellation rejected', 8, 0, 1, 1, 0, 1, N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض عملية إلغاء المنافسة: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}<br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Tender Cancelation has been rejected for: <strong>{1}</strong> <br><br>  Reasons:  <br><br>  {2}  <br><br>         <strong> Details: </strong>          <br>          Purpose: {3}<br><br>          Last Date to accept vendors questions: {4}<br>          Closing Date: {5}<br>          Bid Opening Date: {6} Time: {7} <br>                              <br><br>          EGP Team Monafsat          </p>', N'تم رفض طلب إلغاء دعوة التأهيل رقم:  {0}', N'  Qualification cancelation has been rejected for tender No:  {0}  ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2013, N'RejectQualificationCancle', N'عند رفض إلغاء دعوة التأهيل', N'when Qualification cancellation rejected', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'عزيزنا المستخدم,  تم رفض طلب إلغاء دعوة التأهيل رقم:   {0}  ', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأنه تم رفض عملية إلغاء المنافسة: <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل المنافسة: </strong>          <br>          الغرض من المنافسة: {3}<br><br>          آخر موعد لاستلام الاستفسارات: {4}<br>          آخر موعد لاستلام العروض: {5}<br>          تاريخ فتح المظاريف : {6} الساعة: {7} <br>                              <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            Dear  {0},                </p>      <p style="text-align:right">                  Tender Cancelation has been rejected for: <strong>{1}</strong> <br><br>  Reasons:  <br><br>  {2}  <br><br>         <strong> Details: </strong>          <br>          Purpose: {3}<br><br>          Last Date to accept vendors questions: {4}<br>          Closing Date: {5}<br>          Bid Opening Date: {6} Time: {7} <br>                              <br><br>          EGP Team Monafsat          </p>', N'تم رفض طلب إلغاء دعوة التأهيل رقم:  {0}', N'  Qualification cancelation has been rejected for tender No:  {0}  ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2014, N'CancleQualification', N'الغاء التأهيل', N'when Qualification Cancle', 4, 0, 1, 0, 1, 1, N' عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء دعوة التأهيل رقم:  {0}', N'Dear user, We''re sorry to inform you that the Qualification No: {0} has been canceled', N'تنبيهات منافسات', N'Monafasat Notification', N'  <p style="text-align:center"></p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إلغاء دعوة التأهيل: <strong>{1}</strong> <br><br>
       <strong> تفاصيل الدعوة: </strong>
        <br>
        الغرض من الدعوة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام الوثائق : {4}<br>
        تاريخ التقيم   : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N' <p style="text-align:center"></p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إلغاء دعوة التأهيل: <strong>{1}</strong> <br><br>
       <strong> تفاصيل الدعوة: </strong>
        <br>
        الغرض من الدعوة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لاستلام الوثائق : {4}<br>
        تاريخ التقيم   : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>', N'تم  إلغاء دعوة التأهيل رقم: {0}', N'Qualification with number : {0} has been canceled')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2019, N'sendApprovedManerApprovedBlocked', N'ارسال طلب الموافقة على المنع إلي المورد', N'sendApprovedManerApProvedBlocked', 4, 1, 1, 1, 1, 1, N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ', N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ', N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ', N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ', N'عزيزنا العميل {0} ، نود تنبيهكم بأنه تم إضافتكم إلي قائمة المنع ')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2020, N'ApproveBlockFromManagerToSecretary', N'عمليات تم الموافقة عليها من رئيس لجنة المنع', N'ApproveBlockFromMangerToSecretary', 26, 1, 1, 1, 1, 16, N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2021, N'ApproveBlockFromManagerToMonafasatAdmin', N'ارسال طلب الموافقة على المنع', N'ApproveBlockFromManagerToMonafasatAdmin', 12, 1, 1, 1, 1, 16, N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2022, N'SendRequestBlockFromSecretaryToBlockManger', N'ارسال طلب اعتماد إلي رئيس لجنة المنع', N'SendRequestBlockFromSecretaryToBlockManger', 11, 1, 1, 1, 1, 16, N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2023, N'SendRequestBlockFromMonafasatAdminToBlockSecrtary', N'عمليات مرفوضة من رئيس لجنة المنع', N'SendRequestBlockFromMonafasatAdminToBlockSecrtary', 26, 1, 1, 1, 1, 16, N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد', N'عزيزنا المستخدم, نود تنبيهكم بوجود عمليات بحاجة الى اعتماد')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2024, N'sendRejectBlockToMonafasatAdmin', N'ارسال طلب رفض المنع', N'sendRejectBlockToMonafasatAdmin', 12, 1, 1, 1, 1, 16, N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2025, N'AppoveBlockFromMangerToAccountMangerSpecialist', N'عمليات تم الموافقة عليها من رئيس لجنة المنع', N'AppoveBlockFromMangerToAccountMangerSpecialist', 38, 1, 1, 1, 1, 16, N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم اضافة المورد {1} برقم سجل {0} الي قائمة المنع')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2026, N'sendRejectBlockToSecretary', N'رفض طلب منع', N'sendRejectBlockToSecretary', 26, 1, 1, 1, 1, 16, N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}', N'عزيزنا المستخدم : نود تنبيهكم بأنة تم رفض طلب منع  المورد {1} برقم سجل {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2027, N'approveInitialAwarding', N'اعتماد الترسية المبدئي
', N'when initial awarding approval', 8, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية المبدئي للمنافسة رقم :  {0}', N'عزيزنا المستخدم، تم اعتماد الترسية المبدئي للمنافسة رقم :  {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم اعتماد الترسية المبدئي  للمنافسة رقم:: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم اعتماد الترسية المبدئي  للمنافسة رقم:: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'تم اعتماد الترسية المبدئي للمنافسة رقم :  {0}
', N'تم اعتماد الترسية المبدئي للمنافسة رقم :  {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2028, N'rejectInitialAwarding', N'رفض الترسية المبدئي', N'when initila awarding rejection', 8, 1, 1, 0, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية  المبدئي المقدم للمنافسة رقم: {0}  ', N'عزيزنا المستخدم، تم رفض تقرير الترسية  المبدئي المقدم للمنافسة رقم: {0}  ', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم رفض تقرير الترسية  المبدئي المقدم للمنافسة   
<strong>{1}</strong> <br><br>

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
      
       Initial Tender Awarding has been rejected for : <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat    </p>', N'تم رفض تقرير الترسية  المبدئي المقدم للمنافسة رقم: {0}  
', N'تم رفض تقرير الترسية  المبدئي المقدم للمنافسة رقم: {0}  
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2029, N'sendAwardingToApproveAfterInitialAwarding', N'اعتماد الترسية بعد الاعتماد المبدئي
 بانتظار  ', N'when initial awarding approval and waiting for last approval', 25, 1, 1, 1, 1, 1, N'عزيزنا المستخدم، هنالك طلب بانتظار إعتماد  الترسية  بعد الاعتماد المبدئي للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب بانتظار إعتماد  الترسية  بعد الاعتماد المبدئي للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم اعتماد الترسية المبدئي وبانتظار اعتماد  الترسية النهائي للمنافسة رقم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'
    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم اعتماد الترسية المبدئي وبانتظار اعتماد  الترسية النهائي للمنافسة رقم: <strong>{1}</strong> <br><br>

        <strong> تفاصيل المنافسة: </strong>
        <br>
        الغرض من المنافسة: {2}<br><br>
        آخر موعد لاستلام الاستفسارات: {3} <br>
        آخر موعد لاستلام العروض: {4}<br>
        تاريخ فتح المظاريف : {5} الساعة: {6} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
', N'تم اعتماد الترسية المبدئي وبانتظار اعتماد  الترسية النهائي للمنافسة رقم: {0}', N'تم اعتماد الترسية المبدئي وبانتظار اعتماد  الترسية النهائي للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2030, N'rejecttenderawarding', N'عند رفض الترسية', N'when tender awarding rejected', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم رفض تقرير الترسية المقدم للمنافسة   
<strong>{1}</strong> <br><br>

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
      
        Tender Awarding has been rejected for : <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat    </p>', N'تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}  
', N'Tender Awarding has been rejected for tender No: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2031, N'approvetenderawarding', N'اعتماد ترسية منافسة', N'when tender awarding approved', 7, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0} ', N' Dear user, Tender Awarding has been approved for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2032, N'vroAwardingWaitingApproval', N'إنتظار اعتماد الترسية ', N'when tender awarding need approval', 34, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم ارسال تقرير الترسية للاعتماد للمنافسة رقم: {0}
', N'Dear user, tender awarding sent for approval for tender number {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم إرسال تقرير الترسية لاعتماده من قبلكم: <strong>{1}</strong> <br><br>

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
      
        Tender Awarding is waiting your approval: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>', N'تم ارسال تقرير الترسية للاعتماد للمنافسة رقم: {0}
', N'Tender awarding sent for approval for tender number: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2033, N'vroApproveTenderAwarding', N'اعتماد ترسية منافسة', N'when VRO tender awarding approved', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0} ', N'عزيزنا المستخدم، تم اعتماد الترسية للمنافسة رقم: {0} ', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center">&nbsp;</p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2034, N'vroRejectTenderAwarding', N'عند رفض الترسية', N'when tender awarding rejected', 35, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N'عزيزنا المستخدم، تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}
', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم رفض تقرير الترسية المقدم للمنافسة   
<strong>{1}</strong> <br><br>

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
      
        Tender Awarding has been rejected for : <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat    </p>', N'تم رفض تقرير الترسية المقدم للمنافسة رقم: {0}  
', N'Tender Awarding has been rejected for tender No: {0}
')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2035, N'rejecttendercancellation', N'عند رفض إلغاء المنافسة', N'when tender cancellation rejected', 24, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2036, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 24, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
       ', N'Dear user, Tender cancelation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center"></p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2037, N'canceltender', N'عند إلغاء المافسة', N'When tender canceled', 23, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، نأسف لإبلاغكم أنه تم إلغاء المنافسة رقم:  {0}
       ', N'Dear user, Tender cancelation has been rejected for tender No: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N' <p style="text-align:center"></p>
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (2038, N'tenderwaitingcancelation', N'عند انتظار الغاء المنافسه', N'When tender waiting cancelation', 23, 0, 1, 1, 0, 1, N'
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7860, N'offerCheckingDateSet', N'تم إدخال تاريخ فحص العروض لمنافسة', N'Offers checking date have been set', 22, 0, 1, 1, 0, 1, N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'السلام عليكم ورحمة الله وبركاته

عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}

تاريخ فحص العروض: {1}', N'السلام عليكم ورحمة الله وبركاته

عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}

تاريخ فحص العروض: {1}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}', N'عزيزنا المستخدم، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7870, N'ApproveExtendOffers', N'اعتماد طلب تمديد سريان العروض', N'Approve Extend Offers', 24, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}', N'عزيزنا المستخدم، تم اعتماد طلب تمديد سريان العروض للمنافسة رقم {0} ', N'Dear User, the request to extend the validity of offers for Competition {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7871, N'PlaintStoppingPeriodEnd', N'تم إنتهاء فترة التوقف', N'Plaint stopping period has been ended', 24, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأنه تم إنتهاء فترة  توقف  الترسية المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأنه تم إنتهاء فترة  توقف  الترسية المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم إنتهاء فترة  توقف  الترسية للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7872, N'AcceptExtendOffersValidityBySupplier', N'الموافقة على طلب تمديد سريان العروض', N'Accept Extend Offers Validity ', 23, 1, 1, 1, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'  Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'  Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" قبل  تمديد سريان العروض للمنافسة رقم: ({1})', N'  Dear user, we would like to inform you that the supplier "{0}" before the validity of the offers for competition no: {{1})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7873, N'RejectExtendOffersValidityBySupplier', N'رفض طلب تمديد سريان العروض', N'Reject Extend Offers Validity ', 24, 1, 1, 1, 1, 16, N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})', N'عزيزنا المستخدم، نود ابلاغكم ان المورد "{0}" رفض تمديد سريان العروض للمنافسة رقم: ({1})', N'Dear User, we would like to inform you that the supplier "{0}" has declined to extend the validity of the offers to the competition: {{1})')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7874, N'SendExtendOffersToApprove', N'ارسال طلب تمديد سريان العروض للاعتماد', N'Send extend offers validity to approve', 23, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'  Dear User, the request to extend the validity of offers for accreditation to Competition {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'  Dear User, the request to extend the validity of offers for accreditation to Competition {0}', N'عزيزنا المستخدم، تم ارسال طلب تمديد سريان العروض للإعتماد  للمنافسة رقم  {0}', N'  Dear User, the request to extend the validity of offers for accreditation to Competition {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7875, N'RejectExtendOffers ', N'رفض طلب تمديد سريان العروض', N'Reject Extend Offers ', 24, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'  Dear User, the request to extend the validity of offers for Contest No. {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'  Dear User, the request to extend the validity of offers for Contest No. {0}', N'عزيزنا المستخدم، تم رفض اعتماد طلب تمديد سريان العروض  للمنافسة رقم {0}', N'  Dear User, the request to extend the validity of offers for Contest No. {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7876, N'PlaintEsclationCreated', N'تم إنشاء طلب تصعيد تظلم', N'Plaint esclation request has been created', 24, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تصعيد تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تصعيد تظلم من أحد الموردين على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7877, N'PlaintSentToApproval', N'تم إرسال طلب التظلم للإعتماد', N'Plaint request sent to approved', 23, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأن هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  نود تنبيهكم بأن هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم بانتظار الاعتماد للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7878, N'PlaintRequestCreated', N'تم إنشاء طلب تظلم', N'Plaint request has beaan created', 24, 0, 1, 1, 0, 16, N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم  {0},                </p>      <p style="text-align:right">                  عزيزنا المستخدم, نود تنبيهكم بأن هنالك طلب تظلم من مقدم  أحد الموردين على المنافسة رقم: <strong>{1}</strong> <br><br>                 <br><br>          فريق عمل منافسات      </p>', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}', N'عزيزنا المستخدم، هنالك طلب تظلم من أحد الموردين على المنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7880, N'vendorsubmitoffer', N'ارسال عرض ', N'Submit Offer', 1, 0, 1, 1, 1, 1, N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'عميلنا العزيز، نحيطكم علماً أنه تم استلام عرض على المنافسة رقم:{0}', N'تنبيهات منافسات', N'Monafasat Notification', N'<p style="text-align:center">&nbsp;</p> 
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
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7881, N'SendFirstNegotitionToApprove', N'ارسال طلب التفاوض المرحلة الأولى للاعتماد', N'Send first negotiation to approve', 23, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، هنالك طلب  تفاوض بانتظار الاعتماد لمنافسة رقم: {0}', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7882, N'ApprovePlaintRequest', N'اعتماد طلب تظلم', N'Approve plaint request', 8, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب تظام للمنافسة رقم {0} ', N'Dear User, the plaint request for Competition {0} is approved', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the request of plaint for Competition {0} is approved', N'عزيزنا المستخدم، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the plaint request  for Competition {0} is approved')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7883, N'ApprovePlaintRequest', N'الرد على طلب تظلم', N'Approve plaint request', 24, 0, 1, 0, 1, 16, N'عزيزنا المستخدم، تم اعتماد طلب تظام للمنافسة رقم {0} ', N'Dear User, the plaint request for Competition {0} is approved', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا المستخدم، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the request of plaint for Competition {0} is approved', N'عزيزنا المستخدم، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the plaint request  for Competition {0} is approved')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7884, N'ApprovePlaintRequest', N'الرد على طلب تظلم', N'Approve plaint request', 4, 0, 1, 0, 1, 16, N'عزيزنا العميل، تم الرد علي طلب تظام للمنافسة رقم {0} ', N'Dear User, the plaint request for Competition {0} is approved', N'تنبيهات منافسات', N'Monafasat Notification', N'عزيزنا العميل، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the request of plaint for Competition {0} is approved', N'عزيزنا العميل، تم اعتماد طلب تظلم للمنافسة رقم {0} ', N'Dear User, the plaint request  for Competition {0} is approved')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7885, N'PlaintApproved', N'تم قبول التظلم', N'Plaint request has been approved', 24, 0, 1, 1, 0, 16, N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7886, N'PlaintRejected', N'تم رفض التظلم', N'Plaint request has been rejected', 24, 0, 1, 1, 0, 16, N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        عزيزنا المستخدم, نود تنبيهكم بأنه تم رفض طلب التظلم المنافسة رقم: <strong>{1}</strong> <br><br>
     
        <br><br>
        فريق عمل منافسات
    </p>
', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}', N'عزيزنا المستخدم،  تم رفض طلب التظلم  للمنافسة رقم: {0}')
GO
INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn]) VALUES (7887, N'AddNewRound', N'تم اضافة جولة للمزايدة', N'New Round has been added', 4, 0, 1, 0, 1, 1, N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'عزيزنا المستخدم، تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'تنبيهات منافسات', N'Monafasat Notification', N'

    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود انتباهكم أنه تم قبول تقرير العرض الفني للمنافسة: <strong>{1}</strong> <br><br>

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
      
        Technical Evaluation for following tender has been approved: <strong>{1}</strong> <br><br>

        <strong> Details: </strong>
        <br>
        Purpose: {2}<br><br>
        Last Date to accept vendors questions: {3}<br>
        Closing Date: {4}<br>
        Bid Opening Date: {5} Time: {6} <br>
        
        
        <br><br>
        EGP Team Monafasat
    </p>
', N'تم قبول تقرير العرض الفني للمنافسة رقم: {0}', N'تم قبول تقرير العرض الفني للمنافسة رقم: {0}')
GO
