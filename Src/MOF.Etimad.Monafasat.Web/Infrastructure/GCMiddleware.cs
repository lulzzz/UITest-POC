using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class GCMiddleware
   {
      private readonly RequestDelegate _next;

      public GCMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext httpContext)
      {
         await _next(httpContext);
         GC.Collect(2, GCCollectionMode.Forced, true);
         GC.WaitForPendingFinalizers();
      }
   }
}
