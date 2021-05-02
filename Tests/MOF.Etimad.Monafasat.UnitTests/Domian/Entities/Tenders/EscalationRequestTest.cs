using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class EscalationRequestTest
    {
        private readonly int _escalationRequestId = 1;
        private readonly int _plaintRequestId = 2;
        private readonly string _attachmentId = "Id100";
        private readonly string _attachmentName = "Id100";

        [Fact]
        public void Should_Empty_Construct_EscalationRequest()
        {
            var conditionTemplateActivities = new EscalationRequest();
            conditionTemplateActivities.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetCreated()
        {
            var conditionTemplateActivities = new EscalationRequest(0, _plaintRequestId, _attachmentId, _attachmentName);

            conditionTemplateActivities.ShouldNotBeNull();
            conditionTemplateActivities.EscalationRequestId.ShouldBe(0);
            conditionTemplateActivities.PlaintRequestId.ShouldBe(_plaintRequestId);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Constructor_SetUpdated()
        {
            var conditionTemplateActivities = new EscalationRequest(_escalationRequestId, _plaintRequestId, _attachmentId, _attachmentName);

            conditionTemplateActivities.ShouldNotBeNull();
            conditionTemplateActivities.PlaintRequestId.ShouldBe(_plaintRequestId);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_AcceptRevision()
        {
            var conditionTemplateActivities = new EscalationRequest();
            conditionTemplateActivities.AcceptRevision();
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var conditionTemplateActivities = new EscalationRequest();
            conditionTemplateActivities.DeActive();
            conditionTemplateActivities.IsActive.ShouldBe(false);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_UpdateNotes()
        {
            string _escalationNotes = "Esc-Notes";
            var conditionTemplateActivities = new EscalationRequest();
            conditionTemplateActivities.UpdateNotes(_escalationNotes);
            conditionTemplateActivities.EscalationNotes.ShouldBe(_escalationNotes);
            conditionTemplateActivities.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
