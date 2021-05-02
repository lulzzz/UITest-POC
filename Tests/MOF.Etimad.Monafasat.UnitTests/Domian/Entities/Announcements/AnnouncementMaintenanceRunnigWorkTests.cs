using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementMaintenanceRunnigWorkTests
    {
        [Fact]
        public void Should_Construct_AnnouncementMaintenanceRunnigWork()
        {
            var announcementMaintenanceRunnigWorkId = 1;

            var announcementMaintenanceRunnigWork = new AnnouncementMaintenanceRunnigWork(announcementMaintenanceRunnigWorkId);

            announcementMaintenanceRunnigWork.ShouldNotBeNull();
            announcementMaintenanceRunnigWork.MaintenanceRunningWorkId.ShouldBe(announcementMaintenanceRunnigWorkId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementMaintenanceRunnigWork()
        {
            var announcementMaintenanceRunnigWorkId = 1;
            var announcementMaintenanceRunnigWork = new AnnouncementMaintenanceRunnigWork(announcementMaintenanceRunnigWorkId);

            announcementMaintenanceRunnigWork.DeActive();

            announcementMaintenanceRunnigWork.ShouldNotBeNull();
            announcementMaintenanceRunnigWork.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementMaintenanceRunnigWork()
        {
            var announcementMaintenanceRunnigWorkId = 1;
            var announcementMaintenanceRunnigWork = new AnnouncementMaintenanceRunnigWork(announcementMaintenanceRunnigWorkId);
            announcementMaintenanceRunnigWork.DeActive();

            announcementMaintenanceRunnigWork.SetActive();

            announcementMaintenanceRunnigWork.ShouldNotBeNull();
            announcementMaintenanceRunnigWork.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementMaintenanceRunnigWork()
        {
            var announcementMaintenanceRunnigWorkId = 1;
            var announcementMaintenanceRunnigWork = new AnnouncementMaintenanceRunnigWork(announcementMaintenanceRunnigWorkId);

            announcementMaintenanceRunnigWork.Test_Delete();

            announcementMaintenanceRunnigWork.ShouldNotBeNull();
            announcementMaintenanceRunnigWork.State.ShouldBe(ObjectState.Deleted);
        }
    }
}
