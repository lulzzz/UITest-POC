//import { debug } from "util";
function calculateCardPercentage(pubDate, lastDate, remainingDays, remainingHours) {
   var remainingtime;
   var date1;
   if (pubDate === null) {
      
      date1 = new Date();
   } else { date1 = new Date(pubDate); }
   if (remainingDays > 0) {
      remainingtime = remainingDays * 24 + remainingHours;
   } else { remainingtime = remainingHours; }
   var date2 = new Date(lastDate);
   var timeDiff = Math.abs(date1.getTime() - date2.getTime());
   var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) * 24;   
   return Math.floor(Math.ceil((remainingtime / diffDays) * 100) / 10) * 10;
}
function AlertFun(massage, type) {
   var icon = 'check';
   var animatetion;
   if (type === 'success') {
      icon = 'check';
   } else if (type === 'danger') {
      icon = 'error_outline';
      animatetion = {
         enter: 'animated bounceIn',
         exit: 'animated bounceOut'
      };
   } else {

      icon = 'warning';
      animatetion = {
         enter: 'animated bounceIn',
         exit: 'animated bounceOut'
      };
   }
   var notify = $.notify(massage, {
      type: type,
      allow_dismiss: true,
      delay: 5000,
      mouse_over: 'pause',
      animate: animatetion,
      placement: { from: 'top', align: 'center' },
      template: '<div data-notify="container" class="col-xs-12 alert alert-{0}" role="alert"><div class="container"><div class="alert-icon"><i class="material-icons" > ' + icon + '</i></div><button type="button" aria-hidden="true" class="close" data-notify="dismiss"><span aria-hidden="true"><i class="material-icons">clear</i></span></button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div><a href="{3}" target="{4}" data-notify="url"></a></div></div>'

   });
}
$(window).load(function () {
   $('#LoadingSite').fadeOut('slow', function () { $(this).hide(); });
});
$('form').attr('autocomplete', 'off');
$('input').attr('autocomplete', 'off');
var prevScrollpos = window.pageYOffset;
window.onscroll = function () {
   var currentScrollPos = window.pageYOffset;
   if (window.pageYOffset > 50) {
      if (prevScrollpos > currentScrollPos) {
         document.getElementById("sectionsNav").style.top = "0";
      } else {
         document.getElementById("sectionsNav").style.top = "-76px";
      }
      prevScrollpos = currentScrollPos;
   }
}
if (parseInt($('.etd-notification-btn span').text()) > 0) {
   $('.etd-notification-btn').addClass('infinite delay-3s shake');
}
$('button[type="reset"]').click(function () {
   $(".selectpicker").val('default');
   $(".selectpicker").selectpicker("refresh");
});
$(document).click(function () {
   setTimeout(function () {
      if (window.pageYOffset >= 0) {
         document.getElementById("sectionsNav").style.top = "0";
      }
   }, 1200);
});
$('[data-toggle="tooltip"]').click(function () {
   $('body').find('.tooltip').remove()
});
$(document).ready(function () {
   if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
      $('.selectpicker').selectpicker('mobile');
   }
});
$('input[type="number"]').keyup(function (e) {
   console.log($(this)[0].validity.badInput);
   if ($(this)[0].validity.badInput) {
      $(this).val('');
   }
});
$('input[type="number"]').keydown(function (e) {
   // Allow: backspace, delete, tab, escape, enter and .
   if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
      // Allow: Ctrl/cmd+A
      (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
      // Allow: Ctrl/cmd+C
      (e.keyCode === 67 && (e.ctrlKey === true || e.metaKey === true)) ||
      // Allow: Ctrl/cmd+X
      (e.keyCode === 88 && (e.ctrlKey === true || e.metaKey === true)) ||
      // Allow: home, end, left, right
      (e.keyCode >= 35 && e.keyCode <= 39)) {
      // let it happen, don't do anything
      return;
   }
   // Ensure that it is a number and stop the keypress
   if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
      e.preventDefault();
   }
});
