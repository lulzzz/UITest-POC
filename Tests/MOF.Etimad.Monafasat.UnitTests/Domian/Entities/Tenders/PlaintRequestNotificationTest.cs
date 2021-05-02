using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class PlaintRequestNotificationTest
    {
        public readonly int _plaintRequestNotificationId = 100;
        private readonly bool _isSent = true;

        [Fact]
        public void Should_Empty_Construct_PlaintRequestNotification()
        {
            var plaintRequestNotification = new PlaintRequestNotification();
            plaintRequestNotification.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_UpdateEntity()
        {
            var plaintRequestNotification = new PlaintRequestNotification(_plaintRequestNotificationId, _isSent);
            plaintRequestNotification.ShouldNotBeNull();
            plaintRequestNotification.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_Constructor_CreateEntity()
        {
            var plaintRequestNotification = new PlaintRequestNotification(0, _isSent);
            plaintRequestNotification.ShouldNotBeNull();
            plaintRequestNotification.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateApprovalDate()
        {
            var plaintRequestNotification = new PlaintRequestNotification();
            plaintRequestNotification.UpdateApprovalDate();
            plaintRequestNotification.ApprovalDate.ShouldNotBeNull();
            plaintRequestNotification.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var plaintRequestNotification = new PlaintRequestNotification();
            plaintRequestNotification.DeActive();
            plaintRequestNotification.IsActive.ShouldBe(false);
            plaintRequestNotification.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_Update_ReturnsObj()
        {
            var plaintRequestNotification = new PlaintRequestNotification();
            var obj = plaintRequestNotification.Update(_isSent);
            obj.ShouldNotBeNull();
            obj.ShouldBeOfType(typeof(PlaintRequestNotification));
            obj.IsSent.ShouldBeTrue();
            obj.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
