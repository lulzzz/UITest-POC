using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class SupplierEnquiriesOnTenderRequirement : IAuthorizationRequirement { }

   public class SupplierEnquiriesOnTenderHandler : AuthorizationHandler<SupplierEnquiriesOnTenderRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupplierEnquiriesOnTenderRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.TechnicalCommitteeUser, RoleNames.supplier, RoleNames.Auditer };
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
