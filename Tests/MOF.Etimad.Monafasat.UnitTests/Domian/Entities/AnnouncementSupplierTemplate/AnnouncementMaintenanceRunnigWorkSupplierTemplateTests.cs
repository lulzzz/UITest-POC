using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementMaintenanceRunnigWorkSupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {
            var announcementMaintenanceRunnigWorkSupplierTemplate = new AnnouncementMaintenanceRunnigWorkSupplierTemplate();

            announcementMaintenanceRunnigWorkSupplierTemplate.ShouldNotBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.Id.ShouldBe(0);
            announcementMaintenanceRunnigWorkSupplierTemplate.MaintenanceRunningWork.ShouldBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementMaintenanceRunnigWorkSupplierTemplate.AnnouncementSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {
            var announcementMaintenanceRunnigWorkSupplierTemplateId = 1;

            var announcementMaintenanceRunnigWorkSupplierTemplate = new AnnouncementMaintenanceRunnigWorkSupplierTemplate(announcementMaintenanceRunnigWorkSupplierTemplateId);

            announcementMaintenanceRunnigWorkSupplierTemplate.ShouldNotBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.MaintenanceRunningWorkId.ShouldBe(announcementMaintenanceRunnigWorkSupplierTemplateId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {
            var announcementMaintenanceRunnigWorkSupplierTemplateId = 1;
            var announcementMaintenanceRunnigWorkSupplierTemplate = new AnnouncementMaintenanceRunnigWorkSupplierTemplate(announcementMaintenanceRunnigWorkSupplierTemplateId);

            announcementMaintenanceRunnigWorkSupplierTemplate.DeActive();

            announcementMaintenanceRunnigWorkSupplierTemplate.ShouldNotBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {
            var announcementMaintenanceRunnigWorkSupplierTemplateId = 1;
            var announcementMaintenanceRunnigWorkSupplierTemplate = new AnnouncementMaintenanceRunnigWorkSupplierTemplate(announcementMaintenanceRunnigWorkSupplierTemplateId);

            announcementMaintenanceRunnigWorkSupplierTemplate.SetActive();

            announcementMaintenanceRunnigWorkSupplierTemplate.ShouldNotBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {
            var announcementMaintenanceRunnigWorkSupplierTemplateId = 1;
            var announcementMaintenanceRunnigWorkSupplierTemplate = new AnnouncementMaintenanceRunnigWorkSupplierTemplate(announcementMaintenanceRunnigWorkSupplierTemplateId);

            announcementMaintenanceRunnigWorkSupplierTemplate.Test_Delete();

            announcementMaintenanceRunnigWorkSupplierTemplate.ShouldNotBeNull();
            announcementMaintenanceRunnigWorkSupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}