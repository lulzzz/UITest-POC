using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OffersCheckSecretaryRequirement : IAuthorizationRequirement { }

   public class OffersCheckSecretaryHandler : AuthorizationHandler<OffersCheckSecretaryRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersCheckSecretaryRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersPurchaseManager };
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
