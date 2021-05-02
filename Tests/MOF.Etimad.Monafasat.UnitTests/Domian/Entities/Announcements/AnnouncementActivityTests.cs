using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementActivityTests
    {
        [Fact]
        public void Should_Construct_AnnouncementActivity()
        {
            var activityId = 1;

            var announcementActivity = new AnnouncementActivity(activityId);

            announcementActivity.ShouldNotBeNull();
            announcementActivity.ActivityId.ShouldBe(activityId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementActivity()
        {
            var activityId = 1;
            var announcementActivity = new AnnouncementActivity(activityId);

            announcementActivity.DeActive();

            announcementActivity.ShouldNotBeNull();
            announcementActivity.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementActivity()
        {
            var activityId = 1;
            var announcementActivity = new AnnouncementActivity(activityId);
            announcementActivity.DeActive();

            announcementActivity.SetActive();

            announcementActivity.ShouldNotBeNull();
            announcementActivity.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementActivity()
        {
            var activityId = 1;
            var announcementActivity = new AnnouncementActivity(activityId);

            announcementActivity.Test_Delete();

            announcementActivity.ShouldNotBeNull();
            announcementActivity.State.ShouldBe(ObjectState.Deleted);
        }
    }
}
