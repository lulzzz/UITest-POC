using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
 using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILocalContentSettingsQueries
    {
        Task<LocalContentSettingsViewModel> GetLocalContentSettings();
    }
}
