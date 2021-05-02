using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "BasicTenderInfo")]
   public class BasicTenderInfoViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public BasicTenderInfoViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId)
      {
         TenderInformationModel tenderModel = await _ApiClient.GetAsync<TenderInformationModel>("Tender/GetTenderByIdToApplyOffer/" + TenderId, null);
         return View(tenderModel);
      }
   }
}
