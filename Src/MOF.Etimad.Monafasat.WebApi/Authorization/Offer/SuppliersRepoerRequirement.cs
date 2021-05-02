using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class SuppliersReportRequirement : IAuthorizationRequirement { }

    public class SuppliersReportHandler : AuthorizationHandler<SuppliersReportRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public SuppliersReportHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuppliersReportRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.ApproveTenderAward, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOpeningAndCheckSecretary,
                RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager,
                RoleNames.OffersPurchaseSecretary, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser, RoleNames.FinancialSupervisor };
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
