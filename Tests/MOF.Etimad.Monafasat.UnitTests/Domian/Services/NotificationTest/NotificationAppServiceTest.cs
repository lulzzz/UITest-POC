using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.IDMDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.NotificationDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.NotificationTest
{
    public class NotificationAppServiceTest
    {
        private readonly Mock<IMemoryCache> _cache;
        private readonly Mock<IIDMProxy> _idmProxy;
        private readonly Mock<INotificationQueries> _iNotificationQuerie;
        private readonly Mock<IBranchServiceQueries> _branchQuery;
        private readonly Mock<ICommitteeQueries> _committeeQueries;
        private readonly Mock<INotificationProxy> _notificationProxy;
        private readonly Mock<ILogger<NotificationAppService>> _logger;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
        private readonly Mock<IINotificationCommands> _notifayCommands;
        private readonly AppDbContext _moqAppDbContext;
        public readonly NotificationAppService _sut;

        public NotificationAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "TestingDB")
              .Options;
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _cache = new Mock<IMemoryCache>();
            _idmProxy = new Mock<IIDMProxy>();
            _iNotificationQuerie = new Mock<INotificationQueries>();
            _notifayCommands = new Mock<IINotificationCommands>();
            _logger = new Mock<ILogger<NotificationAppService>>();
            _branchQuery = new Mock<IBranchServiceQueries>();
            _committeeQueries = new Mock<ICommitteeQueries>();
            _notificationProxy = new Mock<INotificationProxy>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForCachingInformation());
            _mapper = new Mock<IMapper>();
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _sut = new NotificationAppService(_notificationProxy.Object, _idmProxy.Object, _cache.Object, _iNotificationQuerie.Object,
            _notifayCommands.Object, _genericCommandRepository.Object, _logger.Object, _mapper.Object, _branchQuery.Object, _committeeQueries.Object, _httpContext.Object, _rootConfigurationMock.Object);
        }

        [Fact]
        public async Task ShouldAddNotificationSettingByUserIdSuccess()
        {
            UserProfile userProfile = new NotificationsDefaults().ReturnUserProfileDefaults();
            NotificationOperationCode notificationOperationCode = new BranchUserDefaults().ReturnNotificationOperationCode();

            _iNotificationQuerie.Setup(m => m.GetNotificationSettingsByCodeId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new NotificationsDefaults().GetUserNotificationSettings());
            });

            _iNotificationQuerie.Setup(q => q.GetDefaultSettingByCodeId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<NotificationOperationCode>(notificationOperationCode);
            });

            await _sut.AddNotificationSettingByUserId(1, userProfile, 1);
            _genericCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }


        [Theory]
        [InlineData("")]
        [InlineData("69N3QwUl%208o0yRoVEsRsYA==")]
        public async Task ShoulSaveOperationCodeSuccess(string idString)
        {
            NotificationOperationCode notificationOperationCode = new BranchUserDefaults().ReturnNotificationOperationCode();


            OperationCodeViewModel operationCode = new OperationCodeViewModel()
            {
                IdString = idString
            };
            _iNotificationQuerie.Setup(q => q.FindNotificationOperationCode(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<NotificationOperationCode>(notificationOperationCode);
            });

            await _sut.SaveOperationCode(operationCode);
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }        

        [Theory]
        [InlineData(true)]
        [InlineData(false)]

        public async Task ShoulResendFailNotificationSuccess(bool isNotify)
        {
            _iNotificationQuerie.Setup(q => q.GetNewNotification())
            .Returns(() =>
            {
                return Task.FromResult<List<BaseNotification>>(new NotificationsDefaults().getBaseNotification());
            });
            _notificationProxy.Setup(x=>x.SendSMS(It.IsAny<string>(), It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<bool>(isNotify);
           });

            _notificationProxy.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        .Returns(() =>
        {
            return Task.FromResult<bool>(isNotify);
        });


            await _sut.ResendFailNotification();
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }



        [Fact]
        public async Task ShoulSendNotificationByUserIdSuccess()
        {
            UserAPIModel userAPIModel = new UserAPIModel() {Email = "aaIDM@IDM.com", PhoneNumber = "0505050500" };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel() { 
            TemplateName="templatename",
            BranchId=1,
            CommitteeId=1,
            Link="link",
            RecipientnName= "RecipientnName",
                Args=new NotificationArguments() {BodyEmailArgs=new object[] {"email"},SMSArgs = new object[] { "sms" } }

            };
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

            await _sut.SendNotificationByUserId(1,1,"username", mainNotificationTemplateModel);
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ShoulNotSendNotificationByUserIdSuccess()
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            await _sut.SendNotificationByUserId(1, 1, "username", mainNotificationTemplateModel);
            _notifayCommands.VerifyAll();
        }




        [Theory]
        [InlineData(1)]
        public async Task ShoulSendNotificationForCommittesUsersSuccess(int committeeId)
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            _committeeQueries.Setup(m => m.FindAgencyCodeByCommitteeId(It.IsAny<int>()))
      .Returns(() =>
      {
          return Task.FromResult<GovAgency>(new AgencyDefaults().GetGovAgency());
      });

            MockDataForSendNotifications();
            await _sut.SendNotificationForCommitteeUsers(1, committeeId, mainNotificationTemplateModel);
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }



        [Theory]
        [InlineData(1)]
        public async Task ShoulSendNotificationForBranchUsersSuccess(int branchid)
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            _branchQuery.Setup(m => m.GetAgencyCodeByBranchId(It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<GovAgency>(new AgencyDefaults().GetGovAgency());
             });


            MockDataForSendNotifications();
            await _sut.SendNotificationForBranchUsers(1, branchid, mainNotificationTemplateModel);
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }


        private void MockDataForSendNotifications()
        {
            UserAPIModel userAPIModel = new UserAPIModel() { Email = "aaIDM@IDM.com", PhoneNumber = "0505050500" };

       
            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByUserId(It.IsAny<int>(), It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<UserNotificationSetting>(new NotificationsDefaults().GetUserNotificationSettingWithProfileId()[0]);
           });

            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByRoleAndOperationCode(It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
       .Returns(() =>
       {
           return Task.FromResult<List<UserNotificationSetting>>(new NotificationsDefaults().GetUserNotificationSettingWithProfileId());
       });

            _iNotificationQuerie.Setup(q => q.GetNotificationSettingByUserIdAndUserType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
.Returns(() =>
{
    return Task.FromResult<List<UserNotificationSetting>>(new NotificationsDefaults().GetUserNotificationSettingWithProfileId());
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



            _idmProxy.Setup(i => i.GetMonafasatUsersByAgencyTypeAndRoleName(It.IsAny<UsersSearchCriteriaModel>()))
                      .Returns(() =>
                      {
                          return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                      });
            _mapper.Setup(x => x.Map<QueryResult<EmployeeIntegrationModel>>(It.IsAny<QueryResult<EmployeeIntegrationModel>>())).Returns(() =>
            {
                return new BranchDefaults().GetEmployeeIntegrationModel();
            });

      
        }

        [Fact]        
        public async Task ShoulSendNotificationForUsersByRoleNameAndAgencySuccess()
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            MockDataForSendNotifications();
            await _sut.SendNotificationForUsersByRoleNameAndAgency(1, "roleName", mainNotificationTemplateModel,"agencycode",1,new List<int>() {102});
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task ShoulSendInvitationByEmailSuccess()
        {
            List<string> suppliersEmails = new List<string>() { "hsawak@etimad.sa" };
            Tender tender = new TenderDefault().GetGeneralTender();
              var result=await _sut.SendInvitationByEmail(suppliersEmails, tender);
            Assert.IsType<bool>(result);
        }


        [Theory]
        [InlineData("hsawak@etimad.sa","")]
        [InlineData("", "0533286913")]
        public async Task ShoulSendNotificationByEmailAndSmsForRolesChangedSuccess(string email,string mobil)
        {
            await _sut.SendNotificationByEmailAndSmsForRolesChanged(102, email,mobil);
            _notifayCommands.Verify(m => m.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task ShoulSendSolidarityInvitationByEmailSuccess()
        {
            List<string> suppliersEmails = new List<string>() { "hsawak@etimad.sa" };
            Tender tender = new TenderDefault().GetGeneralTender();
            var result =  await _sut.SendSolidarityInvitationByEmail(suppliersEmails, tender, "supplierName");
            Assert.IsType<bool>(result);
        }



        [Fact]
        public async Task ShoulSendEmailSuccess()
        {
            var ListOfEmails = new Dictionary<string, string>();
            ListOfEmails.Add("key", "value");
            EmailModel model = new EmailModel() { ListOfEmails = ListOfEmails,Subject="subject" };
            var result = await _sut.SendEmail(model);
            Assert.IsType<bool>(result);
        }


        [Fact]
        public async Task ShoulSendSmsSuccess()
        {
            var ListOfNumbers = new Dictionary<string, string>();
            ListOfNumbers.Add("key", "value");
            SmsModel model = new SmsModel() { ListOfNumbers = ListOfNumbers };
            var result = await _sut.SendSms(model);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ShouSendInvitationBySmsSuccess()
        {
            List<string> suppliersMobileNo = new List<string>() { "0533286913" };
            Tender tender = new TenderDefault().GetGeneralTender();
            var result = await _sut.SendInvitationBySms(suppliersMobileNo, tender);
            Assert.IsType<bool>(result);
        }


        [Fact]
        public async Task ShoulSendSolidarityInvitationBySmsSuccess()
        {
            List<string> suppliersMobileNo = new List<string>() { "0533286913" };
            Tender tender = new TenderDefault().GetGeneralTender();
            var result = await _sut.SendSolidarityInvitationBySms(suppliersMobileNo, tender, "supplierName");
            Assert.IsType<bool>(result);
        }


        [Fact]
        public void  ShoulGetOperationCodeSmsSuccess()
        {
            _iNotificationQuerie.Setup(q => q.GetOperationCode())
              .Returns(() =>
              {
                  return new NotificationsDefaults().GetNotificationOperationCodeModel();
              });

            var result = _sut.GetOperationCode();
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShoulSaveNotificationSettingForSupplierSuccess()
        {
            List<UserNotificatioSettingModel> userNotificatioSettings = new NotificationsDefaults().GetUserNotificationSettingModel();

            _iNotificationQuerie.Setup(q => q.GetSupplierUser(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<Supplier>(new IDMDefaults().GetSupplierData());
             });

            await _sut.SaveNotificationSetting(userNotificatioSettings,1, Enums.UserRole.NewMonafasat_Supplier, "cr");
            _notifayCommands.Verify(m => m.UpdateSupplierAsync(It.IsAny<Supplier>()), Times.Once);

        }

        [Fact]
        public async Task ShoulSaveNotificationSettingForUserSuccess()
        {
            List<UserNotificatioSettingModel> userNotificatioSettings = new NotificationsDefaults().GetUserNotificationSettingModel();

            _iNotificationQuerie.Setup(q => q.GetUser(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
           });


            await _sut.SaveNotificationSetting(userNotificatioSettings, 1, Enums.UserRole.NewMonafasat_TechnicalCommitteeUser, "cr");
            _notifayCommands.Verify(m => m.UpdateAsync(It.IsAny<UserProfile>()), Times.Once);
        }


        [Fact]
        public async Task ShoulSendNotificationByUserIdDirectPurchaseSuccess()
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            MoqUser();
            MockDataForSendNotifications();

            _idmProxy.Setup(i => i.GetEmployeeDetailsByRoleName(It.IsAny<string>()))
                    .Returns(() =>
                    {
                        return Task.FromResult<List<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeesData());
                    });

            await _sut.SendNotificationDirectByUserId(1, 1, mainNotificationTemplateModel);
            _notifayCommands.VerifyAll();
        }



        [Fact]
        public async Task ShoulNotSendNotificationByUserIdDirectPurchaseSucess()
        {
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel()
            {
                TemplateName = "templatename",
                BranchId = 1,
                CommitteeId = 1,
                Link = "link",
                RecipientnName = "RecipientnName",
                Args = new NotificationArguments() { BodyEmailArgs = new object[] { "email" }, SMSArgs = new object[] { "sms" } }

            };
            await _sut.SendNotificationDirectByUserId(1, 1, mainNotificationTemplateModel);
            _notifayCommands.VerifyAll();
        }


       

        [Fact]
        public async Task ShoulGetUserSuccess()
        {
            _iNotificationQuerie.Setup(q => q.GetUser(It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
             });
            var result = await _sut.GetUser(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShoulGetNotificationPanelForCrSuccess()
        {
            _iNotificationQuerie.Setup(q => q.GetNotificationPanelForCr(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<List<NotificationPanelModel>>(new NotificationsDefaults().GetNotificationPanelModel());
             });
            var result = await _sut.GetNotificationPanelForCr("1");
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShoulGetNotificationPanelCountSuccess()
        {
            _iNotificationQuerie.Setup(q => q.GetNotificationPanelCount(It.IsAny<int>(), It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<int>(1);
             });
            var result = await _sut.GetNotificationPanelCount(1,1);
            Assert.IsType<int>(result);
        }


        [Fact]
        public async Task ShoulGetNotificationPanelCountForSupplierSuccess()
        {
            _iNotificationQuerie.Setup(q => q.GetNotificationPanelCountForSupplier(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<int>(1);
             });
            var result = await _sut.GetNotificationPanelCountForSupplier("1");
            Assert.IsType<int>(result);
        }


        private void MoqUser()
        {
            var claim = new Claim("selectedAgency", "selectedAgency,022001000000");
            var usernum = new Claim(IdentityConfigs.Claims.Role, "NewMonafasat_TechnicalCommitteeUser");
            var agencyCode = new Claim(IdentityConfigs.Claims.Agency, "022001000000");
            var idintity = new GenericIdentity(IdentityConfigs.Claims.SelectedGovAgency, "selectedAgency");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);
            idintity.AddClaim(agencyCode);
            _httpContext.SetupGet(x => x.HttpContext.User.Identity).Returns(() => { return idintity; });
            _httpContext.SetupGet(x => x.HttpContext.User.Claims).Returns(() => { return idintity.Claims; });
        }


    }
}
