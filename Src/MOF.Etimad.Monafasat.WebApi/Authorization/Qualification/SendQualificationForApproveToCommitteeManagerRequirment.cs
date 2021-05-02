using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class SendQualificationForApproveToCommitteeManagerRequirment : IAuthorizationRequirement
    { }
    public class SendQualificationForApproveToCommitteeManagerHandler : AuthorizationHandler<SendQualificationForApproveToCommitteeManagerRequirment>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public SendQualificationForApproveToCommitteeManagerHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SendQualificationForApproveToCommitteeManagerRequirment requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.PreQualificationCommitteeSecretary };
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
