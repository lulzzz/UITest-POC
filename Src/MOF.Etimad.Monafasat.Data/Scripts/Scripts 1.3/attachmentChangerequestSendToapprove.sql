select * from LookUps.NotificationOperationCode where OperationCode = 'approvetenderattachment'



update  LookUps.NotificationOperationCode set PanelTemplateAr = N'هنالك طلب تعديل جديد على المرفقات بانتظار الاعتماد للمنافسة رقم:{0}', PanelTemplateEn = N'The attachments has been modified for the tender No.: {0}'
, EmailBodyTemplateAr = N'<p style="text-align:center">&nbsp;</p>
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
</p>',
EmailBodyTemplateEn = N'    <p style="text-align:center">&nbsp;</p>
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
    </p>'

 where NotificationOperationCodeId in (45,916)
