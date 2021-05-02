using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierTenderQuantityTableItemJsonTest
    {
        private const int itemNumber = 1;

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTableItemJson()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            _ = new SupplierTenderQuantityTableItemJson();

            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.Delete();
            Assert.Equal(ObjectState.Deleted, supplierTenderQuantityTableItemJson.State);
        }

        [Fact]
        public void Should_Create()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.Create();
            Assert.Equal(ObjectState.Added, supplierTenderQuantityTableItemJson.State);
        }

        [Fact]
        public void Should_Update()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.Update();
            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete_WithItemNumber()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, ItemNumber = 1 } });
            supplierTenderQuantityTableItemJson.Delete(itemNumber);
            Assert.Empty(supplierTenderQuantityTableItemJson.SupplierTenderQuantityTableItems);
        }

        [Fact]
        public void Should_DeleteRow()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1, ItemNumber = 1 } });
            supplierTenderQuantityTableItemJson.DeleteRow(itemNumber);
            Assert.Empty(supplierTenderQuantityTableItemJson.SupplierTenderQuantityTableItems);
        }

        [Fact]
        public void Should_UpadteSupplierQTableItems()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.UpadteSupplierQTableItems(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItems_Second()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.UpadteSupplierQTableItems(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 2 } });
            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_RemoveAlternative()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.RemoveAlternative();
            Assert.True(string.IsNullOrEmpty(supplierTenderQuantityTableItemJson.SupplierTenderQuantityTableItems[0].AlternativeValue));
        }


        [Fact]
        public void Should_UpadteSupplierQTableItemsAlternativeValue()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.UpadteSupplierQTableItemsAlternativeValue(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpadteSupplierQTableItemsAlternativeValue_Second()
        {
            SupplierTenderQuantityTableItemJson supplierTenderQuantityTableItemJson = new SupplierTenderQuantityTableItemJson(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 1 } });
            supplierTenderQuantityTableItemJson.UpadteSupplierQTableItemsAlternativeValue(new List<SupplierTenderQuantityTableItem>() { new SupplierTenderQuantityTableItem() { Id = 2 } });
            supplierTenderQuantityTableItemJson.ShouldNotBeNull();
        }
    }
}
