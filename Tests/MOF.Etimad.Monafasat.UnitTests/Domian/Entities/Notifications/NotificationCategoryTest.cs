using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class NotificationCategoryTest
    {
        [Fact]
        public void Should_Construct_NotificationCategory()
        {
            NotificationCategory notificationCategory = new NotificationCategory();
            _ = notificationCategory.Id;
            _ = notificationCategory.ArabicName;
            _ = notificationCategory.EnglishName;

            notificationCategory.ShouldNotBeNull();
        }
    }
}
