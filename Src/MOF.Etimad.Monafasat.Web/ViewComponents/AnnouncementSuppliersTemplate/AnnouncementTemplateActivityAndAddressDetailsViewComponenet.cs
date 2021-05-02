using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.AnnouncementSuppliersTemplate
{
   [ViewComponent(Name = "AnnouncementTemplateActivityAndAddressDetails")]
   public class AnnouncementTemplateActivityAndAddressDetailsViewComponenet : ViewComponent
   {
      private readonly IApiClient _ApiClient;
      public AnnouncementTemplateActivityAndAddressDetailsViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string announcementIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<AnnouncementTemplateActivityAndAreaDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementTemplateActivityAndAddressDetails/" + Util.Decrypt(announcementIdString), null);
            return View(result);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }


      }

   }
}
