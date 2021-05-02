using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Enquiry
{

   [ViewComponent(Name = "TenderDetailsForExtendOffers")]
   public class TenderDetailsForExtendOffersViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;

      public TenderDetailsForExtendOffersViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderIdString)
      {
         int tenderId = Util.Decrypt(tenderIdString);
         var extendOffersValidityModel = await _ApiClient.GetAsync<ExtendOffersValidityModel>("CommunicationRequest/GetTenderDetailsForCheckingOrDirectPurchaseSecretary/" + tenderId, null);
         return View(extendOffersValidityModel);
      }

   }
}
