using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "LargeQualificationSupplierDetails")]
   public class LargeQualificationSupplierDetails : ViewComponent
   {
      private IApiClient _ApiClient;
      public LargeQualificationSupplierDetails(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         QualificationLargeEvaluation result = await _ApiClient.GetAsync<QualificationLargeEvaluation>("Qualification/GetLargeQualificationSupplierDetails/" + qualificationId, null);
         return View(result);
      }

   }
}
