using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Web.Base
{

   [AutoValidateAntiforgeryToken]
   public class BaseController : Controller
   {
      protected RootConfiguration _rootConfiguration;
      public BaseController(IOptionsSnapshot<RootConfiguration> rootConfiguration)
      {
         _rootConfiguration = rootConfiguration.Value;
      }

      public void AddMessage(string message)
      {
         this.TempData["Message"] = message;
      }

      public void AddWarnning(string message)
      {
         this.TempData["Warnning"] = message;
      }

      public void AddError(string error)
      {
         this.TempData["Error"] = error;
      }

      public JsonResult JsonErrorMessage(string errorMessage)
      {
         var response = HttpContext.Response;
         response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
         return Json(new { message = errorMessage });
      }
      public static string RemoveSpecialChars(string str)
      {
         string[] chars = new string[] { "/", "!", "@", "#", "$", "%", "^", "&", "*", "+", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "،", "," };
         for (int i = 0; i < chars.Length; i++)
         {
            if (str.Contains(chars[i]))
            {
               str = str.Replace(chars[i], "");
            }
         }
         return str;
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="context"></param>
      public override void OnActionExecuting(ActionExecutingContext context)
      {
         var isSubscribe = _rootConfiguration.SubScriptionConfiguration.IsSubscribe;

         if (isSubscribe.ToLower() == "true" && !context.HttpContext.User.IsSubscription() && !Request.Path.Value.Contains("/Account/SubscriptionUrlAsync") && User.IsInRole(RoleNames.supplier))
         {
            context.Result = new RedirectResult("/Account/SubscriptionUrlAsync");
            return;
         }
         if (User.UserRoles().Count > 0 && User.UserCategory() == 6 && string.IsNullOrEmpty(User.SupplierNumber()))
            context.HttpContext.Response.Redirect("/account/changeComericalRegisteration");
         if (User.UserRoles().Count > 0 && (User.UserCategory() == 1 || User.UserIsGovVendor() || User.UserCategory() == (int)Enums.IDMUserCategory.VRO) && string.IsNullOrEmpty(User.SupplierAgency()))
            context.HttpContext.Response.Redirect("/account/changeAgency");
         var request = context.HttpContext.Request;
         var response = context.HttpContext.Response;
         var language = "";
         if (!request.Cookies.ContainsKey("language"))
         {
            response.Cookies.Append("language", "ar-SA", new CookieOptions { Expires = DateTime.Today.AddYears(1) });
            language = "ar-SA";
         }
         else
            language = request.Cookies["language"];
      }

      [NonAction]
      public List<string> GetModalStateErrors()
      {
         var result = new List<string>();

         foreach (var modelState in ViewData.ModelState.Values)
         {
            foreach (var error in modelState.Errors)
            {
               result.Add(error.ErrorMessage);
            }
         }
         return result;
      }
      [NonAction]
      protected string FormatErrorMessage(List<string> errors)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(Resources.SharedResources.ErrorMessages.PleaseFixTheErrors);

         sb.Append("<ul>");

         foreach (var item in errors)
            sb.AppendLine($"<li>{item}</li>");

         sb.Append("</ul>");

         return sb.ToString();
      }


   }
}
