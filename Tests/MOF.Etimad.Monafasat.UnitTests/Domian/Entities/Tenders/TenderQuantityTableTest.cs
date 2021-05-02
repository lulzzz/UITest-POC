using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using System.Collections.Generic;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderQuantityTableTest
    {
        private const long id = 1;
        private const int formId = 1;
        private const int tender = 1;
        private const string name = "name";
        private const int tableId = 1;
        private long itemId = 0;
        private const int itemNumber = 1;

        [Fact]
        public void Should_Construct_TenderQuantityTable()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            _ = new TenderQuantityTable(id, name, formId);
            _ = new TenderQuantityTable(tender, name, formId);
            _ = new TenderQuantityTable();
            _ = tenderQuantityTable.TenderId;
            _ = tenderQuantityTable.Tender;

            tenderQuantityTable.ShouldNotBeNull();
        }

        [Fact]
        public void Should_SaveQuantityTableBulkItems()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.SaveQuantityTableBulkItems(tableId, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, name);
            tenderQuantityTable.SaveQuantityTableBulkItems(0, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, name);
            Assert.NotEmpty(tenderQuantityTable.QuantitiyItemsJson.TenderQuantityTableItems);
        }

        [Fact]
        public void Should_DeleteQuantityTableItems()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.DeleteQuantityTableItems(itemNumber);
            Assert.Empty(tenderQuantityTable.QuantitiyItemsJson.TenderQuantityTableItems);
        }

        [Fact]
        public void Should_DeleteTenderQuantityTableWithItems()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.DeleteTenderQuantityTableWithItems();
            Assert.False(tenderQuantityTable.IsActive);
        }

        [Fact]
        public void Should_DeleteTenderQuantityTablesItems()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.DeleteTenderQuantityTablesItems();
            Assert.Equal(ObjectState.Deleted, tenderQuantityTable.QuantitiyItemsJson.State);
        }

        [Fact]
        public void Should_UpdateName()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            string newName = "newName";
            tenderQuantityTable.UpdateName(newName);
            Assert.Equal(newName, tenderQuantityTable.Name);
        }

        [Fact]
        public void Should_DeActive()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.DeActive();
            Assert.False(tenderQuantityTable.IsActive);
        }

        [Fact]
        public void Should_Delete()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.Delete();
            Assert.Equal(ObjectState.Deleted, tenderQuantityTable.State);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderQuantityTable tenderQuantityTable = new TenderQuantityTable(id, new List<TenderQuantityItemDTO>() { new TenderQuantityItemDTO() }, formId);
            tenderQuantityTable.SetAddedState();
            Assert.Equal(0, tenderQuantityTable.Id);
        }

    }
}
