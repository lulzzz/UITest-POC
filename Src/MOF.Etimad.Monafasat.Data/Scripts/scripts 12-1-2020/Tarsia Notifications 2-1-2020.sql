  
 update LookUps.NotificationOperationCode set EmailBodyTemplateAr = N'<div dir="rtl">
<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة ({3}) رقم ({4}).<br><br>
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
	<br>
	(قرار الترسية لا يرتب أي التزام قانوني أو مالي على الجهة الحكومية إلا بعد توقيع العقد من جميع الأطراف)
	<br>
	فريق عمل منصة اعتماد
</p>
</div>',
EmailBodyTemplateEn =
 N'<div dir="rtl">
<p style="text-align:center">&nbsp;</p>
<p style="text-align:right">
	السادة / {0},<br>
	السلام عليكم ورحمة الله وبركاته ...
</p>
<p style="text-align:right">
	) إشارة إلى عرضكم رقم{1}) بتاريخ ({2}) المقدم :<br>
	للمنافسة ({3}) رقم ({4}).<br><br>
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
	<br>
	(قرار الترسية لا يرتب أي التزام قانوني أو مالي على الجهة الحكومية إلا بعد توقيع العقد من جميع الأطراف)
	<br>
	فريق عمل منصة اعتماد
</p>
</div>'where NotificationOperationCodeId = 747  
 
 insert into LookUps.NotificationCategory values (18, N'منتجات القائمة الإلزامية','Mandatory list products');

INSERT [LookUps].[NotificationOperationCode] ([NotificationOperationCodeId], [OperationCode], [ArabicName], [EnglishName], [UserRoleId], [DefaultSMS], [DefaultEmail], [CanEditEmail], [CanEditSMS], [NotificationCategoryId], [SmsTemplateAr], [SmsTemplateEn], [EmailSubjectTemplateAr], [EmailSubjectTemplateEn], [EmailBodyTemplateAr], [EmailBodyTemplateEn], [PanelTemplateAr], [PanelTemplateEn], [CreatedAt], [CreatedBy], [IsActive], [UpdatedAt], [UpdatedBy]) VALUES (990, N'approvaloperation', N'إعتماد', N'Approval operation', 42, 1, 1, 1, 1, 18, N'عزيزنا المستخدم، فضلاً استخدم كود التفعيل: {0}', N'
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
 

