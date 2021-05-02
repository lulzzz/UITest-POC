using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class FavouriteSupplierTenderTest
    {
        private readonly string _supplierCRNumber = "CR1001";
        private readonly int _tenderId = 100;

        [Fact]
        public void Should_Empty_Construct_FavouriteSupplierTender()
        {
            var favouriteSupplierTender = new FavouriteSupplierTender();
            favouriteSupplierTender.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierCRNumber()
        {
            var favouriteSupplierTender = new FavouriteSupplierTender(_supplierCRNumber);
            favouriteSupplierTender.ShouldNotBeNull();
            favouriteSupplierTender.SupplierCRNumber.ShouldBe(_supplierCRNumber);
            favouriteSupplierTender.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Construct_TenderIdAndSupplierCRNumber()
        {
            var favouriteSupplierTender = new FavouriteSupplierTender(_tenderId, _supplierCRNumber);
            favouriteSupplierTender.ShouldNotBeNull();
            favouriteSupplierTender.TenderId.ShouldBe(_tenderId);
            favouriteSupplierTender.SupplierCRNumber.ShouldBe(_supplierCRNumber);
            favouriteSupplierTender.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_DeActive()
        {
            var favouriteSupplierTender = new FavouriteSupplierTender();
            favouriteSupplierTender.DeActive();
            favouriteSupplierTender.IsActive.ShouldBe(false);
            favouriteSupplierTender.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var favouriteSupplierTender = new FavouriteSupplierTender();
            favouriteSupplierTender.SetActive();
            favouriteSupplierTender.IsActive.ShouldBe(true);
            favouriteSupplierTender.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
