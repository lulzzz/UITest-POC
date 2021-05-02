using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "ApplyOfferQuantityTables")]
   public class ApplyOfferQuantityTablesViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public ApplyOfferQuantityTablesViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string offerIdString)
      {
         OfferQuantityTableHtmlVM qtableHtml = new OfferQuantityTableHtmlVM();
         var _offerId = Util.Decrypt(offerIdString);
         qtableHtml = await _ApiClient.GetAsync<OfferQuantityTableHtmlVM>("Offer/GetQuantityTables/" + _offerId, null);
         return View(qtableHtml);
      }
   }
}
