using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "TenderBasicDetails")]
   public class TenderBasicDetailsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public TenderBasicDetailsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsById/" + tenderIdString, null);
         return View(tenderDetailsModel);
      }
   }
}
