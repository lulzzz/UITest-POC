using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NotifayTypeEntityTest
    {
        [Fact]
        public void Should_Construct_NotifayTypeEntity()
        {
            NotifayTypeEntity notifayTypeEntity = new NotifayTypeEntity();
            _ = notifayTypeEntity.NotifayTypeId;
            _ = notifayTypeEntity.Name;

            notifayTypeEntity.ShouldNotBeNull();
        }
    }
}
