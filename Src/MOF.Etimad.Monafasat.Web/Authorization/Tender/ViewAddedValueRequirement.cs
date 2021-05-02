using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ViewAddedValueRequirement : IAuthorizationRequirement { }

   public class ViewAddedValueHandler : AuthorizationHandler<ViewAddedValueRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewAddedValueRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.ProductManager, RoleNames.ProductManagerDisplay };
         bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
         if (hasAccess)
         {
            context.Succeed(requirement);
            return Task.CompletedTask;
         }
         context.Fail();
         return Task.CompletedTask;
      }
   }
}
