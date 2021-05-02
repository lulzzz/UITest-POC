using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "QuantitiesTable")]
   public class QuantitiesTableViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public QuantitiesTableViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         QuantitiesTablesModel quantitiesTablesModel = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesById/" + TenderId, null);
         //TempData["isquantitiesTablesChanged"] = quantitiesTablesModel.QuantitiesTablesChanges.Any();
         return View(quantitiesTablesModel);
      }
   }
}
