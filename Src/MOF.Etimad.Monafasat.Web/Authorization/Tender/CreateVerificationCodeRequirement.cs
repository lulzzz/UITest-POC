using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CreateVerificationCodeRequirement : IAuthorizationRequirement { }

   public class CreateVerificationCodeHandler : AuthorizationHandler<CreateVerificationCodeRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateVerificationCodeRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> {RoleNames.ManagerGrievanceCommittee, RoleNames.Auditer, RoleNames.OffersOppeningManager,
            RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager, RoleNames.ApproveTenderAward, RoleNames.UnitManagerUser,
            RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary , RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckManager,RoleNames.PreQualificationCommitteeManager,
            RoleNames.UnitSecretaryUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2};
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
