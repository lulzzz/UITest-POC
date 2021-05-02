using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DataEntryRequirement : IAuthorizationRequirement { }

   public class DataEntryHandler : AuthorizationHandler<DataEntryRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataEntryRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.DataEntry, RoleNames.OffersPurchaseSecretary, RoleNames.OffersCheckSecretary };
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
