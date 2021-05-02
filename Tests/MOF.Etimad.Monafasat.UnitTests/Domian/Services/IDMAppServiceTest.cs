using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Services
{
    public class IDMAppServiceTest
    {

        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly IDMAppService _iDMAppService;
        private readonly Mock<INotificationAppService> _notificationAppServiceMock;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IIDMQueries> _moqiDMQueries;
        private readonly Mock<ICommandService> _moqCommandService;
        private readonly Mock<INotificationQueries> _moqNotificationQueries;
        private readonly Mock<IIDMProxy> _moqiDMProxy;

        public IDMAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestingDB")
                 .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _notificationAppServiceMock = new Mock<INotificationAppService>();
            _moqiDMProxy = new Mock<IIDMProxy>();
            _mapper = new Mock<IMapper>();
            _moqCommandService = new Mock<ICommandService>();
            _moqNotificationQueries = new Mock<INotificationQueries>();
            _moqiDMQueries = new Mock<IIDMQueries>();
            _iDMAppService = new IDMAppService(_moqiDMProxy.Object,_moqiDMQueries.Object,_mapper.Object,_moqCommandService.Object, _httpContext.Object,_moqNotificationQueries.Object, _notificationAppServiceMock.Object, null);
        }

        [Fact]
        public void ShouldConstructIDMAppService()
        {
            Assert.NotNull(_iDMAppService);
            Assert.IsType<IDMAppService>(_iDMAppService);
        }

        [Fact]
        public void ShouldGetALlRoles()
        {

            var result = _iDMAppService.GetIDMRoles();
            Assert.NotNull(result);
        }
    }
}
