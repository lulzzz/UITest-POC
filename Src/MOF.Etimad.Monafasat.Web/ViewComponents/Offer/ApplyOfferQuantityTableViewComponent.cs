using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "ApplyOfferQuantityTable")]
   public class ApplyOfferQuantityTableViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public ApplyOfferQuantityTableViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public IViewComponentResult Invoke(int tenderId, int offerId, string tableId, int formId, bool isReadOnly)
      {
         return View(new TableModel { TenderId = tenderId, OfferId = offerId, TableId = tableId, FormId = formId, IsReadOnly = isReadOnly });
      }
   }
}
