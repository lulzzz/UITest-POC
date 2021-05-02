using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "MediumConfigQualificationDetails")]
   public class MediumConfigQualificationDetails : ViewComponent
   {
      private IApiClient _ApiClient;
      public MediumConfigQualificationDetails(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         QualificationMediumEvaluation result = await _ApiClient.GetAsync<QualificationMediumEvaluation>("Qualification/GetMediumConfigQualificationDetails/" + qualificationId, null);
         return View(result);
      }

   }
}
