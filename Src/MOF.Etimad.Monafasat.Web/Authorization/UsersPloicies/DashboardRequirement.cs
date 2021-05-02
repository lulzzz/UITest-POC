using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DashboardRequirement : IAuthorizationRequirement { }

   public class DashboardHandler : AuthorizationHandler<DashboardRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.supplier, RoleNames.MonafasatBlackListCommittee, RoleNames.MonafasatBlockListSecritary, RoleNames.PurshaseSpecialist, RoleNames.PrePlanningAuditor,
            RoleNames.PrePlanningCreator, RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.MonafasatBlockListSecritary, RoleNames.DataEntry,
            RoleNames.Auditer, RoleNames.supplier, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary,
            RoleNames.OffersCheckManager, RoleNames.CustomerService, RoleNames.supplier, RoleNames.TechnicalCommitteeUser, RoleNames.MonafasatBlackListCommittee, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1,
            RoleNames.UnitSpecialistLevel2, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.ApproveTenderAward, RoleNames.SecretaryGrievanceCommittee, RoleNames.ManagerGrievanceCommittee,
            RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary, RoleNames.UnitBusinessManagement, RoleNames.PrePlanningAuditor, RoleNames.PrePlanningCreator, RoleNames.UnAssingedUser,
            RoleNames.AccountsManagementSpecialist, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser, RoleNames.ProductManager, RoleNames.ProductManagerDisplay,
            RoleNames.MandatoryListApprover, RoleNames.MandatoryListOfficer, RoleNames.LocalContentOfficer, RoleNames.FinancialSupervisor };

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
