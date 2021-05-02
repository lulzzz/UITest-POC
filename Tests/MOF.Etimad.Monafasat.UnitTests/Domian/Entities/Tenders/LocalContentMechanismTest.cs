using MOF.Etimad.Monafasat.Core.Entities;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class LocalContentMechanismTest
    {
        private readonly int _localContentMechanismReferenceId = 1;
        [Fact]
        public void Should_ConstructLocalContentMechanismTest()
        {
            LocalContentMechanism localContentMechanism = new LocalContentMechanism(_localContentMechanismReferenceId);
            Assert.Equal(_localContentMechanismReferenceId, localContentMechanism.LocalContentMechanismPreferenceId);
        }
    }
}
