using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AuditerRequirement : IAuthorizationRequirement { }

   public class AuditerHandler : AuthorizationHandler<AuditerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuditerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.EtimadOfficer, RoleNames.Auditer, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersCheckManager };
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
