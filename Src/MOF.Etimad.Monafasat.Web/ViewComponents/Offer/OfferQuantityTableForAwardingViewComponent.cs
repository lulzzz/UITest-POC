using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferQuantityTableForAwarding")]
   public class OfferQuantityTableForAwardingViewComponent : ViewComponent
   {
      private readonly IApiClient _apiClient;
      public OfferQuantityTableForAwardingViewComponent(IApiClient apiClient)
      {
         _apiClient = apiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string offerIdStr, string tenderIdStr)
      {

         var tenderId = Util.Decrypt(tenderIdStr);
         var offerId = Util.Decrypt(offerIdStr);
         QuantitiesTablesForAwardingModel offer = await _apiClient.GetAsync<QuantitiesTablesForAwardingModel>("Offer/GetOfferQuantityTableForAwarding/" + tenderId + "/" + offerId, null);
         offer.QuantityTableStepDTO.IsReadOnly = true;
         return View(offer);
      }
   }
}
