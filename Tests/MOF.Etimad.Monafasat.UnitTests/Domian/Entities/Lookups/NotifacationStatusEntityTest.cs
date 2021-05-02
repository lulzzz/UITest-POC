using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NotifacationStatusEntityTest
    {
        [Fact]
        public void Should_Construct_NotifacationStatusEntity()
        {
            NotifacationStatusEntity notifacationStatusEntity = new NotifacationStatusEntity();
            _ = notifacationStatusEntity.NotifacationStatusEntityId;
            _ = notifacationStatusEntity.Name;

            notifacationStatusEntity.ShouldNotBeNull();
        }
    }
}
