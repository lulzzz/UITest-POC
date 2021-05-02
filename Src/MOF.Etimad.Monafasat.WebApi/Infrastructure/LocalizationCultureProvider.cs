using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;

namespace MOF.Etimad.Monafasat.WebApi.Infrastructure
{
    public class CultureSettingResourceFilter : IResourceFilter, IOrderedFilter
    {
        public int Order
        {
            get
            {
                return int.MinValue;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // By this time the response would already have been started, so do not try to modify the response
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            var language = "";
            if (!request.Headers.ContainsKey("language"))
            {
                response.Cookies.Append("language", "ar-SA", new CookieOptions { Expires = DateTime.Today.AddYears(1), HttpOnly = true });

                language = "ar-SA";
            }
            else
                language = request.Headers["language"];
            var culture = new CultureInfo(language);
            // set your culture here
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }


}
