using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "MoreBasicDetails")]
   public class MoreBasicDetailsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public MoreBasicDetailsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         BasicTenderModel tenderModel = await _ApiClient.GetAsync<BasicTenderModel>("Tender/GetBasicById/" + TenderId, null);
         return View(tenderModel);
      }
   }
}
