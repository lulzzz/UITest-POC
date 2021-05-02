using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;

namespace MOF.Etimad.Monafasat.Services
{
    public class LocalContentPreferenceService : ILocalContentPreferenceService
    {
        public LocalContentPreferenceService()
        {

        }
        public decimal CalculateOfferLocalContentPreference(LocalContentPreferenceModel data)
        {
            if (data.currentOfferPrice != 0)
            {
                var result = (((data.lowestOfferPrice / data.currentOfferPrice) * data.priceWeight) + ((((data.supplierLocalContentPercentage / 100) * (data.localContentTargetWeight / 100)) + ((data.supplierBaselinePercentage / 100) * (data.baselineWeight / 100)) + (data.isIncludedInMarket ? (data.includedInMarketWeight / 100) : 0)) * data.localContentWeight));
                return Math.Round(result, 2);
            }
            throw new BusinessRuleException("يجب ان تكون قيمة العرض المراد حسابه اكبر من 0");
        }
    }
}
