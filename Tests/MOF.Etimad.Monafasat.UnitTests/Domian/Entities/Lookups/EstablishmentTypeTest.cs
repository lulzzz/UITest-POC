using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class EstablishmentTypeTest
    {
        [Fact]
        public void Should_Construct_EstablishmentType()
        {
            EstablishmentType establishmentType = new EstablishmentType();
            _ = establishmentType.EstablishmentTypeId;
            _ = establishmentType.Name;
            _ = establishmentType.Size;
            _ = establishmentType.CommercialRegistrationNo;

            establishmentType.ShouldNotBeNull();
        }
    }
}
