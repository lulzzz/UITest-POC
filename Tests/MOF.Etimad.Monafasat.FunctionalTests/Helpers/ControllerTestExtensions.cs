using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public static class ControllerTestExtensions
    {
        public static T WithIdentity<T>(this T controller, Claim[] claims) where T : Controller
        {
            controller.EnsureHttpContext();

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }

        public static T WithAnonymousIdentity<T>(this T controller) where T : Controller
        {
            controller.EnsureHttpContext();

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, ""),
                                new Claim(ClaimTypes.Name, "")
                            }, "TestAuthentication"));

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }

        private static T EnsureHttpContext<T>(this T controller) where T : Controller
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }

            if (controller.ControllerContext.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            return controller;
        }
    }
}
