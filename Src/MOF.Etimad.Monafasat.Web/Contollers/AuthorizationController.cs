using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class AuthorizationController : BaseController
   {
      public AuthorizationController(IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      { }
      public IActionResult AccessDenied()
      {
         return View();
      }
      public IActionResult SupplierHasNoCR()
      {
         return View();
      }
   }
}
