//using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading;

namespace MOF.Etimad.Monafasat.Web.Filters
{
   public class LocalizationActionFilter : ActionFilterAttribute
   {
      public LocalizationActionFilter()
      {
      }

      public override void OnActionExecuting(ActionExecutingContext actionContext)
      {
         var request = actionContext.HttpContext.Request;
         var response = actionContext.HttpContext.Response;
         if (!request.Cookies.ContainsKey("language"))
            response.Cookies.Append("language", "ar-SA", new CookieOptions { Expires = DateTime.Today.AddYears(1) });
         //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(request.Cookies["language"]);
         var language = "";
         if (!string.IsNullOrEmpty(request.Cookies["language"]))
            language = request.Cookies["language"].Split("|")[0].Split("=")[1];
         response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                  new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
         response.Cookies.Append("language", CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                  new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
         Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);

         base.OnActionExecuting(actionContext);
      }
   }
}
