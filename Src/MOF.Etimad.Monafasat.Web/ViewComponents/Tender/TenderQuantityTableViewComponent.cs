using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{

   [ViewComponent(Name = "TenderQuantityTable")]
   public class TenderQuantityTableViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public TenderQuantityTableViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      #region TenderNews 
      public IViewComponentResult Invoke(int tenderId, string tableId, int formId, bool isReadOnly)
      {
         return View(new TableModel { TenderId = tenderId, TableId = tableId, FormId = formId, IsReadOnly = isReadOnly });
      }
      #endregion 
   }
}
