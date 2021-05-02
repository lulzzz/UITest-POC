using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "Attachments")]
   public class AttachmentsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public AttachmentsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         TenderAttachmentModel tenderModel = await _ApiClient.GetAsync<TenderAttachmentModel>("Tender/GetAttachmentsByTenderId/" + TenderId, null);
         //TempData["isAttachmentsChanged"] = tenderModel.AttachmentsChanges.Any();
         return View(tenderModel);
      }
   }
}
