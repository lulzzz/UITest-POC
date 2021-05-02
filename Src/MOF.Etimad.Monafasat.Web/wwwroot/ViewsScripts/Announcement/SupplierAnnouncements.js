$(document).ready(function () {
   $('li#menuSupplierAnnouncements a').addClass('active');
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

         $('#searchBtnColaps').click()
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
      announcements: [], selectedTypeList: [], typeList: [],
      totalCount: 0,
      currentPage: 1,
      pageSize: 10,
      resource_url: '/Announcement/SupplierAnnouncementsPaging',
      queryString: '',
      sort: "SubmitionDate",
      sortDirection: "DESC",
   },
   methods: {
      updateResource: function (data) {
         this.announcements = data;
         this.totalCount = this.$refs.vpaginator.totalCount;
         this.currentPage = this.$refs.vpaginator.currentPage;
         this.pageSize = this.$refs.vpaginator.pageSize;
      },
      getAgency: function () {
         var url_string = decodeURI(window.location.href)
         var url = new URL(url_string);
         $.get('/Tender/GetAllAgenciesAsync').done(function (result) {
            grid.agencyList = result;
            setTimeout(function () {
               $('#AgencyCode').selectpicker('val', url.searchParams.get('AgencyCode'));
               $('#AgencyCode').selectpicker('refresh');
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
      Withdraw: function (_id) {
         var url = '/Announcement/SupplierAnnouncementDetails';
         url = url + '?announcmentIdString=' + _id;
         window.location = url;
      },
      detailsUrl: function (_id) {
         var url = '/Announcement/SupplierAnnouncementDetails';
         return url + '?announcmentIdString=' + _id;
      },
      search: function () {
         this.queryString = $('#searchCriteriaForm').serialize();
         this.queryString = this.queryString + '&Sort=' + this.sort + '&SortDirection=' + this.sortDirection + '&PageSize=' + this.pageSize + '&TenderTypeId=' + this.selectedTypeList;
         this.resource_url = '/Announcement/SupplierAnnouncementsPaging' + '?' + this.queryString;
      },
      clear: function () {
         this.queryString = '';
         this.resource_url = '/Announcement/SupplierAnnouncementsPaging';
         $('#tenderName').val('');
         $('#referenceNumber').val('');
         $('#AgencyCode').val('');
         $("#TenderTypeId").val("");
         this.selectedTypeList = '';
         $(".selectpicker").selectpicker("refresh");
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
      document.cookie = "url=/Announcement/SupplierAnnouncements" + (_encodedurl === "?undefined" ? "" : _encodedurl) + "";
      this.getAgency();
   }
});
