using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.NotificationTest
{

    public class NotificationQueriesTest
    {
        private readonly NotificationQueries _sut;
        public NotificationQueriesTest()
        {
            _sut = new NotificationQueries(InitialData.context);
        }

        [Fact]
        public async Task ShouldFindUserNotificationSettingSucess()
        {
            var result = await _sut.FindUserNotificationSetting(1,"1", Enums.UserRole.NewMonafasat_Supplier);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldFindUserNotificationSettingbyUserProfileIdSuccess()
        {
            var result = await _sut.FindUserNotificationSettingbyUserProfileId(1);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ShouldGetNotificationPanelForCrSuccess()
        {
            var result = await _sut.GetNotificationPanelForCr("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAllNotificationsForCrAsyncSuccess()
        {
            SearchCriteria criteria = new SearchCriteria() { PageSize=1,PageNumber=1};
            var result = await _sut.GetAllNotificationsForCrAsync("1", criteria);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetNotificationPanelCountForSupplierSuccess()
        {
            var result = await _sut.GetNotificationPanelCountForSupplier("1");
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetNotificationPanelCountSuccess()
        {
            var result = await _sut.GetNotificationPanelCount(1,1);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task ShouldGetAllNotificationsAsyncSuccess()
        {
            SearchCriteria criteria = new SearchCriteria() { PageSize = 1, PageNumber = 1 };
            var result = await _sut.GetAllNotificationsAsync("NewMonafasat_DataEntry", 1,1,1, criteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetNotificationPanelSuccess()
        {
            var result = await _sut.GetNotificationPanel("NewMonafasat_DataEntry", 1, 1, 1);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetNotificationSettingsByCodeIdSuccess()
        {
            var result = await _sut.GetNotificationSettingsByCodeId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetDefaultSettingByCodeIdSuccess()
        {
            var result = await _sut.GetDefaultSettingByCodeId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetDefaultSettingByUserTypesSuccess()
        {
            var result = await _sut.GetDefaultSettingByUserTypes(new List<int>() { 1,2,3});
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldFindNotificationOperationCodeSuccess()
        {
            var result = await _sut.FindNotificationOperationCode(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetUserSuccess()
        {
            var result = await _sut.GetUser(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetSupplierUserSuccess()
        {
            var result = await _sut.GetSupplierUser("SelectedCr1");
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetNotificationByIdSuccess()
        {
            var result = await _sut.GetNewNotification();
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldGetOperationCodeSuccess()
        {
            var result =  _sut.GetOperationCode();
            Assert.NotNull(result);
        }

        //GetNotificationCodes(OperationCodeSearchCriteria criteria)

        [Fact]
        public async Task ShouldGetNotificationCodesSuccess()
        {
            OperationCodeSearchCriteria criteria = new OperationCodeSearchCriteria() { PageNumber = 1, PageSize = 1 };
            var result = await _sut.GetNotificationCodes(criteria);
            Assert.NotNull(result);
        }


    }
}
