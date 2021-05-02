using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SP_DeleteTenderQuantityTableWithItemsTest
    {
        private readonly bool _isDeleted = true;

        [Fact]
        public void Should_SP_DeleteTenderQuantityTableWithItems_SetValues()
        {
            var sP_DeleteTenderQuantityTableWithItems = new SP_DeleteTenderQuantityTableWithItems() { IsDeleted = _isDeleted };

            sP_DeleteTenderQuantityTableWithItems.ShouldNotBeNull();
            sP_DeleteTenderQuantityTableWithItems.IsDeleted.ShouldBeTrue();
        }
    }
}
