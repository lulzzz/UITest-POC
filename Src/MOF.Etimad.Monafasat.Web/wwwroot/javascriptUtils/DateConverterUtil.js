function convertUmmalquraToGregorian(hijriDate) {
 
   var from = 'ummalqura';
   var to = 'gregorian';
   var calendarFrom = $.calendars.instance(from);
   var calendarTo = $.calendars.instance(to);
   if (hijriDate) {
      try { calendarFrom.parseDate("dd/mm/yyyy", hijriDate) }
      catch { return null; }
      var jdDate = calendarFrom.parseDate("dd/mm/yyyy", hijriDate).toJD();
      var gregorianDate = calendarTo.formatDate('mm/dd/yyyy', calendarTo.fromJD(jdDate));
   }
   return gregorianDate;
}
  
function convertGregorianToUmmalqura(gregorianDate) {
   var from = 'gregorian';
   var to = 'ummalqura';
   var calendarFrom = $.calendars.instance(from);
   var calendarTo = $.calendars.instance(to);
   if (gregorianDate) {
      var jdDate = calendarFrom.parseDate("dd/mm/yyyy", gregorianDate).toJD();
      var hijriDate = calendarTo.formatDate('dd/mm/yyyy', calendarTo.fromJD(jdDate));
   }
   return hijriDate;
}

function dateToString(dateValue) {
   var day = dateValue.getDate() > 9 ? dateValue.getDate() : '0' + dateValue.getDate();
   var month = dateValue.getMonth() > 8 ? (dateValue.getMonth() + 1) : '0' + (dateValue.getMonth() + 1);
   return day + '/' + month + '/' + dateValue.getFullYear();
}
