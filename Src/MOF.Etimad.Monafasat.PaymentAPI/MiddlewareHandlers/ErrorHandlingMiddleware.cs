using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.PaymentCallbackAPI.MiddlewareHandlers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (!(ex is BusinessRuleException || ex is UnHandledAccessException))
                    _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is EntityNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is BusinessRuleException) code = HttpStatusCode.Conflict;
            else if (exception is UnHandledAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is Exception) code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
