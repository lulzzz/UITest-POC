using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Branches
{
    public class AddressTypeTests
    {
        [Fact]
        public void Should_Construct_AddressType()
        {
            var addressType = new AddressType { AddressTypeName = "name" };

            addressType.ShouldNotBeNull();
            addressType.AddressTypeId.ShouldBe(0);
            addressType.AddressTypeName.ShouldBe("name");
        }
    }
}