$(document).ready(function () {
   $('li#menuUnderOperationAgencyAnnouncements a').addClass('active');
});
var cardsCont = document.getElementById('cardsresult');
$('button[type="reset"]').click(function () {
   $(".selectpicker").val('default');
   $(".selectpicker").selectpicker("refresh");
});
function toggleViewGrid() {
   cardsCont.classList.remove('bounceInLeft');
   cardsCont.classList.add('fadeOut');
}
function DeleteAnnouncement() {
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
      announcements: [],
      selectedStatus: '',
      announcementStatusList: [],
      selectedTypeList: [],
      typeList: [],
      activitiesList: [],
      subActivitiesList: [],
      selectedActivity: [],
      selectedSubActivities: [],
      totalCount: 0,
      currentPage: 1,
      pageSize: 10,
      resource_url: '/Announcement/UnderOperationAgencyAnnouncementsPaging',
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
      },
      fillDropDowns: function () {
         var url_string = decodeURI(window.location.href);
         var url = new URL(url_string);
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
         $.get('/Announcement/GetTenderTypes').done(function (result) {
            grid.typeList = result;
            setTimeout(function () {
               if (url.searchParams.get('TenderTypeId')) {
                  $('#TenderTypeId').selectpicker('val', url.searchParams.get('TenderTypeId'));
               }
               $('#TenderTypeId').selectpicker('refresh');
            }, 1000);
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

      },

      search: function () {
         this.queryString = $('#searchCriteriaForm').serialize();
         this.queryString = this.queryString + '&TenderTypeId=' + this.selectedTypeList + '&AnnouncementStatusId=' + this.selectedStatus
            + '&ActivityId=' + this.selectedActivity + '&SubActiviesIdString=' + this.selectedSubActivities
            + '&SortDirection=' + this.sortDirection + '&Sort=' + this.sort + '&PageSize=' + this.pageSize;
         this.resource_url = '/Announcement/UnderOperationAgencyAnnouncementsPaging' + '?' + this.queryString;
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
         this.resource_url = '/Announcement/UnderOperationAgencyAnnouncementsPaging';
         $('#tenderName').val('');
         $('#referenceNumber').val('');
         $('#TenderStatusId').val('');
         $("#TenderTypeId").val("");
         $("#activitiesList").val("");
         $("#subActivitiesList").val("");
         $("#subActivitiesList").prop('disabled', true);
         $(".selectpicker").selectpicker("refresh");
         this.selectedTypeList = '';
         this.selectedStatus = '';
         this.selectedActivity = '';
         this.selectedSubActivities = '';
      },

      EditAnnouncement: function (id) {
         var url = '/Announcement/CreateAnnouncement?announcementIdString=' + id;
         window.location = url;
      },
      ShowDeleteModal: function (id) {
         $('#deleteModel').modal("show");
         $('#announcementIdString').val(id);
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
      document.cookie = "url=/Announcement/UnderOperationAgencyAnnouncements" + (_encodedurl == "?undefined" ? "" : _encodedurl) + "";
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
$(document).ready(function () {
   var url_string = decodeURI(window.location.href);
   var url = new URL(url_string);
   $("#activitiesList").change(function () {
      var activitiesListId = $("#activitiesList").val();
      if (activitiesListId > 0) {
         $.get("/Tender/GetSubActivitiesAsync?mainAcivityId=" + $("#activitiesList").val(), function (data, status) {
            if (data.length > 0) {
               $("#subActivitiesList").prop('disabled', false);
               $("#subActivitiesList option[value='']").text("برجاء الاختيار");
            }
            else {
               $("#subActivitiesList").prop('disabled', true);
               $("#subActivitiesList option[value='']").text("لا يوجد بيانات");
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
         $("#subActivitiesList option[value='0']").text("الرجاء اختيار نشاط المنافسة اولا");
      }
   });
});
