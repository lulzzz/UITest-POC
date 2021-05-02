using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LocalContentSettingsCommands : ILocalContentSettingsCommands
    {
        private readonly IAppDbContext _appDbContext;

        public LocalContentSettingsCommands(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<LocalContentSetting> Get()
        {
            return await _appDbContext.LocalContentSettings.FirstOrDefaultAsync();
        }

        public async Task Update(LocalContentSetting localContentSetting)
        {
            _appDbContext.LocalContentSettings.Update(localContentSetting);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
