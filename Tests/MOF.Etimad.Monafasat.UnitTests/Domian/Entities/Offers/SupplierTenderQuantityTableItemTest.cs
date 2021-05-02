using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierTenderQuantityTableItemTest
    {
        private const long id = 1;
        private const long columnId = 1;
        private const long tenderFormHeaderId = 1;
        private const string value = "val";
        private const long itemNumber = 1;
        private const int activityTemplateId = 1;
        private const int TenderItemId = 1;

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTableItem()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber);
            _ = new SupplierTenderQuantityTableItem();

            supplierTenderQuantityTableItem.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTableItem_Second()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateItems()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.UpdateItems(columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber);
            supplierTenderQuantityTableItem.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateValue()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.UpdateValue(value);
            Assert.Equal(value, supplierTenderQuantityTableItem.Value);
        }

        [Fact]
        public void Should_UpdateDefault()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.UpdateDefault(true);
            Assert.True(supplierTenderQuantityTableItem.IsDefault);
        }

        [Fact]
        public void Should_UpdateAlternativeValue()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.UpdateAlternativeValue(value);
            Assert.Equal(value, supplierTenderQuantityTableItem.AlternativeValue);
        }

        [Fact]
        public void Should_UpdateSupplierTenderQuantityTableItem()
        {
            SupplierTenderQuantityTableItem supplierTenderQuantityTableItem = new SupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber, TenderItemId);
            supplierTenderQuantityTableItem.UpdateSupplierTenderQuantityTableItem(id, columnId, tenderFormHeaderId, activityTemplateId, value, itemNumber);
            supplierTenderQuantityTableItem.ShouldNotBeNull();
        }

    }
}
