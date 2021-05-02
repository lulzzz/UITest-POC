using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LocalContentSettingsController : BaseController
    {
        private readonly ILocalContentSettingsAppService  _localContentSettingsAppService;

        public LocalContentSettingsController(IOptionsSnapshot<RootConfigurations> rootConfiguration, ILocalContentSettingsAppService localContentSettingsAppService) : base(rootConfiguration)
        {
            _localContentSettingsAppService = localContentSettingsAppService;
        }

        //[Authorize(RoleNames.LocalContentOfficer)]
        [HttpGet]
        [Route("Find")]
        public async Task<LocalContentSettingsViewModel> Find()
        {
            return await _localContentSettingsAppService.Get();
        }

        //[Authorize(RoleNames.LocalContentOfficer)]
        [HttpPost]
        [Route("Update")]
        public async Task Update([FromBody] LocalContentSettingsViewModel viewModel)
        {
            await _localContentSettingsAppService.UpdateLocalContentSettings(viewModel);
        }
    }
}
