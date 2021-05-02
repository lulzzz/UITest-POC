using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{
   [ViewComponent(Name = "NegotiationQuantityTablesPaging")]
   public class NegotiationQuantityTablesPagingViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public NegotiationQuantityTablesPagingViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public IViewComponentResult Invoke(int tenderId, int offerId, string tableId, int formId, bool isReadOnly, int negotiationId)
      {
         return View(new TableModel { TenderId = tenderId, OfferId = offerId, TableId = tableId, FormId = formId, IsReadOnly = isReadOnly, negotiationId = negotiationId });
      }

   }
}
