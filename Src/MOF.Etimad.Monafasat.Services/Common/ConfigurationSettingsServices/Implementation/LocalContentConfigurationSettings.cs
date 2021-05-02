using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LocalContentConfigurationSettings : ILocalContentConfigurationSettings
    {
        private readonly IAppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly RootConfigurations _configuration;

        public LocalContentConfigurationSettings(IAppDbContext context, IMemoryCache cache, IOptionsSnapshot<RootConfigurations> optionsSnapShot)
        {
            _context = context;
            _cache = cache;
            _configuration = optionsSnapShot.Value;
        }
        public async Task<ConfigurationSetting> getLocalContentSettingsDate()
        {
            var localContentSettings = await _cache.GetOrCreateAsync("LocalContentConfigSettings", async entry =>
            {
                int seconds = int.Parse(_configuration.ChachingConfiguration.CachingMinutes);
                entry.SlidingExpiration = TimeSpan.FromSeconds(seconds);
                return await _context.ConfigurationSettings.FirstOrDefaultAsync(setting => setting.Id == (int)Enums.ConfigurationSetting.LocalContent);
            });
            return localContentSettings;
        }
    }
}
