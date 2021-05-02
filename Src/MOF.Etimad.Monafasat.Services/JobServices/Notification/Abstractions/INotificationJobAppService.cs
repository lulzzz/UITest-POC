using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotificationJobAppService
    {
        Task AddNotificationSettingByUserId(int notificationCodeId, List<UserProfile> users, int roleid);
        Task SendNotificationByUserId(int notificationCodeId, int userId, string userName, MainNotificationTemplateModel mainNotificationTemplateModel);
        Task SendNotifications(int notificationCodeId, string agencyCode, int agencyType, MainNotificationTemplateModel mainView, string UserRole, int committeeId = 0, int branchid = 0);
        Task<bool> SendNotificationForSuppliers(int notificationCodeId, List<string> crsList, MainNotificationTemplateModel mainNotificationTemplateModel, Dictionary<string, Dictionary<int, string>> dynamicParameterList = null);
    }
}
