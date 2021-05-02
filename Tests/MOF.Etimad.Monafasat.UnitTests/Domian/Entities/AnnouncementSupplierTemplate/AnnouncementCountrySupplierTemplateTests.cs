using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementCountrySupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementCountrySupplierTemplate()
        {
            var announcementCountrySupplierTemplate = new AnnouncementCountrySupplierTemplate();

            announcementCountrySupplierTemplate.ShouldNotBeNull();
            announcementCountrySupplierTemplate.Id.ShouldBe(0);
            announcementCountrySupplierTemplate.Country.ShouldBeNull();
            announcementCountrySupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementCountrySupplierTemplate.Announcement.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementCountrySupplierTemplate()
        {
            var announcementCountryId = 1;

            var announcementCountrySupplierTemplate = new AnnouncementCountrySupplierTemplate(announcementCountryId);

            announcementCountrySupplierTemplate.ShouldNotBeNull();
            announcementCountrySupplierTemplate.CountryId.ShouldBe(announcementCountryId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementCountrySupplierTemplate()
        {
            var announcementCountryId = 1;
            var announcementCountrySupplierTemplate = new AnnouncementCountrySupplierTemplate(announcementCountryId);

            announcementCountrySupplierTemplate.DeActive();

            announcementCountrySupplierTemplate.ShouldNotBeNull();
            announcementCountrySupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementCountrySupplierTemplate()
        {
            var announcementCountryId = 1;
            var announcementCountrySupplierTemplate = new AnnouncementCountrySupplierTemplate(announcementCountryId);

            announcementCountrySupplierTemplate.SetActive();

            announcementCountrySupplierTemplate.ShouldNotBeNull();
            announcementCountrySupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementCountrySupplierTemplate()
        {
            var announcementCountryId = 1;
            var announcementCountrySupplierTemplate = new AnnouncementCountrySupplierTemplate(announcementCountryId);

            announcementCountrySupplierTemplate.Delete();

            announcementCountrySupplierTemplate.ShouldNotBeNull();
            announcementCountrySupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}