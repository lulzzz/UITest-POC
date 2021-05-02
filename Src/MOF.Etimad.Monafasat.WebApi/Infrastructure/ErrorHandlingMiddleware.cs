using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using Newtonsoft.Json;
using NLog;
using System;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace MOF.Etimad.Monafasat.WebApi.Infrastructure
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.next = next;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context) /* other dependencies */
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
                LogEventInfo theEvent = new LogEventInfo(NLog.LogLevel.Error, logger.Name, ex.Message);
                GlobalDiagnosticsContext.Set("userName", GetCurrenUserId());
                if (!(ex is BusinessRuleException || ex is UnHandledAccessException))
                    _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }

        private string GetCurrenUserId()
        {
            string username = null;
            if (_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "fullname") != null)
            {
                username = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "fullname").Value;
            }
            if (_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "preferred_username") != null)
            {
                username += " | ";
                username += _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value;
            }
            return username;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is EntityNotFoundException || exception is WebServiceException) code = HttpStatusCode.NotFound;
            else if (exception is BusinessRuleException) code = HttpStatusCode.Conflict;
            else if (exception is UnHandledAccessException) code = HttpStatusCode.MethodNotAllowed;
            else if (exception is AuthorizationException) code = HttpStatusCode.Unauthorized;
            else if (exception is Exception) code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
