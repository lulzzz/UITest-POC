// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;

namespace MOF.Etimad.Monafasat.Web.Controllers
{

   public class HomeController : BaseController
   {
      public HomeController(IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         //_iPro = provider.CreateProtector("HomeController");
      }

      public IActionResult UnAssingedUser()
      {
         return View();
      }

      public IActionResult Error()
      {
         return View();
      }
   }
}
