
  update LookUps.NotificationOperationCode set 
  EmailBodyTemplateAr = N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عزيزنا المستخدم {0}، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {1}
    </p>
    <br />
    <p style="text-align:right">
        تاريخ فحص العروض: {2}
    </p>
</div>',
  EmailBodyTemplateEn = N'<div dir="rtl">
    <p style="text-align:center">
        السلام عليكم ورحمة الله وبركاته
    </p>
    <br />
    <p style="text-align:right">
        عزيزنا المستخدم {0}، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {1}
    </p>
    <br />
    <p style="text-align:right">
        تاريخ فحص العروض: {2}
    </p>
</div>',
SmsTemplateAr = N'عزيزنا المستخدم{0}، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {1}',
SmsTemplateEn = N'عزيزنا المستخدم{0}، تم إدخال تاريخ ووقت فحص العروض للمنافسة رقم {1}'

  
  where NotificationOperationCodeId in (785, 7860)
  go