using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "QualificationSupplierIDMInfo")]
   public class QualificationSupplierIDMInfo : ViewComponent
   {
      private IApiClient _ApiClient;
      public QualificationSupplierIDMInfo(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string SupplierId)
      {
         SupplierFullDataViewModel result = await _ApiClient.GetAsync<SupplierFullDataViewModel>("Qualification/GetSupplierIDMInfo/" + SupplierId, null);
         result.YasserInfo = result.YasserInfo ?? new YasserApiModel();
         return View(result);

      }

   }
}

