using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementActivitySupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementActivitySupplierTemplate()
        {
            var announcementActivitySupplierTemplate = new AnnouncementActivitySupplierTemplate();

            announcementActivitySupplierTemplate.ShouldNotBeNull();
            announcementActivitySupplierTemplate.AnnouncementActivityId.ShouldBe(0);
            announcementActivitySupplierTemplate.Activity.ShouldBeNull();
            announcementActivitySupplierTemplate.AnnouncementId.ShouldBe(0);
            announcementActivitySupplierTemplate.AnnouncementSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementActivitySupplierTemplate()
        {
            var activityId = 1;

            var announcementActivitySupplierTemplate = new AnnouncementActivitySupplierTemplate(activityId);

            announcementActivitySupplierTemplate.ShouldNotBeNull();
            announcementActivitySupplierTemplate.ActivityId.ShouldBe(activityId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementActivitySupplierTemplate()
        {
            var activityId = 1;
            var announcementActivitySupplierTemplate = new AnnouncementActivitySupplierTemplate(activityId);

            announcementActivitySupplierTemplate.DeActive();

            announcementActivitySupplierTemplate.ShouldNotBeNull();
            announcementActivitySupplierTemplate.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementActivitySupplierTemplate()
        {
            var activityId = 1;
            var announcementActivitySupplierTemplate = new AnnouncementActivitySupplierTemplate(activityId);

            announcementActivitySupplierTemplate.SetActive();

            announcementActivitySupplierTemplate.ShouldNotBeNull();
            announcementActivitySupplierTemplate.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementActivitySupplierTemplate()
        {
            var activityId = 1;
            var announcementActivitySupplierTemplate = new AnnouncementActivitySupplierTemplate(activityId);

            announcementActivitySupplierTemplate.Test_Delete();

            announcementActivitySupplierTemplate.ShouldNotBeNull();
            announcementActivitySupplierTemplate.State.ShouldBe(ObjectState.Deleted);
        }
    }
}