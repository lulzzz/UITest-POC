using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "AcceptedPrequalificationSuppliers")]
   public class AcceptedPrequalificationSuppliers : ViewComponent
   {
      private IApiClient _ApiClient;
      public AcceptedPrequalificationSuppliers(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         AwardingDetailsModel result = await _ApiClient.GetAsync<AwardingDetailsModel>("Qualification/GetAcceptedPreQualificationDocuments/" + qualificationId, null);
         return View(result);
      }

   }
}
