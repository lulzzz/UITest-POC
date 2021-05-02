using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "Approval")]
   public class ApprovalViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public ApprovalViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString, int TenderStatusId)
      {
         var tenderHistory = await _ApiClient.GetAsync<TenderHistoryModel>("Tender/GetTenderHistoryByUserIdAndTenderIdAndStatusId/" + tenderIdString + "/" + TenderStatusId, null);

         tenderHistory = tenderHistory ?? new ViewModel.TenderHistoryModel { StatusId = TenderStatusId };
         return View(tenderHistory);
      }
   }
}
