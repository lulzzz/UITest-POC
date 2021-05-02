using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "MediumQualificationSupplierDetails")]
   public class MediumQualificationSupplierDetails : ViewComponent
   {
      private IApiClient _ApiClient;
      public MediumQualificationSupplierDetails(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         QualificationMediumEvaluation result = await _ApiClient.GetAsync<QualificationMediumEvaluation>("Qualification/GetMediumQualificationSupplierDetails/" + qualificationId, null);
         return View(result);
      }

   }
}
