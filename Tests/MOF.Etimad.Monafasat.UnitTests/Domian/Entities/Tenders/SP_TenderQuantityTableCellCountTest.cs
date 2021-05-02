using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SP_TenderQuantityTableCellCountTest
    {
        private readonly long _itemCount = 20;


        [Fact]
        public void Should_SP_TenderQuantityTableCellCount_SetValues()
        {
            var sP_QuantityTableCellCount = new SP_TenderQuantityTableCellCount() { ItemCount = _itemCount };
            sP_QuantityTableCellCount.ShouldNotBeNull();
            sP_QuantityTableCellCount.ItemCount.ShouldBe(_itemCount);
        }
    }
}
