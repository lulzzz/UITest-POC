using System.Collections.Generic;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class PlaintRequestTest
    {
        private readonly int _plaintRequestId = 100;
        private readonly int _offerId = 200;
        private readonly string _plaintReason = "P-Reason";
        private readonly bool _isEscalation = true;
        private readonly string _attachmentId = "Id15";
        private readonly string _attachmentName = "Att_Name";
        private readonly List<CommunicationAttachmentModel> _attachments = new List<CommunicationAttachmentModel>() {
                new CommunicationAttachmentModel() { }
            };

        [Fact]
        public void Should_Empty_Construct_PlaintRequest()
        {
            var plaintRequest = new PlaintRequest();
            plaintRequest.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Contructor_UpdateAttachments()
        {
            var plaintRequest = new PlaintRequest(_plaintRequestId, _offerId, _plaintReason, _attachments, _isEscalation);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            plaintRequest.Attachments.ShouldNotBe(null);
            plaintRequest.Attachments[0].State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Contructor_CreateEntity()
        {
            var plaintRequest = new PlaintRequest(0, _offerId, _plaintReason, _attachments, _isEscalation);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            plaintRequest.Attachments.ShouldNotBe(null);
            plaintRequest.Attachments[0].State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdatePlaintRequest()
        {
            var plaintRequest = new PlaintRequest();
            plaintRequest.UpdatePlaintRequest(_plaintRequestId, _offerId, _plaintReason, _attachments, _isEscalation);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            plaintRequest.Attachments.ShouldNotBe(null);
            plaintRequest.Attachments[0].State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_CreatePlaintRequest()
        {
            var plaintRequest = new PlaintRequest();
            plaintRequest.UpdatePlaintRequest(0, _offerId, _plaintReason, _attachments, _isEscalation);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            plaintRequest.Attachments.ShouldNotBe(null);
            plaintRequest.Attachments[0].State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateNotes_ThrowsException()
        {
            var _notes = string.Empty;
            var _exceptionMessage = Resources.CommunicationRequest.ErrorMessages.EnterNotes;
            Should.Throw<BusinessRuleException>(() => new PlaintRequest()
            .UpdateNotes(_notes)).Message.ShouldBe(_exceptionMessage);
        }

        [Fact]
        public void Should_UpdateNotes_CreateEntity()
        {
            var _notes = "Test Notes";
            var plaintRequest = new PlaintRequest();
            plaintRequest.UpdateNotes(_notes);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.Notes.ShouldBe(_notes);
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateNotes_UpdateEntity()
        {
            var _notes = "Test Notes";
            var plaintRequest = new PlaintRequest(_plaintRequestId, _offerId, _plaintReason, _attachments, _isEscalation);
            plaintRequest.UpdateNotes(_notes);
            plaintRequest.ShouldNotBeNull();
            plaintRequest.Notes.ShouldBe(_notes);
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_AcceptRevision()
        {
            var plaintRequest = new PlaintRequest();
            plaintRequest.AcceptRevision();
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var plaintRequest = new PlaintRequest();
            plaintRequest.DeActive();
            plaintRequest.IsActive.ShouldBe(false);
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateAttachments_Returns_PlaintRequest()
        {
            var plaintRequest = new PlaintRequest(_plaintRequestId, _offerId, _plaintReason, _attachments, _isEscalation);
            var plaintRequestObj = plaintRequest.UpdateAttachments(_attachments);

            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            plaintRequestObj.ShouldNotBeNull();
            plaintRequestObj.ShouldBeOfType(typeof(PlaintRequest));
            plaintRequestObj.IsEscalation.ShouldBe(false);
            plaintRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
        } 
        
        [Theory]
        [InlineData("AttachmentId","Attachment Name")]
        public void ShouldEscalatePlaintRequest(string attachmentId, string attachmentName)
        {
            var plaintRequest = new CommunicationRequestDefault().GetPlaintRequest();
            plaintRequest.EscalatePlaintRequest(attachmentId, attachmentName);

            Assert.Equal(ObjectState.Added, plaintRequest.State);
            Assert.True(plaintRequest.IsEscalation);
        }
    }
}
