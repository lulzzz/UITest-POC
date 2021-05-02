using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "LargeConfigQualificationDetails")]
   public class LargeConfigQualificationDetails : ViewComponent
   {
      private IApiClient _ApiClient;
      public LargeConfigQualificationDetails(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         QualificationLargeEvaluation result = await _ApiClient.GetAsync<QualificationLargeEvaluation>("Qualification/GetLargeConfigQualificationDetails/" + qualificationId, null);
         return View(result);
      }

   }
}
