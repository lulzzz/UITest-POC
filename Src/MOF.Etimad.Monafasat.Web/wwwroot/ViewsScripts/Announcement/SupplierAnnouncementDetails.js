var code = '';
function PrepareModel(id, progressDoneSuccessfully, code) {
   tender.AnnouncementIdString = id;
   tender.ProgressDoneSuccessfully = progressDoneSuccessfully;
   tender.agencyCode = code;
}
var tender = new Vue({
   el: '#actionsDiv',
   data: {
      AnnouncementIdString: '',
      ProgressDoneSuccessfully: '',
      agencyCode: '',
      agreeReport: false,
      isPressed: false
   },
   methods: {
      sendRequesttoJoinTender: function () {
         $('#LoadingSite').fadeIn();
         $.post('/Announcement/JoinAnnouncement', { announcmentIdString: tender.AnnouncementIdString })
            .done(function (e) {
               $('#LoadingSite').fadeOut();
               var currentUrl = window.location.href;
               window.location.href = currentUrl;
               AlertFun(tender.ProgressDoneSuccessfully, alertMessageType.Success);
            }).fail(function (error) {
               $('#LoadingSite').fadeOut();
               AlertFun(error.responseJSON.message, alertMessageType.Danger);
            });
      },
      withdrawRequesttoJoinTender: function () {
         this.isPressed = true;
         if (!this.agreeReport) {
            return false;
         }
         $('#LoadingSite').fadeIn();
         $.post('/Announcement/WithdrawJoinRequest', { announcmentIdString: tender.AnnouncementIdString })
            .done(function (e) {
               $('#LoadingSite').fadeOut();
               var currentUrl = window.location.href;
               window.location.href = currentUrl;
               AlertFun(tender.ProgressDoneSuccessfully, alertMessageType.Success);
            }).fail(function (error) {
               $('#LoadingSite').fadeOut();
               AlertFun(error.responseJSON.message, alertMessageType.Danger);
            });
      }
   }
});
