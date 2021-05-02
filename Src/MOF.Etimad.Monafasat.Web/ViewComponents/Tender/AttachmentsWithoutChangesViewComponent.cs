using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "AttachmentsWithoutChanges")]
   public class AttachmentsWithoutChangesViewComponent : ViewComponent
   {
      private IApiClient _apiClient;
      public AttachmentsWithoutChangesViewComponent(IApiClient apiClient)
      {
         _apiClient = apiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         TenderAttachmentModel tenderModel = await _apiClient.GetAsync<TenderAttachmentModel>("Tender/GetAttachmentsByTenderId/" + TenderId, null);
         return View(tenderModel);
      }
   }
}
