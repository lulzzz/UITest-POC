using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class UIElementsController : BaseController
   {
      public UIElementsController(IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      { }
      public IActionResult Index()
      {
         return View();
      }
      public IActionResult Create()
      {
         return View();
      }
      public IActionResult Details()
      {
         return View();
      }
      public IActionResult Wizard()
      {
         return View();
      }
      public IActionResult FileUpload()
      {
         return View();
      }
      public IActionResult TendarDetails()
      {
         return View();
      }
      [AllowAnonymous]
      public IActionResult Home()
      {
         return View();
      }
      public ActionResult userProfile()
      {
         return View();
      }
      public ActionResult Charts()
      {
         return View();
      }
   }
}
