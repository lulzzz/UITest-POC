using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "QualificationSupplierData")]
   public class QualificationSupplierData : ViewComponent
   {
      private IApiClient _ApiClient;
      public QualificationSupplierData(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string qualificationId, string SupplierId)
      {
         QualificationSupplierDataReviewViewModel result = await _ApiClient.GetAsync<QualificationSupplierDataReviewViewModel>("Qualification/GetSupplierDataReviewModel/" + Util.Decrypt(qualificationId) + "/" + SupplierId, null);
         return View(result);
      }

   }
}

