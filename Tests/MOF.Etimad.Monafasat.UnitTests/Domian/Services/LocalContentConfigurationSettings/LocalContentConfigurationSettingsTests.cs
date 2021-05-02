using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class LocalContentConfigurationSettingsTests
    {
        private readonly Mock<IMemoryCache> _cache;

        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly LocalContentConfigurationSettings _sut;
        public LocalContentConfigurationSettingsTests()
        {
            _cache = new Mock<IMemoryCache>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForCachingInformation());
            _sut = new LocalContentConfigurationSettings(InitialData.context, _cache.Object, _rootConfigurationMock.Object);
        }

        [Fact]
        public async Task getLocalContentSettings()
        {
            var entryMock = new Mock<ICacheEntry>();
            _cache.Setup(m => m.CreateEntry(It.IsAny<object>()))
            .Returns(entryMock.Object);

            var result = await _sut.getLocalContentSettingsDate();
            Assert.NotNull(result);
            Assert.Equal("default Setting", result.Description);
        }
    }
}
