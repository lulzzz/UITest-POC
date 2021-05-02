using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "QuantitiesTableWithoutChanges")]
   public class QuantitiesTableWithoutChangesViewComponent : ViewComponent
   {
      private IApiClient _apiClient;
      public QuantitiesTableWithoutChangesViewComponent(IApiClient apiClient)
      {
         _apiClient = apiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         QuantityTableStepDTO quantitiesTablesModel = await _apiClient.GetAsync<QuantityTableStepDTO>("Tender/GetTenderQuantityItemsDetailsById/" + TenderId, null);
         return View(quantitiesTablesModel);
      }
   }
}
