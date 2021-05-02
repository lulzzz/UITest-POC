using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "PreQualificationDetail")]
   public class PreQualificationDetailViewComponenet : ViewComponent
   {
      private IApiClient _ApiClient;
      public PreQualificationDetailViewComponenet(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(int id)
      {
         try
         {
            var result = await _ApiClient.GetAsync<PreQualificationDetailsModel>("Qualification/GetPreQualificationDetails/" + id, null);
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
