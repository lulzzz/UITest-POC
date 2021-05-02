using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class OffersReportRequirement : IAuthorizationRequirement { }

    public class OffersReportHandler : AuthorizationHandler<OffersReportRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public OffersReportHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersReportRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser };
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
