using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class GetFinancialYearRequirement : IAuthorizationRequirement { }

    public class GetFinancialYearHandler : AuthorizationHandler<GetFinancialYearRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetFinancialYearHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetFinancialYearRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry , RoleNames.Auditer , RoleNames.MonafasatAccountManager , RoleNames.MonafasatAdmin , RoleNames.OffersOppeningSecretary , RoleNames.OffersOppeningManager , RoleNames.OffersCheckSecretary , RoleNames.OffersCheckManager , RoleNames.CustomerService , RoleNames.supplier , RoleNames.TechnicalCommitteeUser, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager,
             RoleNames.PreQualificationCommitteeManager ,RoleNames.PreQualificationCommitteeSecretary ,RoleNames.EtimadOfficer, RoleNames.PurshaseSpecialist ,RoleNames.VROOpenAndCheckingViewPolicy, RoleNames.UnitBusinessManagement };
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
