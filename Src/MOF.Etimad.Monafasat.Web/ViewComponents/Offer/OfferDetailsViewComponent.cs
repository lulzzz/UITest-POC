using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferDetails")]
   public class OfferDetailsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OfferDetailsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         TenderOffersStepModel tenderModel = await _ApiClient.GetAsync<TenderOffersStepModel>("Tender/GetOfferDetailsById/" + TenderId, null);
         return View(tenderModel);

      }
   }
}
