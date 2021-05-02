using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LocalContentSettingsAppService : ILocalContentSettingsAppService
    {
        private readonly ILocalContentSettingsCommands _settingsCommands;
        private readonly ILocalContentSettingsQueries _settingsQueries;

        public LocalContentSettingsAppService(ILocalContentSettingsCommands settingsCommands, ILocalContentSettingsQueries settingsQueries)
        {
            _settingsCommands = settingsCommands;
            _settingsQueries = settingsQueries;
        }
        public async Task<LocalContentSettingsViewModel> Get()
        {
            return await _settingsQueries.GetLocalContentSettings();
        }

        public async Task UpdateLocalContentSettings(LocalContentSettingsViewModel viewModel)
        {
            var localContentSetting = await _settingsCommands.Get();
            localContentSetting.Update(viewModel.NationalProductPercentage, (decimal)viewModel.HighValueContractsAmmount, (decimal)viewModel.LocalContentMaximumPercentage, (decimal)viewModel.PriceWeightAfterAdjustment, (decimal)viewModel.LocalContentWeightAndFinancialMarket);
            await _settingsCommands.Update(localContentSetting);
        }
    }
}
