using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.AnnouncementSuppliersTemplate
{
   [ViewComponent(Name = "AnnouncementTendersDetail")]
   public class AnnouncementTendersDetailViewComponenet : ViewComponent
   {
      private readonly IApiClient _ApiClient;
      public AnnouncementTendersDetailViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string announcementIdString)
      {
         try
         {
            int id = Util.Decrypt(announcementIdString);
            var result = await _ApiClient.GetAsync<TenderAnnouncementSuppliersTemplateDetails>("AnnouncementSupplierTemplate/GetAnnouncementTendersDetail/" + id, null);
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
