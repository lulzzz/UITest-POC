using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class RejectionHistoryTest
    {
        private readonly int _reuestId = 10;
        private readonly int _requestsRejectionTypeId = 1;
        private readonly int _statusId = 3;
        private readonly string _rejectionReason = "rejection_Reasontest";

        [Fact]
        public void Should_Empty_Construct_rejectionHistory()
        {
            var rejectionHistory = new RejectionHistory();
            rejectionHistory.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var rejectionHistory = new RejectionHistory(_reuestId, _requestsRejectionTypeId, _statusId, _rejectionReason);
            rejectionHistory.ShouldNotBeNull();
            rejectionHistory.ReuestId.ShouldBe(_reuestId);
            rejectionHistory.RequestsRejectionTypeId.ShouldBe(_requestsRejectionTypeId);
            rejectionHistory.StatusId.ShouldBe(_statusId);
            rejectionHistory.RejectionReason.ShouldBe(_rejectionReason);
            rejectionHistory.State.ShouldBe(SharedKernal.ObjectState.Added);
        }
    }
}
