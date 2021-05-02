using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "AwardingDetails")]
   public class AwardingDetailsViewComponent : ViewComponent
   {
      private readonly IApiClient _apiClient;
      public AwardingDetailsViewComponent(IApiClient ApiClient)
      {
         _apiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int tenderId)
      {
         var result = await _apiClient.GetAsync<AwardingDetailsModel>("Tender/GetAwardingInformationForSupplier/" + tenderId, null);
         return View(result);
      }

   }
}
