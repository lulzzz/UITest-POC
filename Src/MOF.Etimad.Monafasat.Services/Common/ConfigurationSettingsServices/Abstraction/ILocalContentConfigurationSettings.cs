using MOF.Etimad.Monafasat.Core.Entities.Settings;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILocalContentConfigurationSettings
    {
        Task<ConfigurationSetting> getLocalContentSettingsDate();
    }
}
