using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{
   //used in pages: Tender/Details & _DetailsEnquiryContent
   [ViewComponent(Name = "TenderDetailsForPlaint")]
   public class TenderDetailsForPlaintViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;

      public TenderDetailsForPlaintViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         var tenderModel = await _ApiClient.GetAsync<TenderPlaintDatailsModel>("CommunicationRequest/FindTenderForPlaintRequestById/" + tenderIdString, null);
         return View(tenderModel);
      }

   }
}
