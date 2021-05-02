using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Qualification
{
   [ViewComponent(Name = "PreQualificationPartialVisitorDetail")]
   public class PreQualificationPartialVisitorDetailViewComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      public PreQualificationPartialVisitorDetailViewComponent(IApiClient ApiClient)
      {
         _ApiClient = ApiClient;
      }
      public async Task<IViewComponentResult> InvokeAsync(string qualificationId)
      {
         try
         {
            var preQualificationApplyDocumentsModel = await _ApiClient.GetAsync<PreQualificationDetailsModel>("Qualification/GetPreQualificationDetails/" + Util.Decrypt(qualificationId), null);
            return View(preQualificationApplyDocumentsModel);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }


      }

   }
}
