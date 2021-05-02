using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderQuantitiyItemsChangeJsonTest
    {
        private const int tableId = 1;
        private const int itemNumber = 1;


        [Fact]
        public void Should_Construct_TenderDatesChange()
        {
            TenderQuantitiyItemsChangeJson tenderQuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(tableId);
            _ = new TenderQuantitiyItemsChangeJson();
            _ = tenderQuantitiyItemsChangeJson.TenderQuantityTableChanges;

            tenderQuantitiyItemsChangeJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            TenderQuantitiyItemsChangeJson tenderQuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(tableId);
            tenderQuantitiyItemsChangeJson.Delete();
            Assert.Equal(ObjectState.Deleted, tenderQuantitiyItemsChangeJson.State);
        }

        [Fact]
        public void Should_Delete_()
        {
            TenderQuantitiyItemsChangeJson tenderQuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(tableId);
            tenderQuantitiyItemsChangeJson.Delete(itemNumber);
            Assert.Empty(tenderQuantitiyItemsChangeJson.TenderQuantityTableItemChanges);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderQuantitiyItemsChangeJson tenderQuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(tableId);
            tenderQuantitiyItemsChangeJson.SetAddedState();
            Assert.Equal(0, tenderQuantitiyItemsChangeJson.Id);
        }

        [Fact]
        public void Should_SetUpdateState()
        {
            TenderQuantitiyItemsChangeJson tenderQuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(tableId);
            tenderQuantitiyItemsChangeJson.SetUpdateState();
            tenderQuantitiyItemsChangeJson.ShouldNotBeNull();
        }
    }
}
