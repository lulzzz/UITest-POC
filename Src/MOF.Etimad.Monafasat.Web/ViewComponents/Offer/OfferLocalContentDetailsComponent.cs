using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Offer
{
   [ViewComponent(Name = "OfferLocalContentDetailsComponent")]
   public class OfferLocalContentDetailsComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public OfferLocalContentDetailsComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }

      public async Task<IViewComponentResult> InvokeAsync(string offerIdString)
      {
         try
         {
            OfferLocalContentDetailsModel model = await _ApiClient.GetAsync<OfferLocalContentDetailsModel>("Offer/GetOfferLocalContentDetails/" + offerIdString, null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
      }
   }
}
