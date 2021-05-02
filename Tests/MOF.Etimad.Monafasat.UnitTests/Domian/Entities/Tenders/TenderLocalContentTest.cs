using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using Shouldly;
using System.Collections.Generic;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderLocalContentTest
    {
        private readonly int _tenderId = 1;
        private readonly List<int> localContentMechanismIds = new List<int>() { 1, 3 };
        private readonly bool isApplyProvisionsMandatoryList = true;
        private readonly int? minimumBaseline = 10;
        private readonly int? minimumPercentageLocalContentTarget = 10;

        [Fact]
        public void ConstructLocalContent()
        {
            TenderLocalContent localContent = new TenderLocalContent();
            localContent.UpdateTenderLocalContent(_tenderId, localContentMechanismIds, isApplyProvisionsMandatoryList, minimumBaseline, minimumPercentageLocalContentTarget);
            localContent.State.ShouldBe(SharedKernal.ObjectState.Added);
            localContent.TenderId.ShouldBe(_tenderId);
            localContent.IsApplyProvisionsMandatoryList.ShouldBe(isApplyProvisionsMandatoryList);
            localContent.MinimumBaseline.ShouldBe(minimumBaseline.Value);
            localContent.MinimumPercentageLocalContentTarget.ShouldBe(minimumPercentageLocalContentTarget.Value);
        }

        [Fact]
        public void ShouldUpdateLocalContentSettingsValueForUnit()
        {
            TenderLocalContent localContent = new TenderLocalContent();

            localContent.UpdateTenderLocalContentSettingsForUnit(_tenderId, minimumBaseline, minimumPercentageLocalContentTarget, 10, 60, 40, 50, 50, 5);
            localContent.State.ShouldBe(SharedKernal.ObjectState.Modified);
            localContent.TenderId.ShouldBe(_tenderId);
            localContent.MinimumBaseline.ShouldBe(minimumBaseline.Value);
            localContent.MinimumPercentageLocalContentTarget.ShouldBe(minimumPercentageLocalContentTarget.Value);
            localContent.LocalContentMaximumPercentage.ShouldBe(10);
            localContent.PriceWeightAfterAdjustment.ShouldBe(60);
            localContent.LocalContentWeightAndFinancialMarket.ShouldBe(40);
            localContent.BaselineWeight.ShouldBe(50);
            localContent.LocalContentTargetWeight.ShouldBe(50);
            localContent.FinancialMarketListedWeight.ShouldBe(5);

        }
        
        [Fact]
        public void ShouldThrowExceptionIfPriceWeightvaluePlusFinantionalMarketNotExual100()
        {
            TenderLocalContent localContent = new TenderLocalContent(); 
            var exception = Assert.Throws<BusinessRuleException>(() => localContent.UpdateTenderLocalContentSettingsForUnit(_tenderId, minimumBaseline, minimumPercentageLocalContentTarget, 10, 60, 60, 50, 50, 5));
            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.TotalValuesMustEqual100, exception.Message);
        }

        [Fact]
        public void ShouldSetLocalContentSettingsValue()
        {
            TenderLocalContent localContent = new TenderLocalContent();

            localContent.SetTenderLocalContentSettings(_tenderId, 50000000, 10, 60, 40);
            localContent.TenderId.ShouldBe(_tenderId);
            localContent.HighValueContractsAmmount.ShouldBe(50000000);
            localContent.LocalContentMaximumPercentage.ShouldBe(10);
            localContent.PriceWeightAfterAdjustment.ShouldBe(60);
            localContent.LocalContentWeightAndFinancialMarket.ShouldBe(40);
            localContent.BaselineWeight.ShouldBe(50);
            localContent.LocalContentTargetWeight.ShouldBe(50);
            localContent.FinancialMarketListedWeight.ShouldBe(5);

        }
        [Fact]
        public void ShouldSetNationalProductPercentage()
        {
            TenderLocalContent localContent = new TenderLocalContent();

            localContent.SetNationalProductPercentage(10);
            localContent.NationalProductPercentage.ShouldBe(10);
        }

        [Fact]
        public void ShouldSetNationalProductPercentageThrowExceptionWhenValueEqualZero()
        {
            TenderLocalContent localContent = new TenderLocalContent();
            var exception = Assert.Throws<BusinessRuleException>(() => localContent.SetNationalProductPercentage(0));
            Assert.Equal(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageMoreThanZero, exception.Message);
        }

        [Fact]
        public void Should_CreateLocalContent()
        {
            var tenderLocalContent = new TenderLocalContent();
            tenderLocalContent.CreateLocalContentMechanism(new List<int>() { 1, 3 });
            tenderLocalContent.ShouldNotBeNull();
            tenderLocalContent.LocalContentMechanism.ShouldNotBeNull();
        }

    }
}
