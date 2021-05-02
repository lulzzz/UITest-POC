using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
// 

namespace MOF.Etimad.Monafasat.ViewModel
{
    //[Serializable]
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Enums.UserRole UserType { get; set; }
        public NotificationSettingModel NotificationSetting { get; set; }
        public List<BasicNotificationModel> Notifications { get; set; }
    }


    public class UserNotificatioSettingModel
    {
        public string Id { get; set; }
        public string UserIdString { get; set; }
        public string OperationName { get; set; }
        public bool CanEditEmail { get; set; }
        public bool CanEditMessage { get; set; }
        public bool EmailEnabled { get; set; }
        public bool SMSEnabled { get; set; }
        public bool IsArabic { get; set; }
        public int NotificationCodeId { get; set; }
        public string OperationCode { get; set; }
        public string EmailTemplateCode { get; set; }
        public string categoryName { get; set; }


    }

    public class UserCategoryNotificatioSettingModel
    {

        public string categoryName { get; set; }
        public List<UserNotificatioSettingModel> userNotificatioSettingModels { get; set; }


    }

}
