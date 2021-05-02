    
-- Reject Request For Extend Qualification Date Bug #68615


	update LookUps.NotificationOperationCode set EmailBodyTemplateAr = N'   <p style="text-align:center">&nbsp;</p>   
		   <p style="text-align:right">السلام عليكم  {0},</p>

		         <p style="text-align:right">
		   نود تنبيهكم بأنه تم رفض تمديد تواريخ التاهيل : <strong>{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>  
		          <strong> تفاصيل التاهيل: </strong>
				  <br><br>
				  آخر موعد لاستلام الاستفسارات: {4}<br>
				  آخر موعد لتقديم وثائق التأهيل : {5}<br>
				  تاريخ فحص و تقييم وثائق التأهيل : {8} الساعة: {9}
				  <br><br>
				  فريق عمل منافسات
				  </p>

' where NotificationOperationCodeId = 1017