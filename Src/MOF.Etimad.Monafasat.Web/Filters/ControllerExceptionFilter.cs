using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MOF.Etimad.Monafasat.Web.Filters
{
   public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

      public ControllerExceptionFilterAttribute(
          IWebHostEnvironment hostingEnvironment,
          ITempDataDictionaryFactory tempDataDictionaryFactory)
      {
         _hostingEnvironment = hostingEnvironment;
         _tempDataDictionaryFactory = tempDataDictionaryFactory;
      }

      public override void OnException(ExceptionContext context)
      {

         //How construct a temp data here which I want to pass to error page?
         var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);
         tempData["Error"] = context.Exception.Message;
         var result = new ViewResult
         {
            ViewName = "Error"
         };
         context.ExceptionHandled = true; // mark exception as handled
         context.Result = new JsonResult("Error !!!");
      }
   }
}
