using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AwardingReportRequirement : IAuthorizationRequirement { }

   public class AwardingReportHandler : AuthorizationHandler<AwardingReportRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AwardingReportRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.ApproveTenderAward, RoleNames.OffersCheckManager, RoleNames.OffersCheckSecretary, RoleNames.supplier, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary };
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
