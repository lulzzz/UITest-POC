/* http://keith-wood.name/calendars.html
   Arabic localisation for calendars datepicker for jQuery.
   Khaled Al Horani -- خالد الحوراني -- koko.dw@gmail.com */
var arrleft = 'keyboard_arrow_left';
var arrRight = 'keyboard_arrow_right';
var tody = 'اليوم';
function getLanguage() {
   if (getCookie('language') == 'en-US') {
      arrleft = 'keyboard_arrow_right';
      arrRight = 'keyboard_arrow_left';
      tody = 'Today';
   }
}
function getCookie(cname) {
   var name = cname + "=";
   var decodedCookie = decodeURIComponent(document.cookie);
   var ca = decodedCookie.split(';');
   for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') {
         c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
         return c.substring(name.length, c.length);
      }
   }
   return "";
}
window.load = getLanguage();
(function($) {
	$.calendarsPicker.regionalOptions['ar'] = {
		renderer: $.calendarsPicker.defaultRenderer,
       prevText: '<i class="material-icons">'+ arrRight +'</i>', prevStatus: 'عرض الشهر السابق',
		prevJumpText: '&#x3c;&#x3c;', prevJumpStatus: '',
       nextText: '<i class="material-icons">'+ arrleft +'</i>', nextStatus: 'عرض الشهر القادم',
		nextJumpText: '&#x3e;&#x3e;', nextJumpStatus: '',
       currentText: tody, currentStatus: 'عرض الشهر الحالي',
       todayText: tody, todayStatus: 'عرض الشهر الحالي',
       clearText: '<i class="material-icons text-danger">delete_outline</i>', clearStatus: 'امسح التاريخ الحالي',
       closeText: '<i class="material-icons text-default">clear</i>', closeStatus: 'إغلاق بدون حفظ',
		yearStatus: 'عرض سنة آخرى', monthStatus: 'عرض شهر آخر',
		weekText: 'أسبوع', weekStatus: 'أسبوع السنة',
		dayStatus: 'اختر D, M d', defaultStatus: 'اختر يوم',
		isRTL: true
	};
	$.calendarsPicker.setDefaults($.calendarsPicker.regionalOptions['ar']);
})(jQuery);
