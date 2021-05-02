using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementHistoryTests
    {
        [Fact]
        public void Should_Construct_Empty()
        {
            var announcementHistory = new AnnouncementHistory();

            announcementHistory.ShouldNotBeNull();
            announcementHistory.Id.ShouldBe(0);
            announcementHistory.AnnouncementId.ShouldBe(0);
            announcementHistory.Announcement.ShouldBeNull();
            announcementHistory.AnnouncementStatus.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementHistory()
        {
            const int userId = 1;
            const int statusId = 2;

            var announcementHistory = new AnnouncementHistory(userId, statusId);

            announcementHistory.ShouldNotBeNull();
            announcementHistory.UserId.ShouldBe(userId);
            announcementHistory.StatusId.ShouldBe(statusId);
        }

        [Fact]
        public void Should_Construct_AnnouncementHistory_With_Rejection_Reason()
        {
            const int userId = 1;
            const int statusId = 2;
            const string rejectionReason = "rejection";

            var announcementHistory = new AnnouncementHistory(userId, statusId, rejectionReason);

            announcementHistory.ShouldNotBeNull();
            announcementHistory.RejectionReason.ShouldBe(rejectionReason);
        }

        [Fact]
        public void Should_DeActive_AnnouncementHistory()
        {
            var userId = 1;
            const int statusId = 2;
            var announcementHistory = new AnnouncementHistory(userId, statusId);

            announcementHistory.DeActive();

            announcementHistory.ShouldNotBeNull();
            announcementHistory.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementHistory()
        {
            var userId = 1;
            const int statusId = 2;
            var announcementHistory = new AnnouncementHistory(userId, statusId);
            announcementHistory.DeActive();

            announcementHistory.SetActive();

            announcementHistory.ShouldNotBeNull();
            announcementHistory.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementHistory()
        {
            const int userId = 1;
            const int statusId = 2;

            var announcementHistory = new AnnouncementHistory(userId, statusId);

            announcementHistory.Delete();

            announcementHistory.ShouldNotBeNull();
            announcementHistory.State.ShouldBe(ObjectState.Deleted);
        }
    }
}
