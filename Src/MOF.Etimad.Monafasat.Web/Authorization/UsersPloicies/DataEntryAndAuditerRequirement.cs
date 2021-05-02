using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DataEntryAndAuditerRequirement : IAuthorizationRequirement { }

   public class DataEntryAndAuditerHandler : AuthorizationHandler<DataEntryAndAuditerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataEntryAndAuditerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.EtimadOfficer, RoleNames.PurshaseSpecialist, RoleNames.OffersPurchaseSecretary
         , RoleNames.OffersCheckSecretary , RoleNames.OffersPurchaseManager
         , RoleNames.OffersCheckManager};
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
