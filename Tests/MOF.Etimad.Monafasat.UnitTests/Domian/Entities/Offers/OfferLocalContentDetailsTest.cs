using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class OfferLocalContentDetailsTest
    {

        [Fact]
        public void  SetCorporationSizeSmallOrMedium_ReturnTrue()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();
            
            offerlocalContentDetails.SetCorporationSizeSmallOrMedium();

            Assert.True(offerlocalContentDetails.IsSmallOrMediumCorporation);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void SetCorporationSizeLarg_ReturnFalse()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetCorporationSizeLarg();

            Assert.False(offerlocalContentDetails.IsSmallOrMediumCorporation);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Theory]
        [InlineData(25)]
        public void SetLocalContentDesiredPercentage_UpdateDesiredValue(decimal desiredValue)
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetLocalContentDesiredPercentage(desiredValue);

            Assert.Equal(desiredValue,offerlocalContentDetails.LocalContentDesiredPercentage);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }
        [Theory]
        [InlineData(25)]
        public void SetLocalContentBaseLinePercentage_UpdateBaseLine(decimal baseLineValue)
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetLocalContentBaseLinePercentage(baseLineValue);

            Assert.Equal(baseLineValue, offerlocalContentDetails.LocalContentPercentage);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void SetIsSupplierBindedToBaseLine_UpdateValueWithTrue()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetIsSupplierBindedToBaseLine(true);
            Assert.True(offerlocalContentDetails.IsBindedToTheLowestBaseLine);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

       [Fact]
        public void SetIsSupplierBindedToBaseLine_UpdateValueWithFalse()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetIsSupplierBindedToBaseLine(false);

            Assert.False(offerlocalContentDetails.IsBindedToTheLowestBaseLine);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void SetIsSupplierBindedToTheLowestLocalContent_UpdateValueWithTrue()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(true);
            Assert.True(offerlocalContentDetails.IsBindedToTheLowestLocalContent);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void SetOfferFinancialWeight_UpdateOfferLocalContentWith20()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetOfferFinancialWeight(20);
            Assert.Equal("20", offerlocalContentDetails.OfferPriceAfterLocalContent.ToString());
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }
                
        [Fact]
        public void SetSetOfferPriceAfterSMEA_UpdateOfferLocalContentPriceSMEAWith2200()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetOfferPriceAfterSMEA(2200);
            Assert.Equal("2200", offerlocalContentDetails.OfferPriceAfterSmallAndMediumCorporations.ToString());
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void CreateEntity()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.CreateEntity();
            Assert.Equal(ObjectState.Added, offerlocalContentDetails.State);
        }

        [Fact]
        public void SetIsSupplierBindedToTheLowestLocalContent_UpdateValueWithFalse()
        {
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(false);

            Assert.False(offerlocalContentDetails.IsBindedToTheLowestLocalContent);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }


        [Fact]
        public void SetIsIncludedInMoneyMarket_ReturnTrue()
        {
          
            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.SetIsIncludedInMoneyMarket(true);

            Assert.True(offerlocalContentDetails.IsIncludedInMoneyMarket);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void UpdateBaseLineDate_UpdateDateWithDateNow()
        {

            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.UpdateBaseLineDate();

            Assert.Equal(DateTime.Now.Date,offerlocalContentDetails.BaseLineUpdatedDate.Date);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void UpdateLocalContentDesiredPercentageDate_UpdateDateWithDateNow()
        {

            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.UpdateLocalContentDesiredPercentageDate();

            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.LocalContentDesiredPercentageUpdatedDate.Date);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }
        [Fact]
        public void UpdateCorporationSizeDate_UpdateDateWithDateNow()
        {

            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.UpdateCorporationSizeDate();

            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.CorporationSizeUpdatedDate.Date);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }
        [Fact]
        public void UpdateIsIncludedInMoneyMarketDate_UpdateDateWithDateNow()
        {

            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.UpdateIsExistInMoneyMarketDate();

            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.IsIncludedInMoneyMarketUpdatedDate.Date);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

        [Fact]
        public void AddInitialDataAtOfferApply_UpdateDatesWithDateNowAndIsBindedToMandatoryListTrue()
        {

            OfferlocalContentDetails offerlocalContentDetails = new OfferlocalContentDetails();

            offerlocalContentDetails.AddInitialDataAtOfferApply();

            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.CorporationSizeUpdatedDate.Date);
            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.BaseLineUpdatedDate.Date);
            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.IsIncludedInMoneyMarketUpdatedDate.Date);
            Assert.Equal(DateTime.Now.Date, offerlocalContentDetails.LocalContentDesiredPercentageUpdatedDate.Date);
            Assert.True(offerlocalContentDetails.IsBindedToMandatoryList);
            Assert.Equal(ObjectState.Modified, offerlocalContentDetails.State);
        }

    }
}
