using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.PaymentCallbackAPI.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.PaymentCallbackAPI.MiddlewareHandlers
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfigurationRoot _configuration;
        private readonly string _userName;
        private readonly string _userPassword;
        private readonly string _userCalim;
        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            _userName = _configuration.GetSection("EsbSettings:PayemntNotificationUserName").Value;
            _userPassword = _configuration.GetSection("EsbSettings:PayemntNotificationUserPassword").Value;
            _userCalim = _configuration.GetSection("EsbSettings:PayemntNotificationUserClaim").Value;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var authHeader = context.Request.Headers.FirstOrDefault(a => a.Key == "Authorization").Value.ToString();
                if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring("Basic ".Length).Trim();
                    System.Console.WriteLine(token);
                    var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialstring.Split(':');
                    if (credentials[0] == _userName && credentials[1] == _userPassword)
                    {
                        var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, _userCalim) };
                        var identity = new ClaimsIdentity(claims, "Basic");
                        context.User = new ClaimsPrincipal(identity);
                    }
                }
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var result = JsonConvert.SerializeObject(new PaymentNotificationResponseModel() { StatusCode = context.Response.StatusCode.ToString(), StatusDesc = HttpStatusCode.Unauthorized.ToString() });
                await context.Response.WriteAsync(result);
                return;
            }
        }
    }
    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}

