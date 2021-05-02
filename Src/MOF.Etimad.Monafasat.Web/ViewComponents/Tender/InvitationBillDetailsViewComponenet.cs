using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{
   [ViewComponent(Name = "InvitationBillDetailsViewComponenet")]
   public class InvitationBillDetailsViewComponenet : ViewComponent
   {
      private IApiClient _ApiClient;
      public InvitationBillDetailsViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int TenderId, string StatusId)
      {
         PurchaseModel model = await _ApiClient.GetAsync<PurchaseModel>("Tender/GetInvitationBillDetailsModelByTenderId/" + TenderId, null);

         model.TenderId = TenderId;
         model.StatusIdString = StatusId;
         model.StatusId = Util.Decrypt(StatusId);
         model.TenderIdString = Util.Encrypt(TenderId);
         return View(model);
      }
   }
}
