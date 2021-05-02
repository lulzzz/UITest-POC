using MOF.Etimad.Monafasat.Core.Entities;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class AddressTests
    {
        private readonly string _address = "Address 1, Street 1";
        private readonly int _addressTypeId = 1;
        private readonly int _branchId = 5;
        [Fact]
        public void ConstructAddress()
        {
            Address address = new Address(_address, _addressTypeId, _branchId);

            Assert.Equal(_address, address.AddressName);
            Assert.Equal(_addressTypeId, address.AddressTypeId);
        }
    }
}
