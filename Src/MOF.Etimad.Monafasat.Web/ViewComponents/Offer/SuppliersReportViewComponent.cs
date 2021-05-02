using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "SuppliersReportView")]
   public class SuppliersReportViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public SuppliersReportViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         int tenderId = Util.Decrypt(tenderIdString);
         QueryResult<BasicTenderModel> tenderModel = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Offer/GetSuppliersReportByTenderId/" + tenderId, null);
         return View(tenderModel);
      }
   }
}
