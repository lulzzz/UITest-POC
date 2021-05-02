using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DataEntryAuditorHandler : AuthorizationHandler<DataEntryAuditorRequirement>
    {
        private readonly ITenderAppService _tenderService;

        public DataEntryAuditorHandler(IServiceProvider serviceProvider)
        {
            _tenderService = serviceProvider.GetService<ITenderAppService>();
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataEntryAuditorRequirement requirement)
        {
            var role = context.User.Claims.FirstOrDefault(x => x.Type == "role");
            if (role == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (!(role.Value == RoleNames.Auditer || role.Value == RoleNames.DataEntry || role.Value == RoleNames.PurshaseSpecialist || role.Value == RoleNames.EtimadOfficer))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var selectedAgency = context.User.Claims.FirstOrDefault(x => x.Type == "selectedAgency").Value;
            var xx = _tenderService.FindTenderByAgencyCode(selectedAgency);
            if (xx == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
