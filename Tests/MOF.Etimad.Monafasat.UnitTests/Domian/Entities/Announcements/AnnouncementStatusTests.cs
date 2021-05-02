using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementStatusTests
    {
        [Fact]
        public void Should_Construct_AnnouncementStatus()
        {
            var announcementStatus = new AnnouncementStatus { Name = "name" };

            announcementStatus.ShouldNotBeNull();
            announcementStatus.Id.ShouldBe(0);
            announcementStatus.Name.ShouldBe("name");
        }
    }
}