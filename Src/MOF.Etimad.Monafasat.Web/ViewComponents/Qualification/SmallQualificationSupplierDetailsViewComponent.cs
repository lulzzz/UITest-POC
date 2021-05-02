using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "SmallQualificationSupplierDetails")]
   public class SmallQualificationSupplierDetails : ViewComponent
   {
      private IApiClient _ApiClient;
      public SmallQualificationSupplierDetails(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int qualificationId)
      {
         QualificationSmallEvaluation result = await _ApiClient.GetAsync<QualificationSmallEvaluation>("Qualification/GetSmallQualificationSupplierDetails/" + qualificationId, null);
         return View(result);
      }

   }
}
