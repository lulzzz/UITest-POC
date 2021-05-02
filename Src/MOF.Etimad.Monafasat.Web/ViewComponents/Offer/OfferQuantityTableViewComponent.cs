using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferQuantityTable")]
   public class OfferQuantityTableViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OfferQuantityTableViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public IViewComponentResult Invoke(int tenderId, int offerId, string tableId, int formId, bool isReadOnly)
      {
         return View(new TableModel { TenderId = tenderId, OfferId = offerId, TableId = tableId, FormId = formId, IsReadOnly = isReadOnly });
      }
   }
}
