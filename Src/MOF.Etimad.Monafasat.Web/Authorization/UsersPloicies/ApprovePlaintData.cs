using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ApprovePlaintData : IAuthorizationRequirement { }

   public class ApprovePlaintDataHandler : AuthorizationHandler<ApprovePlaintData>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApprovePlaintData requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager };
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
