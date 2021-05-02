using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementJoinRequestTests
    {
        [Fact]
        public void Should_Construct_Empty()
        {
            var announcementJoinRequest = new AnnouncementJoinRequest();

            announcementJoinRequest.ShouldNotBeNull();
            announcementJoinRequest.Id.ShouldBe(0);
            announcementJoinRequest.joinRequestStatus.ShouldBeNull();
            announcementJoinRequest.Announcement.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementJoinRequest()
        {
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;

            var announcementJoinRequest = new AnnouncementJoinRequest(announcementId, cr, statusId);

            announcementJoinRequest.ShouldNotBeNull();
            announcementJoinRequest.AnnouncementId.ShouldBe(announcementId);
            announcementJoinRequest.Cr.ShouldBe(cr);
            announcementJoinRequest.StatusId.ShouldBe(statusId);
        }
        [Fact]
        public void Should_UpdateAnnouncementJoinRequest()
        {
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequest = new AnnouncementJoinRequest(announcementId, cr, statusId);

            announcementJoinRequest.UpdateAnnouncementJoinRequest(2, "456", 2);

            announcementJoinRequest.ShouldNotBeNull();
            announcementJoinRequest.AnnouncementId.ShouldBe(2);
            announcementJoinRequest.Cr.ShouldBe("456");
            announcementJoinRequest.StatusId.ShouldBe(2);
        }
        [Fact]
        public void Should_WithDraw_AnnouncementJoinRequest()
        {
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequest = new AnnouncementJoinRequest(announcementId, cr, statusId);

            announcementJoinRequest.WithDraw();

            announcementJoinRequest.ShouldNotBeNull();
            announcementJoinRequest.AnnouncementId.ShouldBe(announcementId);
            announcementJoinRequest.Cr.ShouldBe(cr);
            announcementJoinRequest.StatusId.ShouldBe((int)Enums.AnnouncementJoinRequestStatus.WithDraw);
        }
    }
}
