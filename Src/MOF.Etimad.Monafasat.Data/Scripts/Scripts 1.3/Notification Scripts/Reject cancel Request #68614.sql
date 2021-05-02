
	update LookUps.NotificationOperationCode set EmailBodyTemplateAr = N'   <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأنه تم رفض عملية إلغاء التأهيل رقم : <strong>{1}</strong> <br><br>
  للأسباب :   {2}
        <br><br>
        فريق عمل منافسات
    </p> ' where NotificationOperationCodeId = 2001