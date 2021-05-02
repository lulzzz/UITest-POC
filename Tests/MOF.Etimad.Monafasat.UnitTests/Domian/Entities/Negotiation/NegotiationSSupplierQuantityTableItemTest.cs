using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationSSupplierQuantityTableItemTest
    {
        [Theory]
        [InlineData(1, 1, 1, 1, "Test", 1)]
        public void ShouldCreateNewNegotiationSupplierQItem(long id, long columnId, long? headerId, int? activityId, string value, long itemNumber)
        {
            NegotiationSupplierQuantityTableItem negotiationS = new NegotiationSupplierQuantityTableItem();

            NegotiationSupplierQuantityTableItem negotiationSupplier = new NegotiationSupplierQuantityTableItem(id, columnId, headerId, activityId, value, itemNumber);

            Assert.Equal(columnId, negotiationSupplier.ColumnId);
            Assert.Equal(value, negotiationSupplier.Value);
            Assert.Equal(headerId, negotiationSupplier.TenderFormHeaderId);
            Assert.Equal(activityId, negotiationSupplier.ActivityTemplateId);
            Assert.Equal(itemNumber, negotiationSupplier.ItemNumber);
        }
    }
}
