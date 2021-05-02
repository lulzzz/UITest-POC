using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementConstructionWorkTests
    {
        [Fact]
        public void Should_Construct_AnnouncementConstructionWork()
        {
            var announcementConstructionWorkId = 1;

            var announcementConstructionWork = new AnnouncementConstructionWork(announcementConstructionWorkId);

            announcementConstructionWork.ShouldNotBeNull();
            announcementConstructionWork.ConstructionWorkId.ShouldBe(announcementConstructionWorkId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementConstructionWork()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWork = new AnnouncementConstructionWork(announcementConstructionWorkId);

            announcementConstructionWork.DeActive();

            announcementConstructionWork.ShouldNotBeNull();
            announcementConstructionWork.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementConstructionWork()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWork = new AnnouncementConstructionWork(announcementConstructionWorkId);
            announcementConstructionWork.DeActive();

            announcementConstructionWork.SetActive();

            announcementConstructionWork.ShouldNotBeNull();
            announcementConstructionWork.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementConstructionWork()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWork = new AnnouncementConstructionWork(announcementConstructionWorkId);

            announcementConstructionWork.Test_Delete();

            announcementConstructionWork.ShouldNotBeNull();
            announcementConstructionWork.State.ShouldBe(ObjectState.Deleted);
        }
    }
}