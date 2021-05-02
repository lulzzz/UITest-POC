using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class QuantityTableRowTypeTest
    {
        private const int id = 1;
        private const string nameAr = "name";
        private const string nameEn = "name";

        [Fact]
        public void Should_Construct_QuantityTableRowType()
        {
            QuantityTableRowType quantityTableRowType = new QuantityTableRowType(id, nameEn, nameAr);
            _ = new QuantityTableRowType();
            quantityTableRowType.ShouldNotBeNull();
        }
    }
}
