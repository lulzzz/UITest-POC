using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OpeningQuantityTablesComponent")]
   public class OpeningQuantityTablesComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OpeningQuantityTablesComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string offerIdString, bool allowEdit)
      {
         QuantitiesTemplateModel qtableHtml = new QuantitiesTemplateModel();
         if (offerIdString == null)
         {
         }
         else
         {
            qtableHtml = await _ApiClient.GetAsync<QuantitiesTemplateModel>("Offer/GetOpeningQuantityTablesTest/" + Util.Decrypt(offerIdString) + "/" + allowEdit, null);
         }
         return View(qtableHtml);
      }
   }
}
