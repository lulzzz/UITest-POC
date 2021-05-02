using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{

   [ViewComponent(Name = "TenderQuantityTableChanges")]
   public class TenderQuantityTableChangesViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public TenderQuantityTableChangesViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      #region TenderNews 
      public IViewComponentResult Invoke(int tenderId, string tableId, int formId, bool isReadOnly, bool isNewTable, bool isTableDeleted)
      {
         return View(new TableModel { TenderId = tenderId, TableId = tableId, FormId = formId, IsReadOnly = isReadOnly, IsNewTable = isNewTable, IsTableDeleted = isTableDeleted });
      }
      #endregion 
   }
}
