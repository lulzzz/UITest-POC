/* http://keith-wood.name/calendars.html
   Arabic localisation for UmmAlQura calendar for jQuery v2.0.0.
   Written by Amro Osama March 2013. */
(function ($) {
	$.calendars.calendars.ummalqura.prototype.regionalOptions['ar'] = {
		name: 'UmmAlQura', // The calendar name
		epochs: ['BAM', 'AM'],
		//monthNames: ['المحرّم', 'صفر', 'ربيع الأول', 'ربيع الثاني', 'جمادى الاول', 'جمادى الآخر', 'رجب', 'شعبان', 'رمضان', 'شوّال', 'ذو القعدة', 'ذو الحجة'],
		//monthNamesShort: ['المحرّم', 'صفر', 'ربيع الأول', 'ربيع الثاني', 'جمادى الاول', 'جمادى الآخر', 'رجب', 'شعبان', 'رمضان', 'شوّال', 'ذو القعدة', 'ذو الحجة'],
		//dayNames: ['الأحد', 'الإثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
		//dayNamesMin: ['الأحد', 'الإثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
		//dayNamesShort: ['الأحد', 'الإثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],
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
