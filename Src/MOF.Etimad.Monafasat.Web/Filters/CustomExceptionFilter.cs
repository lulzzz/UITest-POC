using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System.Linq;
using System.Net;

namespace MOF.Etimad.Monafasat.Web.Filters
{
   public class CustomExceptionFilter : IExceptionFilter
   {
      private readonly IModelMetadataProvider _modelMetadataProvider;

      public CustomExceptionFilter(
          IModelMetadataProvider modelMetadataProvider)
      {
         _modelMetadataProvider = modelMetadataProvider;
      }

      public void OnException(ExceptionContext context)
      {
         var isJsonRequest = IsJsonRequest(context.HttpContext.Request);
         var isFormRequest = context.HttpContext.Request.HasFormContentType;

         if (isJsonRequest)
         {
            HandleJsonRequest(context);
         }
         else if (isFormRequest)
         {
            HandleFormRequest(context);
         }
      }

      private void HandleJsonRequest(ExceptionContext context)
      {
         JsonResult result = null;
         switch (context.Exception)
         {
            case BusinessRuleException ce:
               var error = ce.Message;
               result = new JsonResult(new { error });
               break;
            default:
               break;
         }

         if (result != null)
         {
            result.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = result;
         }
      }

      private void HandleFormRequest(
          ExceptionContext context)
      {
         switch (context.Exception)
         {
            case BusinessRuleException ce:
               var result = new ViewResult
               {
                  ViewName = context.ActionDescriptor.RouteValues["action"],
                  ViewData = new ViewDataDictionary(
                       _modelMetadataProvider,
                       context.ModelState)
               };
               result.ViewData.ModelState.AddModelError("", "");
               context.Result = result;
               break;
         }
      }

      private bool IsJsonRequest(HttpRequest request)
      {
         var typedHeaders = request.GetTypedHeaders();
         return typedHeaders.Accept?
                 .Any(a => a.MediaType == "application/json")
             ?? false;
      }
   }
}
