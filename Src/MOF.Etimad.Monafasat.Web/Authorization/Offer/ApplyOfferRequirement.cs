using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ApplyOfferRequirement : IAuthorizationRequirement { }

   public class ApplayOfferHandler : AuthorizationHandler<ApplyOfferRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApplyOfferRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckManager, RoleNames.OffersCheckSecretary, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.supplier, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager };
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
