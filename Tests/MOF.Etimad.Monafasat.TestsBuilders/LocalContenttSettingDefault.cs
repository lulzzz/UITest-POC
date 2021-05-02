using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class LocalContenttSettingDefault
    {
        public LocalContentSettingsViewModel GetLocalContentSettingDefault()
        {
            return new LocalContentSettingsViewModel
            {
                HighValueContractsAmmount = 50000000,
                NationalProductPercentage = 10,
                LocalContentMaximumPercentage = 10,
                PriceWeightAfterAdjustment = 60,
                LocalContentWeightAndFinancialMarket = 40,
            };
        }
    }
}
