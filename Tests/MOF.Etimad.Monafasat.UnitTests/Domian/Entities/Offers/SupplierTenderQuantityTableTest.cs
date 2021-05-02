using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierTenderQuantityTableTest
    {
        private const string name = "Name";
        private const long tenderQTableId = 1;
        private const int offerId = 1;
        private const int tableId = 1;
        private const string tableName = "table name";
        private const long currentItemId = 1;
        private long itemId = 0;
        private const int itemNumber = 1;

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTable()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            _ = new SupplierTenderQuantityTable();

            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTable_second()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(offerId, name);
            _ = new SupplierTenderQuantityTable();
            _ = supplierTenderQuantityTable.Id;
            _ = supplierTenderQuantityTable.Offer;
            _ = supplierTenderQuantityTable.TenderQuantityTable;
            _ = supplierTenderQuantityTable.AdjustedFinalPrice;

            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItems()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.UpadteSupplierQTableItems(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, Value = " new val" } });
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_AddSupplierQTableItems()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.AddSupplierQTableItems(new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { ColumnId = 1, Value = "new val" } });
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItemsDefault()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.UpadteSupplierQTableItemsDefault(new Dictionary<long, bool>() { { 1, true } });
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItemsDefault_Second()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.UpadteSupplierQTableItemsDefault(new Dictionary<long, bool>() { { 2, true } });
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateOfferSupplierQItemsIsVerifiedMandatoryList()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, ItemNumber = 1 } });
            supplierTenderQuantityTable.UpdateOfferSupplierQItemsIsVerifiedMandatoryList(1, true);
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItemsAlternativeValue()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.UpadteSupplierQTableItemsAlternativeValue(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_RemoveAlternative()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTable.RemoveAlternative();
            supplierTenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_SaveQuantityTableItems()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            //supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            var result = supplierTenderQuantityTable.SaveQuantityTableItems(tableId, new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { ColumnId = 1, Value = "new val" } }, tableName, currentItemId, out itemId);
            supplierTenderQuantityTable.ShouldNotBeNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_SaveQuantityTableItems_Second()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, ItemNumber = 1, TenderFormHeaderId = 1 } });
            var result = supplierTenderQuantityTable.SaveQuantityTableItems(tableId, new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { ColumnId = 1, Value = "new val", ItemNumber = 1, TenderFormHeaderId = 1, Id = 1 } }, tableName, currentItemId, out itemId);
            supplierTenderQuantityTable.ShouldNotBeNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_SaveQuantityTableItems_Third()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, ItemNumber = 1, TenderFormHeaderId = 1 } });
            var result = supplierTenderQuantityTable.SaveQuantityTableItems(0, new List<ViewModel.TenderQuantityItemDTO>() { new ViewModel.TenderQuantityItemDTO() { ColumnId = 1, Value = "new val", ItemNumber = 1, TenderFormHeaderId = 1, Id = 1 } }, tableName, currentItemId, out itemId);
            supplierTenderQuantityTable.ShouldNotBeNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_DeleteQuantityTableItems()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            var result = supplierTenderQuantityTable.DeleteQuantityTableItems(itemNumber);
            supplierTenderQuantityTable.ShouldNotBeNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_DeActive()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.DeActive();
            Assert.False(supplierTenderQuantityTable.IsActive);
        }

        [Fact]
        public void Should_DeleteTableAndItems()
        {
            SupplierTenderQuantityTable supplierTenderQuantityTable = new SupplierTenderQuantityTable(name, tenderQTableId, null);
            supplierTenderQuantityTable.DeleteTableAndItems();
            Assert.False(supplierTenderQuantityTable.IsActive);
        }



    }
}
