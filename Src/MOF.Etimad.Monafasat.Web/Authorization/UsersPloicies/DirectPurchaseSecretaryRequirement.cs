using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DirectPurchaseSecretaryRequirement : IAuthorizationRequirement { }

   public class DirectPurchaseSecretaryHandler : AuthorizationHandler<DirectPurchaseSecretaryRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DirectPurchaseSecretaryRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersPurchaseSecretary };

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
