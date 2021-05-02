var allSupplierAnnouncemnetObj = {
   pleaseChooseActivity: null,
   pleaseChoose: null,
   noData: null,
   dateRangeValidationMessage: null
};
function initialize(obj)
{
   allSupplierAnnouncemnetObj = obj;
}

$(document).ready(function () {
   $('li#menuVisitorAnnouncements a').addClass('active');
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
var grid = new Vue({
   el: '#AnnouncementGrid',
   data: {
      TenderName: '',
      ReferenceNumber: '',
      AgencyCode: '',
      selectedAgency: '',
      agencyList: [],
      announcements: [],
      activitiesList: [],
      subActivitiesList: [],
      selectedActivity: '',
      selectedSubActivity: '',
      selectedTypeList: [],
      typeList: [],
      areasList: [],
      selectedAreaList: '',
      announcementPublishDateCriteriaId: '',
      announcementActiveStatusId: '',
      totalCount: 0,
      currentPage: 1,
      pageSize: 10,
      resource_url: '/Announcement/AllVisitorAnnouncementsPaging',
      queryString: '',
      sort: "SubmitionDate",
      sortDirection: "DESC"
   },
   methods: {
      updateResource: function (data) {
         this.announcements = data;
         this.totalCount = this.$refs.vpaginator.totalCount;
         this.currentPage = this.$refs.vpaginator.currentPage;
         this.pageSize = this.$refs.vpaginator.pageSize;
         setTimeout(function () {
            updateAgencyLogos();
         }, 1000);
      },
      getAgency: function () {
         var url_string = decodeURI(window.location.href);
         var url = new URL(url_string);
         $.get('/Tender/GetAllAgenciesAsync').done(function (result) {
            grid.agencyList = result;
            $('#agencyList').selectpicker('refresh');
            setTimeout(function () {
               $('#agencyList').selectpicker('val', url.searchParams.get('AgencyCode'));
               $('#agencyList').selectpicker('refresh');
            }, 1000);
         }).fail(function (error) {
            console.log(error.statusText);
         });
         $.get('/Tender/GetAreasAsync').done(function (result) {
            grid.areasList = result;
            setTimeout(function () {
               $('#areaList').selectpicker('refresh');
               if (url.searchParams && url.searchParams.get('TenderAreasIdString')) {
                  $('#areaList').selectpicker('val', (url.searchParams.get('TenderAreasIdString') ? url.searchParams.get('TenderAreasIdString').split(',') : ''));
               }
               $('#areaList').selectpicker('refresh');
            }, 1000);
         }).fail(function (error) {
            console.log(error.statusText);
         });
         $.get('/Tender/GetMainActivitiesAsync').done(function (result) {
            grid.activitiesList = result;
            setTimeout(function () {
               $('#activitiesList').selectpicker('refresh');
               if (url.searchParams && url.searchParams.get('TenderActivityId')) {
                  $('#activitiesList').selectpicker('val', url.searchParams.get('TenderActivityId'));
               }
               $('#activitiesList').selectpicker('refresh');
            }, 1000);
         }).fail(function (error) {
            console.log(error.statusText);
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
      detailsUrl: function (_id) {
         var url = '/Announcement/SupplierAnnouncementDetails';
         return url + '?announcmentIdString=' + _id;
      },
      getAnnouncementsByActiveStatusId: function (activeStatusId) {
         this.announcementActiveStatusId = activeStatusId;
         this.search();
      },
      search: function () {
         this.queryString = $('#searchCriteriaForm').serialize();
         var startDate = "";
         var endDate = "";
         if ($("#fromLastJoinDate").val() != "") {
            var i = $("#fromLastJoinDate").val().split('/');
            if ($('input[name="cb_LastJoinDate"]').is(':checked')) {
               $("#hdnfromLastJoinDate").val(i[0] + ',' + i[1] + ',' + i[2] + ',' + 'G');
               var i = $("#fromLastJoinDate").val().split('/');
               startDate = i[1] + '/' + i[0] + '/' + i[2];
            }
            else {
               $("#hdnfromLastJoinDate").val(i[0] + ',' + i[1] + ',' + i[2] + ',' + 'H');
               startDate = convertUmmalquraToGregorian($("#fromLastJoinDate").val());
            }
         }
         startDate = new Date(startDate);
         if ($("#toLastJoinDate").val() != "") {
            var x = $("#toLastJoinDate").val().split('/');
            if ($('input[name="cb_LastJoinDate"]').is(':checked')) {
               $("#hdntoLastJoinDate").val(x[0] + ',' + x[1] + ',' + x[2] + ',' + 'G');
               var i = $("#toLastJoinDate").val().split('/');
               endDate = i[1] + '/' + i[0] + '/' + i[2];
            }
            else {
               $("#hdntoLastJoinDate").val(x[0] + ',' + x[1] + ',' + x[2] + ',' + 'H');
               endDate = convertUmmalquraToGregorian($("#toLastJoinDate").val());
            }
            endDate = new Date(endDate);
         }
         if (startDate != "" && endDate != "") {
            if (startDate > endDate) {
               AlertFun(dateRangeValidationMessage, alertMessageType.Danger);
               return false;
            }
         }
         this.queryString = this.queryString + '&AreasId=' + this.selectedAreaList + '&ActivityId=' + this.selectedActivity + '&SubActivityId=' + this.selectedSubActivity + '&AgencyCode=' + this.selectedAgency
            + '&FromLastJoinDateString=' + $("#hdnfromLastJoinDate").val() + '&ToLastJoinDateString=' + $("#hdntoLastJoinDate").val()
            + '&AnnouncementPublishDateCriteriaId=' + this.announcementPublishDateCriteriaId + '&AnnouncementActiveStatusId=' + this.announcementActiveStatusId + '&TenderTypeId=' + this.selectedTypeList
            + '&SortDirection=' + this.sortDirection + '&Sort=' + this.sort + '&PageSize=' + this.pageSize;
         this.resource_url = '/Announcement/AllVisitorAnnouncementsPaging' + '?' + this.queryString;
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
         var pleaseChooseActivity = allSupplierAnnouncemnetObj.pleaseChooseActivity;
         this.queryString = '';
         $('#tenderName').val('');
         $('#referenceNumber').val('');
         $('#agencyList').val('');
         $('#PublishDate').val('');
         $('#TenderCategory').val('');
         $('#activitiesList').val('');
         $('#subActivitiesList').val('0');
         $("#TenderTypeId").val("");
         $('#areaList').val('');
         $("#subActivitiesList").prop('disabled', true);

         this.selectedAreaList = '';
         this.selectedActivity = '';
         this.selectedSubActivity = '';
         this.selectedAgency = '';
         this.selectedTypeList = '';
         this.announcementPublishDateCriteriaId = '';
         $(".selectpicker").selectpicker("refresh");
         $("#subActivitiesList option[value='0']").text(pleaseChooseActivity);

         this.queryString = this.queryString + '&AreasId=' + this.selectedAreaList + '&ActivityId=' + this.selectedActivity + '&SubActivityId=' + this.selectedSubActivity + '&AgencyCode=' + this.selectedAgency
            + '&FromLastJoinDateString=' + $("#hdnfromLastJoinDate").val() + '&ToLastJoinDateString=' + $("#hdntoLastJoinDate").val()
            + '&AnnouncementPublishDateCriteriaId=' + this.announcementPublishDateCriteriaId + '&AnnouncementActiveStatusId=' + this.announcementActiveStatusId + '&TenderTypeId=' + this.selectedTypeList
            + '&SortDirection=' + this.sortDirection + '&Sort=' + this.sort + '&PageSize=' + this.pageSize;
         this.resource_url = '/Announcement/AllVisitorAnnouncementsPaging' + '?' + this.queryString;

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
      document.cookie = "url=/Announcement/AllVisitorAnnouncementsPaging" + (_encodedurl == "?undefined" ? "" : _encodedurl) + "";
      this.getAgency();
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
$(document).ready(function () {
   var url_string = decodeURI(window.location.href);
   var url = new URL(url_string);
   $("#activitiesList").change(function () {
      var activitiesListId = $("#activitiesList").val();
      var pleaseChooseActivity = allSupplierAnnouncemnetObj.pleaseChooseActivity;
      var pleaseChoose = allSupplierAnnouncemnetObj.pleaseChoose;
      var noData = allSupplierAnnouncemnetObj.noData;
      if (activitiesListId > 0) {
         $.get("/Tender/GetSubActivitiesAsync?mainAcivityId=" + $("#activitiesList").val(), function (data, status) {
            if (data.length > 0) {
               $("#subActivitiesList").prop('disabled', false);
               $("#subActivitiesList option[value='']").text(pleaseChoose);
            }
            else {
               $("#subActivitiesList").prop('disabled', true);
               $("#subActivitiesList option[value='']").text(noData);
            }
            grid.subActivitiesList = data;
            setTimeout(function () {
               $('#subActivitiesList').selectpicker('refresh');
               if (url.searchParams && url.searchParams.get('TenderSubActivityId')) {
                  $('#subActivitiesList').selectpicker('val', url.searchParams.get('TenderSubActivityId'));
               }
               $('#subActivitiesList').selectpicker('refresh');
            }, 1000);
         });
      }
      else {
         $("#subActivitiesList option[value='0']").text(pleaseChooseActivity);
      }
   });
});
