using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "ActivityDetails")]
   public class ActivityDetailsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public ActivityDetailsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         TenderRelationsModel tenderRelationModel = await _ApiClient.GetAsync<TenderRelationsModel>("Tender/GetRelationsDetailsByTenderId/" + Util.Decrypt(tenderIdString), null);
         return View(tenderRelationModel);

      }
   }
}
