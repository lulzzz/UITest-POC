using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DashboardRequirement : IAuthorizationRequirement { }

    public class DashboardHandler : AuthorizationHandler<DashboardRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public DashboardHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardRequirement requirement)
        {
            var newRoles = httpContextAccessor.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();

            List<string> accessedRoles = new List<string> {RoleNames.supplier, RoleNames.MonafasatBlackListCommittee, RoleNames.MonafasatBlockListSecritary,RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.DataEntry, RoleNames.OffersOppeningSecretary, RoleNames.MandatoryListApprover, RoleNames.MandatoryListOfficer,
            RoleNames.OffersCheckSecretary, RoleNames.Auditer, RoleNames.OffersOppeningManager,RoleNames.PrePlanningAuditor,RoleNames.PrePlanningCreator, RoleNames.OffersCheckManager, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin , RoleNames.TechnicalCommitteeUser, RoleNames.OffersPurchaseManager , RoleNames.OffersPurchaseSecretary, RoleNames.ApproveTenderAward , RoleNames.SecretaryGrievanceCommittee, RoleNames.ManagerGrievanceCommittee,RoleNames.PreQualificationCommitteeSecretary,RoleNames.PreQualificationCommitteeManager, RoleNames.UnitBusinessManagement,RoleNames.AccountsManagementSpecialist, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser, RoleNames.LocalContentOfficer };

            bool hasAccess = IdentityConfigs.CheckForUserNewRoles(newRolesList, accessedRoles);
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