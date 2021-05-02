function PrepareModel(id, progressDoneSuccessfully) {
   tender.AnnouncementIdString = id;
   tender.ProgressDoneSuccessfully = progressDoneSuccessfully;
}
 
var tender = new Vue({
      el: '#actionsDiv',
      data: {
         AnnouncementIdString: '',
         ProgressDoneSuccessfully:'',
      agreeReport: false,
            isPressed : false
         },
         methods: {
      sendRequesttoJoinTender: function () {
               debugger;
               $('#LoadingSite').fadeIn();
               $.post('/Announcement/JoinAnnouncement', { announcmentIdString: tender.AnnouncementIdString })
                  .done(function (e) {
      $('#LoadingSite').fadeOut();
                     window.location.href = window.location.href;
                     AlertFun(tender.ProgressDoneSuccessfully, alertMessageType.Success);
               }).fail(function (error) {
      $('#LoadingSite').fadeOut();
                  AlertFun(error.responseJSON.message, alertMessageType.Danger);
               });
            },
          withdrawRequesttoJoinTender: function () {
             debugger;
             this.isPressed = true;
             if (!this.agreeReport) {

                return false;
             }
             $('#LoadingSite').fadeIn();
             $.post('/Announcement/WithdrawJoinRequest', { announcmentIdString: tender.AnnouncementIdString })
                  .done(function (e) {
      $('#LoadingSite').fadeOut();
                     window.location.href = window.location.href;
                     AlertFun(tender.ProgressDoneSuccessfully, alertMessageType.Success);
               }).fail(function (error) {
      $('#LoadingSite').fadeOut();
                  AlertFun(error.responseJSON.message, alertMessageType.Danger);
               });
          }
         }
      }); 
