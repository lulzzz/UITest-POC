using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "BasicOfferTenderInfo")]
    public class BasicOfferTenderInfo : ViewComponent
   {
      private IApiClient _ApiClient;
      public BasicOfferTenderInfo(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         BasicTenderModel tenderModel = await _ApiClient.GetAsync<BasicTenderModel>("Tender/GetBasicById/" + TenderId, null);
         return View(tenderModel);
      }
   }
}
