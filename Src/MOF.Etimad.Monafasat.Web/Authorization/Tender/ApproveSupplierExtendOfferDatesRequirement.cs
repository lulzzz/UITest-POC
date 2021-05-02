using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ApproveSupplierExtendOfferDatesRequirement : IAuthorizationRequirement { }

   public class ApproveSupplierExtendOfferDatesHandler : AuthorizationHandler<ApproveSupplierExtendOfferDatesRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApproveSupplierExtendOfferDatesRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.PreQualificationCommitteeSecretary };
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
