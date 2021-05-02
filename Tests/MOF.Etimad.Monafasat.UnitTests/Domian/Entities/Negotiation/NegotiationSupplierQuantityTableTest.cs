using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationSupplierQuantityTableTest
    {

        [Theory]
        [InlineData(1, "Table 1", 1)]
        public void ShouldCreateNewNegotiationQuantityTable(int negotiationSecondStageId, string qtName, long _SupplierQuantityTableId)
        {
            List<NegotiationSupplierQuantityTableItem> negotia = new List<NegotiationSupplierQuantityTableItem>();

            NegotiationSupplierQuantityTable supplierQuantityTable = new NegotiationSupplierQuantityTable(negotiationSecondStageId, qtName, _SupplierQuantityTableId, negotia);

            Assert.Equal(negotiationSecondStageId, supplierQuantityTable.refNegotiationSecondStage);
            Assert.Equal(ObjectState.Added, supplierQuantityTable.State);
        }

        [Fact]
        public void ShouldDeleteQuantityTable()
        {
            NegotiationSupplierQuantityTable supplierQuantityTable = new NegotiationSupplierQuantityTable();

            supplierQuantityTable.DeleteTable();

            Assert.Equal(ObjectState.Deleted, supplierQuantityTable.State);
        }
        [Fact]
        public void ShouldDeleteQuantityTableItems()
        {
            NegotiationSupplierQuantityTable supplierQuantityTable = new NegotiationDefaults().GetNegotiationSecondStageQunaitityTables();

            supplierQuantityTable.DeleteItems();

            Assert.Equal(ObjectState.Deleted, supplierQuantityTable.NegotiationQuantityItemJson.State);
        }

        [Fact]
        public void ShouldDeleteQuantityTableItemsWithItemNumber()
        {
            NegotiationSupplierQuantityTable supplierQuantityTable = new NegotiationDefaults().GetNegotiationSecondStageQunaitityTables();
            List<NegotiationSupplierQuantityTableItem> tableItems = new List<NegotiationSupplierQuantityTableItem>(supplierQuantityTable.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems);
            supplierQuantityTable.DeleteqtItems(tableItems);

            Assert.Empty(supplierQuantityTable.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems);
        }

        [Fact]
        public void ShouldUpadteNegotiationQTableItems()
        {
            NegotiationSupplierQuantityTable supplierQuantityTable = new NegotiationDefaults().GetNegotiationSecondStageQunaitityTables();
            List<TenderQuantityItemDTO> quantityItemDTOList = new List<TenderQuantityItemDTO>() {
            new NegotiationDefaults().GetTenderQuantityItemDTO(),
            };
            supplierQuantityTable.UpadteNegotiationQTableItems(quantityItemDTOList);

            Assert.NotEmpty(supplierQuantityTable.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems);
        }
    }
}
