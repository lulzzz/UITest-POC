using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class ExceptionMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<ExceptionMiddleware> _logger;

      public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, bool isDevelopment)
      {
         _next = next;
         _logger = logger;
         IsDevelopment = isDevelopment;
      }

      public bool IsDevelopment { get; }

      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _next(context);
         }
         catch (AuthorizationException ex)
         {
            await HandleExceptionAsync(context, ex);
         }
         catch (Exception ex)
         {
            if (!(ex is BusinessRuleException || ex is UnauthorizedAccessException || ex is AuthorizationException))
               _logger.LogError(ex.Message + "\n" + ex.StackTrace);
            await HandleExceptionAsync(context, ex);
         }
      }

      private async Task HandleExceptionAsync(HttpContext context, Exception exception)
      {
         context.Response.ContentType = "application/json";
         if (context.Response.HasStarted)
         {
            _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
            context.Response.Redirect("/Home/Error");
         }
         _logger.LogError(exception.ToString(), "General Exception");
         context.Response.Redirect("/Home/Error");
         var code = HttpStatusCode.InternalServerError;
         if (exception is Exception) code = HttpStatusCode.BadRequest;
         if (exception is UnauthorizedAccessException)
         { context.Response.Redirect("/Authorization/AccessDenied"); return; }
         if (exception.Message == "Logout" || exception is AuthorizationException)
         {
            if (context.Request.Headers.Any(a => a.Key == "X-Requested-With") && context.Request.Headers.FirstOrDefault(a => a.Key == "X-Requested-With").Value.ToString() == "XMLHttpRequest")
            {
               context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
               await context.Response.WriteAsync("Logout");
               return;
            }
            if (exception.Message == "Logout")
               context.Response.Redirect("/Account/Logout");
            else
               context.Response.Redirect("/Authorization/AccessDenied");
            return;
         }
         var result = JsonConvert.SerializeObject(new { error = exception.Message });
         context.Response.StatusCode = (int)code;
         if (IsDevelopment)
            await context.Response.WriteAsync(result + (result != "Logout" ? "\n" + exception.StackTrace : ""));
         else
            context.Response.Redirect("/Home/Error");

      }
   }

   public static class ExceptionMiddlewareExtensions
   {
      public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder, bool isDevelopement)
      {
         return builder.UseMiddleware<ExceptionMiddleware>(isDevelopement);
      }
   }
}
