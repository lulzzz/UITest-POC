using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAttachmentChangesTest
    {
        private readonly string _name = "Attch_Name";
        private readonly string _fileNetReferenceId = "FId1009";
        private readonly int _attachmentTypeId = 2001;
        private readonly int _deletedAttachmentId = 2002;
        private readonly int _changeStatusId = 2002;
        private readonly int _reviewStatusId = 2003;
        private readonly string _rejectReason = "RReason Test";

        [Fact]
        public void Should_Empty_Construct_TenderAttachmentChanges()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges();
            tenderAttachmentChanges.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges(_name, _fileNetReferenceId, _attachmentTypeId, _deletedAttachmentId);
            tenderAttachmentChanges.ShouldNotBeNull();
            tenderAttachmentChanges.Name.ShouldBe(_name);
            tenderAttachmentChanges.FileNetReferenceId.ShouldBe(_fileNetReferenceId);
            tenderAttachmentChanges.AttachmentTypeId.ShouldBe(_attachmentTypeId);
            tenderAttachmentChanges.DeletedAttachmentId.ShouldBe(_deletedAttachmentId);
            tenderAttachmentChanges.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateName()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges();
            tenderAttachmentChanges.Update(_name);
            tenderAttachmentChanges.ShouldNotBeNull();
            tenderAttachmentChanges.Name.ShouldBe(_name);
            tenderAttachmentChanges.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges();
            tenderAttachmentChanges.DeActive();
            tenderAttachmentChanges.ShouldNotBeNull();
            tenderAttachmentChanges.IsActive.ShouldBe(false);
            tenderAttachmentChanges.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges();
            tenderAttachmentChanges.SetActive();
            tenderAttachmentChanges.ShouldNotBeNull();
            tenderAttachmentChanges.IsActive.ShouldBe(true);
            tenderAttachmentChanges.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateChangStatus()
        {
            var tenderAttachmentChanges = new TenderAttachmentChanges();
            tenderAttachmentChanges.UpdateChangStatus(_changeStatusId, _reviewStatusId, _rejectReason);
            tenderAttachmentChanges.ShouldNotBeNull();
            tenderAttachmentChanges.RejectionReason.ShouldBe(_rejectReason);
            tenderAttachmentChanges.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
