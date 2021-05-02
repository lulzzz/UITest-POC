
function startOTPTimer(timeSelector, resendBtnId) {


   var presentTime = document.getElementById(timeSelector).innerHTML;
   var timeArray = presentTime.split(/[:]+/);
   var minutes = timeArray[0];
   var seconds = getOTPSecondsString(timeArray[1] - 1);
   if (seconds == 59) {
      minutes = minutes - 1
   }

   document.getElementById(timeSelector).innerHTML = minutes + ":" + seconds; //Updating the HTML

   if (minutes <= 0 && seconds <= 0) {
      //document.getElementById(timeSelector).innerHTML = "0:00";
      $(resendBtnId).prop("disabled", false);
      clearTimeout(timeout);

      return false;
   };

   var timeout = setTimeout(function () { startOTPTimer(timeSelector, resendBtnId); }, 1000); //Start the timer every 1 second
}

function calculateTime(timeSelector, resendBtnId) {

}

function getOTPSecondsString(sec) {
   if (sec < 10 && sec >= 0) {
      sec = "0" + sec
   };

   if (sec < 0) {
      sec = "59"
   };

   return sec;
}
