using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{

   [ViewComponent(Name = "TenderNews")]
   public class TenderNewsViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public TenderNewsViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      #region TenderNews 
      public async Task<IViewComponentResult> InvokeAsync(int tenderId)
      {
         TenderNewsModel result = await _ApiClient.GetAsync<TenderNewsModel>("Tender/GetTenderNewsByTenderId/" + tenderId, null);
         return View(result);
      }
      #endregion 
   }
}
