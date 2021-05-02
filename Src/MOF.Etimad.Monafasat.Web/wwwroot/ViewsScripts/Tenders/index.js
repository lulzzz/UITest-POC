//check search show
var tenderGrid = document.getElementById('TenderGrid');
var cardsCont = document.getElementById('cardsresult');

$(function () {
   $("input").keypress(function (event) {
      if (event.which == 13) {
         event.preventDefault();
         $("#searchBtn").click();
      }
   });

   toggleViewCards('TenderGrid');
   $('#searchBtnColaps').click(function () {
      var _cards = $('#cardsresult');

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
         _cards.removeClass('col-md-8 ');
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
//function toggleViewCards() {
//   cardsCont.classList.remove('fadeOut');
//   cardsCont.classList.add('bounceInLeft');
//}
//function toggleViewGrid() {
//   cardsCont.classList.remove('bounceInLeft');
//   cardsCont.classList.add('fadeOut');
//}
