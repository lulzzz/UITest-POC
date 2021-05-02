using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class LocalContentMechanismPreferenceTest
    {
        [Fact]
        public void Should_Construct_Area()
        {
            LocalContentMechanismPreference localContentMechanismPreference = new LocalContentMechanismPreference();
            _ = localContentMechanismPreference.Id;
            _ = localContentMechanismPreference.NameAr;
            _ = localContentMechanismPreference.NameEn;
            localContentMechanismPreference.ShouldNotBeNull();
        }
    } 
}
