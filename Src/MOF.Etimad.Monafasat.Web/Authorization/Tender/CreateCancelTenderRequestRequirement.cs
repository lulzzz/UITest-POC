using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CreateCancelTenderRequestRequirement : IAuthorizationRequirement { }

   public class CreateCancelTenderRequestHandler : AuthorizationHandler<CreateCancelTenderRequestRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateCancelTenderRequestRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.PurshaseSpecialist, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.PreQualificationCommitteeSecretary, RoleNames.OffersPurchaseManager };
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
