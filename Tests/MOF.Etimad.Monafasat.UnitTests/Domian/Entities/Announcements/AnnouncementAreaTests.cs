using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementAreaTests
    {
        [Fact]
        public void Should_Construct_AnnouncementArea()
        {
            var areaId = 1;

            var announcementArea = new AnnouncementArea(areaId);

            announcementArea.ShouldNotBeNull();
            announcementArea.AreaId.ShouldBe(areaId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementArea()
        {
            var areaId = 1;
            var announcementArea = new AnnouncementArea(areaId);

            announcementArea.DeActive();

            announcementArea.ShouldNotBeNull();
            announcementArea.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementArea()
        {
            var areaId = 1;
            var announcementArea = new AnnouncementArea(areaId);
            announcementArea.DeActive();

            announcementArea.SetActive();

            announcementArea.ShouldNotBeNull();
            announcementArea.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementArea()
        {
            var areaId = 1;
            var announcementArea = new AnnouncementArea(areaId);

            announcementArea.Delete();

            announcementArea.ShouldNotBeNull();
            announcementArea.State.ShouldBe(ObjectState.Deleted);
        }
    }
}