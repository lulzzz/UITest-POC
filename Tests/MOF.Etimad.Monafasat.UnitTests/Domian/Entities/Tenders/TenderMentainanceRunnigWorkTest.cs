using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderMentainanceRunnigWorkTest
    {
        private const int maintenanceRunningWorkId = 1;

        [Fact]
        public void Should_Construct_TenderMentainanceRunnigWork()
        {
            TenderMaintenanceRunnigWork tenderMaintenanceRunnigWork = new TenderMaintenanceRunnigWork(maintenanceRunningWorkId);
            _ = new TenderMaintenanceRunnigWork();

            tenderMaintenanceRunnigWork.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderMaintenanceRunnigWork tenderMaintenanceRunnigWork = new TenderMaintenanceRunnigWork(maintenanceRunningWorkId);

            tenderMaintenanceRunnigWork.Update();
            tenderMaintenanceRunnigWork.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderMaintenanceRunnigWork tenderMaintenanceRunnigWork = new TenderMaintenanceRunnigWork(maintenanceRunningWorkId);

            tenderMaintenanceRunnigWork.DeActive();
            Assert.False(tenderMaintenanceRunnigWork.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            TenderMaintenanceRunnigWork tenderMaintenanceRunnigWork = new TenderMaintenanceRunnigWork(maintenanceRunningWorkId);

            tenderMaintenanceRunnigWork.SetActive();
            Assert.True(tenderMaintenanceRunnigWork.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderMaintenanceRunnigWork tenderMaintenanceRunnigWork = new TenderMaintenanceRunnigWork(maintenanceRunningWorkId);

            tenderMaintenanceRunnigWork.SetAddedState();
            Assert.Equal(0, tenderMaintenanceRunnigWork.Id);
        }
    }
}
