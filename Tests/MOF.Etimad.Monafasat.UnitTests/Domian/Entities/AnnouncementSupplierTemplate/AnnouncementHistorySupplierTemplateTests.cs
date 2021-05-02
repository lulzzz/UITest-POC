using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementHistorySupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementHistorySupplierTemplate()
        {
            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate();

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.Id.ShouldBe(0);
            announcementHistorySupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementHistorySupplierTemplate.AnnouncementSupplierTemplate.ShouldBeNull();
            announcementHistorySupplierTemplate.AnnouncementStatusSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementHistorySupplierTemplate()
        {
            const int userId = 1;
            const int statusId = 2;

            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate(userId, statusId);

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.UserId.ShouldBe(userId);
            announcementHistorySupplierTemplate.StatusId.ShouldBe(statusId);
        }

        [Fact]
        public void Should_Construct_AnnouncementHistorySupplierTemplate_With_Rejection_Reason()
        {
            const int userId = 1;
            const int statusId = 2;
            const string rejectionReason = "rejection";

            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate(userId, statusId, rejectionReason);

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.RejectionReason.ShouldBe(rejectionReason);
        }

        [Fact]
        public void Should_DeActive_AnnouncementHistorySupplierTemplate()
        {
            var userId = 1;
            const int statusId = 2;
            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate(userId, statusId);

            announcementHistorySupplierTemplate.DeActive();

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementHistorySupplierTemplate()
        {
            var userId = 1;
            const int statusId = 2;
            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate(userId, statusId);

            announcementHistorySupplierTemplate.SetActive();

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementHistorySupplierTemplate()
        {
            const int userId = 1;
            const int statusId = 2;

            var announcementHistorySupplierTemplate = new AnnouncementHistorySupplierTemplate(userId, statusId);

            announcementHistorySupplierTemplate.Delete();

            announcementHistorySupplierTemplate.ShouldNotBeNull();
            announcementHistorySupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}