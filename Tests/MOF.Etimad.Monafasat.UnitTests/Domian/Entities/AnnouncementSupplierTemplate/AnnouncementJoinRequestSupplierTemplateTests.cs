using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementJoinRequestSupplierTemplateTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementJoinRequestSupplierTemplate()
        {
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate();

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.Id.ShouldBe(0);
            announcementJoinRequestSupplierTemplate.JoinRequestStatus.ShouldBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementSupplierTemplate.ShouldBeNull();
            announcementJoinRequestSupplierTemplate.Supplier.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementJoinRequestSupplierTemplate()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };

            var announcementId = 1;
            var cr = "123";
            var statusId = 1;

            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(announcementId);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe(cr);
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe(statusId);
            announcementJoinRequestSupplierTemplate.Attachments.Count.ShouldBeGreaterThanOrEqualTo(1);
            announcementJoinRequestSupplierTemplate.Attachments[0].Name.ShouldBe("name");
        }

        [Fact]
        public void Should_UpdateAnnouncementJoinRequestSupplierTemplate()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.UpdateAnnouncementJoinRequest(1, 1, 2, "rejectReason", "notes");

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(1);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe("123");
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe(2);
            announcementJoinRequestSupplierTemplate.RejectionReason.ShouldBe("rejectReason");
            announcementJoinRequestSupplierTemplate.Notes.ShouldBe("notes");
            announcementJoinRequestSupplierTemplate.Attachments.Count.ShouldBeGreaterThanOrEqualTo(1);
            announcementJoinRequestSupplierTemplate.Attachments[0].Name.ShouldBe("name");
        }

        [Fact]
        public void Should_UpdateAnnouncementJoinRequestSupplierTemplate_Rejected()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.UpdateAnnouncementJoinRequest(1, 1, 5, "rejectReason", "notes");

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(1);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe("123");
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe(5);
            announcementJoinRequestSupplierTemplate.RejectionReason.ShouldBe("rejectReason");
            announcementJoinRequestSupplierTemplate.Notes.ShouldBe("notes");
            announcementJoinRequestSupplierTemplate.Attachments.Count.ShouldBeGreaterThanOrEqualTo(1);
            announcementJoinRequestSupplierTemplate.Attachments[0].Name.ShouldBe("name");
            announcementJoinRequestSupplierTemplate.Attachments[0].IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_UpdateSuppliersTemplateJoinRequestAttachments()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.UpdateSuppliersTemplateJoinRequestAttachments(
                new List<AnnouncementTemplateJoinRequestAttachment>
                {
                    new AnnouncementTemplateJoinRequestAttachment("newname", "456")
                }, cr, false);

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(1);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe("123");
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe(1);
            announcementJoinRequestSupplierTemplate.Attachments.Count.ShouldBeGreaterThanOrEqualTo(2);
            announcementJoinRequestSupplierTemplate.Attachments[0].Name.ShouldBe("name");
            announcementJoinRequestSupplierTemplate.Attachments[0].State.ShouldBe(ObjectState.Deleted);
            announcementJoinRequestSupplierTemplate.Attachments[1].Name.ShouldBe("newname");
        }

        [Fact]
        public void Should_WithDraw_AnnouncementJoinRequestSupplierTemplate()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
            var announcementId = 1;
            var cr = "123";
            var statusId = 1;
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.Withdraw(1, 1);

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(announcementId);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe(cr);
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe((int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn);
        }

        [Fact]
        public void Should_Delete_SupplierFromAnnouncementTemplate()
        {
            var attachments = new List<AnnouncementTemplateJoinRequestAttachment>
            {
                new AnnouncementTemplateJoinRequestAttachment("name", "123")
            };
            var announcementId = 1;
            var cr = "123";
            var statusId = 4;
            var announcementJoinRequestSupplierTemplate = new AnnouncementJoinRequestSupplierTemplate(attachments, announcementId, cr, statusId);

            announcementJoinRequestSupplierTemplate.DeleteSupplierJoinRequest(1, 1, "delete Reason");

            announcementJoinRequestSupplierTemplate.ShouldNotBeNull();
            announcementJoinRequestSupplierTemplate.AnnouncementId.ShouldBe(announcementId);
            announcementJoinRequestSupplierTemplate.Cr.ShouldBe(cr);
            announcementJoinRequestSupplierTemplate.StatusId.ShouldBe((int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted);
        }
    }
}