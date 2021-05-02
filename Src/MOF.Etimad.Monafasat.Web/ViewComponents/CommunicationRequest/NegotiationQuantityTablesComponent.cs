using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{
   [ViewComponent(Name = "NegotiationQuantityTablesViewComponent")]
   public class NegotiationQuantityTablesViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public NegotiationQuantityTablesViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string negotiationId)
      {
         OfferQuantityTableHtmlVM qtableHtml = new OfferQuantityTableHtmlVM();
         var _negotiationId = Util.Decrypt(negotiationId);
         qtableHtml = await _ApiClient.GetAsync<OfferQuantityTableHtmlVM>("CommunicationRequest/GetNegotiationTableQuantityTables/" + _negotiationId, null);
         return View(qtableHtml);
      }
   }
}
