using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationSupplierQuantityJsonTest
    {
        [Theory]
        [InlineData(1)]
        public void ShouldCreateNewQuantityJson(int tableId)
        {
            NegotiationQuantityItemJson negotiationQuantityItemJson = new NegotiationQuantityItemJson(tableId);

            Assert.Equal(tableId, negotiationQuantityItemJson.NegotiationSupplierQuantityTableId);
        }

        [Fact]
        public void ShouldDeleteJsonTable()
        {
            NegotiationQuantityItemJson negotiationQuantityItemJson = new NegotiationQuantityItemJson();

            negotiationQuantityItemJson.Delete();

            Assert.Equal(ObjectState.Deleted, negotiationQuantityItemJson.State);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldDeleteQuantityItemsByItemNumber(long itemNumber)
        {
            NegotiationQuantityItemJson negotiationQuantityItemJson = new NegotiationDefaults().GetNegotiationQuantityJsonWithItems();

            negotiationQuantityItemJson.Delete(itemNumber);

            Assert.All(negotiationQuantityItemJson.NegotiationSupplierQuantityTableItems, c => Assert.NotEqual(itemNumber, c.ItemNumber));
            Assert.Equal(ObjectState.Modified, negotiationQuantityItemJson.State);
        }
    }
}
