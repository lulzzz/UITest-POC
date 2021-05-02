using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Filters
{
   //public class BasicMiddleware
   //{
   //   public Task Invoke(HttpContext context)
   //   {
   //      // do something with context
   //   }
   //}

   public class JsonExceptionMiddleware
   {
      public async Task Invoke(HttpContext context)
      {
         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

         var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
         if (ex == null) return;

         var error = new
         {
            message = ex.Message
         };


         ITempDataDictionaryFactory factory = context.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
         ITempDataDictionary tempData = factory.GetTempData(context);
         tempData["Error"] = ex.Message;

         context.Response.ContentType = "application/json";

         using (var writer = new StreamWriter(context.Response.Body))
         {
            new JsonSerializer().Serialize(writer, error);
            await writer.FlushAsync().ConfigureAwait(false);
         }
      }
   }
}
