using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "ApplyOfferComponent")]
    public class ApplyOfferComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public ApplyOfferComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string tenderId, string offerId)
      {
         OfferModel offer;
         var _tenderId = Util.Decrypt(tenderId);
         ViewBag.tenderIdEncrypt = tenderId;
         if (offerId == null)
         {
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GetQuantityTablesByTenderId/" + _tenderId, null);
         }
         else
         {
            var _offerId = Util.Decrypt(offerId);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GetQuantityTablesByTenderAndOffer/" + _tenderId + "/" + _offerId, null);
         }
         return View(offer);
      }
   }
}
