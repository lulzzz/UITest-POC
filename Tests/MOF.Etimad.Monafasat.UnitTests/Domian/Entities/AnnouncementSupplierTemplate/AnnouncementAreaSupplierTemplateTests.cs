using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementAreaSupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementAreaSupplierTemplate()
        {
            var announcementAreaSupplierTemplate = new AnnouncementAreaSupplierTemplate();

            announcementAreaSupplierTemplate.ShouldNotBeNull();
            announcementAreaSupplierTemplate.Id.ShouldBe(0);
            announcementAreaSupplierTemplate.Area.ShouldBeNull();
            announcementAreaSupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementAreaSupplierTemplate.AnnouncementSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementAreaSupplierTemplate()
        {
            var areaId = 1;

            var announcementAreaSupplierTemplate = new AnnouncementAreaSupplierTemplate(areaId);

            announcementAreaSupplierTemplate.ShouldNotBeNull();
            announcementAreaSupplierTemplate.AreaId.ShouldBe(areaId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementAreaSupplierTemplate()
        {
            var areaId = 1;
            var announcementAreaSupplierTemplate = new AnnouncementAreaSupplierTemplate(areaId);

            announcementAreaSupplierTemplate.DeActive();

            announcementAreaSupplierTemplate.ShouldNotBeNull();
            announcementAreaSupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementAreaSupplierTemplate()
        {
            var areaId = 1;
            var announcementAreaSupplierTemplate = new AnnouncementAreaSupplierTemplate(areaId);

            announcementAreaSupplierTemplate.SetActive();

            announcementAreaSupplierTemplate.ShouldNotBeNull();
            announcementAreaSupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementAreaSupplierTemplate()
        {
            var areaId = 1;
            var announcementAreaSupplierTemplate = new AnnouncementAreaSupplierTemplate(areaId);

            announcementAreaSupplierTemplate.Delete();

            announcementAreaSupplierTemplate.ShouldNotBeNull();
            announcementAreaSupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}