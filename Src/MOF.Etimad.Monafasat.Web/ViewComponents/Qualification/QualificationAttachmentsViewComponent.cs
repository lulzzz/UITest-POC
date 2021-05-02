using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "QualificationAttachments")]
   public class QualificationAttachmentsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public QualificationAttachmentsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string TenderId)
      {
         TenderAttachmentModel tenderModel = await _ApiClient.GetAsync<TenderAttachmentModel>("Tender/GetAttachmentsByTenderId/" + Util.Decrypt(TenderId), null);
         //TempData["isAttachmentsChanged"] = tenderModel.AttachmentsChanges.Any();
         return View(tenderModel);
      }
   }
}
