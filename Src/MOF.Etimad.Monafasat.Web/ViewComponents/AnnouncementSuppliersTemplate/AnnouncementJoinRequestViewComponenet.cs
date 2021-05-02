using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.AnnouncementSuppliersTemplate
{
   [ViewComponent(Name = "AnnouncementJoinRequestViewComponenet")]
   public class AnnouncementJoinRequestViewComponenet : ViewComponent
   {
      private readonly IApiClient _ApiClient;
      public AnnouncementJoinRequestViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string idString)
      {
         try
         {
            int announcementId = Util.Decrypt(idString);
            var result = await _ApiClient.GetAsync<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementJoinRequestDetails/" + announcementId, null);
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
