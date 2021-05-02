using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "QualificationSupplierDocumentAttachment")]
   public class QualificationSupplierDocumentAttachmentViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public QualificationSupplierDocumentAttachmentViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string PreQSupDocID)
      {
         int _PreQSupID = Util.Decrypt(PreQSupDocID);
         List<PreQualificationSupplierAttachmentModel> tenderModel = await _ApiClient.GetListAsync<PreQualificationSupplierAttachmentModel>("Qualification/GetAttachmentsByPreQSupId/" + _PreQSupID, null);
         return View(tenderModel);
      }

   }
}
