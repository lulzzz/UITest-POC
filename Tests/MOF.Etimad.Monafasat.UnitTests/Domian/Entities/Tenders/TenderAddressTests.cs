using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAddressTests
    {
        private readonly string _offersDeliveryAddress = "Address 1, Street 1";
        
        
        [Fact]
        public void ConstructTenderAddressTests()
        {
            TenderAddress address = new TenderAddress(false, _offersDeliveryAddress, "buliding", "floar", "department");

            Assert.Equal(_offersDeliveryAddress, address.OffersDeliveryAddress);
            Assert.Equal(ObjectState.Added, address.State);

        }
        
        [Fact]
        public void UpdateTenderAddressTests()
        {
            TenderAddress address = new TenderAddress();
            address.UpdateTenderAddress(false, _offersDeliveryAddress, "buliding", "floar", "department");

            Assert.Equal(_offersDeliveryAddress, address.OffersDeliveryAddress);
            Assert.Equal(ObjectState.Modified, address.State);

        }
    }
}
