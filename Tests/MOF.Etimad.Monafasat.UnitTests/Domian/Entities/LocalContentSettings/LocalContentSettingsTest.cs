using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using System.Linq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.LocalContentSettings
{
    public class LocalContentSettingsTest
    {


        [Fact]
        public void ShouldUpdateLocalContentSettings()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();

            localContentSetting.Update(10, 500000000, 10, 60, 40);

            Assert.Equal(500000000, localContentSetting.HighValueContractsAmmount);
            Assert.Equal(10, localContentSetting.NationalProductPercentage);
            Assert.Equal(10, localContentSetting.LocalContentMaximumPercentage);
            Assert.Equal(60, localContentSetting.PriceWeightAfterAdjustment);
            Assert.Equal(40, localContentSetting.LocalContentWeightAndFinancialMarket);

        }
        [Fact]
        public void ShouldThrowExceptionWhenNationalProductValueIsZero()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();


            var exception = Assert.Throws<BusinessRuleException>(() => localContentSetting.Update(0, 50000000, 10, 60, 40));

            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageMoreThanZero, exception.Message);
        }  
        
        [Fact]
        public void ShouldThrowExceptionWhenNationalProductValueLessThanZero()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();


            var exception = Assert.Throws<BusinessRuleException>(() => localContentSetting.Update(-1, 50000000, 10, 60, 40));

            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageMoreThanZero, exception.Message);
        }  
        [Fact]
        public void ShouldThrowExceptionWhenAnyValueIsLessThanZeroExceptNationalProduct()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();


            var exception = Assert.Throws<BusinessRuleException>(() => localContentSetting.Update(10, -1, -1, -1, -1));

            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.ValueCannotBoLessThanZero, exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAnyValueMoreThanOneHunderd()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();


            var exception = Assert.Throws<BusinessRuleException>(() => localContentSetting.Update(101, 50000000, 101, 101, 101));

            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.ValueCannotBeMoreThan100, exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionIfPriceWeightvaluePlusFinantionalMarketNotExual100()
        {
            LocalContentSetting localContentSetting = new LocalContentSetting();
            var exception = Assert.Throws<BusinessRuleException>(() => localContentSetting.Update(10, 50000000, 10, 60, 60));
            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.TotalValuesMustEqual100, exception.Message);
        }
    }
}
