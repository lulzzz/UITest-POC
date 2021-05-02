using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "BasicDetails")]
   public class BasicDetailsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public BasicDetailsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         var tenderBasicDataModel = await _ApiClient.GetAsync<AllBasicTenderDataModel>("Tender/GetBasicTenderDetailsById/" + tenderIdString, null);
         return View(tenderBasicDataModel);
      }
   }
}
