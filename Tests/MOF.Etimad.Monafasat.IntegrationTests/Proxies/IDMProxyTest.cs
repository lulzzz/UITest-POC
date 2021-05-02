using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class IDMProxyTest
    {
        private readonly IDMProxy _IDMProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        public IDMProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _IDMProxy = new IDMProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldGetEmployeeDetailsByRoleName()
        {
            //Act
            var result = await _IDMProxy.GetEmployeeDetailsByRoleName("NewMonafasat_Auditer");
            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldGetSuppliersBySearchCriteria()
        {
            //Arrange
            SupplierIntegrationSearchCriteria criteria = new SupplierIntegrationSearchCriteria { };
            //Act
            var result = await _IDMProxy.GetSuppliersBySearchCriteria(criteria);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMonafasatUsersByAgencyType()
        {
            //Arrange
            UsersSearchCriteriaModel criteria = new UsersSearchCriteriaModel { AgencyType = (int)AgencyType.GovVendor,MobileNumber="0533286913",Email="hsawak@etimad.sa",Name="name" };
            //Act
            var result = await _IDMProxy.GetMonafasatUsersByAgencyType(criteria);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMonafasatUsersByAgencyTypeAndRoleName()
        {
            //Arrange
            UsersSearchCriteriaModel criteria = new UsersSearchCriteriaModel { RoleName = "NewMonafasat_Admin" };
            //Act
            var result = await _IDMProxy.GetMonafasatUsersByAgencyTypeAndRoleName(criteria);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAgencyDetailsRelatedToSadad()
        {
            //Act
            var result = await _IDMProxy.GetAgencyDetailsRelatedToSadad("019004001000", (int)AgencyType.PrivateSector);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAgencyLogos()
        {
            //Arrange
            List<string> codes = new List<string> { "019004001000", "019004101000", "012000000000", "015001000000" };
            //Act
            var result = await _IDMProxy.GetAgencyLogos(codes);
            //Assert
            Assert.NotNull(result);
        }

        // can't find a CR that returns data so used assert.null
        [Fact]
        public async Task ShouldGetSupplierInfoByCR()
        {
            //Act
            var result = await _IDMProxy.GetSupplierInfoByCR("1010274503");
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetContactOfficersByCR()
        {
            //Arrange
            List<string> CRs = new List<string> { "1010000154" };
            //Act
            var result = await _IDMProxy.GetContactOfficersByCR(CRs);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetUserbyUserName()
        {
            var result = await _IDMProxy.GetUserbyUserName("5555555555");
            Assert.NotNull(result);
        }

    }
}
