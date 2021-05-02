
update LookUps.NotificationOperationCode set EmailBodyTemplateAr = N'    <p style="text-align:center">&nbsp;</p>
    <p style="text-align:right">
        
                عزيزنا المستخدم : {0},
        
    </p>
    <p style="text-align:right">
      
        هنالك طلب إلغاء بانتظار الإعتماد لدعوة التأهيل رقم : {1} <br />    
        <br><br>
        فريق عمل منافسات
    </p>' where NotificationOperationCodeId = 2007