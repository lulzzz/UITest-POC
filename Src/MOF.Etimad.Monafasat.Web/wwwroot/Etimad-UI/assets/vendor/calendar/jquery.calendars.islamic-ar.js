/* http://keith-wood.name/calendars.html
   Arabic localisation for Islamic calendar for jQuery v2.1.0.
   Written by Keith Wood (wood.keith{at}optusnet.com.au) August 2009.
   Updated by Fahad Alqahtani April 2016. */
(function ($) {
    $.calendars.calendars.islamic.prototype.regionalOptions['ar'] = {
        name: 'UmmAlQura', // The calendar name
        epochs: ['BAM', 'AM'],
        dateFormat: 'dd/mm/yyyy', // See format options on BaseCalendar.formatDate
        firstDay: 6,
        isRTL: true,
        monthNames: ['محرّم.1', 'صفر.2', 'ربيع الأول.3', 'ربيع الثاني.4', 'جمادى الأولى.5', 'جمادى الآخرة.6', 'رجب.7', 'شعبان.8', 'رمضان.9', 'شوال.10', 'ذو القعدة.11', 'ذو الحجة.12'],
        monthNamesShort: ['محرّم.1', 'صفر.2', 'ربيع الأول.3', 'ربيع الثاني.4', 'جمادى الأولى.5', 'جمادى الآخرة.6', 'رجب.7', 'شعبان.8', 'رمضان.9', 'شوال.10', 'ذو القعدة.11', 'ذو الحجة.12'],
        dayNames: ['الأحد', 'الاثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
        dayNamesShort: ['أحد', 'اثنين', 'ثلاثاء', 'أربعاء', 'خميس', 'جمعة', 'سبت'],
        //dayNamesMin: ['أ', 'ا', 'ث', 'أ', 'خ', 'ج', 'س'],
        dayNamesMin: ['الأحد', 'الاثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
    };
})(jQuery);
