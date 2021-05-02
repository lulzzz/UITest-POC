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
   $('li#menuAllAgencySupplierTemplates a').addClass('active');
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
      AgencyCode: '',
      selectedAgency: '',
      agencyList: [],
      announcements: [],
      activitiesList: [],
      announcementStatusList: [],
      selectedStatus: '',
      selectedTypeId: '',
      subActivitiesList: [], selectedTypeList: [], typeList: [],
      selectedActivity: '',
      selectedSubActivity: '',
      areasList: [],
      selectedAreaList: '',
      announcementPublishDateCriteriaId: '',
      referenceNumber: '',
      announcementTemplateName: '',
      announcementApplyingStatusId:'',
      announcementTabId:'',
      announcementActiveStatusId: '',
      totalCount: 0,
      currentPage: 1,
      pageSize: 10,
      resource_url: '/AnnouncementSuppliersTemplate/GetAllAnnouncementSupplierTemplatesForSupplier',
      queryString: '',
      sort: "PublishedDate",
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
         $.get('/Lookup/GetAnnouncementStatusSupplierTemplatesLookup').done(function (result) {
            console.log(result);
            grid.announcementStatusList = result;
            setTimeout(function () {
               if (url.searchParams.get('StatusId')) {
                  $('#statusList').selectpicker('val', url.searchParams.get('StatusId'));
               }
               $('#statusList').selectpicker('refresh');
            }, 1000);
         });
      },
      addJoinRequestToAnnouncementTemplate: function (announcementId) {
         return '/AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcmentIdString=' + announcementId;   
      },
      withdrowFromAnnouncementTemplate: function (announcementId) {
         return '/AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcmentIdString=' + announcementId;
      }, 
      join: function (_id) {
         var url = '/Announcement/SupplierAnnouncementDetails';
         url = url + '?announcmentIdString=' + _id;
         window.location = url;
      },
      detailsUrl: function (_id) {
         return '/AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcmentIdString=' + _id;
      },
      getAnnouncementsByActiveStatusId: function (activeStatusId) {
         if (activeStatusId == 1) {
            this.sort = "PublishedDate"
         }
         else {
            this.sort = "CreatedAt"
         }
         this.announcementTabId = activeStatusId;
         this.search();
      },
      search: function () {
         this.queryString = $('#searchCriteriaForm').serialize();
         this.queryString = this.queryString + "&AnnouncementName=" + this.announcementTemplateName + "&ReferenceNumber=" + this.referenceNumber
            + '&AreasId=' + this.selectedAreaList + '&ActivityId=' + this.selectedActivity + '&SubActivityId=' + this.selectedSubActivity + '&AgencyCode=' + this.selectedAgency 
            + '&AnnouncementPublishedDateDaysId=' + this.announcementPublishDateCriteriaId + '&AnnouncementApplyingStatusId='
            + this.announcementApplyingStatusId + '&StatusId=' + this.selectedStatus + '&TypeId=' + this.selectedTypeId
            + '&AnnouncementTabId=' + this.announcementTabId
            + '&SortDirection=' + this.sortDirection + '&Sort=' + this.sort + '&PageSize=' + this.pageSize;
         this.resource_url = '/AnnouncementSuppliersTemplate/GetAllAnnouncementSupplierTemplatesForSupplier' + '?' + this.queryString;
      },
      clear: function () {
         debugger
         var checkboxDates = $('#searchCriteriaForm .checkbox_appended input:checkbox');
         checkboxDates.each(function () {
            var chk = this;
            var isChecked = $(chk).is(':checked')
            if (isChecked) {
               $(chk).trigger('change');
            }
         })

         this.queryString = '';
         $('#tenderName').val('');
         $('#referenceNumber').val('');
         $('#agencyList').val('');
         $('#PublishDate').val('');
         $('#TenderCategory').val('');
         $('#activitiesList').val('');
         $('#subActivitiesList').val('0');
         $('#areaList').val('');
         $('#StatusId').val('');
         $('#statusList').val('');
         $('#TemplateTypeId').val('');
         $("#ApplyStatus").val("");
         $("#subActivitiesList").prop('disabled', true);
         $(".selectpicker").selectpicker("refresh");
         this.selectedAreaList = '';
         this.selectedActivity = '';
         this.selectedTypeList = '';
         this.selectedSubActivity = '';
         this.selectedAgency = '';
         this.selectedStatus = '';
         this.selectedTypeId = '';
         this.announcementPublishDateCriteriaId = '';
         this.announcementApplyingStatusId = '';
         this.announcementTemplateName = '';
         this.referenceNumber = '';

         this.resource_url = '/AnnouncementSuppliersTemplate/GetAllAnnouncementSupplierTemplatesForSupplier?' + 'AnnouncementTabId=' + this.announcementTabId

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
      document.cookie = "url=/AnnouncementSuppliersTemplate/GetAllAnnouncementSupplierTemplatesForSupplier" + (_encodedurl == "?undefined" ? "" : _encodedurl) + "";
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
      var pleaseChooseActivity = allAgencyAnnouncemnetObj.pleaseChooseActivity;
      var pleaseChoose = allAgencyAnnouncemnetObj.pleaseChoose;
      var noData = allAgencyAnnouncemnetObj.noData;
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
