using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LocalContentSettingsQueries : ILocalContentSettingsQueries
    {
        private readonly IAppDbContext _appDbContext;

        public LocalContentSettingsQueries(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<LocalContentSettingsViewModel> GetLocalContentSettings()
        {
            var localContentSettings = await _appDbContext.LocalContentSettings
                .Select(a => new LocalContentSettingsViewModel()
                {
                    NationalProductPercentage = a.NationalProductPercentage,
                    HighValueContractsAmmount = a.HighValueContractsAmmount,
                    LocalContentMaximumPercentage = a.LocalContentMaximumPercentage,
                    PriceWeightAfterAdjustment = a.PriceWeightAfterAdjustment,
                    LocalContentWeightAndFinancialMarket = a.LocalContentWeightAndFinancialMarket 
                }).FirstOrDefaultAsync();

            return localContentSettings;
        }
    }
}
