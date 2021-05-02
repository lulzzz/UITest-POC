using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CheckTenderOffersRequirement : IAuthorizationRequirement { }

   public class CheckTenderOffersHandler : AuthorizationHandler<CheckTenderOffersRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckTenderOffersRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckManager , RoleNames.OffersCheckSecretary
            , RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.ApproveTenderAward,
            RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary };
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
