using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    internal class MonafasatUserAddTenderHandler : AuthorizationHandler<MonafasatUserAddTenderRequirement>
    {

        public MonafasatUserAddTenderHandler(IServiceProvider serviceProvider)
        {
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MonafasatUserAddTenderRequirement requirement)
        {
            if ((context.User.Claims.FirstOrDefault(x => x.Type == "role") != null) &&
                !(context.User.Claims.FirstOrDefault(x => x.Type == "role").Value == RoleNames.supplier ||
                context.User.Claims.FirstOrDefault(x => x.Type == "role").Value == RoleNames.DataEntry))
            {
                context.Fail();
            }
            if (!(context.Resource is AuthorizationFilterContext filterContext))
            {
                context.Fail();
            }
            var owner = context.User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (owner == null)
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
