using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAttachmentTest
    {
        private readonly string _name = "Attch_Name";
        private readonly string _fileNetReferenceId = "FId1009";
        private readonly int _attachmentTypeId = 2001;
        private readonly int _changeStatusId = 2002;
        private readonly int _reviewStatusId = 2003;
        private readonly string _rejectReason = "RReason Test";

        [Fact]
        public void Should_Empty_Construct_TenderAttachment()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var tenderAttachment = new TenderAttachment(_name, _fileNetReferenceId, _attachmentTypeId, _changeStatusId, _reviewStatusId);
            tenderAttachment.ShouldNotBeNull();
            tenderAttachment.Name.ShouldBe(_name);
            tenderAttachment.FileNetReferenceId.ShouldBe(_fileNetReferenceId);
            tenderAttachment.AttachmentTypeId.ShouldBe(_attachmentTypeId);
            tenderAttachment.ChangeStatusId.ShouldBe(_changeStatusId);
            tenderAttachment.ReviewStatusId.ShouldBe(_reviewStatusId);
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_DeleteEntity()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.DeleteAttachment();
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.SetAddedState();
            tenderAttachment.TenderAttachmentId.ShouldBe(0);
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_DeactivateEntity()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.DeActive();
            tenderAttachment.IsActive.ShouldBe(false);
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdatePHPData()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.UpdatePHPData(_fileNetReferenceId);
            tenderAttachment.FileNetReferenceId.ShouldBe(_fileNetReferenceId);
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateChangStatus()
        {
            var tenderAttachment = new TenderAttachment();
            tenderAttachment.UpdateChangStatus(_changeStatusId, _reviewStatusId, _rejectReason);
            tenderAttachment.ChangeStatusId.ShouldBe(_changeStatusId);
            tenderAttachment.ReviewStatusId.ShouldBe(_reviewStatusId);
            tenderAttachment.RejectionReason.ShouldBe(_rejectReason);
            tenderAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
