using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{
   //used in pages: Tender/Details & _DetailsEnquiryContent
   [ViewComponent(Name = "TenderDetailsForEscalation")]
   public class TenderDetailsForEscalationViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;

      public TenderDetailsForEscalationViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         var tenderModel = await _ApiClient.GetAsync<TenderPlaintDatailsModel>("CommunicationRequest/FindTenderForEscalationRequestById/" + tenderIdString, null);
         return View(tenderModel);
      }

   }
}
