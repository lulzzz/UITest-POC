using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DataEntryAndAuditerAndSupplierRequirement : IAuthorizationRequirement { }

    public class DataEntryAndAuditerAndSupplierHandler : AuthorizationHandler<DataEntryAndAuditerAndSupplierRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public DataEntryAndAuditerAndSupplierHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataEntryAndAuditerAndSupplierRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            //var userRoles = context.User.UserRoles();
            //var role = context.User.Claims.Any(x => (x.Type == ClaimTypes.Role || x.Type == "role") &&( x.Value == RoleNames.OffersCheckSecretary || x.Value == RoleNames.OffersCheckManager) && newRolesList.Contains(x.Value));
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.EtimadOfficer, RoleNames.PurshaseSpecialist };
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
