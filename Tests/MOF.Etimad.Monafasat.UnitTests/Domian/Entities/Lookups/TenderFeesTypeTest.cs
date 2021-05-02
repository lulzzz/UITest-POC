using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderFeesTypeTest
    {
        [Fact]
        public void Should_Construct_TenderFeesType()
        {
            TenderFeesType tenderFeesType = new TenderFeesType();
            _ = tenderFeesType.TenderFeesTypeId;
            _ = tenderFeesType.NameArabic;
            _ = tenderFeesType.NameEnglish;

            tenderFeesType.ShouldNotBeNull();
        }
    }
}
