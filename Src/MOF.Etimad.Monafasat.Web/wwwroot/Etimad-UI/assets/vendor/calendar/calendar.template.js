$(window).on('load',function () {
	datePickerInit();
});

function getCalendarName(element) {
	var isGregorian = $(this).hasClass('datepicker-gregorian');
	var isHijri = $(this).hasClass('datepicker-hijri');

	if (isGregorian) return 'gregorian';
	if (isHijri) return 'islamic';
	else return 'ummalqura';
}

function getConvertCalendarName(element) {
	var from = element.data('calendar');
	if (from == 'islamic' || from == 'ummalqura') { return 'gregorian'; }
	else { return element.hasClass('datepicker-hijri') ? 'islamic' : 'ummalqura'; }
}

function datePickerInit(selecter) {
	selecterAr = selecter ? selecter + '.datepicker' : '.datepicker';
	var selecterMix = selecter ? selecter + '.datepicker-mix' : '.datepicker-mix';
	var isMix, isEnglish, calendarName;

	$(selecterAr + ',' + selecterMix).each(function () {
		var range = $(this).attr('data-year-range') || 'c-20:c+20';
		isMix = $(this).hasClass('datepicker-mix');
		isEnglish = $(this).hasClass('datepicker-en');
		calendarName = getCalendarName(this);

		$(this).data('calendar', calendarName);

        //Yahya: handle min and max dates:       
        var minDate = $(this).attr('min-date');
        var maxDate = $(this).attr('max-date');
       
        if (minDate && maxDate)
        {
            var calendar = $.calendars.instance(calendarName, isEnglish ? 'en' : 'ar');
            var yearRange = calendar.parseDate("dd/mm/yyyy", minDate)._year + ':' + calendar.parseDate("dd/mm/yyyy", maxDate)._year;
          
            $(this).calendarsPicker({
                calendar: $.calendars.instance(calendarName, isEnglish ? 'en' : 'ar'),
                dateFormat: 'dd/mm/yyyy',
                yearRange: yearRange,
                showOtherMonths: true,
                showTrigger: '#calImg',
                minDate: minDate,
                maxDate: maxDate,
                defaultDate: defaultDate,
                selectDefaultDate: true
            });
        }
        else
        {
            $(this).calendarsPicker({
                calendar: $.calendars.instance(calendarName, isEnglish ? 'en' : 'ar'),
                dateFormat: 'dd/mm/yyyy',
                yearRange: range,
                showOtherMonths: true,
                showTrigger: '#calImg'               
            });
        }
		
        // Hijri And Melady CheckBox
       if (isMix) {
         
			if (!$(this).parent().find('div.checkbox_appended').length) {
               var div = $('<label />', {
                  'class': 'checkbox checkbox-primary checkbox_appended  pull-right form-check-label', 'style': 'margin-bottom:-26px;bottom: -9px;left: -17px' });
               var input = $('<input />', {
                  type: 'checkbox', 'class': 'form-check-input', name: 'cb_' + $(this).attr("name"), id: 'cb_' + $(this).attr("id"), value: 'gregorian'
               });
               var spansign = $('<span/>', { 'class': 'form-check-sign' })
               var spancheck = $('<span/>', { 'class': 'check' })
               spansign.append(spancheck);
				if (calendarName == 'gregorian') input.attr('chaecked', null);
               div.append(input);
               div.append(gorgianStr);
         

               div.append(spansign)
                $(this).closest('.form-group').find('label').after(div);
			}
		}
	});
}
var gorgianStr = 'ميلادي';
function getLanguage() {
   if (getCookie('language') == 'en-US') {
      gorgianStr = 'Gorgian';
   } else {
      gorgianStr = 'ميلادي';
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
$(document).on("change", ".checkbox_appended input:checkbox", function () {

   var textBox = $(this).parent().parent().find(".datepicker-mix");
   var from = textBox.data('calendar');
   var to = getConvertCalendarName(textBox);
   //var range = $(this).calendarsPicker('option', 'yearRange');
   var range = textBox.attr('data-year-range') || '1400:c+20';
   var yearRange = range.split(':');
   var newYearRange = '';
   isEnglish = textBox.hasClass('datepicker-en');
   var calendarFrom = $.calendars.instance(from);
   var calendarTo = $.calendars.instance(to);

   try {

      if (textBox.val()) {
         var jdDate = calendarFrom.parseDate("dd/mm/yyyy", textBox.val().trim()).toJD();
         textBox.val(calendarTo.formatDate('dd/mm/yyyy', calendarTo.fromJD(jdDate)));
        
      }

      //Yahya: handle min and max dates:
      var minDate = textBox.attr('min-date');
      var maxDate = textBox.attr('max-date');

      if (minDate && maxDate) {
         var jdDate = calendarFrom.parseDate("dd/mm/yyyy", minDate).toJD();
         minDate = calendarTo.formatDate('dd/mm/yyyy', calendarTo.fromJD(jdDate));
         textBox.attr('min-date', minDate);

         var jdDate = calendarFrom.parseDate("dd/mm/yyyy", maxDate).toJD();
         maxDate = calendarTo.formatDate('dd/mm/yyyy', calendarTo.fromJD(jdDate));
         textBox.attr('max-date', maxDate);

         newYearRange = calendarTo.parseDate("dd/mm/yyyy", minDate)._year + ':' + calendarTo.parseDate("dd/mm/yyyy", maxDate)._year;


         textBox.calendarsPicker('option', 'minDate', minDate);
         textBox.calendarsPicker('option', 'maxDate', maxDate);
      }
      else {
         for (var i = 0; i < yearRange.length; i++) {
            if (!yearRange[i].match('c[+-].*')) {
               var currentYear = parseInt(yearRange[i]);
               if (from != "gregorian")
                  currentYear++;

               var currentDateJDFrom = calendarFrom.newDate(currentYear, calendarFrom.firstMonth, calendarFrom.minDay).toJD();
               var yearTo = calendarTo.formatDate('yyyy', calendarTo.fromJD(currentDateJDFrom));
               yearRange[i] = yearTo;
            }
         }

         newYearRange = yearRange[0] + ':' + yearRange[1];
      }

      textBox.attr('data-year-range', newYearRange);
      textBox.data('calendar', to);
      textBox.calendarsPicker('option', { calendar: $.calendars.instance(to, isEnglish ? 'en' : 'ar'), yearRange: newYearRange });

   }
   catch (eee) {

      textBox.val("");
   }
});
