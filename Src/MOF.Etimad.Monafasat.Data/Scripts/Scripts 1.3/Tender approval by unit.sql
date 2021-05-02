insert into LookUps.TenderAction values (301, N' بانتظار اعتماد مكتب تحقيق الرؤية ', 'Pending VRO Auditer Approval')

update LookUps.NotificationOperationCode set  
EmailBodyTemplateAr= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق <br><br>       <br><br>       فريق عمل منافسات     </p>   ',
EmailBodyTemplateEn= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، تم اعتماد المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق <br><br>       <br><br>       فريق عمل منافسات     </p>  ',
PanelTemplateAr = N' تم اعتماد المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق', PanelTemplateEn = N' تم اعتماد المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق' where OperationCode = 'approveTenderByUnit'

update LookUps.NotificationOperationCode set  
EmailBodyTemplateAr= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق <br><br>       <br><br>       فريق عمل منافسات     </p>   ',
EmailBodyTemplateEn= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، تم رفض المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق <br><br>       <br><br>       فريق عمل منافسات     </p>  ',
PanelTemplateAr = N' تم رفض المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق', PanelTemplateEn = N' تم رفض المنافسة رقم: {0} من قبل مركز كفاءة الإنفاق' where OperationCode = N'rejectTenderByUnit'

update LookUps.NotificationOperationCode set  
EmailBodyTemplateAr= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">  عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0}   <br><br>       <br><br>       فريق عمل منافسات     </p>   ',
EmailBodyTemplateEn= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">  عزيزنا المستخدم، هناك طلب اعتماد للمنافسة رقم: {0}   <br><br>       <br><br>       فريق عمل منافسات     </p>   ',
PanelTemplateAr = N'  هناك طلب اعتماد للمنافسة رقم: {0}', PanelTemplateEn = N'   هناك طلب اعتماد للمنافسة رقم: {0}' where NotificationOperationCodeId = 686

 
update LookUps.NotificationOperationCode set  
CanEditEmail = 1,
EmailBodyTemplateAr= N' <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} <br><br>       <br><br>       فريق عمل منافسات     </p>  ',
EmailBodyTemplateEn= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، هناك طلب تعديل للمنافسة رقم: {0} <br><br>       <br><br>       فريق عمل منافسات     </p>  ',
PanelTemplateAr = N'  هناك طلب تعديل للمنافسة رقم: {0}', PanelTemplateEn = N'  هناك طلب تعديل للمنافسة رقم: {0}' where OperationCode = N'editTenderFromUnit'
 

 
update LookUps.NotificationOperationCode set  
SmsTemplateAr = N'عزيزنا المستخدم، هنالك منافسة تحتاج الى المراجعة رقم: {0} ', 
SmsTemplateEn = N'عزيزنا المستخدم، هنالك منافسة تحتاج الى المراجعة رقم: {0} ',
EmailBodyTemplateAr= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم،  هنالك منافسة تحتاج الى المراجعة رقم: {0} <br><br>       <br><br>       فريق عمل منافسات     </p>   ',
EmailBodyTemplateEn= N'  <p style="text-align:center">&nbsp;</p>      <p style="text-align:right">           </p>      <p style="text-align:right">               عزيزنا المستخدم، هنالك منافسة تحتاج الى المراجعة رقم: {0} <br><br>       <br><br>       فريق عمل منافسات     </p>  ',
PanelTemplateAr = N'  هنالك منافسة تحتاج الى المراجعة رقم: {0} ', PanelTemplateEn = N' هنالك منافسة تحتاج الى المراجعة رقم: {0}' where OperationCode = N'reviewTender'
