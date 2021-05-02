using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OffersCheckSecretaryAndManagerRequirement : IAuthorizationRequirement { }

   public class OffersCheckSecretaryAndManagerHandler : AuthorizationHandler<OffersCheckSecretaryAndManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersCheckSecretaryAndManagerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.ApproveTenderAward, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary };
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
