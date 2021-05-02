using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "RelatedTendersViewComponent")]
   public class RelatedTendersViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public RelatedTendersViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int tenderId)
      {
         var result = await _ApiClient.GetListAsync<BasicTenderModel>("Tender/GetRelatedTendersByActivities/" + tenderId, null);
         return View(result);
      }
   }
}
