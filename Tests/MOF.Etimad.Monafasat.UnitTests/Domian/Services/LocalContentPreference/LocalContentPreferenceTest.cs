using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.LocalContentPreference
{
    public class LocalContentPreferenceTest
    {
        private readonly LocalContentPreferenceService _sut;

        public LocalContentPreferenceTest()
        {
            _sut = new LocalContentPreferenceService();
        }

        [Fact]
        public void ShouldCalculateOfferLocalContentPreference_SuccessOffer()
        {
            LocalContentPreferenceModel localContentPreferenceModel = new LocalContentPreferenceModel()
            {
                currentOfferPrice = 1000,
                lowestOfferPrice = 900,
                supplierLocalContentPercentage = 25,
                supplierBaselinePercentage = 10,
                priceWeight = 60,
                localContentTargetWeight = 50,
                baselineWeight = 50,
                includedInMarketWeight = 5,
                localContentWeight = 40,
                isIncludedInMarket = true,
            };
            var result = _sut.CalculateOfferLocalContentPreference(localContentPreferenceModel);
            Assert.Equal("63.00", result.ToString());
        }

        [Fact]
        public void ShouldCalculateOfferLocalContentPreference_SuccessLowestOffer()
        {
            LocalContentPreferenceModel localContentPreferenceModel = new LocalContentPreferenceModel()
            {
                currentOfferPrice = 900,
                lowestOfferPrice = 900,
                supplierLocalContentPercentage = 25,
                supplierBaselinePercentage = 10,
                priceWeight = 60,
                localContentTargetWeight = 50,
                baselineWeight = 50,
                includedInMarketWeight = 5,
                localContentWeight = 40,
                isIncludedInMarket = true,
            };
            var result = _sut.CalculateOfferLocalContentPreference(localContentPreferenceModel);
            Assert.Equal("69.00", result.ToString());
        }
        [Fact]
        public void ShouldCalculateOfferLocalContentPreference_ThrowError()
        {
            LocalContentPreferenceModel localContentPreferenceModel = new LocalContentPreferenceModel()
            {
                currentOfferPrice = 0,
                lowestOfferPrice = 900,
                supplierLocalContentPercentage = 25,
                supplierBaselinePercentage = 10,
                priceWeight = 60,
                localContentTargetWeight = 50,
                baselineWeight = 50,
                includedInMarketWeight = 5,
                localContentWeight = 40,
                isIncludedInMarket = true,
            };
            var result = Assert.Throws<BusinessRuleException>(() => _sut.CalculateOfferLocalContentPreference(localContentPreferenceModel));
            Assert.Equal("يجب ان تكون قيمة العرض المراد حسابه اكبر من 0", result.Message);
        }
    }
}
