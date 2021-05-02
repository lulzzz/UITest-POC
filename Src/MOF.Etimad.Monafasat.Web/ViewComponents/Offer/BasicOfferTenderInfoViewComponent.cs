using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   //used in pages: Apply Offer - Apply Technical Offer
   [ViewComponent(Name = "BasicOfferTenderInfo")]
   public class BasicOfferTenderInfoViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public BasicOfferTenderInfoViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         BasicOfferTenderInfoModel tenderModel = await _ApiClient.GetAsync<BasicOfferTenderInfoModel>("Tender/GetBasicOfferTenderInfoById/" + TenderId, null);
         return View(tenderModel);


      }
   }
}
