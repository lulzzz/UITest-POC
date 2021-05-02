using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SP_QuantityTableCellCountTest
    {
        private readonly long _tableId = 1005;
        private readonly long _itemCount = 100;
        private readonly long _itemNumber = 20;


        [Fact]
        public void Should_SP_QuantityTableCellCount_SetValues()
        {
            var sP_QuantityTableCellCount = new SP_QuantityTableCellCount() { ItemCount = _itemCount, ItemNumber = _itemNumber, TableId = _tableId };
            sP_QuantityTableCellCount.ShouldNotBeNull();
            sP_QuantityTableCellCount.ItemCount.ShouldBe(_itemCount);
            sP_QuantityTableCellCount.ItemNumber.ShouldBe(_itemNumber);
            sP_QuantityTableCellCount.TableId.ShouldBe(_tableId);
        }
    }
}
