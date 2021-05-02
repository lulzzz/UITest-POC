using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILocalContentSettingsAppService
    {
        Task<LocalContentSettingsViewModel> Get();
        Task UpdateLocalContentSettings(LocalContentSettingsViewModel viewModel);
    }
}
