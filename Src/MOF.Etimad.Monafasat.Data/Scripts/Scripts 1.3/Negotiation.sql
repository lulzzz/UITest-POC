update LookUps.NotificationOperationCode set EmailBodyTemplateAr = 

N'       <p style="text-align:center">&nbsp;</p> 
    <p style="text-align:right">                     عزيزنا المستخدم     </p> 
    <p style="text-align:right">                نود ابلاغكم ان المورد  <br />         <strong> {0}</strong >     
</p>
    <p style="text-align:right">  قبل تخفيض العرض للمنافسة رقم   <strong> {1}</strong><br><br>                 وتقبلوا وافر تحياتنا،،          <br>         فريق عمل منافسات     </p>      
'
where NotificationOperationCodeId in ( 1039,1037)

go

update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N'    <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">                            السلام عليكم 
<br />{0},                </p>      
<p style="text-align:right">                  نود تنبيهكم بأنه تم رفض عملية إلغاء التأهيل:
<strong><br />{1}</strong> <br><br>  للأسباب:  <br><br>  {2}  <br><br>         <strong> تفاصيل التأهيل: </strong>         <br><br>          آخر موعد لاستلام الاستفسارات: {3}<br>          آخر موعد لاستلام العروض: {4}<br>          تاريخ فحص وثائق التأهيل : {5}   {6} <br>                              <br><br>          فريق عمل منافسات      </p>' 


where NotificationOperationCodeId in  (2011,
2012,
2013)
go


update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N' 
  <p style="text-align:center">&nbsp;</p>         <p style="text-align:right">             <span style="font-family:arial,helvetica,sans-serif;font-size:12px">                   <span style="font-family:arial,helvetica,sans-serif;font-size:12px">        السادة {0}                                      السلام عليكم ورحمة الله,             </span>         </span>     </p>     <p style="text-align:right">                        إعلان تمديد تواريخ تاهيل صادر عن {1} – {2} :                  <br><br>                 إسم التاهيل: <strong>{3}</strong><br>         رقم التاهيل: {4}<br>              <strong>{5}</strong>          <br><br>                    آخر موعد لاستلام الاستفسارات: {6}<br>         آخر موعد لاستلام العروض: {7}<br>                              تاريخ ووقت تقييم وثائق التأهيل: {8} الساعة: {9}     <br>          <br><br>                   <br><br>                 وتقبلوا وافر تحياتنا،،          <br><br>         فريق عمل منافسات    </p>
' 


where NotificationOperationCodeId in  (950)

go
update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N'  <p style="text-align:center">&nbsp;</p>     <p style="text-align:right">                          السلام عليكم ورحمة الله  {0},              </p>     <p style="text-align:right">                نود تنبيهكم بأنه تم التعديل على الملفات الخاصة بمستندات التأهيل<br />         <strong> إسم التأهيل: </strong>{1}         <br>         <strong> رقم التأهيل: </strong>{2}         <br><br>                 وتقبلوا وافر تحياتنا،،          <br>         فريق عمل منافسات     </p>      
' 


where NotificationOperationCodeId in  (2000)
go


update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N' 
       <p style="text-align:center"> </p>     <p style="text-align:right">

عزيزنا المستخدم  :
	   هنالك طلب تأجيل تقديم العرض من أحد الموردين على<br />           دعوة تأهيل رقم :{0}         <br>      </p>   </p>     
	   ' 


where NotificationOperationCodeId in  (953)

go

update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N' 
       
  <p style="text-align:center"> </p>     <p style="text-align:right">

: عزيزنا المستخدم 
<br>  	  

 هنالك طلب تأجيل تقديم وثائق التأهيل  من أحد الموردين على<br />           : دعوة تأهيل رقم 

<br>  {0}         <br>      </p>   </p>     
<p><br>         فريق عمل منافسات    </p>
' 


where NotificationOperationCodeId in  (1101,1100)

go
 

update  LookUps.NotificationOperationCode  set 
EmailBodyTemplateAr = 
N' 
     <p style="text-align:right">        
 
 ،عزيزنا المستخدم           
 
  </p>   
  <p style="text-align:right">                  
نود تنبيهكم بوجود طلب
 تخفيض بحاجة للإعتماد لمنافسة رقم 
    <br />
{0}
   </p> 
   
' 
, PanelTemplateAr = N'نود تنبيهكم بوجود طلب
 تخفيض بحاجة للإعتماد لمنافسة رقم 
  {0} '
,  PanelTemplateEn = N'نود تنبيهكم بوجود طلب
 تخفيض بحاجة للإعتماد لمنافسة رقم 
  {0} '
, SmsTemplateAr = N'نود تنبيهكم بوجود طلب
 تخفيض بحاجة للإعتماد لمنافسة رقم 
  {0} '
, SmsTemplateEn = N'نود تنبيهكم بوجود طلب
 تخفيض بحاجة للإعتماد لمنافسة رقم 
  {0} '
  ,
  UserRoleId = 7

where NotificationOperationCodeId in  (1023, 1024)