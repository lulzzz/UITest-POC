using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AnnouncementJoinRequestStatusTest
    {
        [Fact]
        public void Should_Construct_AnnouncementJoinRequestStatus()
        {
            AnnouncementJoinRequestStatus announcementJoinRequestStatus = new AnnouncementJoinRequestStatus();
            _ = announcementJoinRequestStatus.Id;
            _ = announcementJoinRequestStatus.NameAr;
            _ = announcementJoinRequestStatus.NameEn;

            announcementJoinRequestStatus.ShouldNotBeNull();
        }
    }
}
