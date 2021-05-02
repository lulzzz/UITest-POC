using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "TenderDatesWithoutChanges")]
   public class TenderDatesWithoutChangesViewComponent : ViewComponent
   {
      private readonly IApiClient _apiClient;
      public TenderDatesWithoutChangesViewComponent(IApiClient apiClient)
      {
         _apiClient = apiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderDatesModel tenderModel = await _apiClient.GetAsync<TenderDatesModel>("Tender/GetTenderDatesDetailsById/" + Util.Decrypt(tenderIdString), null);
         return View(tenderModel);
      }
   }
}
