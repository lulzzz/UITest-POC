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
   [ViewComponent(Name = "TenderLocalContentDetails")]
   public class TenderLocalContentDetailsViewComponent : ViewComponent
   {
      private readonly IApiClient _apiClient;
      public TenderLocalContentDetailsViewComponent(IApiClient ApiClient)
      {
         _apiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         LocalContentDetailsViewModel localContent = await _apiClient.GetAsync<LocalContentDetailsViewModel>("Tender/GetLocalContentDetailsForSupplierByTenderId/" + Util.Decrypt(tenderIdString), null);
        
         return View(localContent);
      }
   }
}
