using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class IndexRequirement : IAuthorizationRequirement { }

    public class IndexHandler : AuthorizationHandler<IndexRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public IndexHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IndexRequirement requirement)
        {
            var newRoles = httpContextAccessor.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.MonafasatAccountManager,
                RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.CustomerService, RoleNames.supplier,
                RoleNames.TechnicalCommitteeUser, RoleNames.MonafasatBlackListCommittee, RoleNames.ApproveTenderAward, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOpeningAndCheckManager,
                RoleNames.OffersOpeningAndCheckSecretary, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.FinancialSupervisor };
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
