//import { debug } from "util";

var alertMessageType = {
   Success: 'success',
   Warning: 'warning',
   Danger: 'danger',
   Info: 'info'
};

$(window).on('load',function () {
   showAlerts();
});

function showAlerts(obj) {
   obj = obj || $(".message-box div");
   obj.each(function (i) {

      var alert = $(this);
      var timeout = alert.data("alert-message-timeout");
      if (alert.attr("class").indexOf("alert-danger") > 0) {
         alert.prepend(' <strong type="button" class="glyphicon glyphicon-exclamation-sign"></strong> ');
      }
      else if (alert.attr("class").indexOf("alert-warning") > 0) {
         alert.prepend(' <strong type="button" class="glyphicon glyphicon-warning-sign"></strong> ');
      }
      else if (alert.attr("class").indexOf("alert-success") > 0) {
         alert.prepend(' <strong type="button" class="glyphicon glyphicon-ok"></strong> ');
      }
      else if (alert.attr("class").indexOf("alert-info") > 0) {
         alert.prepend(' <strong type="button" class="glyphicon glyphicon-info-sign"></strong> ');
      }

      alert.append(' <button type="button" class="close" style="top: 8px !important;"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>');
      alert.css("width", "0");
      alert.css("opacity", "0");
      alert.show();
      alert.delay(600 * i).animate({ "width": "100%", "margin-right": "0", "opacity": "1" }, 1200, 'swing', function () {
         if (obj.index($(this)) >= obj.length - 1) closeAllHideOrShow();
      });
      if (timeout != 0) {
         setTimeout(function () {
            hideAlertMessage(alert);
         }, timeout);
      }

   });
   //closeAllHideOrShow();
}
function hideAlertMessage(alert) {
   alert.css("overflow", "hidden");
   alert.css("height", "43px");
   alert.animate({ "width": "0", "opacity": "0", "margin-right": "50%" }, 400, function () {
      $(this).remove();
      closeAllHideOrShow();
   });
}
function closeAllHideOrShow() {
   var count = $(".message-box  div.alert").length;

   if ((count) > 1) {
      $(".message-box .closeall").remove();
      $(".message-box ").append('<a href="#" class="closeall" ><span class="glyphicon glyphicon-chevron-up"></span> إخفاء الكل</a>');
   }
   else {
      $(".message-box .closeall").remove();
   }
}

function AlertFun(msg, alertMessageType, timeout) {
   $(".message-box .closeall").remove();
   timeout = (timeout == undefined ? 5 : timeout) * 1000;
   alertMessageType = alertMessageType || "warning";
   var alert = $("<div class='center-block alert alert-dismissible alert-" + alertMessageType.toLowerCase() + "'  data-alert-message-timeout='" + timeout + "'>" + msg + "</div>").appendTo(".message-box");

   showAlerts(alert);

}

$(document).on("click", ".message-box .closeall", function (e) {
   e.preventDefault();
   $(".message-box .closeall").remove();
   $(".message-box .alert").each(function (i) {
      $(this).delay(i * 100).slideUp(100, function () {
         $(this).remove();
      });
   });
});

$(document).on("click", ".message-box .close", function () {
   hideAlertMessage($(this).closest("div"));
});
