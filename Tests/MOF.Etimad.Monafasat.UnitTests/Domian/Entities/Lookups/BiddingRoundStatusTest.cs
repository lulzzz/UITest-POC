using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class BiddingRoundStatusTest
    {
        [Fact]
        public void Should_Construct_BiddingRoundStatus()
        {
            BiddingRoundStatus biddingRoundStatus = new BiddingRoundStatus();
            _ = biddingRoundStatus.BiddingRoundStatusId;
            _ = biddingRoundStatus.Name;

            biddingRoundStatus.ShouldNotBeNull();
        }
    }
}
