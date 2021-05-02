using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OffersOppeningSecretaryRequirement : IAuthorizationRequirement { }

   public class OffersOppeningSecretaryHandler : AuthorizationHandler<OffersOppeningSecretaryRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersOppeningSecretaryRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersOppeningSecretary };
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
