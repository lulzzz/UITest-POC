//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MOF.Etimad.Monafasat.Web.App_Start
//{
//   public class AuthenticatedAttribute : Attribute, IAsyncActionFilter
//   {
//      public AuthenticatedAttribute(/*IConfiguration configuration */)
//      {
//         //_configuration = configuration;
//      }

//      //private IConfiguration _configuration { get; set; }

//      public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//      {
//         var headers = context.HttpContext.Request.Headers;

//         bool result = false;
//         if (headers.ContainsKey("ClientId") && headers.ContainsKey("ClientSecret"))
//         {
//            var clientId = headers["ClientId"].ToString();
//            var clientSecret = headers["ClientSecret"].ToString().Sha256();

//            //var client = await _applicationService.FindByClientIdAsync(clientId);
//            //var secret = client.ClientSecrets.FirstOrDefault()?.Value;
//            //if (clientId.ToString() == client.ClientId && clientSecret == secret)
//               result = true;
//         }

//         if (result)
//            await next();
//         else
//            context.Result = new UnauthorizedResult();
//      }
//   }
//}
