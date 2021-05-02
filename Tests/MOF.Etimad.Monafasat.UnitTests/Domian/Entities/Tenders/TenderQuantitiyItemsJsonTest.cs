using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderQuantitiyItemsJsonTest
    {
        private const int tableId = 1;
        private const int itemNumber = 1;

        [Fact]
        public void Should_Construct_TenderQuantitiyItemsJson()
        {
            TenderQuantitiyItemsJson tenderQuantitiyItemsJson = new TenderQuantitiyItemsJson(tableId);
            _ = new TenderQuantitiyItemsJson();
            _ = tenderQuantitiyItemsJson.TenderQuantityTable;
            tenderQuantitiyItemsJson.ShouldNotBeNull();
        }
        [Fact]
        public void Should_Delete()
        {
            TenderQuantitiyItemsJson tenderQuantitiyItemsJson = new TenderQuantitiyItemsJson(tableId);
            tenderQuantitiyItemsJson.Delete();
            Assert.Equal(ObjectState.Deleted, tenderQuantitiyItemsJson.State);
        }

        [Fact]
        public void Should_Delete_()
        {
            TenderQuantitiyItemsJson tenderQuantitiyItemsJson = new TenderQuantitiyItemsJson(tableId);
            tenderQuantitiyItemsJson.Delete(itemNumber);
            Assert.Empty(tenderQuantitiyItemsJson.TenderQuantityTableItems);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderQuantitiyItemsJson tenderQuantitiyItemsJson = new TenderQuantitiyItemsJson(tableId);
            tenderQuantitiyItemsJson.SetAddedState();
            Assert.Equal(0, tenderQuantitiyItemsJson.Id);
        }

        [Fact]
        public void Should_SetUpdateState()
        {
            TenderQuantitiyItemsJson tenderQuantitiyItemsJson = new TenderQuantitiyItemsJson(tableId);
            tenderQuantitiyItemsJson.SetUpdateState();
            tenderQuantitiyItemsJson.ShouldNotBeNull();
        }

    }
}
