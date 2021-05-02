using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "QuantityTablesDisplayComponent")]
   public class QuantityTablesDisplayComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public QuantityTablesDisplayComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string offerIdString, bool allowEdit, bool isNegotiation = false)
      {
         OfferQuantityTableHtmlVM qtableHtml = new OfferQuantityTableHtmlVM();

         qtableHtml = await _ApiClient.GetAsync<OfferQuantityTableHtmlVM>("Offer/GetQuantityTablesDisplay/" + Util.Decrypt(offerIdString) + "/" + allowEdit + "/" + isNegotiation, null);
         return View(qtableHtml);
      }
   }
}
