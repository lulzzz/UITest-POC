using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderTypeTest
    {
        private const string nameEn = "name";
        private const string nameAr = "name";
        private const decimal buyingCost = 20;
        private const decimal invitationCost = 20;
        private const bool isActive = true;

        [Fact]
        public void Should_Construct_TenderType()
        {
            TenderType tenderType = new TenderType(nameEn, nameAr, buyingCost, invitationCost, isActive);
            _ = new TenderType();
            _ = tenderType.TenderTypeId;

            tenderType.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateCosts()
        {
            TenderType tenderType = new TenderType(nameEn, nameAr, buyingCost, invitationCost, isActive);
            tenderType.UpdateCosts(buyingCost, invitationCost);
            Assert.Equal(buyingCost, tenderType.BuyingCost);
        }
    }
}
