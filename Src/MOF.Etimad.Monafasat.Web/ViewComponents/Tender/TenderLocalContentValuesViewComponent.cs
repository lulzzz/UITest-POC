using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "TenderLocalContentValues")]
   public class TenderLocalContentValuesViewComponent : ViewComponent
   {
      private readonly IApiClient _apiClient;
      public TenderLocalContentValuesViewComponent(IApiClient ApiClient)
      {
         _apiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderLocalContentValuesViewModel localContent = await _apiClient.GetAsync<TenderLocalContentValuesViewModel>("Tender/GetTenderLocalContenetValuesByTenderId/" + Util.Decrypt(tenderIdString), null);
        
         return View(localContent);
      }
   }
}
