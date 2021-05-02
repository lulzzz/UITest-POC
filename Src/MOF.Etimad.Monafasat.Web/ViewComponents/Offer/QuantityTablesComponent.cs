using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "QuantityTablesComponent")]
   public class QuantityTablesComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public QuantityTablesComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string offerIdString)
      {
         OfferQuantityTableHtmlVM qtableHtml = new OfferQuantityTableHtmlVM();
         if (offerIdString == null)
         {
         }
         else
         {
            var _offerId = Util.Decrypt(offerIdString);
            qtableHtml = await _ApiClient.GetAsync<OfferQuantityTableHtmlVM>("Offer/GetQuantityTables/" + _offerId, null);

         }
         //qtableHtml = await _ApiClient.GetAsync<QuantitiesTemplateModel>("Offer/GetQuantityTables/" + 1, null);
         // ViewBag.qtableHtml = qtableHtml;
         return View(qtableHtml);
      }
   }
}
