
-- Request For Extend Qualification Date Bug #68610

 update LookUps.NotificationOperationCode set EmailBodyTemplateAr = N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                السلام عليكم  {0},
        
    </p>
    <p style="text-align:right">
      
        نود تنبيهكم بأن هنالك طلب تمديد بانتظار الإعتماد {1} <br />
        <strong> لدعوة التأهيل: </strong>{2} <br><br>
       <strong> بحيث ستصبح مواعيد دعوة التأهيل كالتالي: </strong>
        <br>
        آخر موعد لاستلام الاستفسارات: {3}<br>
        آخر موعد لتقديم وثائق التاهيل : {4}<br>
        تاريخ فحص التاهيل : {7} الساعة: {8} <br>
        
        
        <br><br>
        فريق عمل منافسات
    </p>
 q
' where NotificationOperationCodeId = 2039