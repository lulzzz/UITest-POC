using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.AnnouncementSuppliersTemplate
{
   [ViewComponent(Name = "AnnouncementSuppliersTemplateDetail")]
   public class AnnouncementSuppliersTemplateDetailViewComponenet : ViewComponent
   {
      private readonly IApiClient _ApiClient;
      public AnnouncementSuppliersTemplateDetailViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int id)
      {
         try
         {
            var result = await _ApiClient.GetAsync<AnnouncementSuppliersTemplateDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementSupplierTemplateDetails/" + id, null);
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
