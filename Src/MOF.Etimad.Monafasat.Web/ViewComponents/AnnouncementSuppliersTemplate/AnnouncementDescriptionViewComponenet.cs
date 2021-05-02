using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.AnnouncementSuppliersTemplate
{
   [ViewComponent(Name = "AnnouncementDescription")]
   public class AnnouncementDescriptionViewComponenet : ViewComponent
   {
      private readonly IApiClient _ApiClient;
      public AnnouncementDescriptionViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string idString)
      {
         try
         {
            int id = Util.Decrypt(idString);
            var result = await _ApiClient.GetAsync<AnnouncementDescriptionModel>("AnnouncementSupplierTemplate/GetAnnouncementDescriptionDetails/" + id, null);
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
