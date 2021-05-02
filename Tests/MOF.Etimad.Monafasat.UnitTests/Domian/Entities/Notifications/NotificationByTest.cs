using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class NotificationByTest
    {
        [Fact]
        public void Should_Construct_NotificationBy()
        {
            NotificationBy notificationBy = new NotificationBy();
            _ = notificationBy.Email;
            _ = notificationBy.Mobile;
            notificationBy.ShouldNotBeNull();
        }
    }
}
