using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{
   [ViewComponent(Name = "Enquiry")]
   public class EnquiryViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;

      public EnquiryViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int tenderId, bool canAddEnquiry)
      {

         var tenderModel = await _ApiClient.GetListAsync<EnquiryModel>("Enquiry/GetAllEnquiryRepliesByTenderId/" + tenderId, null);
         ViewBag.canAddEnquiry = canAddEnquiry;
         return View(tenderModel);

      }
   }
}
