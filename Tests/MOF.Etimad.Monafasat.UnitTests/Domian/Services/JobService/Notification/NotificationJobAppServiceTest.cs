using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.JobServices;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.NotificationDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class NotificationJobAppServiceTest
    {
        private readonly Mock<IMemoryCache> _cache;
        private readonly Mock<IIDMProxy> _idmProxy;
        private readonly Mock<INotificationQueries> _iNotificationQuerie;
        private readonly Mock<IINotificationCommands> _notifayCommands;
        private readonly Mock<ILogger<NotificationAppService>> _logger;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _configuration;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
        public readonly NotificationJobAppService _sut;
        public NotificationJobAppServiceTest()
        {
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _cache = new Mock<IMemoryCache>();
            _idmProxy = new Mock<IIDMProxy>();
            _iNotificationQuerie = new Mock<INotificationQueries>();
            _notifayCommands = new Mock<IINotificationCommands>();
            _logger = new Mock<ILogger<NotificationAppService>>();
            _configuration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _configuration.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForCachingInformation());
            _mapper = new Mock<IMapper>();
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _sut = new NotificationJobAppService(_genericCommandRepository.Object, _idmProxy.Object, _cache.Object, _iNotificationQuerie.Object,
                _notifayCommands.Object, _logger.Object, _mapper.Object, _configuration.Object);
        }

        [Fact]
        public async Task Should_AddNotificationSettingByUserId()
        {
            //Arrange
            UserProfile userProfileDefault = new BranchUserDefaults().ReturnUserProfileDefaults();
            List<UserProfile> listUserProfile = new List<UserProfile>();
            listUserProfile.Add(userProfileDefault);
            List<UserNotificationSetting> userNotificationSettings = new BranchUserDefaults().GetUserNotificationSettings();
            NotificationOperationCode notificationOperationCode = new BranchUserDefaults().ReturnNotificationOperationCode();
            _iNotificationQuerie.Setup(q => q.GetNotificationSettingsByCodeId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<UserNotificationSetting>>(userNotificationSettings);
                });
            _iNotificationQuerie.Setup(q => q.GetDefaultSettingByCodeId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<NotificationOperationCode>(notificationOperationCode);
                });
            //Act
            await _sut.AddNotificationSettingByUserId(It.IsAny<int>(), listUserProfile, It.IsAny<int>());
            //Assert
            _genericCommandRepository.Verify(g => g.UpdateRange(It.IsAny<List<UserProfile>>()), Times.Once);
            _genericCommandRepository.Verify(g => g.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_SendNotifications()
        {
            //Arrange
            List<UserNotificationSetting> userNotificationSettings = new BranchUserDefaults().GetUserNotificationSettings();
            MainNotificationTemplateModel mainNotificationTemplateModel = new UserNotificationSettingBuilder().ReturnMainNotificationTemplateModel();
            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByUserIdAndUserType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<UserNotificationSetting>>(userNotificationSettings);
                });
            _idmProxy.Setup(q => q.GetMonafasatUsersByAgencyTypeAndRoleName(It.IsAny<UsersSearchCriteriaModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(It.IsAny<QueryResult<EmployeeIntegrationModel>>());
                });
            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByRoleAndOperationCode(It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<UserNotificationSetting>>(userNotificationSettings);
                });
            //Act
            await _sut.SendNotifications(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), mainNotificationTemplateModel, It.IsAny<string>());
            //Assert

        }

        [Theory]
        [InlineData(1,102,"User Name")]
        public async Task Should_SendNotificationByUserId(int notificationCodeId, int userId, string userName)
        {
            //Arrange
            MainNotificationTemplateModel mainNotificationTemplateModel = new UserNotificationSettingBuilder().ReturnMainNotificationTemplateModel();
            UserAPIModel userAPIModel = new UserAPIModel() { Email = "aaIDM@IDM.com",PhoneNumber = "0505050500"};

            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByUserId(It.IsAny<int>(), It.IsAny<int>()))
.Returns(() =>
{
   return Task.FromResult<UserNotificationSetting>(new NotificationsDefaults().GetUserNotificationSettingWithProfileId()[0]);
});

            _idmProxy.Setup(q => q.GetUserbyUserName(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserAPIModel>(userAPIModel);
            });

            var entryMock = new Mock<ICacheEntry>();
            _cache.Setup(m => m.CreateEntry(It.IsAny<object>()))
               .Returns(entryMock.Object);



            _iNotificationQuerie.Setup(q => q.FindAllNotificationOperationCode())
      .Returns(() =>
      {
          return Task.FromResult<List<NotificationOperationCode>>(new NotificationsDefaults().GetNotificationOperationCode());
      });




            //Act
            await _sut.SendNotificationByUserId(notificationCodeId,userId,userName, mainNotificationTemplateModel);
            //Assert
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
