using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "InvitationsViewComponent")]
   public class InvitationsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public InvitationsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         ViewBag.TenderIdString = tenderIdString;
         return View();
      }
   }
}
