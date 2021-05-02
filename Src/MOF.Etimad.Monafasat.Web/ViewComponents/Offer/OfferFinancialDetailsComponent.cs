using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferFinancialDetails")]
   public class OfferFinancialDetailsComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OfferFinancialDetailsComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int offerId)
      {
         OfferFinancialDetailsModel offer = await _ApiClient.GetAsync<OfferFinancialDetailsModel>("offer/OfferFinancialDetails/" + offerId, null);
         return View(offer);
      }
   }
}
