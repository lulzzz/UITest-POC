using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "CommunicationRequestsViewComponenet")]
   public class CommunicationRequestsViewComponenet : ViewComponent
   {
      private IApiClient _ApiClient;
      public CommunicationRequestsViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderCommunicationRequestModel model = await _ApiClient.GetAsync<TenderCommunicationRequestModel>("Tender/GetCommunicationRequestsDetailsTenderId/" + Util.Decrypt(tenderIdString), null);

         return View(model);
      }
   }
}
