using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "CheckingResults")]
   public class CheckingResultsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public CheckingResultsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int tenderId)
      {
         var result = await _ApiClient.GetAsync<CheckingResultsModel>("Tender/GetCheckingResultsInformation/" + tenderId, null);
         return View(result);
      }

   }
}
