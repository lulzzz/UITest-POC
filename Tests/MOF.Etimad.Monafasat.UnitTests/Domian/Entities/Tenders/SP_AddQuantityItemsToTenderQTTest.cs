using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SP_AddQuantityItemsToTenderQTTest
    {
        private readonly long _itemsJsonId = 1000;

        [Fact]
        public void Should_SP_AddQuantityItemsToTenderQT_SetValues()
        {
            var sP_AddQuantityItemsToTenderQT = new SP_AddQuantityItemsToTenderQT() { ItemsJsonId = _itemsJsonId };
            sP_AddQuantityItemsToTenderQT.ShouldNotBeNull();
            sP_AddQuantityItemsToTenderQT.ItemsJsonId.ShouldBe(_itemsJsonId);
        }
    }
}
