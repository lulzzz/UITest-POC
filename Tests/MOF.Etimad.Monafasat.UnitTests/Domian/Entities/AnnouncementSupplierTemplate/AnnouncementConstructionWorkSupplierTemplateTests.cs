using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementConstructionWorkSupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementConstructionWorkSupplierTemplate()
        {
            var announcementConstructionWorkSupplierTemplate = new AnnouncementConstructionWorkSupplierTemplate();

            announcementConstructionWorkSupplierTemplate.ShouldNotBeNull();
            announcementConstructionWorkSupplierTemplate.Id.ShouldBe(0);
            announcementConstructionWorkSupplierTemplate.ConstructionWork.ShouldBeNull();
            announcementConstructionWorkSupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementConstructionWorkSupplierTemplate.Announcement.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementConstructionWorkSupplierTemplate()
        {
            var announcementConstructionWorkId = 1;

            var announcementConstructionWorkSupplierTemplate = new AnnouncementConstructionWorkSupplierTemplate(announcementConstructionWorkId);

            announcementConstructionWorkSupplierTemplate.ShouldNotBeNull();
            announcementConstructionWorkSupplierTemplate.ConstructionWorkId.ShouldBe(announcementConstructionWorkId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementConstructionWorkSupplierTemplate()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWorkSupplierTemplate = new AnnouncementConstructionWorkSupplierTemplate(announcementConstructionWorkId);

            announcementConstructionWorkSupplierTemplate.DeActive();

            announcementConstructionWorkSupplierTemplate.ShouldNotBeNull();
            announcementConstructionWorkSupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementConstructionWorkSupplierTemplate()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWorkSupplierTemplate = new AnnouncementConstructionWorkSupplierTemplate(announcementConstructionWorkId);

            announcementConstructionWorkSupplierTemplate.SetActive();

            announcementConstructionWorkSupplierTemplate.ShouldNotBeNull();
            announcementConstructionWorkSupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementConstructionWorkSupplierTemplate()
        {
            var announcementConstructionWorkId = 1;
            var announcementConstructionWorkSupplierTemplate = new AnnouncementConstructionWorkSupplierTemplate(announcementConstructionWorkId);

            announcementConstructionWorkSupplierTemplate.Test_Delete();

            announcementConstructionWorkSupplierTemplate.ShouldNotBeNull();
            announcementConstructionWorkSupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}