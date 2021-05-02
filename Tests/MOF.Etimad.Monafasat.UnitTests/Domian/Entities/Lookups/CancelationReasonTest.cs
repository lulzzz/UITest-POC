using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class CancelationReasonTest
    {
        private const int canelationReason = 1;

        [Fact]
        public void Should_Construct_CancelationReason()
        {
            CancelationReason cancelationReason = new CancelationReason(canelationReason);
            _ = cancelationReason.NameAr;
            _ = cancelationReason.NameEn;

            cancelationReason.ShouldNotBeNull();
        }
    }
}
