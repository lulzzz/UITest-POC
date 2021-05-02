var allAgencyAnnouncemnetObj = {
   pleaseChooseActivity: null,
   pleaseChoose: null,
   noData: null,
   dateRangeValidationMessage: null
};
function initialize(obj)
{
   allAgencyAnnouncemnetObj = obj;
}

$(document).ready(function () {
   $('li#menuAllAgencyAnnouncements a').addClass('active');
});
var cardsCont = document.getElementById('cardsresult');
$('button[type="reset"]').click(function () {
   $(".selectpicker").val('default');
   $(".selectpicker").selectpicker("refresh");
});
$(function () {
   $("input").keypress(function (event) {
      if (event.which === 13) {
         event.preventDefault();
         $("#searchBtn").click();
      }
   });
   $('#searchBtnColaps').click(function () {
      var _cards = $('#cardsresult');
      var _grid = $('#gridresult');
      if (_cards.hasClass('col-md-12')) {
         _cards.removeClass('col-md-12');
         _cards.addClass('col-md-8');
         _cards.find('.monafasa-item').removeClass('col-md-6');
         _cards.find('.monafasa-item').addClass('col-md-12');
         _cards.find('.monafasa-item').removeClass('col-lg-6');
         _cards.find('.monafasa-item').addClass('col-lg-12');
         _cards.find('.monafasa-item').removeClass('col-xl-4');
         _cards.find('.monafasa-item').addClass('col-xl-6');
      } else if (_cards.hasClass('col-md-8')) {
         _cards.addClass('col-md-12');
         _cards.removeClass('col-md-8');
         _cards.find('.monafasa-item').addClass('col-md-6');
         _cards.find('.monafasa-item').removeClass('col-md-12');
         _cards.find('.monafasa-item').addClass('col-lg-6');
         _cards.find('.monafasa-item').removeClass('col-lg-12');
         _cards.find('.monafasa-item').addClass('col-xl-4');
         _cards.find('.monafasa-item').removeClass('col-xl-6');
      }
      $('#closeSearch').on("click", function () {
         $('#searchBtnColaps').click();
      });
   });
});
function toggleViewGrid() {
   cardsCont.classList.remove('bounceInLeft');
   cardsCont.classList.add('fadeOut');
}
//function DeleteAnnouncement (id) {
//   debugger
//   axios.get("DeleteAnnouncement", {
//      params: {
//         announcementIdString:,
//      }
//   })
//}
function DeleteAnnouncement () {
   var id = $('#announcementIdString').val();
   axios.get("DeleteAnnouncement", {
      params: {
         announcementIdString: id,
      }
   }).then(res => {
      setTimeout(function () {
         $('#LoadingSite').fadeOut();
         location.reload();
      }, 3000);
      AlertFun("تم الحذف بنجاح", alertMessageType.Success);

   })
      .catch(error => {
         $('#LoadingSite').fadeOut();
         AlertFun(error.response.data.message, alertMessageType.Danger);
      });
}
var grid = new Vue({
   el: '#AnnouncementGrid',
   data: {
      TenderName: '',
      ReferenceNumber: '',
      AgencyCode: '',
      selectedAgency: '',
      agencyList: [],
      announcements: [],
      branchesList: [],
      selectedBranch: '',
      selectedCreatedByUser: '',
      createdByList: [],
      selectedApprovedByUser: '', selectedTypeList: [], typeList: [],
      approvedByList: [],
      selectedStatus: '',
      announcementStatusList: [],
      totalCount: 0,
      currentPage: 1,
      pageSize: 10,
      resource_url: '/Announcement/AllAgencyAnnouncementsPaging',
      queryString: '',
      type:'',
      sort: "SubmitionDate",
      sortDirection: "DESC"
   },
   methods: {
      updateResource: function (data) {
         this.announcements = data;
         this.totalCount = this.$refs.vpaginator.totalCount;
         this.currentPage = this.$refs.vpaginator.currentPage;
         this.pageSize = this.$refs.vpaginator.pageSize;
      },
      EditAnnouncement: function (id) {
         var url = '/Announcement/CreateAnnouncement?announcementIdString=' + id;
         window.location = url;
      },
      ShowDeleteModal: function (id) {
         $('#deleteModel').modal("show");
         $('#announcementIdString').val(id);
      },
    
      fillDropDowns: function () {
         var url_string = decodeURI(window.location.href);
         var url = new URL(url_string);
         $.get('/Tender/GetAllAgenciesAsync').done(function (result) {
            grid.agencyList = result;
            setTimeout(function () {
               $('#agencyList').selectpicker('val', url.searchParams.get('AgencyCode'));
               $('#agencyList').selectpicker('refresh');
            }, 1000);
         }).fail(function (error) {
            console.log(error.statusText);
         });
         $.get('/Tender/GetAllBranchesByAgencyCode').done(function (result) {
            grid.branchesList = result;
            setTimeout(function () {
               if (url.searchParams.get('BranchId')) {
                  $('#branchId').selectpicker('val', url.searchParams.get('BranchId'));
               }
               $('#branchId').selectpicker('refresh');
            }, 1000);
         });
         $.get('/Lookup/GetAnnouncementStatusAsync').done(function (result) {
            console.log(result);
            grid.announcementStatusList = result;
            setTimeout(function () {
               if (url.searchParams.get('TenderStatusId')) {
                  $('#TenderStatusId').selectpicker('val', url.searchParams.get('TenderStatusId'));
               }
               $('#TenderStatusId').selectpicker('refresh');
            }, 1000);
         });
         $.get('/Tender/GetAllDataEntryUsersAsync', null, function (result) {
            grid.createdByList = result;
            setTimeout(function () {
               if (url.searchParams.get('CreatedBy')) {
                  $('#createdBy').selectpicker('val', url.searchParams.get('CreatedBy'));
               }
               $('#createdBy').selectpicker('refresh');
            }, 1000);
         });
         $.get('/Tender/GetAllAuditorUsersAsync', null, function (result) {
            grid.approvedByList = result;
            setTimeout(function () {
               if (url.searchParams.get('ApprovedBy')) {
                  $('#approvedBy').selectpicker('val', url.searchParams.get('ApprovedBy'));
               }
               $('#approvedBy').selectpicker('refresh');
            }, 1000);
         });
         $.get('/Announcement/GetTenderTypes').done(function (result) {
            grid.typeList = result;
            setTimeout(function () {
               if (url.searchParams.get('TenderTypeId')) {
                  $('#TenderTypeId').selectpicker('val', url.searchParams.get('TenderTypeId'));
               }
               $('#TenderTypeId').selectpicker('refresh');
            }, 1000);
         });
      },
      redirectToDetail: function (_id) {
         var url = '/Announcement/GetAnnouncementDetails';
         url = url + '?AnnouncementIdString=' + _id;
         window.location = url;
      },
      detailsUrl: function (_id) {
         var url = '/Announcement/GetAnnouncementDetails';
         return url + '?AnnouncementIdString=' + _id;
      },
      getAllAnnouncement: function () {
         this.type = "All";
         this.search();
      },
      getCurrentAnnouncement: function () {
         this.type = "Current";
         this.search();
      },
      getFinishedAnnouncement: function () {
         this.type = "Finished";
         this.search();
      },
      search: function () {
         this.queryString = $('#searchCriteriaForm').serialize();
         var startDate = "";
         var endDate = "";
         if ($("#fromPublishDate").val() != "") {
            var i = $("#fromPublishDate").val().split('/');
            if ($('input[name="cb_PublishDate"]').is(':checked')) {
               $("#hdnfromPublishDate").val(i[0] + ',' + i[1] + ',' + i[2] + ',' + 'G');
               var i = $("#fromPublishDate").val().split('/');
               startDate = i[1] + '/' + i[0] + '/' + i[2];
            }
            else {
               $("#hdnfromPublishDate").val(i[0] + ',' + i[1] + ',' + i[2] + ',' + 'H');
               startDate = convertUmmalquraToGregorian($("#fromPublishDate").val());
            }
         }
         startDate = new Date(startDate);
         if ($("#toPublishDate").val() != "") {
            var x = $("#toPublishDate").val().split('/');
            if ($('input[name="cb_PublishDate"]').is(':checked')) {
               $("#hdntoPublishDate").val(x[0] + ',' + x[1] + ',' + x[2] + ',' + 'G');
               var i = $("#toPublishDate").val().split('/');
               endDate = i[1] + '/' + i[0] + '/' + i[2];
            }
            else {
               $("#hdntoPublishDate").val(x[0] + ',' + x[1] + ',' + x[2] + ',' + 'H');
               endDate = convertUmmalquraToGregorian($("#toPublishDate").val());
            }
            endDate = new Date(endDate);
         }
         if (startDate != "" && endDate != "") {
            if (startDate > endDate) {
               AlertFun(dateRangeValidationMessage, alertMessageType.Danger);
               return false;
            }
         }

         //if (this.type == "All") {
         //   this.queryString = this.queryString + searchCriteria + '&TenderStatusId=' + this.selectedStatus;
         //}
         //else if (this.type == "Current") {
         //   this.queryString = this.queryString + "&AnnouncementStatusId=";
         //}
         //else if (this.type == "Finished") {
         //   this.queryString = this.queryString + "&AnnouncementStatusId=";
         //}
         //this.queryString = this.queryString + '&Sort=' + this.sort + '&SortDirection=' + this.sortDirection + '&PageSize=' + this.pageSize;
         this.queryString = this.queryString + "&type=" + this.type + '&AgencyCode=' + this.selectedAgency + '&BranchId=' + this.selectedBranch + '&TenderTypeId=' + this.selectedTypeList
            + '&FromPublishDateString=' + $("#hdnfromPublishDate").val() + '&ToPublishDateString=' + $("#hdntoPublishDate").val()
            + '&AnnouncementStatusId=' + this.selectedStatus + '&CreatedBy=' + this.selectedCreatedByUser + '&ApprovedBy=' + this.selectedApprovedByUser
            + '&SortDirection=' + this.sortDirection + '&Sort=' + this.sort + '&PageSize=' + this.pageSize;
         this.resource_url = '/Announcement/AllAgencyAnnouncementsPaging' + '?' + this.queryString;
      },
      clear: function () {


         var checkboxDates = $('#searchCriteriaForm .checkbox_appended input:checkbox');
         checkboxDates.each(function () {
            var chk = this;
            var isChecked = $(chk).is(':checked')
            if (isChecked) {
               $(chk).trigger('change');
            }
         })

         this.queryString = '';
         this.resource_url = '/Announcement/AllAgencyAnnouncementsPaging?type=' + this.type;
         $('#tenderName').val('');
         $('#referenceNumber').val('');
         $('#AgencyCode').val('');
         $('#PublishDate').val('');
         $('#TenderStatusId').val('');
         $("#TenderTypeId").val("");
         $('#createdBy').val('');
         $('#approvedBy').val('');
         $("#hdnfromPublishDate").val('');
         $("#hdntoPublishDate").val('');
         $(".selectpicker").selectpicker("refresh");
         this.selectedAgency = '';
         this.selectedTypeList = '';
         this.selectedCreatedByUser = '';
         this.selectedApprovedByUser = '';
         this.selectedBranch = '';
         this.selectedStatus = '';

      }
   },
   created: function () {
      this.resource_url = decodeURI(decodeURI(this.resource_url + "?" + window.location.href.split('?')[1]));
      var _encodedurl = encodeURI(((this.resource_url.split('?')[1]) ? "?" + (this.resource_url.split('?')[1]) : ''));
      var cookies = document.cookie.split(";");
      for (var i = 0; i < cookies.length; i++) {
         var cookie = cookies[i];
         var eqPos = cookie.indexOf("=");
         var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
         document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
      }
      document.cookie = "url=/Announcement/AllAgencyAnnouncements" + (_encodedurl == "?undefined" ? "" : _encodedurl) + "";
      this.fillDropDowns();
   },
   updated: function () {
      var _cards = $('#cardsresult');
      if (_cards.hasClass('col-md-12')) {
         _cards.find('.monafasa-item').addClass('col-md-6');
         _cards.find('.monafasa-item').removeClass('col-md-12');
         _cards.find('.monafasa-item').addClass('col-lg-6');
         _cards.find('.monafasa-item').removeClass('col-lg-12');
         _cards.find('.monafasa-item').addClass('col-xl-4');
         _cards.find('.monafasa-item').removeClass('col-xl-6');
      } else if (_cards.hasClass('col-md-8')) {
         _cards.find('.monafasa-item').removeClass('col-md-6');
         _cards.find('.monafasa-item').addClass('col-md-12');
         _cards.find('.monafasa-item').removeClass('col-lg-6');
         _cards.find('.monafasa-item').addClass('col-lg-12');
         _cards.find('.monafasa-item').removeClass('col-xl-4');
         _cards.find('.monafasa-item').addClass('col-xl-6');
      }
      $('[data-toggle="tooltip"]').tooltip();
   }
});
