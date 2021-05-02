using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class MaintenanceRunningWorkTest
    {
        [Fact]
        public void Should_Construct_MaintenanceRunningWork()
        {
            MaintenanceRunningWork maintenanceRunningWork = new MaintenanceRunningWork();
            _ = maintenanceRunningWork.MaintenanceRunningWorkId;
            _ = maintenanceRunningWork.NameAr;
            _ = maintenanceRunningWork.NameEn;
            _ = maintenanceRunningWork.TenderMentainanceRunnigWorks;

            maintenanceRunningWork.ShouldNotBeNull();
        }
    }
}
