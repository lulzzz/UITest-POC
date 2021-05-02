using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OffersCheckManagerRequirement : IAuthorizationRequirement { }

   public class OffersCheckManagerHandler : AuthorizationHandler<OffersCheckManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersCheckManagerRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckManager, RoleNames.ApproveTenderAward, RoleNames.OffersPurchaseManager, RoleNames.OffersOpeningAndCheckManager };
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
