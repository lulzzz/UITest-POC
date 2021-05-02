using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class QualificationIndexRequirement : IAuthorizationRequirement { }

   public class QualificationIndexHandler : AuthorizationHandler<QualificationIndexRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QualificationIndexRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary,
            RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.CustomerService, RoleNames.supplier, RoleNames.TechnicalCommitteeUser, RoleNames.FinancialSupervisor,
            RoleNames.MonafasatBlackListCommittee, RoleNames.PreQualificationCommitteeSecretary, RoleNames.PreQualificationCommitteeManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager };
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
