using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   internal class MonafasatUserAddTenderHandler : AuthorizationHandler<MonafasatUserAddTenderRequirement>
   {
      public MonafasatUserAddTenderHandler(IServiceProvider serviceProvider)
      {
         var x = serviceProvider;
      }
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MonafasatUserAddTenderRequirement requirement)
      {

         var filterContext = context.Resource as AuthorizationFilterContext;
         if (filterContext == null)
         {
            context.Fail();
            return Task.FromResult(0);
         }
         var owner = context.User.Claims.FirstOrDefault(c => c.Type == "sub");
         if (owner == null)
         {
            context.Fail();
            return Task.FromResult(0);
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
