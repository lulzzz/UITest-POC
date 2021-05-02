using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class BillStatusTest
    {
        [Fact]
        public void Should_Construct_BillStatus()
        {
            BillStatus billStatus = new BillStatus();
            _ = billStatus.BillStatusId;
            _ = billStatus.BillStatusNameAr;
            _ = billStatus.BillStatusNameEn;

            billStatus.ShouldNotBeNull();
        }
    }
}
