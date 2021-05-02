function PrepareModel(id, agencyCode) {
   obj.announcementIdString = id;
   obj.agencyCode = agencyCode;
}
$(document).ready(function () {
   setTimeout(function () {
      updateAgencyLogos(obj.agencyCode);
   }, 1000);
});
obj = new Vue({
   el: '#actionsDiv',
   data: {
      announcementIdString: '',
      agencyCode: ''
   },
   methods: {
      ShowApprovalData: function () {
         document.getElementById('timer').innerHTML = 05 + ":" + 00;
         $("#btnResendVerificationCode").prop("disabled", true);

         startTimer()
         createVerificationCode();
         modelContentObj.approveAction = 'true';
         modelButtonsObj.approveAction = 'true';
         modelContentObj.rejectAction = 'false';
         modelButtonsObj.rejectAction = 'false';
      },
      ShowRejectionData: function () {
         modelContentObj.approveAction = 'false';
         modelButtonsObj.approveAction = 'false';
         modelContentObj.rejectAction = 'true';
         modelButtonsObj.rejectAction = 'true';
      },
      SendAnnouncementForApproval: function () {
         $('#LoadingSite').fadeIn();
         debugger
         axios.get("SendAnnouncementForApprovalAsync?announcementIdString=" + this.announcementIdString)
            .then(res => {
               $('#LoadingSite').fadeOut();
               obj.SuccessMessage("تم الإرسال بنجاح");
               window.location = '/Announcement/AllAgencyAnnouncements'
            })
            .catch(error => {
               $('#LoadingSite').fadeOut();
               obj.ErrorMessage(error.response.data.message);
            });

      },
      ReopenAnnouncement: function () {
         $('#LoadingSite').fadeIn();
         axios.get("ReopenAnnouncementAsync", {
            params: {
               announcementIdString: this.announcementIdString,
            }
         })
            .then(res => {
               $('#LoadingSite').fadeOut();
               obj.SuccessMessage("تمت العملية بنجاح");
               window.location = '/Announcement/CreateAnnouncement?announcementIdString=' + this.announcementIdString
            })
            .catch(error => {
               $('#LoadingSite').fadeOut();
               obj.ErrorMessage(error.response.data.message);
            });
      },
      SuccessMessage: function (message) {
         AlertFun(message, alertMessageType.Success);
      },
      ErrorMessage: function (message) {
         AlertFun(message, alertMessageType.Danger);
      }
   },
})

modelContentObj = new Vue({
   el: "#modelContenetDiv",
   data: {
      approveAction: 'true',
      rejectAction: 'false',
      verificationCode: '',
      rejectionReason: '',
   },

})

modelButtonsObj = new Vue({
   el: "#modelbuttonsDiv",
   data: {
      approveAction: 'true',
      rejectAction: 'false'
   },
   methods: {
      ResendVerificationCode: function () {
         document.getElementById('timer').innerHTML = 05 + ":" + 00;
         $("#btnResendVerificationCode").prop("disabled", true);
         startTimer()
         createVerificationCode()
      },
      ApproveAnnouncement: function () {
         if (modelContentObj.verificationCode == '') {
            return false;
         }
         $('#LoadingSite').fadeIn();
         axios.get("ApproveAnnouncementAsync", {
            params: {
               IdString: obj.announcementIdString,
               VerificationCode: modelContentObj.verificationCode
            }
         })
            .then(res => {
               $('#LoadingSite').fadeOut();
               setTimeout(function () {
                  window.location = '/Announcement/AllAgencyAnnouncements'
               }, 6000);
               obj.SuccessMessage("سيكون اخر  موعد لإستلام طلبات الانضمام للإعلان هو " + res.data.lastAnnouncementJoinDate);
            })
            .catch(error => {
               $('#LoadingSite').fadeOut();
               obj.ErrorMessage(error.response.data.message);
            });

      },
      RejectAnnouncement: function () {
         if (modelContentObj.rejectionReason == '') {
            return false;
         }
         $('#LoadingSite').fadeIn();
         axios.get("RejectApproveAnnouncementAsync", {
            params: {
               IdString: obj.announcementIdString,
               rejectionReason: modelContentObj.rejectionReason
            }
         })
            .then(res => {
               $('#LoadingSite').fadeOut();
               obj.SuccessMessage("تمت العمليه بنجاح");
               window.location = '/Announcement/AllAgencyAnnouncements'
            })
            .catch(error => {
               $('#LoadingSite').fadeOut();
               obj.ErrorMessage(error.response.data.message);
            });
      },
   }
})

function createVerificationCode() {
   axios.get("CreateVerificationCode", {
      params: {
         idString: obj.announcementIdString,
      }
         })
            .then(res => {
   $('#LoadingSite').fadeOut();
})
   .catch(error => {
      $('#LoadingSite').fadeOut();
      obj.ErrorMessage(error.response.data.message);
   });
      }

function checkSecond(sec) {
   if (sec < 10 && sec >= 0) { sec = "0" + sec };
   if (sec < 0) { sec = "59" };
   return sec;
}

function checkTime(min, sec, myVar) {
   if (min <= 0 && sec <= 0) {
      $("#btnResendVerificationCode").prop("disabled", false);
      clearTimeout(myVar)
   };
   return sec;
}

var myVar;
function startTimer() {
   clearTimeout(myVar);
   var presentTime = document.getElementById('timer').innerHTML;
   var timeArray = presentTime.split(/[:]+/);
   var m = timeArray[0];
   var s = checkSecond((timeArray[1] - 1));
   if (s == 59) { m = m - 1 }
   document.getElementById('timer').innerHTML = m + ":" + s;
   myVar = setTimeout(startTimer, 1000);
   checkTime(m, s, myVar);
   if (m < 0 && s > 0) {
      document.getElementById('timer').innerHTML = "0:00";
      return false;
   }
}
