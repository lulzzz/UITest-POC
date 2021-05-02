using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AuditerAndTechnicalRequirement : IAuthorizationRequirement { }

   public class AuditerAndTechnicalHandler : AuthorizationHandler<AuditerAndTechnicalRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuditerAndTechnicalRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.EtimadOfficer, RoleNames.Auditer, RoleNames.TechnicalCommitteeUser, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary };
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
