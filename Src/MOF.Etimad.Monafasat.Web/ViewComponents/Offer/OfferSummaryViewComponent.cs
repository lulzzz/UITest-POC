using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferSummary")]
   public class OfferSummaryViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OfferSummaryViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string OfferIdString)
      {
         OfferFullDetailsModel offerModel = await _ApiClient.GetAsync<OfferFullDetailsModel>("Offer/GetOfferFullDetails/" + OfferIdString, null);
         return View(offerModel);

      }
   }
}
