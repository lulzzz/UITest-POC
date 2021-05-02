using MOF.Etimad.Monafasat.Core.Entities.Settings;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILocalContentSettingsCommands
    {
        Task Update(LocalContentSetting localContentSetting);
        Task<LocalContentSetting> Get();
    }
}
